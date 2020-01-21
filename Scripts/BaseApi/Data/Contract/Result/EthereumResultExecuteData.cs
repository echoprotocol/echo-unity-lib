using Base.Config;
using Base.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Json;
using CodeDeposit = Base.Config.ChainTypes.CodeDeposit;
using TransactionException = Base.Config.ChainTypes.TransactionException;


namespace Base.Data.Contract.Result.Ethereum
{
    public sealed class ResultExecuteData : ContractResultData
    {
        private const string EXEC_RES_FIELD_KEY = "exec_res";
        private const string TR_RECEIPT_FIELD_KEY = "tr_receipt";


        public ExecutionResultData Result { get; set; }
        public TransactionReceiptData Receipt { get; set; }

        public override ChainTypes.ContractResult Type => ChainTypes.ContractResult.Ethereum;

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { EXEC_RES_FIELD_KEY,           Result },
                { TR_RECEIPT_FIELD_KEY,         Receipt }
            }).Build();
        }

        public static ResultExecuteData Create(JObject value)
        {
            var token = value.Root;
            var instance = new ResultExecuteData();
            instance.Result = value.TryGetValue(EXEC_RES_FIELD_KEY, out token) ? token.ToObject<ExecutionResultData>() : null;
            instance.Receipt = value.TryGetValue(TR_RECEIPT_FIELD_KEY, out token) ? token.ToObject<TransactionReceiptData>() : null;
            return instance;
        }
    }


    public sealed class ExecutionResultData : SerializableObject
    {
        [JsonProperty("excepted"), JsonConverter(typeof(TransactionExceptionEnumConverter))]
        public TransactionException Excepted { get; private set; }
        [JsonProperty("new_address"), JsonConverter(typeof(AddressToSpaceTypeIdConverter))]
        public SpaceTypeId NewAddress { get; private set; }
        [JsonProperty("output"), JsonConverter(typeof(ByteArrayConverter))]
        public byte[] Output { get; private set; }
        [JsonProperty("code_deposit"), JsonConverter(typeof(CodeDepositEnumConverter))]
        public CodeDeposit CodeDeposit { get; private set; }
        [JsonProperty("gas_for_deposit")]
        public ulong GasForDeposit { get; private set; }
        [JsonProperty("deposit_size")]
        public uint DepositSize { get; private set; }
    }


    public sealed class TransactionReceiptData : SerializableObject
    {
        [JsonProperty("status_code")]
        public byte StatusCode { get; private set; }
        [JsonProperty("gas_used")]
        public ulong GasUsed { get; private set; }
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
        [JsonProperty("block_num")]
        public uint BlockNumber { get; private set; }
        [JsonProperty("trx_num")]
        public uint TransactionNumber { get; private set; }
        [JsonProperty("op_num")]
        public uint OperationNumber { get; private set; }
    }
}