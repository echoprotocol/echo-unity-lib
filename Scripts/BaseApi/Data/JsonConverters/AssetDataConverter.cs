using System;
using Base.Data.Assets;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class AssetDataConverter : JsonCustomConverter<AssetData, JObject>
    {
        protected override AssetData Deserialize(JObject value, Type objectType) => new AssetData(value);

        protected override JObject Serialize(AssetData value) => JObject.Parse(value.ToString());
    }
}