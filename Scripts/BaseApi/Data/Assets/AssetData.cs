using Base.Data.Json;
using Buffers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Assets
{
    [JsonConverter(typeof(AssetDataConverter))]
    public sealed class AssetData : SerializableObject, ISerializeToBuffer
    {
        public readonly static AssetData EMPTY = new AssetData(0, SpaceTypeId.EMPTY);

        private const string AMOUNT_FIELD_KEY = "amount";
        private const string ASSET_ID_FIELD_KEY = "asset_id";


        public long Amount { get; private set; }
        public SpaceTypeId Asset { get; private set; }

        public AssetData(long amount, SpaceTypeId asset)
        {
            Amount = amount;
            Asset = asset;
        }

        public AssetData(JObject value)
        {
            var token = value.Root;
            Amount = value.TryGetValue(AMOUNT_FIELD_KEY, out token) ? token.ToObject<long>() : 0;
            Asset = value.TryGetValue(ASSET_ID_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { AMOUNT_FIELD_KEY,     Amount },
                { ASSET_ID_FIELD_KEY,   Asset }
            }).Build();
        }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteInt64(Amount);
            Asset.ToBuffer(buffer);
            return buffer;
        }
    }
}