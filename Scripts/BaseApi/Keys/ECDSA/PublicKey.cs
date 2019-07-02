using System;
using System.Security.Cryptography;
using Base.Config;
using Base.Data.Json;
using Base58Check;
using BigI;
using Buffers;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using ECurve;
using Newtonsoft.Json;
using Tools.Assert;
using Tools.Hash;
using Tools.HexBinDec;


namespace Base.Keys.ECDSA
{
    [JsonConverter(typeof(ECPublicKeyConverter))]
    public sealed class PublicKey : IPublicKey
    {
        private readonly Point q = null;


        public Point Q => q;

        private PublicKey() { }

        private PublicKey(Point q)
        {
            this.q = q;
        }

        public void Dispose() => q.Dispose();

        private static PublicKey FromBuffer(byte[] buffer)
        {
            if (buffer.ToHexString().Equals(new string('0', 33 * 2)))
            {
                return new PublicKey(null);
            }
            return new PublicKey(Point.DecodeFrom(Curve.SecP256k1, buffer));
        }

        private byte[] ToBuffer(bool compressed)
        {
            if (q.IsNull())
            {
                return new byte[33].Fill((byte)0x00);
            }
            return q.GetEncoded(compressed);
        }

        public static PublicKey FromPoint(Point point) => new PublicKey(point);

        public IPublicKey ToUncompressed()
        {
            var buffer = q.GetEncoded(false);
            var point = Point.DecodeFrom(Curve.SecP256k1, buffer);
            buffer.Clear();
            return FromPoint(point);
        }

        private byte[] ToBlockchainAddress()
        {
            var buffer = ToBuffer(true);
            var hash = SHA512.Create().HashAndDispose(buffer);
            buffer.Clear();
            var result = RIPEMD160.Create().HashAndDispose(hash);
            hash.Clear();
            return result;
        }

        public override string ToString() => ToPublicKeyString();

        public string ToPublicKeyString(string addressPrefix = null)
        {
            if (addressPrefix.IsNull())
            {
                addressPrefix = ChainConfig.AddressPrefix;
            }
            var buffer = ToBuffer(true);
            var hash = RIPEMD160.Create().HashAndDispose(buffer);
            var checksum = hash.Slice(0, 4);
            hash.Clear();
            var key = buffer.Concat(checksum);
            buffer.Clear();
            checksum.Clear();
            var result = addressPrefix + Base58CheckEncoding.Encode(key);
            key.Clear();
            return result;
        }

        public static IPublicKey FromPublicKeyString(string publicKey, string addressPrefix = null)
        {
            try
            {
                if (addressPrefix.IsNull())
                {
                    addressPrefix = ChainConfig.AddressPrefix;
                }
                var prefix = publicKey.Substring(0, addressPrefix.Length);
                Assert.Equal(
                    addressPrefix, prefix,
                    string.Format("Expecting key to begin with {0}, instead got {1}", addressPrefix, prefix)
                );
                publicKey = publicKey.Substring(addressPrefix.Length);
                var key = Base58CheckEncoding.DecodePlain(publicKey);
                var checksum = key.Slice(key.Length - 4);
                var buffer = key.Slice(0, key.Length - 4);
                key.Clear();
                var hash = RIPEMD160.Create().HashAndDispose(buffer);
                var newChecksum = hash.Slice(0, 4);
                hash.Clear();
                if (!checksum.DeepEqual(newChecksum))
                {
                    throw new InvalidOperationException("Checksum did not match");
                }
                var result = FromBuffer(buffer);
                buffer.Clear();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public string ToAddressString(string addressPrefix = null)
        {
            if (addressPrefix.IsNull())
            {
                addressPrefix = ChainConfig.AddressPrefix;
            }
            var buffer = ToBuffer(true);
            var hash = SHA512.Create().HashAndDispose(buffer);
            buffer.Clear();
            var firstChecksum = RIPEMD160.Create().HashAndDispose(hash);
            hash.Clear();
            var secondChecksum = RIPEMD160.Create().HashAndDispose(firstChecksum);
            var checksum = secondChecksum.Slice(0, 4);
            secondChecksum.Clear();
            buffer = firstChecksum.Concat(checksum);
            firstChecksum.Clear();
            checksum.Clear();
            var result = addressPrefix + Base58CheckEncoding.EncodePlain(buffer);
            buffer.Clear();
            return result;
        }

        public string ToPtsAddy()
        {
            var buffer = ToBuffer(true);
            var firstHash = SHA256.Create().HashAndDispose(buffer);
            buffer.Clear();
            var secondHash = RIPEMD160.Create().HashAndDispose(firstHash);
            firstHash.Clear();
            var hash = new byte[] { 0x38 }.Concat(secondHash); // version 56(decimal)
            secondHash.Clear();
            var firstChecksum = SHA256.Create().HashAndDispose(hash);
            var secondChecksum = SHA256.Create().HashAndDispose(firstChecksum);
            firstChecksum.Clear();
            var checksum = secondChecksum.Slice(0, 4);
            secondChecksum.Clear();
            buffer = hash.Concat(checksum);
            checksum.Clear();
            hash.Clear();
            var result = Base58CheckEncoding.EncodePlain(buffer);
            buffer.Clear();
            return result;
        }

        private IPublicKey Child(byte[] offset)
        {
            Assert.Equal(offset.Length, 32, "offset length");
            var buffer = ToBuffer(true);
            offset = buffer.Concat(offset);
            buffer.Clear();
            var hash = SHA256.Create().HashAndDispose(offset);
            offset.Clear();
            var c = BigInteger.FromBuffer(hash);
            hash.Clear();
            if (c.CompareTo(Curve.SecP256k1.N) >= 0)
            {
                throw new InvalidOperationException("Child offset went out of bounds, try again");
            }
            var cG = Curve.SecP256k1.G.Multiply(c);
            c.Dispose();
            var qPrime = q.Addition(cG);
            if (Curve.SecP256k1.IsInfinity(qPrime))
            {
                throw new InvalidOperationException("Child offset derived to an invalid key, try again");
            }
            return FromPoint(qPrime);
        }

        public static IPublicKey FromHex(string hexString)
        {
            var data = hexString.FromHex2Data();
            var result = FromBuffer(data);
            data.Clear();
            return result;
        }

        public string ToHex()
        {
            var data = ToBuffer(true);
            var result = data.ToHexString();
            data.Clear();
            return result;
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj is PublicKey)
            {
                return Equals((PublicKey)obj);
            }
            return false;
        }

        public bool Equals(IPublicKey other) => ToString().Equals(other.ToNullableString());

        public int CompareTo(IPublicKey other)
        {
            if (other is PublicKey)
            {
                return CompareTo((PublicKey)other);
            }
            throw new ArgumentException("Can't compare different public key types");
        }

        private int CompareTo(PublicKey other)
        {
            return string.Compare(ToAddressString(), other.ToAddressString(), StringComparison.Ordinal);
        }

        public static int Compare(PublicKey a, PublicKey b) => a.CompareTo(b);

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            var key = ToBuffer(true);
            var result = (buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING)).WriteBytes(key, false);
            key.Clear();
            return result;
        }
    }
}