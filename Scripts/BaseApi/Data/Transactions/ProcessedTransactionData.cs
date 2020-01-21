using Base.Data.Operations.Result;
using Newtonsoft.Json;


namespace Base.Data.Transactions
{
    public sealed class ProcessedTransactionData : SignedTransactionData
    {
        [JsonProperty("operation_results")]
        public OperationResultData[] OperationResults { get; set; }
        [JsonProperty("fees_collected")]
        public long FeesCollected { get; set; }

        public ProcessedTransactionData() : base()
        {
            OperationResults = new OperationResultData[0];
        }
    }
}