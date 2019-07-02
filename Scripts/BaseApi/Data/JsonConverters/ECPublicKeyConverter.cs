using System;
using Base.Keys;
using Base.Keys.ECDSA;


namespace Base.Data.Json
{
    public sealed class ECPublicKeyConverter : JsonCustomConverter<IPublicKey, string>
    {
        protected override IPublicKey Deserialize(string value, Type objectType) => PublicKey.FromPublicKeyString(value);

        protected override string Serialize(IPublicKey value) => value.ToString();
    }
}