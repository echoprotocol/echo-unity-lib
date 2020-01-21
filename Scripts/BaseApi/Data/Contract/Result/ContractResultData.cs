using Base.Config;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Contract.Result
{
    [JsonConverter(typeof(ContractResultDataPairConverter))]
    public abstract class ContractResultData : SerializableObject
    {
        public abstract ChainTypes.ContractResult Type { get; }

        public ContractResultData Clone() => (ContractResultData)JsonConvert.DeserializeObject(Serialize(), GetType());
    }
}