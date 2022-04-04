﻿using Djinni.Data.Entities;
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
            var myType = value.GetType();
            List<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties().Where(p => p.GetIndexParameters().Length == 1));
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(value, new object[0]);
                sb.AppendLine();
                sb.Append(@"" + prop.Name + "=" + propValue + "");
            }
            sb.AppendLine();
            sb.Append("}");
            return sb.ToString();
        }

        public T DeSerialize<T>(string serializeData, T target) where T : new()
        {
            var deserializedObjects = Extract.ExtractData(serializeData);

            foreach (var obj in deserializedObjects)
            {
                var properties = Extract.ExtractValuesFromData(obj);
                foreach (var property in properties)
                {
                    var propInfo = target.GetType().GetProperty(property.PropertyName);
                    propInfo?.SetValue(target,Convert.ChangeType(property.Value, propInfo.PropertyType), null);
                }
            }
            return target;
        }
    }
}
