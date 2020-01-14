using Newtonsoft.Json;


namespace Base.Data.Contract
{
    public sealed class ContractStatisticsObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("most_recent_op")]
        public SpaceTypeId MostRecentOperation { get; private set; }
        [JsonProperty("total_ops")]
        public uint TotalOperations { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}