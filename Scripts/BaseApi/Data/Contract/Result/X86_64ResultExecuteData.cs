using Base.Config;
using Base.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Json;
using ExecutionException = Base.Config.ChainTypes.ExecutionException;


namespace Base.Data.Contract.Result.X86_64
{
    public sealed class ResultExecuteData : ContractResultData
    {
        private const string CONTRACT_ID_FIELD_KEY = "contract_id";
        private const string RESULT_FIELD_KEY = "result";


        public SpaceTypeId Contract { get; set; }
        public ExecutionResultData Result { get; set; }

        public override ChainTypes.ContractResult Type => ChainTypes.ContractResult.X86_64;

        public override string Serialize()
        {
            var builder = new JsonBuilder();
            builder.WriteOptionalClassKeyValuePair(CONTRACT_ID_FIELD_KEY, Contract);
            builder.WriteKeyValuePair(RESULT_FIELD_KEY, Result);
            return builder.Build();
        }

        public static ResultExecuteData Create(JObject value)
        {
            var token = value.Root;
            var instance = new ResultExecuteData();
            instance.Contract = value.TryGetValue(CONTRACT_ID_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : null; // optional
            instance.Result = value.TryGetValue(RESULT_FIELD_KEY, out token) ? token.ToObject<ExecutionResultData>() : null;
            return instance;
        }
    }


    public sealed class ExecutionResultData : SerializableObject
    {
        [JsonProperty("error"), JsonConverter(typeof(ExecutionExceptionEnumConverter))]
        public ExecutionException Excepted { get; private set; }
        [JsonProperty("gas_used")]
        public ulong GasUsed { get; private set; }
        [JsonProperty("output"), JsonConverter(typeof(ByteArrayConverter))]
        public byte[] Output { get; private set; }
        [JsonProperty("logs")]
        public LogEntryData[] Logs { get; private set; }
    }


    public sealed class LogEntryData : SerializableObject
    {
        [JsonProperty("hash")]
        public string Hash { get; private set; }
        [JsonProperty("log")]
        public string Log { get; private set; }
        [JsonProperty("id")]
        public uint Id { get; private set; }
        [JsonProperty("block_num")]
        public uint BlockNumber { get; private set; }
        [JsonProperty("trx_num")]
        public uint TransactionNumber { get; private set; }
        [JsonProperty("op_num")]
        public uint OperationNumber  { get; private set; }
    }
}