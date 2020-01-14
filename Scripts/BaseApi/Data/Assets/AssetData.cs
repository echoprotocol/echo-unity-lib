using Base.Data.Json;
using Base.Data.Operations.Fee;
using Buffers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Assets
{
    [JsonConverter(typeof(AssetDataConverter))]
    public sealed class AssetData : SerializableObject, ISerializeToBuffer, IFeeAsset
    {
        public readonly static AssetData EMPTY = new AssetData(0, SpaceTypeId.EMPTY);

        private const string AMOUNT_FIELD_KEY = "amount";
        private const string ASSET_ID_FIELD_KEY = "asset_id";


        public long Amount { get; private set; }
        public SpaceTypeId AssetId { get; private set; }

        public AssetData(long amount, SpaceTypeId assetId)
        {
            Amount = amount;
            AssetId = assetId;
        }

        public AssetData(JObject value)
        {
            var token = value.Root;
            Amount = value.TryGetValue(AMOUNT_FIELD_KEY, out token) ? token.ToObject<long>() : 0;
            AssetId = value.TryGetValue(ASSET_ID_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { AMOUNT_FIELD_KEY,     Amount },
                { ASSET_ID_FIELD_KEY,   AssetId }
            }).Build();
        }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteInt64(Amount);
            AssetId.ToBuffer(buffer);
            return buffer;
        }

        public AssetData FeeAsset => this;
    }
}