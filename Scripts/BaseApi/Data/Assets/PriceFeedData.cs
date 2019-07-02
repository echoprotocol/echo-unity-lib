using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class PriceFeedData : SerializableObject
    {
        [JsonProperty("settlement_price")]
        public PriceData SettlementPrice { get; private set; }
        [JsonProperty("core_exchange_rate")]
        public PriceData CoreExchangeRate { get; private set; }
        [JsonProperty("maintenance_collateral_ratio")]
        public ushort MaintenanceCollateralRatio { get; private set; }
        [JsonProperty("maximum_short_squeeze_ratio")]
        public ushort MaximumShortSqueezeRatio { get; private set; }
    }
}