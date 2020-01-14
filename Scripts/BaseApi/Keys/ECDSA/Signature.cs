using System;
using System.Security.Cryptography;
using System.Text;
using BigI;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using ECurve;
using Tools.Assert;
using Tools.Hash;
using Tools.HexBinDec;


namespace Base.Keys.ECDSA
{
    public sealed class Signature : IDisposable
    {
        private readonly byte i;
        private readonly BigInteger r;
        private readonly BigInteger s;


        private Signature(BigInteger r, BigInteger s, byte i)
        {
            this.r = r;
            this.s = s;
            this.i = i;
        }

        public void Dispose()
        {
            r.Dispose();
            s.Dispose();
        }

        public static Signature FromHex(string hexString)
        {
            var buffer = hexString.FromHex2Data();
            var result = FromBuffer(buffer);
            buffer.Clear();
            return result;
        }

        public static Signature FromBuffer(byte[] buffer)
        {
            Assert.Equal(buffer.Length, 65, "Invalid signature length");
            var i = buffer[0];
            Assert.Equal(i - 27, (i - 27) & 7, "Invalid signature parameter");
            var data = buffer.Slice(1, 32);
            var r = BigInteger.FromBuffer(data);
            data.Clear();
            data = buffer.Slice(33);
            var s = BigInteger.FromBuffer(data);
            data.Clear();
            return new Signature(r, s, i);
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
            var buffer = new byte[65];
            buffer[0] = i;
            var data = r.ToBuffer(32);
            Array.Copy(data, 0, buffer, 1, 32);
            data.Clear();
            data = s.ToBuffer(32);
            Array.Copy(data, 0, buffer, 33, 32);
            data.Clear();
            return buffer;
        }

        public PublicKey RecoverPublicKeyFromBuffer(byte[] buffer)
        {
            var hash = SHA256.Create().HashAndDispose(buffer);
            var result = RecoverPublicKey(hash);
            hash.Clear();
            return result;
        }

        private PublicKey RecoverPublicKey(byte[] bufferSha256)
        {
            var e = BigInteger.FromBuffer(bufferSha256);
            var q = ECDSA.RecoverPublicKey(Curve.SecP256k1, e, new ECSignature(r, s), (byte)((i - 27) & 3));
            e.Dispose();
            return PublicKey.FromPoint(q);
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
            var hash = SHA256.Create().HashAndDispose(buffer);
            var result = SignBufferSha256(hash, privateKey);
            hash.Clear();
            return result;
        }

        private static Signature SignBufferSha256(byte[] bufferSha256, PrivateKey privateKey)
        {
            if (bufferSha256.Length != 32)
            {
                throw new ArgumentException("bufferSha256: 32 byte buffer requred");
            }
            var nonce = uint.MinValue;
            var e = BigInteger.FromBuffer(bufferSha256);
            ECSignature ecSignature = null;
            var i = byte.MinValue;
            while (true)
            {
                ecSignature?.Dispose();
                ecSignature = ECDSA.Sign(Curve.SecP256k1, bufferSha256, privateKey.D, nonce++);
                var der = ecSignature.ToDER();
                var lengthR = der[3];
                var lengthS = der[5 + lengthR];
                der.Clear();
                if (lengthR == 32 && lengthS == 32)
                {
                    i = ECDSA.CalculatePublicKeyRecoveryParameter(Curve.SecP256k1, e, ecSignature, privateKey.PublicKey.Q);
                    i += 4;  // compressed
                    i += 27; // compact  //  24 or 27 :( forcing odd-y 2nd key candidate)
                    break;
                }
                if (nonce % 10 == 0)
                {
                    CustomTools.Console.DebugWarning(nonce, "attempts to find canonical signature");
                }
            }
            e.Dispose();
            var result = new Signature(ecSignature.R.Clone(), ecSignature.S.Clone(), i);
            ecSignature.Dispose();
            return result;
        }

        public bool VerifyHex(string hexString, PublicKey publicKey)
        {
            var buffer = hexString.FromHex2Data();
            var result = VerifyBuffer(buffer, publicKey);
            buffer.Clear();
            return result;
        }

        public bool VerifyBuffer(byte[] buffer, PublicKey publicKey)
        {
            var hash = SHA256.Create().HashAndDispose(buffer);
            var result = VerifyHash(hash, publicKey);
            hash.Clear();
            return result;
        }

        private bool VerifyHash(byte[] hash, PublicKey publicKey)
        {
            if (hash.Length != 32)
            {
                CustomTools.Console.DebugWarning("A sha256 hash should be 32 bytes long, instead got ", hash.Length);
            }
            return ECDSA.Verify(Curve.SecP256k1, hash, new ECSignature(r, s), publicKey.Q);
        }
    }
}