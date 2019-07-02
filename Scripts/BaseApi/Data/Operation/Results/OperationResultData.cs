using Buffers;
using Base.Config;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Operations
{
    [JsonConverter(typeof(OperationResultDataPairConverter))]
    public abstract class OperationResultData : SerializableObject, ISerializeToBuffer
    {
        public abstract object Value { get; }
        public abstract ChainTypes.OperationResult Type { get; }
        public abstract ByteBuffer ToBufferRaw(ByteBuffer buffer = null);

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            return ToBufferRaw(buffer);
        }

        public OperationResultData Clone() => (OperationResultData)JsonConvert.DeserializeObject(Serialize(), GetType());
    }
}