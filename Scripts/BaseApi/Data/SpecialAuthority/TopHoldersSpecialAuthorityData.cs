using Base.Config;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.SpecialAuthorities
{
    public sealed class TopHoldersSpecialAuthorityData : SpecialAuthorityData
    {
        private const string ASSET_FIELD_KEY = "asset";
        private const string NUM_TOP_HOLDERS_FIELD_KEY = "num_top_holders";


        public SpaceTypeId Asset { get; set; }
        public byte NumTopHolders { get; set; }

        public override ChainTypes.SpecialAuthority Type => ChainTypes.SpecialAuthority.TopHolders;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            Asset.ToBuffer(buffer);
            buffer.WriteUInt8(NumTopHolders);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { ASSET_FIELD_KEY,              Asset },
                { NUM_TOP_HOLDERS_FIELD_KEY,    NumTopHolders }
            }).Build();
        }

        public static TopHoldersSpecialAuthorityData Create(JObject value)
        {
            var token = value.Root;
            var instance = new TopHoldersSpecialAuthorityData();
            instance.Asset = value.TryGetValue(ASSET_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.NumTopHolders = value.TryGetValue(NUM_TOP_HOLDERS_FIELD_KEY, out token) ? token.ToObject<byte>() : byte.MinValue;
            return instance;
        }
    }
}