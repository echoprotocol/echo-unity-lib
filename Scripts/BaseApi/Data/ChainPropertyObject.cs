using Newtonsoft.Json;


namespace Base.Data
{
    // id "2.10.x"
    public sealed class ChainPropertyObject : IdObject
    {
        [JsonProperty("chain_id")]
        public string ChainId { get; private set; }
        [JsonProperty("immutable_parameters")]
        public ImmutableChainParametersData ImmutableParameters { get; private set; }
    }


    public sealed class ImmutableChainParametersData : SerializableObject
    {
        [JsonProperty("min_committee_member_count")]
        public ushort MinCommitteeMemberCount { get; private set; }
        [JsonProperty("num_special_accounts")]
        public uint NumSpecialAccounts { get; private set; }
        [JsonProperty("num_special_assets")]
        public uint NumSpecialAssets { get; private set; }
    }
}