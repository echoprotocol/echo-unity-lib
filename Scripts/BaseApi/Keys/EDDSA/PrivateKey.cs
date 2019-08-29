using System;
using System.Security.Cryptography;
using System.Text;
using Base58Check;
using BigI;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using Tools.Assert;
using Tools.Hash;
using Tools.HexBinDec;


namespace Base.Keys.EDDSA
{
    public sealed class PrivateKey : IPrivateKey
    {
        private readonly BigInteger d = null;

        private PublicKey publicKey = null;


        private PrivateKey() { }

        private PrivateKey(BigInteger d)
        {
            this.d = d;
        }

        public void Dispose()
        {
            d.Dispose();
            publicKey?.Dispose();
        }

        public static PrivateKey FromBuffer(byte[] buffer)
        {
            if (buffer.OrEmpty().Length == 0)
            {
                throw new ArgumentException("Empty buffer");
            }
            if (buffer.Length != 32)
            {
                throw new ArgumentException("Expecting 32 bytes, instead got " + buffer.Length);
            }
            return new PrivateKey(BigInteger.FromBuffer(buffer));
        }

        public static IPrivateKey FromSeed(string seed)
        {
            var data = Encoding.UTF8.GetBytes(seed);
            var result = FromSeed(data);
            data.Clear();
            return result;
        }

        public static IPrivateKey FromSeed(byte[] seed)
        {
            var hash = SHA256.Create().HashAndDispose(seed);
            var result = FromBuffer(hash);
            hash.Clear();
            return result;
        }

        public static IPrivateKey FromWif(string wif)
        {
            var fullBuffer = Base58CheckEncoding.DecodePlain(wif);
            Assert.Equal(0x80, fullBuffer.First(), string.Format("Expected version {0}, instead got {1}", 0x80, fullBuffer.First()));
            var buffer = fullBuffer.Slice(0, fullBuffer.Length - 4);
            var checksum = fullBuffer.Slice(fullBuffer.Length - 4);
            fullBuffer.Clear();
            var firstChecksum = SHA256.Create().HashAndDispose(buffer);
            var secondChecksum = SHA256.Create().HashAndDispose(firstChecksum);
            firstChecksum.Clear();
            var newChecksum = secondChecksum.Slice(0, 4);
            secondChecksum.Clear();
            if (!checksum.DeepEqual(newChecksum))
            {
                throw new InvalidOperationException("Checksum did not match");
            }
            checksum.Clear();
            newChecksum.Clear();
            var key = buffer.Slice(1, 32);
            buffer.Clear();
            var result = FromBuffer(key);
            key.Clear();
            return result;
        }

        public string ToWif()
        {
            var key = ToBuffer();
            var buffer = new byte[] { 0x80 }.Concat(key);
            key.Clear();
            var firstChecksum = SHA256.Create().HashAndDispose(buffer);
            var secondChecksum = SHA256.Create().HashAndDispose(firstChecksum);
            firstChecksum.Clear();
            var checksum = secondChecksum.Slice(0, 4);
            secondChecksum.Clear();
            var fullBuffer = buffer.Concat(checksum);
            buffer.Clear();
            checksum.Clear();
            var wif = Base58CheckEncoding.EncodePlain(fullBuffer);
            fullBuffer.Clear();
            return wif;
        }

        public PublicKey PublicKey => publicKey ?? (publicKey = PublicKey.DerivePublicKey(this));

        public IPublicKey ToPublicKey() => PublicKey;

        public byte[] Sign(byte[] data)
        {
            var signature = Signature.SignBuffer(data, this);
            var result = signature.ToBuffer();
            signature.Dispose();
            return result;
        }

        public byte[] ToBuffer() => d.ToBuffer(32);

        public string ToHex()
        {
            var key = ToBuffer();
            var result = key.ToHexString();
            key.Clear();
            return result;
        }

        public override string ToString() => ToHex();
    }
}