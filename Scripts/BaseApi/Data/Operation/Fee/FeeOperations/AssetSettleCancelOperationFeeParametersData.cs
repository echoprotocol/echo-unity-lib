using Base.Config;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class AssetSettleCancelOperationFeeParametersData : FeeParametersData
    {
        public override ChainTypes.FeeParameters Type => ChainTypes.FeeParameters.AssetSettleCancelOperation;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder().Build();
        }

        public static AssetSettleCancelOperationFeeParametersData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AssetSettleCancelOperationFeeParametersData();
            return instance;
        }
    }
}