using System;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class SpaceTypeIdConverter : JsonCustomConverter<SpaceTypeId, JToken>
    {
        protected override SpaceTypeId Deserialize(JToken value, Type objectType) => ConvertFrom(value);

        protected override JToken Serialize(SpaceTypeId value) => ConvertTo(value);

        private static JToken ConvertTo(SpaceTypeId value)
        {
            return SpaceTypeId.EMPTY.Equals(value) ? JToken.FromObject(0) : JToken.FromObject(value.ToString());
        }

        private static SpaceTypeId ConvertFrom(JToken value)
        {
            return value.Type.Equals(JTokenType.String) ? SpaceTypeId.Create(value.ToString()) : SpaceTypeId.EMPTY;
        }
    }
}