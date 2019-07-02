using System;
using Base.Data;


namespace Base.Keys
{
    public interface IPublicKey : ISerializeToBuffer, IEquatable<IPublicKey>, IComparable<IPublicKey>, IDisposable { }
}