using Newtonsoft.Json;

namespace Framework.Net8;

public abstract record BaseRecord
{
    public virtual string GetValue()
    {
        return (JsonConvert.SerializeObject(this));
    }

    public virtual void SetValue(string jsonString)
    {
        if (!string.IsNullOrEmpty(jsonString) && AppUtility.IsValidJson(jsonString))
        {
            this.SetValue(JsonConvert.DeserializeObject(jsonString, this.GetType()));
        }
        else
        {
            throw new Exception("Empty OR Invalidate Json String");
        }
    }

    public virtual void SetValue(Object copyRecord)
    {
        if (copyRecord != null && this.GetType() == copyRecord.GetType())
        {
            foreach (var property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(copyRecord));
            }
        }
    }
}

