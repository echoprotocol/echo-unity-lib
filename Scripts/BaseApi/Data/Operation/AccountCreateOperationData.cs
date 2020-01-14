using Base.Config;
using Base.Data.Accounts;
using Base.Data.Assets;
using Base.Keys;
using Base.Keys.EDDSA;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations
{
    public sealed class AccountCreateOperationData : OperationData
    {
        private const string FEE_FIELD_KEY = "fee";
        private const string REGISTRAR_FIELD_KEY = "registrar";
        private const string NAME_FIELD_KEY = "name";
        private const string ACTIVE_FIELD_KEY = "active";
        private const string OPTIONS_FIELD_KEY = "options";
        private const string ECHORAND_KEY_FIELD_KEY = "echorand_key";
        private const string EXTENSIONS_FIELD_KEY = "extensions";


        public override AssetData Fee { get; set; }
        public SpaceTypeId Registrar { get; set; }
        public string Name { get; set; }
        public AuthorityData Active { get; set; }
        public PublicKey EchorandKey { get; set; }
        public AccountOptionsData Options { get; set; }
        public object Extensions { get; set; }

        public override ChainTypes.Operation Type => ChainTypes.Operation.AccountCreate;

        public bool IsEquelKey(AuthorityClassification role, KeyPair key)
        {
            switch (role)
            {
                case AuthorityClassification.Active:
                    if (Active != null && Active.KeyAuths != null)
                    {
                        foreach (var keyAuth in Active.KeyAuths)
                        {
                            if (key.Equals(keyAuth.Key))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                case AuthorityClassification.Echorand:
                    return key.Equals(EchorandKey);
                default:
                    return false;
            }
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            Fee.ToBuffer(buffer);
            Registrar.ToBuffer(buffer);
            buffer.WriteString(Name);
            Active.ToBuffer(buffer);
            EchorandKey.ToBuffer(buffer);
            Options.ToBuffer(buffer);
            //Extensions.ToBuffer(buffer); // todo
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { FEE_FIELD_KEY,                Fee },
                { REGISTRAR_FIELD_KEY,          Registrar },
                { NAME_FIELD_KEY,               Name },
                { ACTIVE_FIELD_KEY,             Active },
                { ECHORAND_KEY_FIELD_KEY,       EchorandKey },
                { OPTIONS_FIELD_KEY,            Options },
                { EXTENSIONS_FIELD_KEY,         Extensions }
            }).Build();
        }

        public static AccountCreateOperationData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AccountCreateOperationData();
            instance.Fee = value.TryGetValue(FEE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.Registrar = value.TryGetValue(REGISTRAR_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.Name = value.TryGetValue(NAME_FIELD_KEY, out token) ? token.ToObject<string>() : string.Empty;
            instance.Active = value.TryGetValue(ACTIVE_FIELD_KEY, out token) ? token.ToObject<AuthorityData>() : null;
            instance.EchorandKey = value.TryGetValue(ECHORAND_KEY_FIELD_KEY, out token) ? token.ToObject<PublicKey>() : null;
            instance.Options = value.TryGetValue(OPTIONS_FIELD_KEY, out token) ? token.ToObject<AccountOptionsData>() : null;
            instance.Extensions = value.TryGetValue(EXTENSIONS_FIELD_KEY, out token) ? token.ToObject<object>() : new object();
            return instance;
        }
    }
}