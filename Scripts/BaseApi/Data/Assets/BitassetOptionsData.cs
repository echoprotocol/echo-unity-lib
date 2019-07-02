using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class BitassetOptionsData : SerializableObject
    {
        [JsonProperty("feed_lifetime_sec")]
        public uint FeedLifetimeSeconds { get; private set; }
        [JsonProperty("minimum_feeds")]
        public byte MinimumFeeds { get; private set; }
        [JsonProperty("force_settlement_delay_sec")]
        public uint ForceSettlementDelaySeconds { get; private set; }
        [JsonProperty("force_settlement_offset_percent")]
        public ushort ForceSettlementOffsetPercent { get; private set; }
        [JsonProperty("maximum_force_settlement_volume")]
        public ushort MaximumForceSettlementVolume { get; private set; }
        [JsonProperty("short_backing_asset")]
        public SpaceTypeId ShortBackingAsset { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}