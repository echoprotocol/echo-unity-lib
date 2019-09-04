using Newtonsoft.Json;


namespace Base.Data.Contract
{
    // id "2.14.x"
    public sealed class ContractStatisticsObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("most_recent_op")]
        public SpaceTypeId MostRecentOperation { get; private set; }
        [JsonProperty("total_ops")]
        public uint TotalOperations { get; private set; }
    }
}