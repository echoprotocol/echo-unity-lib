using System;


namespace Base.Keys
{
    public interface IPass : IDisposable
    {
        byte[] Get();
    }
}