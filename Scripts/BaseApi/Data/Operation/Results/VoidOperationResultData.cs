using Buffers;
using Base.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Base.Data.Operations
{
    public sealed class VoidOperationResultData : OperationResultData
    {
        private object value;


        public override object Value => value;

        public override ChainTypes.OperationResult Type => ChainTypes.OperationResult.Void;

        public VoidOperationResultData()
        {
            value = new object();
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            return buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
        }

        public override string Serialize() => JsonConvert.SerializeObject(value);

        public static VoidOperationResultData Create(JToken value)
        {
            return new VoidOperationResultData
            {
                value = value.ToObject<object>()
            };
        }
    }
}