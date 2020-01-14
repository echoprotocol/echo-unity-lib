using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class AssetDynamicDataObject : IdObject
    {
        [JsonProperty("current_supply")]
        public long CurrentSupply { get; private set; }
        [JsonProperty("confidential_supply")]
        public long ConfidentialSupply { get; private set; }
        [JsonProperty("accumulated_fees")]
        public long AccumulatedFees { get; private set; }
        [JsonProperty("fee_pool")]
        public long FeePool { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}