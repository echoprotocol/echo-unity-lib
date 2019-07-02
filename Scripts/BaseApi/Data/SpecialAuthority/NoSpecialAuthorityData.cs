using Buffers;
using Base.Config;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.SpecialAuthorities
{
    public sealed class NoSpecialAuthorityData : SpecialAuthorityData
    {
        public override ChainTypes.SpecialAuthority Type => ChainTypes.SpecialAuthority.No;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder().Build();
        }

        public static NoSpecialAuthorityData Create(JObject value)
        {
            var token = value.Root;
            var instance = new NoSpecialAuthorityData();
            return instance;
        }
    }
}