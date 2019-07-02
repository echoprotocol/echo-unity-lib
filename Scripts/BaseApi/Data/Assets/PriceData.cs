using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class PriceData : SerializableObject
    {
        [JsonProperty("base")]
        public AssetData Base { get; private set; }
        [JsonProperty("quote")]
        public AssetData Quote { get; private set; }
    }
}