using System;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class CustomDataConverter : JsonCustomConverter<CustomData, JObject>
    {
        protected override CustomData Deserialize(JObject value, Type objectType)
        {
            return (value.IsNull() || value.Type.Equals(JTokenType.Null)) ? null : new CustomData(value);
        }

        protected override JObject Serialize(CustomData value) => JObject.Parse(value.ToNullableString());
    }
}