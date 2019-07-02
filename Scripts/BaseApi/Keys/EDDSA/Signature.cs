using System;
using System.Text;
using BigI;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using ED25519REF10;
using Tools.Assert;
using Tools.HexBinDec;


namespace Base.Keys.EDDSA
{
    public sealed class Signature : IDisposable
    {
        private readonly BigInteger r;
        private readonly BigInteger s;


        private Signature(BigInteger r, BigInteger s)
        {
            this.r = r;
            this.s = s;
        }

        public void Dispose()
        {
            r.Dispose();
            s.Dispose();
        }

        public static Signature FromHex(string hexString) => FromBuffer(hexString.FromHex2Data());

        public static Signature FromBuffer(byte[] buffer)
        {
            Assert.Equal(buffer.Length, 64, "Invalid signature length");
            var data = buffer.Slice(0, 32);
            var r = BigInteger.FromBuffer(data);
            data.Clear();
            data = buffer.Slice(32);
            var s = BigInteger.FromBuffer(data);
            data.Clear();
            return new Signature(r, s);
        }

        public string ToHex()
        {
            var buffer = ToBuffer();
            var result = buffer.ToHexString();
            buffer.Clear();
            return result;
        }

        public byte[] ToBuffer()
        {
            var buffer = new byte[64];
            var data = r.ToBuffer(32);
            Array.Copy(data, 0, buffer, 0, 32);
            data.Clear();
            data = s.ToBuffer(32);
            Array.Copy(data, 0, buffer, 32, 32);
            data.Clear();
            return buffer;
        }

        public static Signature Sign(string str, PrivateKey privateKey)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var result = SignBuffer(buffer, privateKey);
            buffer.Clear();
            return result;
        }

        public static Signature SignHex(string hexString, PrivateKey privateKey)
        {
            var buffer = hexString.FromHex2Data();
            var result = SignBuffer(buffer, privateKey);
            buffer.Clear();
            return result;
        }

        public static Signature SignBuffer(byte[] buffer, PrivateKey privateKey)
        {
            var privateBuffer = privateKey.ToBuffer();
            var publicBuffer = privateKey.PublicKey.ToBuffer();
            var data = ED25519.Sign(buffer, publicBuffer, privateBuffer);
            privateBuffer.Clear();
            publicBuffer.Clear();
            var result = FromBuffer(data);
            data.Clear();
            return result;
        }
    }
}