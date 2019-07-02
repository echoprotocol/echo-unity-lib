using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Base.Requests;
using Base.Responses;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketSharp;


public sealed class ConnectionManager : CustomTools.Singleton.SingletonMonoBehaviour<ConnectionManager>
{
    public static event Action<Connection> OnConnectionChanged;
    public static event Action<Connection> OnConnectionOpened;
    public static event Action<Connection> OnConnectionClosed;
    public static event Action<Connection, Response> OnMessageReceived;
    public static event Action<string> OnConnectionAttemptsDone;

    private const int MAX_PROCESSING_RECEIVED_MESSAGE_PER_UPDATE = 20;

    public const string HTTP = "http://";
    public const string WSS = "wss://";
    public const string WS = "ws://";
    public const string SEPARATOR = "://";
    public const string DOT = ".";

    [SerializeField] private bool pingHostBeforeConnecting = true;
    [SerializeField] private float delayBetweenTryConnect = 5f;
    [SerializeField] private int tryConnectCount = 5;
    [SerializeField] private bool sendFromIndividualThread = true;

    private string url = string.Empty;
    private float lastTryConnectTime;
    private int connectAttempts;
    private bool connectProcessing;

    private static bool lastServerAvaliableFlag;
    private static Connection openConnection;


    public static string PingUrl => HTTP + "www.google.com/";

    private void Update() => openConnection?.DequeuReceivedMessages(MAX_PROCESSING_RECEIVED_MESSAGE_PER_UPDATE);

    private void OnApplicationQuit()
    {
        if (!openConnection.IsNull())
        {
            openConnection.ConnectionOpened -= ConnectionOpened;
            openConnection.ConnectionClosed -= ConnectionClosed;
            openConnection.MessageReceived -= MessageReceived;
            openConnection.Disconnect();
            openConnection.Dispose();
            openConnection = null;
        }
    }

    protected override void OnDestroy()
    {
        OnConnectionChanged = null;
        OnConnectionOpened = null;
        OnConnectionClosed = null;
        OnMessageReceived = null;
        base.OnDestroy();
    }

    // call when client connect to server successful
    private static void ConnectionOpened(Connection connection)
    {
        CustomTools.Console.DebugLog("Connected to", connection.FullUrl);
        OnConnectionOpened.SafeInvoke(connection);
    }

    // call when client close connection
    private static void ConnectionClosed(Connection connection)
    {
        CustomTools.Console.DebugLog("Connection closed");
        OnConnectionClosed.SafeInvoke(connection);
    }

    // call if received message not find event for processing
    private static void MessageReceived(Connection connection, Response msg)
    {
        CustomTools.Console.DebugLog(CustomTools.Console.LogYellowColor("Received unbinding message:"), CustomTools.Console.LogWhiteColor(msg));
        OnMessageReceived.SafeInvoke(connection, msg);
    }

    public static bool IsConnected
    {
        get { return IsServerAvaliable && (openConnection.ReadyState.Equals(WebSocketState.Connecting) || openConnection.ReadyState.Equals(WebSocketState.Open)) && openConnection.IsAlive; }
    }

    public static bool IsServerAvaliable => !openConnection.IsNull() && lastServerAvaliableFlag;

    public static WebSocketState ReadyState => IsServerAvaliable ? openConnection.ReadyState : WebSocketState.Closed;

    public static void DoAll(List<Request> requests)
    {
        if (requests.IsEmpty())
        {
            return;
        }
        var notSended = new List<Request>(requests);
        foreach (var request in requests)
        {
            if (IsServerAvaliable && openConnection.Send(request))
            {
                if (request.Debug)
                {
                    request.PrintDebugLog();
                }
                notSended.Remove(request);
            }
        }
        requests.Clear();
        requests.AddRange(notSended);
    }

    public static void Subscribe(string responseTitle, int subscribeId, Action<JToken[]> subscribeCallback, bool debug, bool singleCall = false)
    {
        if (!openConnection.IsNull() && !subscribeCallback.IsNull())
        {
            if (singleCall)
            {
                openConnection.AddRegular(subscribeId, response =>
                {
                    if (debug)
                    {
                        response.PrintDebugLog(responseTitle);
                    }
                    response.SendNoticeData(subscribeCallback);
                    Unsubscribe(response.RequestId);
                });
            }
            else
            {
                openConnection.AddRegular(subscribeId, response =>
                {
                    if (debug)
                    {
                        response.PrintDebugLog(responseTitle);
                    }
                    response.SendNoticeData(subscribeCallback);
                });
            }
        }
    }

    public static void Unsubscribe(int subscribeId) => openConnection?.RemoveRegular(subscribeId);

    public bool InitConnect()
    {
        if (url.IsNull() || url.Equals(string.Empty))
        {
            return false;
        }
        if (connectProcessing)
        {
            return false;
        }
        if (connectAttempts >= tryConnectCount)
        {
            if ((Time.realtimeSinceStartup - lastTryConnectTime) <= delayBetweenTryConnect)
            {
                return false;
            }
            connectAttempts = 0;
            if (!OnConnectionAttemptsDone.IsNull())
            {
                OnConnectionAttemptsDone(url);
                return false;
            }
        }
        if (pingHostBeforeConnecting)
        {
            connectProcessing = true;
            StartCoroutine(PingAndConnect(url));
        }
        else
        {
            lastServerAvaliableFlag = true;
            Connect(url);
            lastTryConnectTime = Time.realtimeSinceStartup;
            connectAttempts++;
        }
        return true;
    }

    public void ReconnectTo(string newUrl)
    {
        if (newUrl.IsNull() || newUrl.Equals(string.Empty))
        {
            return;
        }
        if (newUrl.Equals(url))
        {
            return;
        }
        url = newUrl;
        Disconnect(true);
        lastServerAvaliableFlag = false;
        InitConnect();
    }

    public void Disconnect(bool resetConnection = false)
    {
        openConnection?.Disconnect();
        if (resetConnection)
        {
            openConnection = null;
        }
    }

    private IEnumerator PingAndConnect(string targetHost)
    {
        var parts = targetHost.Split(new[] { SEPARATOR }, StringSplitOptions.None);
        var scheme = parts.First();
        var host = parts.Last();
        var pindHostRequest = new WWW(HTTP + host);
        yield return pindHostRequest;
        if (lastServerAvaliableFlag = pindHostRequest.error.IsNull())
        {
            Connect(scheme + SEPARATOR + host);
        }
        else
        {
            CustomTools.Console.DebugError("ConnectionManager", "TryConnect()", "Host", host, "doesn't pinging.", "Error -", pindHostRequest.error);
        }
        lastTryConnectTime = Time.realtimeSinceStartup;
        connectAttempts++;
        connectProcessing = false;
    }

    private void Connect(string targetHost)
    {
        if (openConnection.IsNull())
        {
            openConnection = new Connection(targetHost, sendFromIndividualThread);
            openConnection.ConnectionOpened += ConnectionOpened;
            openConnection.ConnectionClosed += ConnectionClosed;
            openConnection.MessageReceived += MessageReceived;
            OnConnectionChanged.SafeInvoke(openConnection);
        }
        openConnection.Connect();
    }
}