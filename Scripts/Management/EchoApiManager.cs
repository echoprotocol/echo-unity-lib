using System;
using System.Collections.Generic;
using Base;
using Base.Api;
using Base.Api.Database;
using Base.Config;
using Base.Data;
using Base.Data.Assets;
using Base.Data.Operations;
using Base.Data.Transactions;
using Base.Requests;
using Base.Responses;
using Base.Storage;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using RSG;
using Tools.HexBinDec;
using WebSocketSharp;


public sealed class EchoApiManager : CustomTools.Singleton.SingletonMonoBehaviour<EchoApiManager>, ISender
{
    public static event Action<string> OnConnectionOpened;
    public static event Action<string> OnConnectionClosed;

    public static event Action OnAllApiInitialized;
    public static event Action<DatabaseApi> OnDatabaseApiInitialized;
    public static event Action<NetworkBroadcastApi> OnNetworkBroadcastApiInitialized;
    public static event Action<HistoryApi> OnHistoryApiInitialized;
    public static event Action<RegistrationApi> OnRegistrationApiInitialized;

    private static string chainId = string.Empty;
    private static RequestIdentificator identificators;

    private readonly static List<Request> requestBuffer = new List<Request>();

    [UnityEngine.SerializeField] private bool sendByUpdate = true;

    private DatabaseApi database;
    private NetworkBroadcastApi networkBroadcast;
    private HistoryApi history;
    private RegistrationApi registration;
    private AuthorizationContainer authorizationContainer;


    public static string ChainId => chainId.OrEmpty();

    public static bool CanDoRequest => IsInstanceExist && ConnectionManager.IsConnected;

    public DatabaseApi Database => database ?? (database = DatabaseApi.Create(this));

    public NetworkBroadcastApi NetworkBroadcast => networkBroadcast ?? (networkBroadcast = NetworkBroadcastApi.Create(this));

    public HistoryApi History => history ?? (history = HistoryApi.Create(this));

    public RegistrationApi Registration => registration ?? (registration = RegistrationApi.Create(this));

    public AuthorizationContainer Authorization => authorizationContainer ?? (authorizationContainer = new AuthorizationContainer());


    #region UnityCallbacks
    protected override void Awake()
    {

        var input = "000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f";
        var output = "13c6c45b3c3043fc0dd58c02955dd3bfbbe05b43feda45b5799a0b4582f70315";
        var data = input.FromHex2Data();
        UnityEngine.Debug.Log("0x 00 01 02 03 04 05 06 07 08 09 0a 0b 0c 0d 0e 0f 10 11 12 13 14 15 16 17 18 19 1a 1b 1c 1d 1e 1f");
        UnityEngine.Debug.Log("0x " + data.ToHexString(' '));
        var result = ED25519REF10.ED25519.DerivePublicKey(data);
        UnityEngine.Debug.Log("0x 13 c6 c4 5b 3c 30 43 fc 0d d5 8c 02 95 5d d3 bf bb e0 5b 43 fe da 45 b5 79 9a 0b 45 82 f7 03 15");
        UnityEngine.Debug.Log("0x " + result.ToHexString(' '));




		identificators = new RequestIdentificator(0);
		ConnectionManager.OnConnectionChanged += InitRegularCallbacks;
		base.Awake();
	}

    private void Update()
    {
        UpConnection();
        if (sendByUpdate && CanSend)
        {
            ConnectionManager.DoAll(requestBuffer);
        }
    }

    private void UpConnection()
    {
        if (ConnectionManager.ReadyState.Equals(WebSocketState.Closed) || ConnectionManager.ReadyState.Equals(WebSocketState.Closing))
        {
            ConnectionManager.Instance.InitConnect();
        }
    }

    private bool CanSend => ConnectionManager.ReadyState.Equals(WebSocketState.Open);

    protected override void OnDestroy()
    {
        ConnectionManager.OnConnectionChanged -= InitRegularCallbacks;
        base.OnDestroy();
    }

    private void OnApplicationQuit() => requestBuffer.Clear();
    #endregion


    #region Initialization
    private void ConnectionOpened(Response response)
    {
        CustomTools.Console.DebugLog("EchoApiManager class", CustomTools.Console.LogMagentaColor("Regular Callback:"), "ConnectionOpened()");
        InitializeApi(LoginApi.Create(this));
        response.SendResultData<string>(url => OnConnectionOpened.SafeInvoke(url), null);
    }

    private void ConnectionClosed(Response response)
    {
        CustomTools.Console.DebugLog("EchoApiManager class", CustomTools.Console.LogMagentaColor("Regular Callback:"), "ConnectionClosed()");
        ResetApi();
        response.SendResultData<string>(reason => OnConnectionClosed.SafeInvoke(reason), null);
    }

