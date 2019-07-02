using System;


namespace Base.Keys
{
    public interface IPrivateKey : IDisposable
    {
        IPublicKey ToPublicKey();
        byte[] Sign(byte[] data);
    }
}