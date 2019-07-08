using Newtonsoft.Json;


namespace Base.Data.Contract
{
    // id "1.14.x"
    public sealed class ContractObject : IdObject
    {
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("destroyed")]
        public bool Destroyed { get; private set; }
        [JsonProperty("statistics")]
        public SpaceTypeId Statistics { get; private set; }
        [JsonProperty("supported_asset_id", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId SupportedAsset { get; private set; }
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}