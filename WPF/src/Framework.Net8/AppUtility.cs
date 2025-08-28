using System.Collections;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Framework.Net8;

public static class AppUtility
{
    public static bool IsValidJson(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        try
        {
            var json = JContainer.Parse(value);
            return true;
        }
        catch
        {
            // ignore the error
        }
        return false;
    }

    public static string ObjectToString(object? item)
    {
        item ??= string.Empty;
        string returnValue = string.Empty;

        if (item.GetType() == typeof(String[]))
        {
            // item type is String[]
            string[] valueList = (string[])item;
            if (valueList != null && valueList.Length > 0)
            {
                foreach (string value in valueList)
                {
                    if (string.IsNullOrEmpty(returnValue))
                    {
                        returnValue = value;
                    }
                    else
                    {
                        returnValue = string.Concat(returnValue, ", ", value);
                    }
                }
            }
        }
        else if (item.GetType() == typeof(String))
        {
            // item type is String
            returnValue = item.ToString() ?? "";
        }
        else
        {
            // item type is String  OR others
            returnValue = JsonConvert.SerializeObject(item);
        }
        return returnValue ?? "";
    }

    public static double ObjectToDouble(object item)
    {
        return Double.TryParse(ObjectToString(item), out double returnValue) == true ? returnValue : 0.0;
    }

    public static DateTime EnsureUtcDate(DateTime item)
    {
        return DateTime.TryParse(item.ToShortDateString(), out DateTime returnValue) == true ? returnValue : DateTime.MinValue;
    }

    public static DateTime ObjectToDate(object item)
    {
        var returnValue = DateTime.MinValue;

        if (Double.TryParse(ObjectToString(item), out double doubleItem))
        {
            returnValue = EnsureUtcDate(DateTime.FromOADate(doubleItem));
        }
        return (returnValue);
    }

    public static DateTime ToTradeDate(string item)
    {
        var returnValue = DateTime.MinValue;
        if (!string.IsNullOrEmpty(item))
        {
            int month = 1;
            int year = 2000;
            if (double.TryParse(item, out double theDateDouble))
            {
                var theDate = DateTime.FromOADate(theDateDouble);
                year = theDate.Day + 2000;
                month = theDate.Month;

            }
            else
            {
                string[] theDateString = item.ToString().Split(' ');
                switch (theDateString[0])
                {
                    case "JLY":
                        month = 7;
                        break;
                }
                if (int.TryParse(theDateString[1], out int intyear))
                {
                    year = 2000 + intyear;
                }
            }
            returnValue = EnsureUtcDate(new DateTime(year, month, 1));
        }
        return (returnValue);
    }

    public static DateTime YYYYMMDDToDate(string item)
    {
        int year = 0, month = 0, day = 0;
        bool yearFlag = false, monthFlag = false, dayFlag = false;
        var returnValue = DateTime.MinValue;
        if (!string.IsNullOrEmpty(item))
        {
            if (item.Length == 8)
            {
                yearFlag = int.TryParse(item.Substring(0, 4), out year);
                monthFlag = int.TryParse(item.Substring(4, 2), out month);
                dayFlag = int.TryParse(item.Substring(6, 2), out day);
            }
            else if (item.Length == 10)
            {
                yearFlag = int.TryParse(item.Substring(0, 4), out year);
                monthFlag = int.TryParse(item.Substring(5, 2), out month);
                dayFlag = int.TryParse(item.Substring(8, 2), out day);
            }
            if (yearFlag && monthFlag && dayFlag)
                returnValue = EnsureUtcDate(new DateTime(year, month, day));
        }
        return (returnValue);
    }

    public static string GetInfoTypeDescription(int infoType) => infoType switch
    {
        AdditionalInfoType.TotalSeconds => "S",
        AdditionalInfoType.TotalMilliseconds => "MS",
        _ => string.Empty
    };

    public static async Task ExecuteSqlQuery(string sqlQuery, string dbConnection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var connection = new SqlConnection(dbConnection);

        var command = new SqlCommand(sqlQuery, connection);
        command.CommandTimeout = 0;
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task<int> ExecuteScalarSqlQuery(string sqlQuery, string dbConnection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var connection = new SqlConnection(dbConnection);
        var command = new SqlCommand(sqlQuery, connection);
        command.CommandTimeout = 0;
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        var count = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        return (count is DBNull ? 0 : Convert.ToInt32(count));
    }

    public static async Task<IEnumerable<TEntity>> ExecuteReaderSqlQuery<TEntity>(string sqlQuery, string dbConnection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<TEntity> returnList = new List<TEntity>();
        using var connection = new SqlConnection(dbConnection);
        var command = new SqlCommand(sqlQuery, connection);
        command.CommandTimeout = 0;
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        var readerList = reader.Cast<IDataRecord>();
        foreach (var item in readerList)
        {
            var entity = Activator.CreateInstance<TEntity>();
            for (var i = 0; i < item.FieldCount; i++)
            {
                if (entity != null)
                {
                    foreach (var property in entity.GetType().GetProperties())
                    {
                        if (property.Name == item.GetName(i))
                        {
                            property.SetValue(entity, item.GetString(i));
                        }
                    }
                }
            }
            returnList = returnList.Append<TEntity>(entity);
        }
        return returnList;
    }

    public static string LoadSQLStatement(string resourceName, string sqlFileName)
    {
        using var stm = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName + sqlFileName);
        if (stm == null) return String.Empty;
        return new StreamReader(stm).ReadToEnd();
    }

    public static string Sha256(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}