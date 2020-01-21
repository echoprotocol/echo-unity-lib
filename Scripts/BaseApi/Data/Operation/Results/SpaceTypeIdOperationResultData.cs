using Buffers;
using Base.Config;
using Newtonsoft.Json.Linq;


namespace Base.Data.Operations.Result
{
    public sealed class SpaceTypeIdOperationResultData : OperationResultData
    {
        private SpaceTypeId value;


        public override object Value => value;

        public override ChainTypes.OperationResult Type => ChainTypes.OperationResult.SpaceTypeId;

        public SpaceTypeIdOperationResultData()
        {
            value = SpaceTypeId.EMPTY;
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            return value.ToBuffer(buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING));
        }

        public override string Serialize() => value.Serialize();

        public static SpaceTypeIdOperationResultData Create(JToken value)
        {
            return new SpaceTypeIdOperationResultData
            {
                value = value.ToObject<SpaceTypeId>()
            };
        }
    }
}