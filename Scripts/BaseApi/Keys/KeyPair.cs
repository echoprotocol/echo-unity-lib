using System;
using System.Text;
using Base.Data;
using Buffers;
using CustomTools.Extensions.Core;
using Tools.Assert;


namespace Base.Keys
{
    public class KeyPair : IEquatable<KeyPair>, IEquatable<IPublicKey>, IDisposable
    {
        private const string ACTIVE_KEY = "active";
        private const string ECHORAND_KEY = "echorand";

        private readonly IPrivateKey privateKey = null;


        private KeyPair() { }

        public KeyPair(AuthorityClassification role, string userName, IPass password, IPrivateKeyFactory factory)
        {
            var buffer = new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            var data = Encoding.UTF8.GetBytes(userName.Trim());
            buffer.WriteBytes(data, false);
            data.Clear();
            data = GetRoleData(role);
            buffer.WriteBytes(data, false);
            data.Clear();
            data = password.Get();
            buffer.WriteBytes(data, false);
            data.Clear();
            var seed = buffer.ToArray();
            buffer.Dispose();
            privateKey = factory.FromSeed(seed);
            seed.Clear();
        }

        public KeyPair(IWif wif, IPrivateKeyFactory factory)
        {
            privateKey = factory.FromWif(wif.Get());
        }

        public KeyPair(IPrivateKey privateKey, string associatePublicKey = null)
        {
            this.privateKey = privateKey;
            if (!associatePublicKey.IsNull())
            {
                Assert.Equal(associatePublicKey, Public.ToString(), "Associate public key doesn't equal with generated public key");
            }
        }

        public void Dispose() => privateKey.Dispose();

        public bool Equals(KeyPair otherKeyPair) => Equals(otherKeyPair.Public);

        public bool Equals(IPublicKey publicKey) => Public.Equals(publicKey);

        public IPrivateKey Private => privateKey;

        public IPublicKey Public => privateKey.ToPublicKey();

        private byte[] GetRoleData(AuthorityClassification role)
        {
            if (role.Equals(AuthorityClassification.Active))
            {
                return Encoding.UTF8.GetBytes(ACTIVE_KEY);
            }
            if (role.Equals(AuthorityClassification.Echorand))
            {
                return Encoding.UTF8.GetBytes(ECHORAND_KEY);
            }
            return new byte[0];
        }
    }
}