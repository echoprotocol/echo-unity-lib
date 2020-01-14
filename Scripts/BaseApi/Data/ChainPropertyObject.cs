using Newtonsoft.Json;


namespace Base.Data
{
    public sealed class ChainPropertyObject : IdObject
    {
        [JsonProperty("chain_id")]
        public string ChainId { get; private set; }
        [JsonProperty("immutable_parameters")]
        public ImmutableChainParametersData ImmutableParameters { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }


    }


    public sealed class ImmutableChainParametersData : SerializableObject
    {
        [JsonProperty("min_committee_member_count")]
        public ushort MinCommitteeMemberCount { get; private set; }
    }
}