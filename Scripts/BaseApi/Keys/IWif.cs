using System;


namespace Base.Keys
{
    public interface IWif : IDisposable
    {
        string Get();
    }
}