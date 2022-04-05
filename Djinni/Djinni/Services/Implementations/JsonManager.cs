using Djinni.Data.Entities;
using Djinni.Services.Interfaces;
using System.Reflection;
using System.Text;

namespace Djinni.Services.Implementations
{
    public class JsonManager : IJsonManager
    {
        public string CustomSerializer(object value)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append("{");
            IList<Person> values = value as IList<Person>;
            foreach (var prop in values)
            {
                sb.AppendLine();
                sb.Append(@"" + "Firstname:"+ prop.firstName + "");
                sb.AppendLine();
                sb.Append(@"" + "LastName:" + prop.lastName + "");
                sb.AppendLine();
                sb.Append(@"" + "City:" + prop.address.city + "");
                sb.AppendLine();
                sb.Append('\t');
            }

            sb.AppendLine();
            sb.Append("}");
            return sb.ToString();
        }

        public T DeSerialize<T>(string serializeData, T target) where T : new()
        {
            var deserializedObjects = Export.ExportData(serializeData);

            var properties = Export.ExporttValuesFromData(deserializedObjects);
            foreach (var property in properties)
            {
                var propInfo = target.GetType().GetProperty(property.PropertyName);
                propInfo?.SetValue(target, Convert.ChangeType(property.Value, propInfo.PropertyType), null);
            }
            return target;
        }
    }
}
