using Base.Data.Pairs;
using Newtonsoft.Json;


namespace Base.Data.Contract
{
    public sealed class ContractInfoData : SerializableObject
    {
        [JsonProperty("contract_info")]
        public ContractObject ContractInfo { get; private set; }
        [JsonProperty("code")]
        public string Code { get; private set; }
        [JsonProperty("storage")]
        public KeyValuePair[] Storage { get; private set; }
    }
}