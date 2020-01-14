//#define BY_PASS
#define BY_WIF

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Data;
using Base.Data.Accounts;
using Base.Data.Balances;
using Base.Data.Pairs;
using Base.Data.Transactions;
using Base.Keys;
using Base.Storage;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using RSG;
using Tools.Json;


public sealed class AuthorizationContainer
{
    public enum AuthorizationResult
    {
        Ok,             // authorized
        Failed,         // username password pair is't correct
        UserNotFound,   // user nor found
        Error           // some error
    }


    public sealed class AuthorizationData : IDisposable
    {
#if BY_PASS
        private IPass password;
#elif BY_WIF
        private IWif wif;
#else
#endif


        public UserNameFullAccountDataPair UserNameData { get; private set; }

#if BY_PASS
        public AuthorizationData(UserNameFullAccountDataPair userNameData, IPass password)
        {
            this.password = password;
#elif BY_WIF
        public AuthorizationData(UserNameFullAccountDataPair userNameData, IWif wif)
        {
            this.wif = wif;
#else
        public AuthorizationData(UserNameFullAccountDataPair userNameData)
        {
#endif
            UserNameData = userNameData;
        }

        public void UpdateAccountData(IdObject idObject)
        {
            if (idObject.Id.Equals(UserNameData.Value.Account.Id))
            {
                UserNameData.Value.Account = (AccountObject)idObject;
            }
            else
            if (idObject.Id.Equals(UserNameData.Value.Statistics.Id))
            {
                UserNameData.Value.Statistics = (AccountStatisticsObject)idObject;
            }
            else
            if (idObject.Id.SpaceType.Equals(SpaceType.AccountBalance))
            {
                for (var i = 0; i < UserNameData.Value.Balances.OrEmpty().Length; i++)
                {
                    if (idObject.Id.Equals(UserNameData.Value.Balances[i].Id))
                    {
                        UserNameData.Value.Balances[i] = (AccountBalanceObject)idObject;
                        break;
                    }
                }
            }
        }

        public void Reset()
        {
#if BY_PASS
            password?.Dispose();
            password = null;
#elif BY_WIF
            wif?.Dispose();
            wif = null;
#else
#endif
        }

#if BY_PASS
        public async Task<bool> Update(IPass newPassword)
        {
            var keys = Keys.FromSeed(UserNameData.Key, newPassword);
            var isValid = await ValidateKeys(keys);
            keys.Dispose();
            if (isValid)
            {
                password?.Dispose();
                password = newPassword;
            }
#elif BY_WIF
        public async Task<bool> Update(IWif newWif)
        {
            var keys = Keys.FromWif(newWif);
            var isValid = await ValidateKeys(keys);
            keys.Dispose();
            if (isValid)
            {
                wif?.Dispose();
                wif = newWif;
            }
#else
        public async Task<bool> Update()
        {
            var isValid = false;
#endif
            return isValid;
        }

        public async Task<bool> ValidateKeys()
        {
            var keys = TryGetKeys();
            var isValid = await ValidateKeys(keys);
            keys?.Dispose();
            return isValid;
        }

        private async Task<bool> ValidateKeys(Keys keys)
        {
            if (keys.IsNull())
            {
                return false;
            }
            var result = await keys.GetValidatedKeysFor(UserNameData.Value.Account);
            return !result.IsNull();
        }

#if BY_PASS
        public void Dispose() => password?.Dispose();

        public Keys TryGetKeys() => password.IsNull() ? null : Keys.FromSeed(UserNameData.Key, password);
#elif BY_WIF
        public void Dispose() => wif?.Dispose();

        public Keys TryGetKeys() => wif.IsNull() ? null : Keys.FromWif(wif);
#else
        public void Dispose() { }

        public Keys TryGetKeys() => null;
#endif
    }


    public static event Action<AuthorizationData> OnAuthorizationChanged;

    private AuthorizationData authorization;


    public AuthorizationData Current
    {
        get { return authorization; }
        private set
        {
            if (authorization != value)
            {
                if (!authorization.IsNull())
                {
                    Repository.OnGetObject -= authorization.UpdateAccountData;
                }
                authorization?.Dispose();
                authorization = value;
                if (!authorization.IsNull())
                {
                    Repository.OnGetObject += authorization.UpdateAccountData;
                }
                OnAuthorizationChanged.SafeInvoke(authorization);
            }
        }
    }

#if BY_PASS
    private IPromise<AuthorizationResult> AuthorizationBy(UserNameFullAccountDataPair dataPair, IPass password)
#elif BY_WIF
    private IPromise<AuthorizationResult> AuthorizationBy(UserNameFullAccountDataPair dataPair, IWif wif)
#else
    private IPromise<AuthorizationResult> AuthorizationBy(UserNameFullAccountDataPair dataPair)
#endif
    {
        return new Promise<AuthorizationResult>(async (resolve, reject) =>
        {
#if BY_PASS
            var authData = new AuthorizationData(dataPair, password);
#elif BY_WIF
            var authData = new AuthorizationData(dataPair, wif);
#else
            var authData = new AuthorizationData(dataPair);
#endif
            try
            {
                if (await authData.ValidateKeys())
                {
                    Current = authData;
                    resolve(AuthorizationResult.Ok);
                }
                else
                {
                    authData.Dispose();
                    resolve(AuthorizationResult.Failed);
                }
            }
            catch
            {
                authData.Dispose();
                resolve(AuthorizationResult.Error);
            }
        });
    }

#if BY_PASS
    private IPromise<AuthorizationResult> AuthorizationBy(uint id, IPass password)
#elif BY_WIF
    private IPromise<AuthorizationResult> AuthorizationBy(uint id, IWif wif)
#else
    private IPromise<AuthorizationResult> AuthorizationBy(uint id)
#endif
    {
        return EchoApiManager.Instance.Database.GetFullAccount(SpaceTypeId.ToString(SpaceType.Account, id), true).Then(result =>
        {
            if (result.IsNull())
            {
                return Promise<AuthorizationResult>.Resolved(AuthorizationResult.UserNotFound);
            }
#if BY_PASS
            return AuthorizationBy(result, password);
#elif BY_WIF
            return AuthorizationBy(result, wif);
#else
            return Promise<AuthorizationResult>.Resolved(AuthorizationResult.Failed);
#endif
        });
    }

#if BY_PASS
    public IPromise<AuthorizationResult> AuthorizationBy(string userName, IPass password)
#elif BY_WIF
    public IPromise<AuthorizationResult> AuthorizationBy(string userName, IWif wif)
#else
    public IPromise<AuthorizationResult> AuthorizationBy(string userName)
#endif
    {
        return EchoApiManager.Instance.Database.GetFullAccount(userName.Trim(), true).Then(result =>
        {
            if (result.IsNull())
            {
                return Promise<AuthorizationResult>.Resolved(AuthorizationResult.UserNotFound);
            }
#if BY_PASS
            return AuthorizationBy(result, password);
#elif BY_WIF
            return AuthorizationBy(result, wif);
#else
            return Promise<AuthorizationResult>.Resolved(AuthorizationResult.Failed);
#endif
        });
    }

    public void ResetAuthorization()
    {
        Current = null;
    }

    public IPromise ProcessTransaction(TransactionBuilder builder, SpaceTypeId asset = null, Action<TransactionConfirmationData> resultCallback = null)
    {
        if (!IsAuthorized)
        {
            return Promise.Rejected(new InvalidOperationException("Isn't Authorized!"));
        }
        return new Promise(async (resolve, reject) =>
        {
            Keys keys = null;

            void Resolve()
            {
                keys?.Dispose();
                resolve();
            }

            void Reject(Exception ex)
            {
                keys?.Dispose();
                reject(ex);
            }

            try
            {
                keys = Current.TryGetKeys();
                if (keys.IsNull())
                {
                    throw new InvalidOperationException("Isn't Authorized!");
                }
                var validKeys = await keys.GetValidatedKeysFor(Current.UserNameData.Value.Account);
                if (!validKeys.IsNull())
                {
                    var existPublicKeys = validKeys.PublicKeys;
                    TransactionBuilder.SetRequiredFees(builder, asset).Then(b => b.GetPotentialSignatures().Then(potentialPublicKeys =>
                    {
                        var availableKeys = new List<IPublicKey>();
                        foreach (var existPublicKey in existPublicKeys)
                        {
                            if (!availableKeys.Contains(existPublicKey) && Array.IndexOf(potentialPublicKeys, existPublicKey) != -1)
                            {
                                availableKeys.Add(existPublicKey);
                            }
                        }
                        if (availableKeys.IsNullOrEmpty())
                        {
                            throw new InvalidOperationException("Available key doesn't find!");
                        }
                        return b.GetRequiredSignatures(availableKeys.ToArray()).Then(requiredPublicKeys =>
                        {
                            if (requiredPublicKeys.IsNullOrEmpty())
                            {
                                throw new InvalidOperationException("Required key doesn't find!");
                            }
                            if (!IsAuthorized)
                            {
                                throw new InvalidOperationException("Isn't Authorized!");
                            }
                            var selectedPublicKey = requiredPublicKeys.First(); // select key
                            b.AddSigner(new KeyPair(validKeys[selectedPublicKey])).Broadcast(resultCallback).Then(Resolve).Catch(Reject);
                        }).Catch(Reject);
                    }).Catch(Reject)).Catch(Reject);
                }
                else
                {
                    throw new InvalidOperationException("Isn't Authorized!");
                }
            }
            catch (Exception ex)
            {
                Reject(ex);
            }
        });
    }

    public bool IsAuthorized => !Current.IsNull();

    public UserNameFullAccountDataPair UserData => IsAuthorized ? Current.UserNameData : null;
}