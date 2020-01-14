using Newtonsoft.Json;


namespace Base.Data.Transactions
{
    public sealed class TransactionObject : IdObject
    {
        [JsonProperty("trx")]
        public SignedTransactionData Transaction { get; set; }
        [JsonProperty("trx_id")]
        public string TransactionId { get; set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}