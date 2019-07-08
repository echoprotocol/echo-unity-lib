using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Data;
using Base.Data.Accounts;


namespace Base.Keys
{
    public class Keys : IDisposable
    {
        private readonly Dictionary<AuthorityClassification, KeyPair> keys = new Dictionary<AuthorityClassification, KeyPair>();


        private Keys() { }

        private Keys(Dictionary<AuthorityClassification, KeyPair> keys)
        {
            this.keys = keys;
        }

        public static Keys FromSeed(string userName, IPass password)
        {
            var keys = new Dictionary<AuthorityClassification, KeyPair>();
            var roles = new[] { AuthorityClassification.Active, AuthorityClassification.Echorand };
            foreach (var role in roles)
            {
                keys[role] = new KeyPair(role, userName, password, EDDSA.KeyFactory.Create());
            }
            return new Keys(keys);
        }

        public static Keys FromWif(IWif wif)
        {
			var keys = new Dictionary<AuthorityClassification, KeyPair>();
			var roles = new[] { AuthorityClassification.Active, AuthorityClassification.Echorand };
			foreach (var role in roles)
			{
				keys[role] = new KeyPair(wif, EDDSA.KeyFactory.Create());
			}
			return new Keys(keys);
        }

        public IPrivateKey this[IPublicKey publicKey]
        {
            get
            {
                foreach (var keyPair in keys.Values)
                {
                    if (keyPair.Equals(publicKey))
                    {
                        return keyPair.Private;
                    }
                }
                return null;
            }
        }

        public IPublicKey this[AuthorityClassification role] => keys.ContainsKey(role) ? keys[role].Public : null;

        public int Count => keys.Count;

        public IPublicKey[] PublicKeys
        {
            get
            {
                var result = new List<IPublicKey>();
                foreach (var keyPair in keys.Values)
                {
                    result.Add(keyPair.Public);
                }
                return result.ToArray();
            }
        }

        public async Task<Keys> GetValidatedKeysFor(AccountObject account)
        {
            return await Task.Run(() =>
            {
                if (account == null)
                {
                    return null;
                }
                var result = new Dictionary<AuthorityClassification, KeyPair>();
                foreach (var pair in keys)
                {
                    if (account.IsEquelKey(pair.Key, pair.Value))
                    {
                        result[pair.Key] = pair.Value;
                    }
                }
                return (result.Count > 0) ? new Keys(result) : null;
            });
        }

        public void Dispose()
        {
            foreach (var pair in keys)
            {
                pair.Value.Dispose();
            }
            keys.Clear();
        }
    }
}