using System;
using Base.Config;
using Base.Data.Json;
using Base58Check;
using BigI;
using Buffers;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json;
using Tools.Assert;
using Tools.HexBinDec;


namespace Base.Keys.EDDSA
{
    [JsonConverter(typeof(EDPublicKeyConverter))]
    public sealed class PublicKey : IPublicKey
    {
        private readonly BigInteger d = null;


        private PublicKey() { }

        private PublicKey(BigInteger d)
        {
            this.d = d;
        }

        public void Dispose() => d.Dispose();

        private static PublicKey FromBuffer(byte[] buffer)
        {
            if (buffer.ToHexString().Equals(new string('0', 33 * 2)))
            {
                return new PublicKey(null);
            }
            return new PublicKey(BigInteger.FromBuffer(buffer));
        }

        public byte[] ToBuffer()
        {
            if (d.IsNull())
            {
                return new byte[33].Fill((byte)0x00);
            }
            return d.ToBuffer(32);
        }

        public static PublicKey DerivePublicKey(PrivateKey k)
        {
            var buffer = k.ToBuffer();
            var key = ED25519.Ref10.DerivePublicKey(buffer);
            buffer.Clear();
            var result = FromBuffer(key);
            key.Clear();
            return result;
        }

        public override string ToString() => ToPublicKeyString();

        public string ToPublicKeyString(string addressPrefix = null)
        {
            if (addressPrefix.IsNull())
            {
                addressPrefix = ChainConfig.AddressPrefix;
            }
            var buffer = ToBuffer();
            var result = addressPrefix + Base58CheckEncoding.EncodePlain(buffer);
            buffer.Clear();
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
                var buffer = Base58CheckEncoding.DecodePlain(publicKey);
                var result = FromBuffer(buffer);
                buffer.Clear();
                return result;
            }
            catch
            {
                return null;
            }
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
            var key = ToBuffer();
            var result = key.ToHexString();
            key.Clear();
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
            return string.Compare(ToPublicKeyString(), other.ToPublicKeyString(), StringComparison.Ordinal);
        }

        public static int Compare(PublicKey a, PublicKey b) => a.CompareTo(b);

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            var key = ToBuffer();
            var result = (buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING)).WriteBytes(key, false);
            key.Clear();
            return result;
        }
    }
}