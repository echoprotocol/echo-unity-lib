using System;
using System.Security.Cryptography;
using DZen.Security.Cryptography;


namespace Tools.Hash
{
    public static class Extensions
    {
        public static byte[] HashAndDispose(this SHA3 sha3, byte[] buffer)
        {
            var hash = sha3.ComputeHash(buffer);
            (sha3 as IDisposable).Dispose();
            return hash;
        }

        public static byte[] HashAndDispose(this SHA256 sha256, byte[] buffer)
        {
            var hash = sha256.ComputeHash(buffer);
            (sha256 as IDisposable).Dispose();
            return hash;
        }

        public static byte[] HashAndDispose(this SHA512 sha512, byte[] buffer)
        {
            var hash = sha512.ComputeHash(buffer);
            (sha512 as IDisposable).Dispose();
            return hash;
        }

        public static byte[] HashAndDispose(this RIPEMD160 ripemd160, byte[] buffer)
        {
            var hash = ripemd160.ComputeHash(buffer);
            (ripemd160 as IDisposable).Dispose();
            return hash;
        }

        public static byte[] HashAndDispose(this HMACSHA256 hmacsha256, byte[] buffer)
        {
            var hash = hmacsha256.ComputeHash(buffer);
            (hmacsha256 as IDisposable).Dispose();
            return hash;
        }
    }


    public static class KECCAK256
    {
        public static SHA3 Create() => new SHA3256Managed { UseKeccakPadding = true };
    }
}