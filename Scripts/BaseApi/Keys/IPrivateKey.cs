using System;


namespace Base.Keys
{
    public interface IPrivateKey : IDisposable
    {
        IPublicKey ToPublicKey();
        string ToWif();
        byte[] Sign(byte[] data);
    }
}