using System;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class NullableDateTimeConverter : JsonCustomConverter<DateTime?, JToken>
    {
        protected override DateTime? Deserialize(JToken value, Type objectType)
        {
            return value.IsNull() || value.Type.Equals(JTokenType.Null) ? null : (DateTime?)DateTimeConverter.ConvertFrom(value);
        }

        protected override JToken Serialize(DateTime? value)
        {
            return value.HasValue ? DateTimeConverter.ConvertTo(value.Value) : null;
        }
    }
}