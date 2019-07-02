using Base.Data.Json;
using Newtonsoft.Json;
using CodeDeposit = Base.Config.ChainTypes.CodeDeposit;
using TransactionException = Base.Config.ChainTypes.TransactionException;


namespace Base.Data.Contract
{
    public sealed class ResultExecuteData : SerializableObject
    {
        [JsonProperty("exec_res")]
        public ExecutionResultData Result { get; private set; }
        [JsonProperty("tr_receipt")]
        public TransactionReceiptData Receipt { get; private set; }
    }


    public sealed class ExecutionResultData : SerializableObject
    {
        [JsonProperty("excepted")]
        public TransactionException Excepted { get; private set; }
        [JsonProperty("new_address"), JsonConverter(typeof(AddressToSpaceTypeIdConverter))]
        public SpaceTypeId NewAddress { get; private set; }
        [JsonProperty("output"), JsonConverter(typeof(ByteArrayConverter))]
        public byte[] Output { get; private set; }
        [JsonProperty("code_deposit")]
        public CodeDeposit CodeDeposit { get; private set; }
        [JsonProperty("gas_refunded")]
        public string GasRefunded { get; private set; }
        [JsonProperty("deposit_size")]
        public ulong DepositSize { get; private set; }
        [JsonProperty("gas_for_deposit")]
        public string GasForDeposit { get; private set; }
    }


    public sealed class TransactionReceiptData : SerializableObject
    {
        [JsonProperty("status_code")]
        public byte StatusCode { get; private set; }
        [JsonProperty("gas_used")]
        public string GasUsed { get; private set; }
        [JsonProperty("bloom")]
        public string Bloom { get; private set; }
        [JsonProperty("log")]
        public LogEntryData[] Logs { get; private set; }
    }


    public sealed class LogEntryData : SerializableObject
    {
        [JsonProperty("address")]
        public string Address { get; private set; }
        [JsonProperty("log")]
        public string[] Logs { get; private set; }
        [JsonProperty("data"), JsonConverter(typeof(ByteArrayConverter))]
        public byte[] Data { get; private set; }
    }
}