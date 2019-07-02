using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class AssetOptionsData : SerializableObject
    {
        [JsonProperty("max_supply")]
        public long MaxSupply { get; private set; }
        [JsonProperty("market_fee_percent")]
        public ushort MarketFeePercent { get; private set; }
        [JsonProperty("max_market_fee")]
        public long MaxMarketFee { get; private set; }
        [JsonProperty("issuer_permissions")]
        public ushort IssuerPermissions { get; private set; }
        [JsonProperty("flags")]
        public ushort Flags { get; private set; }
        [JsonProperty("core_exchange_rate")]
        public PriceData CoreExchangeRate { get; private set; }
        [JsonProperty("whitelist_authorities")]
        public SpaceTypeId[] WhitelistAuthorities { get; private set; }
        [JsonProperty("blacklist_authorities")]
        public SpaceTypeId[] BlacklistAuthorities { get; private set; }
        [JsonProperty("whitelist_markets")]
        public SpaceTypeId[] WhitelistMarkets { get; private set; }
        [JsonProperty("blacklist_markets")]
        public SpaceTypeId[] BlacklistMarkets { get; private set; }
        [JsonProperty("description")]
        public string Description { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}