using Base.Config;
using Base.Data.Assets;
using Buffers;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations
{
    public sealed class AssetIssueOperationData : OperationData
    {
        private const string FEE_FIELD_KEY = "fee";
        private const string ISSUER_FIELD_KEY = "issuer";
        private const string ASSET_TO_ISSUE_FIELD_KEY = "asset_to_issue";
        private const string ISSUE_TO_ACCOUNT_FIELD_KEY = "issue_to_account";
        private const string EXTENSIONS_FIELD_KEY = "extensions";


        public override AssetData Fee { get; set; }
        public SpaceTypeId Issuer { get; set; }
        public AssetData AssetToIssue { get; set; }
        public SpaceTypeId IssueToAccount { get; set; }
        public object[] Extensions { get; set; }

        public override ChainTypes.Operation Type => ChainTypes.Operation.AssetIssue;

        public AssetIssueOperationData()
        {
            Extensions = new object[0];
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            Fee.ToBuffer(buffer);
            Issuer.ToBuffer(buffer);
            AssetToIssue.ToBuffer(buffer);
            IssueToAccount.ToBuffer(buffer);
            buffer.WriteArray(Extensions, (b, item) =>
            {
                if (!item.IsNull())
                {
                    ;
                }
            });
            return buffer;
        }

        public override string Serialize()
        {
            var builder = new JsonBuilder();
            builder.WriteKeyValuePair(FEE_FIELD_KEY, Fee);
            builder.WriteKeyValuePair(ISSUER_FIELD_KEY, Issuer);
            builder.WriteKeyValuePair(ASSET_TO_ISSUE_FIELD_KEY, AssetToIssue);
            builder.WriteKeyValuePair(ISSUE_TO_ACCOUNT_FIELD_KEY, IssueToAccount);
            builder.WriteKeyValuePair(EXTENSIONS_FIELD_KEY, Extensions);
            return builder.Build();
        }

        public static AssetIssueOperationData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AssetIssueOperationData();
            instance.Fee = value.TryGetValue(FEE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.Issuer = value.TryGetValue(ISSUER_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.AssetToIssue = value.TryGetValue(ASSET_TO_ISSUE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.IssueToAccount = value.TryGetValue(ISSUE_TO_ACCOUNT_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.Extensions = value.TryGetValue(EXTENSIONS_FIELD_KEY, out token) ? token.ToObject<object[]>() : new object[0];
            return instance;
        }
    }
}