using Base.Config;
using Base.Data.Json;
using Buffers;
using Newtonsoft.Json;


namespace Base.Data.VestingPolicies
{
    [JsonConverter(typeof(VestingPolicyDataPairConverter))]
    public abstract class VestingPolicyData : SerializableObject, ISerializeToBuffer
    {
        public abstract ChainTypes.VestingPolicy Type { get; }
        public abstract ByteBuffer ToBufferRaw(ByteBuffer buffer = null);

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteVarInt32((int)Type);
            return ToBufferRaw(buffer);
        }

        public VestingPolicyData Clone()
        {
            return (VestingPolicyData)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(this, new VestingPolicyDataPairConverter()), GetType());
        }
    }
}
