namespace Base.Keys.ECDSA
{
    public sealed class KeyFactory : IPrivateKeyFactory
    {
        private KeyFactory() { }

        public static IPrivateKeyFactory Create() => new KeyFactory();

        public IPrivateKey FromSeed(byte[] seed) => PrivateKey.FromSeed(seed);
    }
}