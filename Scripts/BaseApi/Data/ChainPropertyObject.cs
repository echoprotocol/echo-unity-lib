using Newtonsoft.Json;


namespace Base.Data
{
    // id "2.9.x"
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
    }
}