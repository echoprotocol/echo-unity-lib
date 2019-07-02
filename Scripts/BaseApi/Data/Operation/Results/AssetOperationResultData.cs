using Base.Config;
using Base.Data.Assets;
using Buffers;
using Newtonsoft.Json.Linq;


namespace Base.Data.Operations
{
    public sealed class AssetOperationResultData : OperationResultData
    {
        private AssetData value;


        public override object Value => value;

        public override ChainTypes.OperationResult Type => ChainTypes.OperationResult.Asset;

        public AssetOperationResultData()
        {
            value = AssetData.EMPTY;
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            return value.ToBuffer(buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING));
        }

        public override string Serialize() => value.Serialize();

        public static AssetOperationResultData Create(JToken value)
        {
            return new AssetOperationResultData
            {
                value = value.ToObject<AssetData>()
            };
        }
    }
}