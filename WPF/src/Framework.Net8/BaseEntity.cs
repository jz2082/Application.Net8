using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Net8;

public abstract record BaseEntity<T>: BaseRecord where T : struct
{
    [Column(Order = 0)]
    public T Id { get; set; }
    [Column(Order = 1)]
    public DateTime DateCreated { get; set; }
    [Column(Order = 2)]
    public DateTime DateModified { get; set; }
    [Column(Order = 3), Timestamp]
    public byte[] RowVersion { get; set; }

    public virtual bool EntityEquals(Object copyObject)
    {
        if (copyObject == null || this.GetType() != copyObject.GetType()) return false;
        if (ReferenceEquals(this, copyObject)) return true;
        foreach (var property in this.GetType().GetProperties())
        {
            if (property.Name != "Flag" && property.Name != "DateModified" && property.Name != "RowVersion")
            {
                var thisPropertyValue = property.GetValue(this);
                var objectPropertyValue = property.GetValue(copyObject);
                var thisPropertyStringValue = AppUtility.ObjectToString(thisPropertyValue);
                var objectPropertyStringValue = AppUtility.ObjectToString(objectPropertyValue);
                if (!Object.Equals(thisPropertyValue, objectPropertyValue) && thisPropertyStringValue != objectPropertyStringValue)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public override void SetValue(Object copyRecord)
    {
        if (copyRecord != null && this.GetType() == copyRecord.GetType())
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.Name != "Id" && property.Name != "Flag" && property.Name != "RowVersion")
                { 
                    property.SetValue(this, property.GetValue(copyRecord));
                }
            }
        }
    }

}