    private void ResetApi()
    {
        database = null;
        networkBroadcast = null;
        history = null;
    }

    private void InitializeDone()
    {
        OnAllApiInitialized.SafeInvoke();
    }

    private void InitializeApi(LoginApi api)
    {
        api.Login(string.Empty, string.Empty).Then(loginResult =>
        {
            if (loginResult)
            {
                Promise.All(
                    Database.Init().Then(DatabaseApiInitialized),
                    NetworkBroadcast.Init().Then(NetworkBroadcastApiInitialized),
                    History.Init().Then(HistoryApiInitialized),
                    Registration.Init().Then(RegistrationApiInitialized)
                ).Then(InitializeDone).Catch(ex =>
                {
                    CustomTools.Console.DebugError("EchoApiManager class", CustomTools.Console.LogRedColor(ex.Message), "Initialize all api");
                });
            }
            else
            {
                CustomTools.Console.DebugLog("EchoApiManager class", CustomTools.Console.LogRedColor("Login Failed!"), "Login()");
            }
        });
    }

    private IPromise DatabaseApiInitialized(DatabaseApi api)
    {
        return api.GetChainId().Then(SetChainId).Then(() => Repository.SubscribeToNotice(api).Then(() => Repository.SubscribeToDynamicGlobalProperties(api).Then(() =>
        {
            OnDatabaseApiInitialized.SafeInvoke(api);
            return Promise.Resolved();
        })));
    }

    private IPromise NetworkBroadcastApiInitialized(NetworkBroadcastApi api)
    {
        return new Promise((resolved, rejected) =>
        {
            OnNetworkBroadcastApiInitialized.SafeInvoke(api);
            resolved();
        });
    }

    private IPromise HistoryApiInitialized(HistoryApi api)
    {
        return new Promise((resolved, rejected) =>
        {
            OnHistoryApiInitialized.SafeInvoke(api);
            resolved();
        });
    }

    private IPromise RegistrationApiInitialized(RegistrationApi api)
    {
        return new Promise((resolved, rejected) =>
        {
            OnRegistrationApiInitialized.SafeInvoke(api);
            resolved();
        });
    }
    #endregion


    #region Request/Response
    public void Send(Request request)
    {
        requestBuffer.Add(request);
        if (!sendByUpdate && CanSend)
        {
            ConnectionManager.DoAll(requestBuffer);
        }
    }

    public RequestIdentificator Identificators => identificators;

    private void InitRegularCallbacks(Connection connection)
    {
        connection.AddRegular(identificators.OpenId, ConnectionOpened);
        connection.AddRegular(identificators.CloseId, ConnectionClosed);
    }
    #endregion


    private static void SetChainId(string newChainId)
    {
        newChainId = newChainId.OrEmpty();
        chainId = newChainId;
        ChainConfig.SetChainId(newChainId);
    }

    public IPromise CallContract(uint contractId, uint accountId, string bytecode, uint feeAssetId = 0, long amount = 0, Action<TransactionConfirmationData> resultCallback = null)
    {
        if (!Authorization.IsAuthorized)
        {
            return Promise.Rejected(new InvalidOperationException("Isn't Authorized!"));
        }
        var operation = new CallContractOperationData
        {
            Registrar = SpaceTypeId.CreateOne(SpaceType.Account, accountId),
            Value = new AssetData(amount, SpaceTypeId.CreateOne(SpaceType.Asset, feeAssetId)),
            Code = bytecode.OrEmpty(),
            Callee = SpaceTypeId.CreateOne(SpaceType.Contract, contractId)
        };
        return Authorization.ProcessTransaction(new TransactionBuilder().AddOperation(operation), operation.Value.Asset, resultCallback);
    }

    public IPromise DeployContract(uint accountId, string bytecode, uint feeAssetId = 0, Action<TransactionConfirmationData> resultCallback = null)
    {
        if (!Authorization.IsAuthorized)
        {
            return Promise.Rejected(new InvalidOperationException("Isn't Authorized!"));
        }
        var operation = new CallContractOperationData
        {
            Registrar = SpaceTypeId.CreateOne(SpaceType.Account, accountId),
            Value = new AssetData(0, SpaceTypeId.CreateOne(SpaceType.Asset, feeAssetId)),
            Code = bytecode.OrEmpty(),
            Callee = null
        };
        return Authorization.ProcessTransaction(new TransactionBuilder().AddOperation(operation), operation.Value.Asset, resultCallback);
    }
}