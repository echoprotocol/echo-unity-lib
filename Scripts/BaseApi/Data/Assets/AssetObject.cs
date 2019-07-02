using Newtonsoft.Json;


namespace Base.Data.Assets
{
    // id "1.3.x"
    public sealed class AssetObject : IdObject
    {
        [JsonProperty("symbol")]
        public string Symbol { get; private set; }
        [JsonProperty("precision")]
        public byte Precision { get; private set; }
        [JsonProperty("issuer")]
        public SpaceTypeId Issuer { get; private set; }
        [JsonProperty("options")]
        public AssetOptionsData Options { get; private set; }
        [JsonProperty("dynamic_asset_data_id")]
        public SpaceTypeId DynamicAssetData { get; private set; }
        [JsonProperty("bitasset_data_id", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId BitassetData { get; private set; }
        [JsonProperty("buyback_account", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId BuybackAccount { get; private set; }
        [JsonProperty("dividend_data_id", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId DividendData { get; private set; }
    }
}