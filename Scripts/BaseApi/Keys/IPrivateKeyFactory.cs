namespace Base.Keys
{
    public interface IPrivateKeyFactory
    {
        IPrivateKey FromSeed(byte[] seed);
        IPrivateKey FromWif(string wif);
    }
}