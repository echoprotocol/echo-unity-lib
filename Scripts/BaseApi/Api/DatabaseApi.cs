using System;
using Base.Data;
using Base.Data.Accounts;
using Base.Data.Assets;
using Base.Data.Block;
using Base.Data.Contract;
using Base.Data.Contract.Result;
using Base.Data.Operations;
using Base.Data.Pairs;
using Base.Data.Properties;
using Base.Data.Transactions;
using Base.Keys;
using Base.Requests;
using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json.Linq;
using RSG;


namespace Base.Api.Database
{
    public sealed class DatabaseApi : ApiId
    {
        private DatabaseApi(ISender sender) : base(null, sender) { }

        public static DatabaseApi Create(ISender sender) => new DatabaseApi(sender);

        public IPromise<DatabaseApi> Init()
        {
            return new Promise<int>((resolve, reject) =>
            {
#if ECHO_DEBUG
                var debug = false;
#else
                var debug = false;
#endif
                var methodName = "database";
                var parameters = new Parameters { LoginApi.ID, methodName, new object[0] };
                DoRequest(GenerateNewId(), parameters, resolve, reject, methodName, debug);
            }).Then(apiId => (DatabaseApi)Init(apiId));
        }

        public IPromise<string> GetChainId()
        {
            if (IsInitialized)
            {
                return new Promise<string>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = false;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_chain_id";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[0] };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetChainId());
        }

        public IPromise<DynamicGlobalPropertiesObject> GetDynamicGlobalProperties()
        {
            if (IsInitialized)
            {
                return new Promise<DynamicGlobalPropertiesObject>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_dynamic_global_properties";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[0] };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetDynamicGlobalProperties());
        }

        public void GetDynamicGlobalProperties(Action<DynamicGlobalPropertiesObject> onSuccess, Action<Exception> onFailed)
        {
            GetDynamicGlobalProperties().Then(onSuccess).Catch(onFailed);
        }

        public IPromise<GlobalPropertiesObject> GetGlobalProperties()
        {
            if (IsInitialized)
            {
                return new Promise<GlobalPropertiesObject>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_global_properties";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[0] };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetGlobalProperties());
        }

        public void GetGlobalProperties(Action<GlobalPropertiesObject> onSuccess, Action<Exception> onFailed)
        {
            GetGlobalProperties().Then(onSuccess).Catch(onFailed);
        }

        public IPromise<SignedBlockData> GetBlock(uint blockNumber)
        {
            if (IsInitialized)
            {
                return new Promise<SignedBlockData>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_block";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { blockNumber } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetBlock(blockNumber));
        }

        public void GetBlock(uint blockNumber, Action<SignedBlockData> onSuccess, Action<Exception> onFailed)
        {
            GetBlock(blockNumber).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<T[]> GetObjects<T>(SpaceTypeId[] objectIds)
        {
            if (IsInitialized)
            {
                return new Promise<T[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_objects";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { Array.ConvertAll(objectIds, objectId => objectId.ToString()) } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetObjects<T>(objectIds));
        }

        public void GetObjects<T>(SpaceTypeId[] objectIds, Action<T[]> onSuccess, Action<Exception> onFailed)
        {
            GetObjects<T>(objectIds).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<T> GetObject<T>(SpaceTypeId objectId)
        {
            return GetObjects<T>(new[] { objectId }).Then(objects => objects.FirstOr(default(T)));
        }

        public void GetObject<T>(SpaceTypeId objectId, Action<T> onSuccess, Action<Exception> onFailed)
        {
            GetObject<T>(objectId).Then(onSuccess).Catch(onFailed);
        }

        #region AssetObject

        public IPromise<AssetObject> GetAsset(uint id = 0)
        {
            return GetObject<AssetObject>(SpaceTypeId.CreateOne(SpaceType.Asset, id));
        }

        public void GetAsset(uint id, Action<AssetObject> onSuccess, Action<Exception> onFailed)
        {
            GetAsset(id).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<AssetObject[]> GetAssets(uint[] ids)
        {
            return GetObjects<AssetObject>(SpaceTypeId.CreateMany(SpaceType.Asset, ids));
        }

        public void GetAssets(uint[] ids, Action<AssetObject[]> onSuccess, Action<Exception> onFailed)
        {
            GetAssets(ids).Then(onSuccess).Catch(onFailed);
        }

        #endregion

        #region AccountObject

        public IPromise<AccountObject> GetAccount(uint id)
        {
            return GetObject<AccountObject>(SpaceTypeId.CreateOne(SpaceType.Account, id));
        }

        public void GetAccount(uint id, Action<AccountObject> onSuccess, Action<Exception> onFailed)
        {
            GetAccount(id).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<AccountObject[]> GetAccounts(uint[] ids)
        {
            return GetObjects<AccountObject>(SpaceTypeId.CreateMany(SpaceType.Account, ids));
        }

        public void GetAccounts(uint[] ids, Action<AccountObject[]> onSuccess, Action<Exception> onFailed)
        {
            GetAccounts(ids).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<UserNameFullAccountDataPair[]> GetFullAccounts(string[] userNamesOrIds, bool subscribe)
        {
            if (IsInitialized)
            {
                return new Promise<UserNameFullAccountDataPair[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_full_accounts";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { userNamesOrIds, subscribe } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetFullAccounts(userNamesOrIds, subscribe));
        }

        public void GetFullAccounts(string[] userNamesOrIds, bool subscribe, Action<UserNameFullAccountDataPair[]> onSuccess, Action<Exception> onFailed)
        {
            GetFullAccounts(userNamesOrIds, subscribe).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<UserNameFullAccountDataPair> GetFullAccount(string userNameOrId, bool subscribe)
        {
            return GetFullAccounts(new[] { userNameOrId }, subscribe).Then(accounts => accounts.FirstOr(null));
        }

        public void GetFullAccount(string userNameOrId, bool subscribe, Action<UserNameFullAccountDataPair> onSuccess, Action<Exception> onFailed)
        {
            GetFullAccount(userNameOrId, subscribe).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<UserNameAccountIdPair[]> LookupAccounts(string prefixName, uint maxCount)
        {
            if (IsInitialized)
            {
                return new Promise<UserNameAccountIdPair[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "lookup_accounts";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { prefixName.ToLower(), maxCount } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.LookupAccounts(prefixName, maxCount));
        }

        public void LookupAccounts(string prefixName, uint maxCount, Action<UserNameAccountIdPair[]> onSuccess, Action<Exception> onFailed)
        {
            LookupAccounts(prefixName, maxCount).Then(onSuccess).Catch(onFailed);
        }

        #endregion

        public IPromise<T[]> GetRequiredFees<T>(OperationData[] operations, uint assetId)
        {
            if (IsInitialized)
            {
                return new Promise<T[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_required_fees";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { operations, SpaceTypeId.ToString(SpaceType.Asset, assetId) } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetRequiredFees<T>(operations, assetId));
        }

        public IPromise<T> GetRequiredFee<T>(OperationData operation, uint assetId)
        {
            return GetRequiredFees<T>(new[] { operation }, assetId).Then(fees => fees.FirstOr(default));
        }

        public IPromise<IPublicKey[]> GetRequiredSignatures(SignedTransactionData transaction, IPublicKey[] existKeys)
        {
            if (IsInitialized)
            {
                return new Promise<Keys.EDDSA.PublicKey[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_required_signatures";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { transaction, Array.ConvertAll(existKeys, key => key.ToString()) } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                }).Then(keys => keys.Convert(key => (IPublicKey)key));
            }
            return Init().Then(api => api.GetRequiredSignatures(transaction, existKeys));
        }

        public IPromise<IPublicKey[]> GetPotentialSignatures(SignedTransactionData transaction)
        {
            if (IsInitialized)
            {
                return new Promise<Keys.EDDSA.PublicKey[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_potential_signatures";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { transaction } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                }).Then(keys => keys.Convert(key => (IPublicKey)key));
            }
            return Init().Then(api => api.GetPotentialSignatures(transaction));
        }

        public IPromise SubscribeNotice(Action<JToken[]> subscribeResultCallback)
        {
            if (IsInitialized)
            {
                return new Promise((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "set_subscribe_callback";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { requestId, true } };
                    DoRequestVoid(requestId, parameters, () =>
                    {
                        ConnectionManager.Subscribe("subscribe by " + requestId, requestId, subscribeResultCallback, debug);
                        resolve();
                    }, reject, title, debug);
                });
            }
            return Init().Then(api => api.SubscribeNotice(subscribeResultCallback));
        }

        public IPromise SubscribeToDynamicGlobalProperties()
        {
            return GetDynamicGlobalProperties().Then(properties => GetObject<DynamicGlobalPropertiesObject>(properties.Id).Then(result => Promise.Resolved()));
        }

        public IPromise SubscribeToContracts(uint[] contractIds)
        {
            return GetObjects<ContractObject>(SpaceTypeId.CreateMany(SpaceType.Contract, contractIds)).Then(result => Promise.Resolved());
        }

        public IPromise<AssetData[]> GetAccountBalances(uint accountId, uint[] assetIds)
        {
            if (IsInitialized)
            {
                return new Promise<AssetData[]>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_account_balances";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName,
                        new object[]
                        {
                            SpaceTypeId.ToString(SpaceType.Account, accountId),
                            SpaceTypeId.ToStrings(SpaceType.Asset, assetIds)
                        }
                    };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetAccountBalances(accountId, assetIds));
        }

        public void GetAccountBalances(uint accountId, uint[] assetIds, Action<AssetData[]> onSuccess, Action<Exception> onFailed)
        {
            GetAccountBalances(accountId, assetIds).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<AssetData> GetAccountBalance(uint accountId, uint assetId = 0)
        {
            return GetAccountBalances(accountId, new[] { assetId }).Then(balances => balances.FirstOr(null));
        }

        public void GetAccountBalance(uint accountId, uint assetId, Action<AssetData> onSuccess, Action<Exception> onFailed)
        {
            GetAccountBalance(accountId, assetId).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<ContractResultData> GetContractResult(uint resultId)
        {
            if (IsInitialized)
            {
                return new Promise<ContractResultData>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_contract_result";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { SpaceTypeId.ToString(SpaceType.ContractResult, resultId) } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetContractResult(resultId));
        }

        public void GetContractResult(uint resultId, Action<ContractResultData> onSuccess, Action<Exception> onFailed)
        {
            GetContractResult(resultId).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<ContractInfoData> GetContractInfo(uint contractId)
        {
            if (IsInitialized)
            {
                return new Promise<ContractInfoData>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "get_contract";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { SpaceTypeId.ToString(SpaceType.Contract, contractId) } };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.GetContractInfo(contractId));
        }

        public void GetContractInfo(uint contractId, Action<ContractInfoData> onSuccess, Action<Exception> onFailed)
        {
            GetContractInfo(contractId).Then(onSuccess).Catch(onFailed);
        }

        public IPromise<string> CallContractNoChangingState(uint contractId, uint accountId, uint assetId, string bytecode)
        {
            if (IsInitialized)
            {
                return new Promise<string>((resolve, reject) =>
                {
#if ECHO_DEBUG
                    var debug = true;
#else
                    var debug = false;
#endif
                    var requestId = GenerateNewId();
                    var methodName = "call_contract_no_changing_state";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName,
                        new object[]
                        {
                            SpaceTypeId.ToString(SpaceType.Contract, contractId),
                            SpaceTypeId.ToString(SpaceType.Account, accountId),
                            SpaceTypeId.ToString(SpaceType.Asset, assetId),
                            bytecode
                        }
                    };
                    DoRequest(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.CallContractNoChangingState(contractId, accountId, assetId, bytecode));
        }

        public void CallContractNoChangingState(uint contractId, uint accountId, uint assetId, string bytecode, Action<string> onSuccess, Action<Exception> onFailed)
        {
            CallContractNoChangingState(contractId, accountId, assetId, bytecode).Then(onSuccess).Catch(onFailed);
        }

        //public IPromise<string> GetContractBalance(uint contractId, uint accountId, uint assetId)
        //{
        //    var methodName = "70a08231"; // balanceOf(address)
        //    return CallContractNoChangingState(contractId, accountId, assetId, methodName);
        //}

        //public void GetContractBalance(uint contractId, uint accountId, uint assetId, Action<string> onSuccess, Action<Exception> onFailed)
        //{
        //    var methodName = "70a08231"; // balanceOf(address)
        //    CallContractNoChangingState(contractId, accountId, assetId, methodName).Then(onSuccess).Catch(onFailed);
        //}
    }
}