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
        private IPass password;


        public UserNameFullAccountDataPair UserNameData { get; private set; }

        public AuthorizationData(UserNameFullAccountDataPair userNameData, IPass password)
        {
            this.password = password;
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

        public void ResetPass()
        {
            password?.Dispose();
            password = null;
        }

        public async Task<bool> UpdatePassword(IPass newPassword)
        {
            var keys = Keys.FromSeed(UserNameData.Key, newPassword);
            var isValid = await ValidateKeys(keys);
            keys.Dispose();
            if (isValid)
            {
                password?.Dispose();
                password = newPassword;
            }
            return isValid;
        }

        public async Task<bool> ValidateKeys()
        {
            var keys = TryGetKeys();
            var isValid = await ValidateKeys(keys);
            keys.Dispose();
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

        public void Dispose() => password?.Dispose();

        public Keys TryGetKeys() => password.IsNull() ? null : Keys.FromSeed(UserNameData.Key, password);
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

    private IPromise<AuthorizationResult> AuthorizationBy(UserNameFullAccountDataPair dataPair, IPass password)
    {
        return new Promise<AuthorizationResult>(async (resolve, reject) =>
        {
            var authData = new AuthorizationData(dataPair, password);
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

    private IPromise<AuthorizationResult> AuthorizationBy(uint id, IPass password)
    {
        return EchoApiManager.Instance.Database.GetFullAccount(SpaceTypeId.ToString(SpaceType.Account, id), true).Then(result =>
        {
            if (result.IsNull())
            {
                return Promise<AuthorizationResult>.Resolved(AuthorizationResult.UserNotFound);
            }
            return AuthorizationBy(result, password);
        });
    }

    public IPromise<AuthorizationResult> AuthorizationBy(string userName, IPass password)
    {
        return EchoApiManager.Instance.Database.GetFullAccount(userName.Trim(), true).Then(result =>
        {
            if (result.IsNull())
            {
                return Promise<AuthorizationResult>.Resolved(AuthorizationResult.UserNotFound);
            }
            return AuthorizationBy(result, password);
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