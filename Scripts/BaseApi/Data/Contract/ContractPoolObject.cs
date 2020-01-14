using Newtonsoft.Json;


namespace Base.Data.Contract
{
    public sealed class ContractPoolObject : IdObject
    {
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}