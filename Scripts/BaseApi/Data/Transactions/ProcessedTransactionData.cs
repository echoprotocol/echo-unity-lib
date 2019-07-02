using Base.Data.Operations;
using Newtonsoft.Json;


namespace Base.Data.Transactions
{
    public sealed class ProcessedTransactionData : SignedTransactionData
    {
        [JsonProperty("operation_results")]
        public OperationResultData[] OperationResults { get; set; }

        public ProcessedTransactionData() : base()
        {
            OperationResults = new OperationResultData[0];
        }
    }
}