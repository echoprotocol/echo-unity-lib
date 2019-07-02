namespace Base.Keys
{
    public interface IPrivateKeyFactory
    {
        IPrivateKey FromSeed(byte[] seed);
    }
}