using System;
using System.Security.Cryptography;
using System.Text;
using Base58Check;
using BigI;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using ECurve;
using Tools.Assert;
using Tools.Hash;
using Tools.HexBinDec;


namespace Base.Keys.ECDSA
{
    public sealed class PrivateKey : IPrivateKey
    {
        private readonly BigInteger d = null;

        private PublicKey publicKey = null;


        public BigInteger D => d;

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

        private static PrivateKey FromBuffer(byte[] buffer)
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

        public PublicKey PublicKey => publicKey ?? (publicKey = PublicKey.FromPoint(Curve.SecP256k1.G.Multiply(d)));

        public IPublicKey ToPublicKey() => PublicKey;

        public byte[] Sign(byte[] data)
        {
            var signature = Signature.SignBuffer(data, this);
            var result = signature.ToBuffer();
            signature.Dispose();
            return result;
        }

        private byte[] ToBuffer() => d.ToBuffer(32);

        /** ECIES */
        private byte[] GetSharedSecret(PublicKey key, bool legacy = false)
        {
            var kb = key.ToUncompressed().ToBuffer().ToArray();
            var kbP = Point.FromAffine(Curve.SecP256k1, BigInteger.FromBuffer(kb.Slice(1, 33)), BigInteger.FromBuffer(kb.Slice(33, 65)));
            var r = ToBuffer();
            var p = kbP.Multiply(BigInteger.FromBuffer(r));
            r.Clear();
            var s = p.AffineX.ToBuffer(32);
            if (s.Length > 32)
            {
                Array.Resize(ref s, 32);
            }
            // The input to sha512 must be exactly 32-bytes, to match the c++ implementation
            // of GetSharedSecret. Right now S will be shorter if the most significant
            // byte(s) is zero. Pad it backfull 32-bytes
            if (!legacy && s.Length < 32)
            {
                var padding = new byte[32 - s.Length].Fill((byte)0x00);
                var nS = padding.Concat(s);
                s.Clear();
                s = nS;
            }
            // SHA512 used in ECIES
            var result = SHA512.Create().HashAndDispose(s);
            s.Clear();
            return result;
        }

        private IPrivateKey Child(byte[] offset)
        {
            var buffer = ToPublicKey().ToBuffer();
            var data = buffer.ToArray();
            buffer.Dispose();
            offset = data.Concat(offset);
            data.Clear();
            var hash = SHA256.Create().HashAndDispose(offset);
            offset.Clear();
            var c = BigInteger.FromBuffer(hash);
            hash.Clear();
            if (c.CompareTo(Curve.SecP256k1.N) >= 0)
            {
                throw new InvalidOperationException("Child offset went out of bounds, try again");
            }
            var derived = d.Addition(c); //.Modulo(Curve.SecP256k1.N);
            c.Dispose();
            if (derived.Sign == 0)
            {
                throw new InvalidOperationException("Child offset derived to an invalid key, try again");
            }
            return new PrivateKey(derived);
        }

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