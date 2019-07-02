using Base.Config;
using Base.Data.Assets;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations
{
    public sealed class CallContractOperationData : OperationData
    {
        private const string FEE_FIELD_KEY = "fee";
        private const string REGISTRAR_FIELD_KEY = "registrar";
        private const string VALUE_FIELD_KEY = "value";
        private const string CODE_FIELD_KEY = "code";
        private const string CALLEE_FIELD_KEY = "callee";


        public override AssetData Fee { get; set; }
        public SpaceTypeId Registrar { get; set; }
        public AssetData Value { get; set; }
        public string Code { get; set; }
        public SpaceTypeId Callee { get; set; }
     
        public override ChainTypes.Operation Type => ChainTypes.Operation.CallContract;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            Fee.ToBuffer(buffer);
            Registrar.ToBuffer(buffer);
            Value.ToBuffer(buffer);
            buffer.WriteString(Code);
            Callee.ToBuffer(buffer);
            return buffer;
        }

        public override string Serialize()
        {
            var builder = new JsonBuilder();
            builder.WriteKeyValuePair(FEE_FIELD_KEY, Fee);
            builder.WriteKeyValuePair(REGISTRAR_FIELD_KEY, Registrar);
            builder.WriteKeyValuePair(VALUE_FIELD_KEY, Value);
            builder.WriteKeyValuePair(CODE_FIELD_KEY, Code);
            builder.WriteKeyValuePair(CALLEE_FIELD_KEY, Callee);
            return builder.Build();
        }

        public static CallContractOperationData Create(JObject value)
        {
            var token = value.Root;
            var instance = new CallContractOperationData();
            instance.Fee = value.TryGetValue(FEE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.Registrar = value.TryGetValue(REGISTRAR_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.Value = value.TryGetValue(VALUE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.Code = value.TryGetValue(CODE_FIELD_KEY, out token) ? token.ToObject<string>() : string.Empty;
            instance.Callee = value.TryGetValue(CALLEE_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            return instance;
        }
    }
}