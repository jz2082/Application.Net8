using System.Collections;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace Framework.Net8;

public class AdditionalInfo
{
    private readonly IDictionary<string, object> _info = new Dictionary<string, object>();
    private Stopwatch _infoStopwatch = Stopwatch.StartNew();

    #region Constructor

    /// <summary>
    ///     Constructor
    /// </summary>
    public AdditionalInfo()
    {
        _info.Clear();
        _infoStopwatch.Restart();
    }

    /// <summary>
    ///     Constructor with json string
    /// </summary>
    public AdditionalInfo(string jsonString)
    {
        _info.Clear();
        if (AppUtility.IsValidJson(jsonString))
        {
            try
            {
                var infoNew = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                if (infoNew != null)
                {
                    _info = infoNew;
                }
            }
            catch
            {
                _info = new Dictionary<string, object>();
            }
        }
        _infoStopwatch.Restart();
    }

    /// <summary>
    ///     Constructor with IDictionary object
    /// </summary>
    public AdditionalInfo(IDictionary<string, object> info)
    {
        _info.Clear();
        if (info != null)
        {
            foreach (var item in info)
            {
                AddInfo(item.Key, item.Value);
            }
        }
        _infoStopwatch.Restart();
    }

    #endregion

    #region Property

    public int Count
    {
        get { return _info.Count; }
    }

    public bool IsEmpty
    {
        get { return _info.Count == 0; }
    }

    public IDictionary<string, object> Value
    {
        get { return _info; }
    }

    public IDictionary<string, string[]> ValidationProblem
    {
        get 
        {
            var problemList = new Dictionary<string, string[]>();
            if (_info != null)
            {
                foreach (var item in _info)
                {
                    problemList.Add(item.Key, new string[] { item.Value?.ToString() });
                }
            }
            return problemList; 
        }
    }

    #endregion

    #region Method

    private void AddInfo(string keyName, object keyValue)
    {
        if (string.IsNullOrEmpty(keyName)) keyName = Guid.NewGuid().ToString();
        if (_info.ContainsKey(keyName))
        {
            _info[keyName] = keyValue ?? "Null";
        }
        else
        {
            _info.Add(keyName, keyValue ?? "Null");
        }
    }

    private void AddExecutionTime(int infoType)
    {
        var keyValue = infoType switch
        {
            AdditionalInfoType.TotalSeconds => Math.Round(_infoStopwatch.Elapsed.TotalSeconds, 2),
            AdditionalInfoType.TotalMilliseconds => Math.Round(_infoStopwatch.Elapsed.TotalMilliseconds, 2),
            _ => 0
        };
        if (keyValue != 0) AddInfo("ExecutionTime", keyValue);
    }

    public void Clear()
    {
        _info.Clear();
    }

    public bool ContainsKey(string keyName)
    {
        return (_info.ContainsKey(keyName));
    }

    public void Add(string keyName, object keyValue)
    {
        AddInfo(keyName, keyValue);
    }

    public void Add(IDictionary<string, object> info)
    {
        if (info != null)
        {
            foreach (var item in info)
            {
                AddInfo(item.Key, item.Value);
            }
        }
    }

    public void Add(System.Collections.IDictionary info)
    {
        if (info != null)
        {
            foreach (DictionaryEntry item in info)
            {
                AddInfo(item.Key.ToString(), item.Value ?? "");
            }
        }
    }

    public void Add(string objectName, IDictionary<string, object> info)
    {
        foreach (var item in info)
        {
            AddInfo(string.Concat(string.IsNullOrEmpty(objectName) ? Guid.NewGuid().ToString() : objectName, ".", item.Key), item.Value);
        }
    }

    public void AddError(string keyName, string keyValue)
    {
        if (string.IsNullOrEmpty(keyName)) keyName = Guid.NewGuid().ToString();
        if (_info.ContainsKey(keyName))
        {
            _info[keyName] = string.Format("{0}{1}{2}", _info[keyName], Environment.NewLine, keyValue);
        }
        else
        {
            _info.Add(keyName, keyValue);
        }
    }

    public string GetValue()
    {
        return (JsonConvert.SerializeObject(_info));
    }

    public string GetValue(int infoType)
    {
        AddExecutionTime(infoType);
        return (GetValue());
    }

    public object GetValue(string keyName)
    {
        if (_info.ContainsKey(keyName)) return (_info[keyName]);
        return (null);
    }

    public object GetValue(string keyName, bool deleteFlag)
    {
        object returnValue = GetValue(keyName);
        if (deleteFlag && _info.ContainsKey(keyName)) _info.Remove(keyName);
        return (returnValue);
    }

    public string GetStringValue(string keyName)
    {
        return (AppUtility.ObjectToString(GetValue(keyName)));
    }

    public string GetStringValue(string keyName, bool deleteFlag)
    {
        return (AppUtility.ObjectToString(GetValue(keyName, deleteFlag)));
    }

    public void SetExceptionData(int infoType, Exception ex)
    {
        AddExecutionTime(infoType);
        SetExceptionData(ex);
    }

    public void SetExceptionData(Exception ex)
    {
        if (ex != null)
        {
            foreach (var item in _info)
            {
                if (ex.Data.Contains(item.Key))
                {
                    if (item.Value.GetType().Name.Equals("String", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ex.Data[item.Key] = string.Concat(ex.Data[item.Key], " | ", item.Value);
                    }
                    else
                    {
                        ex.Data.Add(string.Concat(item.Key, "-", Guid.NewGuid()), item.Value);
                    }
                }
                else
                {
                    ex.Data.Add(item.Key, item.Value);
                }
            }
        }
    }

    public override string ToString()
    {
        return (ToString(AdditionalInfoType.NoExecutionTime, false));
    }

    public string ToString(int infoType)
    {
        return (ToString(infoType, false));
    }

    public string ToString(bool newLine)
    {
        return (ToString(AdditionalInfoType.NoExecutionTime, newLine));
    }

    public string ToString(int infoType, bool newLine)
    {
        AddExecutionTime(infoType);
        StringBuilder sbInfo = new StringBuilder();
        foreach (var item in _info)
        {
            if (item.Key == "ExecutionTime")
                sbInfo.AppendFormat("{0} = {1}({2}){3}", item.Key, AppUtility.ObjectToString(item.Value), AppUtility.GetInfoTypeDescription(infoType), newLine ? Environment.NewLine : "; ");
            else
                sbInfo.AppendFormat("{0} = {1}{2}", item.Key, AppUtility.ObjectToString(item.Value), newLine ? Environment.NewLine : "; ");
        }
        return sbInfo.ToString();
    }

    #endregion
}

