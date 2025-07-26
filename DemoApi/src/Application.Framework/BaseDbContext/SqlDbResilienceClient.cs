using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Framework;

public class SqlDbResilienceClient : BaseObject
{
    protected readonly PollyOptionSetting _pollySetting;
    private string _dbConnection = string.Empty;
    private int _commandTimeout = 90;
    private SqlConnection _sqlConnection;
    private SqlCommand _sqlCommand;
    private SqlTransaction _sqlTransaction;

    public SqlDbResilienceClient(IOptions<AppSetting> appSetting, IOptions<PollyOptionSetting> pollySetting, ILogger<SqlDbResilienceClient> logger) : base(logger)
    {
        _info.Add("Application", "Application.Framework");
        _info.Add("Module", "SqlDbResilienceClient");

        // get setting
        _pollySetting = pollySetting.Value;
        ApplySetting(new AdditionalInfo(appSetting.Value.GetValue()));
    }

    #region IDisposable Support

    protected override void Dispose(bool disposing)
    {
        DisposeObject();
        base.Dispose(disposing);
    }

    protected void DisposeObject()
    {
        _sqlTransaction?.Dispose();
        _sqlCommand?.Dispose();
        _sqlConnection?.Close();
        _sqlConnection?.Dispose();
    }

    #endregion

    #region Required Implementation

    protected AdditionalInfo ValidateRepository()
    {
        var info = new AdditionalInfo(_info.Value);
        info.Add(AppLogger.Method, "SqlDbResilienceClient.ValidateRepository");
        info.Add(AppLogger.Message, "protected AdditionalInfo ValidateRepository()");

        var errInfo = new AdditionalInfo();
        if (string.IsNullOrEmpty(_dbConnection))
        {
            errInfo.AddError("SqlDbResilienceClient", "Sql Database Connection cannot be null or empty, please correct.");
        }
        return errInfo;
    }

    #endregion

    public override void ApplySetting(AdditionalInfo setting)
    {
        _info.Add(AppLogger.Method, "SqlDbResilienceClient.ApplySetting Setup SqlDb Connection.");
        _info.Add("Message", "Create SqlDb Connection object with Resilience support");

        _commandTimeout = string.IsNullOrEmpty(setting.GetStringValue("CommandTimeout")) 
            ? 90 
            : Convert.ToInt16(setting.GetStringValue("CommandTimeout"));
        if ((_dbConnection != setting.GetStringValue("DbConnection") && 
            !string.IsNullOrEmpty(setting.GetStringValue("DbConnection"))) || 
            _sqlConnection == null)
        {
            _dbConnection = setting.GetStringValue("DbConnection");
            ClearViolation();
            RuleViolation.Add(ValidateRepository().Value);
            if (HasViolation) return;
            try
            {
                DisposeObject();
                _info.Add("Method", "SqlDbResilienceClient.ApplySetting");
                _info.Add("Message", "Create SqlDb Connection object with Resilience support");
                var sqlDbRetryoptions = new SqlRetryLogicOption()
                {
                    NumberOfTries = _pollySetting.RetryCount,
                    DeltaTime = TimeSpan.FromMilliseconds(_pollySetting.RetryDelayMilliSeconds),
                    MaxTimeInterval = TimeSpan.FromSeconds(_pollySetting.DurationOfBreak),
                    TransientErrors = [-1, -2, 0, 109, 233, 997, 1222, 10060, -2146232060, 596]
                };
                _info.Add("SqlRetryLogicOption.NumberOfTries", sqlDbRetryoptions.NumberOfTries);
                _info.Add("DbConnection", _dbConnection
                    .Replace("sqldatadbserver", "XXXXXXXX")
                    .Replace("Dudu1120", "XXXXXXXX")
                    .Replace("sa", "XXXXXXXX")
                    .Replace("dbAdmin", "XXXXXXXX"));
                var sqlDbRetryProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(sqlDbRetryoptions);
                // define the retrying event to report the execution attempts
                sqlDbRetryProvider.Retrying += (object s, SqlRetryingEventArgs e) =>
                {
                    var ex = e.Exceptions[e.Exceptions.Count - 1];
                    _info.Add("Retrying ...", $"attempt {e.RetryCount + 1} - current delay time:{e.Delay}");
                    _logger?.LogWarning(_info.GetValue(AdditionalInfoType.TotalMilliseconds));
                    if (ex != null)
                    {
                        _info.Add("CommandType", _sqlCommand?.CommandType);
                        _info.Add("CommandText", _sqlCommand?.CommandText);
                        _info.Add("CommandTimeout", _sqlCommand?.CommandTimeout);
                        _info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
                        _logger?.LogError(ex, "SqlDbRetryProvider.Retrying ... Please Ignore.");
                    }
                    // It is not a good practice to do time-consuming tasks inside the retrying event which blocks the running task.
                    // Use parallel programming patterns to mitigate it.
                    if (e.RetryCount == 3 - 1)
                    {
                        // This is the last chance to execute the command before throwing the exception.
                    }
                };
                // create SqlDb connection
                _sqlConnection = new SqlConnection()
                {
                    ConnectionString = _dbConnection,
                    RetryLogicProvider = sqlDbRetryProvider
                };
                // create SqlCommand
                _sqlCommand = new SqlCommand()
                {
                    Connection = _sqlConnection,
                    CommandTimeout = _commandTimeout
                };
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                _info.Add("CommandType", _sqlCommand?.CommandType);
                _info.Add("CommandText", _sqlCommand?.CommandText);
                _info.Add("CommandTimeout", _sqlCommand?.CommandTimeout);
                _info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
                DisposeObject();
                _logger?.LogError(ex, "");
            }
        }
    }

    public SqlParameterCollection SqlParameter
    {
        get
        {
            return _sqlCommand?.Parameters;
        }
    }

    public string SqlCommandText
    {
        set
        {
            if (_sqlCommand != null) _sqlCommand.CommandText = value;
        }
    }

    public CommandType SqlCommandType
    {
        set
        {
            if (_sqlCommand != null) _sqlCommand.CommandType = value;
        }
    }

    public void BeginTransaction()
    {
        if (_sqlConnection?.State != ConnectionState.Open)
        {
            _sqlConnection?.Open();
        }
        _sqlTransaction = _sqlConnection?.BeginTransaction();
        if (_sqlCommand != null) _sqlCommand.Transaction = _sqlTransaction;
    }

    public void CommitTransaction()
    {
        if (_sqlConnection?.State == ConnectionState.Open)
        {
            _sqlTransaction?.Commit();
            _sqlTransaction?.Dispose();
            _sqlTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        if (_sqlConnection?.State == ConnectionState.Open)
        {
            _sqlTransaction?.Rollback();
            _sqlTransaction?.Dispose();
            _sqlTransaction = null;
        }
    }

    public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default)
    {
        int returnValue = -1;

        if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
        ClearViolation();
        RuleViolation.Add(ValidateRepository().Value);
        if (HasViolation) return returnValue;
        try
        {
            if (_sqlConnection?.State != ConnectionState.Open && _sqlConnection != null)
            {
                await _sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
            }
            if (_sqlCommand != null)
            {
                returnValue = await _sqlCommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            _info.Add("CommandType", _sqlCommand?.CommandType);
            _info.Add("CommandText", _sqlCommand?.CommandText);
            _info.Add("CommandTimeout", _sqlCommand?.CommandTimeout);
            DisposeObject();
            _info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            _logger?.LogError(ex, "ExecuteNonQueryAsync failed.");
        }
        return returnValue;
    }

}