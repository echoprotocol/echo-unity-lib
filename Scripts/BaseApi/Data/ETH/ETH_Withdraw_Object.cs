using Newtonsoft.Json;


namespace Base.Data.ETH
{
    public sealed class ETH_Withdraw_Object : IdObject
    {
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}