using Buffers;
using Base.Config;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.SpecialAuthorities
{
    [JsonConverter(typeof(SpecialAuthorityDataPairConverter))]
    public abstract class SpecialAuthorityData : SerializableObject, ISerializeToBuffer
    {
        public abstract ChainTypes.SpecialAuthority Type { get; }
        public abstract ByteBuffer ToBufferRaw(ByteBuffer buffer = null);

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteVarInt32((int)Type);
            return ToBufferRaw(buffer);
        }

        public SpecialAuthorityData Clone()
        {
            return (SpecialAuthorityData)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(this, new SpecialAuthorityDataPairConverter()), GetType());
        }
    }
}