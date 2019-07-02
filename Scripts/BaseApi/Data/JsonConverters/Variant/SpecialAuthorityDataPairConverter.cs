using System;
using Base.Config;
using Base.Data.SpecialAuthorities;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class SpecialAuthorityDataPairConverter : JsonCustomConverter<SpecialAuthorityData, JArray>
    {
        protected override SpecialAuthorityData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.SpecialAuthority)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.SpecialAuthority.No:
                    return NoSpecialAuthorityData.Create(value.Last as JObject);
                case ChainTypes.SpecialAuthority.TopHolders:
                    return TopHoldersSpecialAuthorityData.Create(value.Last as JObject);
                default:
                    CustomTools.Console.DebugError("Unexpected special authority type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(SpecialAuthorityData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JToken.FromObject(value.ToString()));
        }
    }
}