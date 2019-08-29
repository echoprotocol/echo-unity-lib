using CustomTools.Extensions.Core;


namespace Base.Keys.EDDSA
{
    public sealed class KeyFactory : IPrivateKeyFactory
    {
        private KeyFactory() { }

        public static KeyFactory Create() => new KeyFactory();

        public IPrivateKey FromSeed(byte[] seed) => PrivateKey.FromSeed(seed);
        public IPrivateKey FromWif(string wif) => PrivateKey.FromWif(wif);

        public IPrivateKey Generate()
        {
            var privateKey = new byte[0];
            var publicKey = new byte[0];
            ED25519.Ref10.CreateKeyPair(out publicKey, out privateKey);
            publicKey.Clear();
            var key = FromSeed(privateKey);
            privateKey.Clear();
            return key;
        }
    }
}