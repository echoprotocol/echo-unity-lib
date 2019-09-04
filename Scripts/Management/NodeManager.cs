using System;
using System.Collections;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json;
using UnityEngine;


public sealed class NodeManager : CustomTools.Singleton.SingletonMonoBehaviour<NodeManager>
{
    public enum ConnectResult
    {
        NoInternet,
        BadRequest,
        Ok
    }


    public static Action<string> OnSelecteHostChanged;

    private const string SELECTED_HOST_KEY = "host";
    private const string HOSTS_LIST_KEY = "hosts_list";

    [SerializeField] private string[] defaultHosts = { "wss://testnet.echo-dev.io/ws" };
    [SerializeField] private bool changeDefaultUrlAfterConnectionAttempts = false;
    [SerializeField] private bool resetAtStart = false;


    public string[] Urls
    {
        get
        {
            if (!PlayerPrefs.HasKey(HOSTS_LIST_KEY))
            {
                PlayerPrefs.SetString(HOSTS_LIST_KEY, JsonConvert.SerializeObject(defaultHosts ?? new string[0]));
            }
            return JsonConvert.DeserializeObject<string[]>(PlayerPrefs.GetString(HOSTS_LIST_KEY));
        }
        private set
        {
            value = value ?? new string[0];
            PlayerPrefs.SetString(HOSTS_LIST_KEY, JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }

    public string LastUrl
    {
        get
        {
            if (!PlayerPrefs.HasKey(SELECTED_HOST_KEY))
            {
                PlayerPrefs.SetString(SELECTED_HOST_KEY, defaultHosts.IsNullOrEmpty() ? string.Empty : defaultHosts[0]);
            }
            return PlayerPrefs.GetString(SELECTED_HOST_KEY);
        }
        private set
        {
            PlayerPrefs.SetString(SELECTED_HOST_KEY, value);
            PlayerPrefs.Save();
            OnSelecteHostChanged.SafeInvoke(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        if (resetAtStart)
        {
            ResetAll();
        }
#endif
        var urls = Urls;
        foreach (var defaultHost in defaultHosts)
        {
            if (!urls.Contains(defaultHost))
            {
                ResetAll();
                break;
            }
        }
    }

    private void Start() => InitConnection();

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ConnectionManager.OnConnectionAttemptsDone -= ConnectionAttemptsDone;
    }

    private void ResetAll()
    {
        CustomTools.Console.DebugWarning("NodeManager", "ResetAll()", "Reset all saved hosts.");
        PlayerPrefs.DeleteKey(HOSTS_LIST_KEY);
        PlayerPrefs.DeleteKey(SELECTED_HOST_KEY);
        PlayerPrefs.Save();
    }

    private void InitConnection()
    {
        var url = LastUrl;
        if (url.IsNull() || (url = url.Trim()).IsNullOrEmpty())
        {
            return;
        }
        if (!Urls.Contains(url))
        {
            return;
        }
        ConnectionManager.OnConnectionAttemptsDone -= ConnectionAttemptsDone;
        ConnectionManager.OnConnectionAttemptsDone += ConnectionAttemptsDone;
        ConnectionManager.Instance.ReconnectTo(LastUrl);
    }

    private void ConnectionAttemptsDone(string url)
    {
        if (changeDefaultUrlAfterConnectionAttempts && IsDefault(url))
        {
            ConnectionManager.Instance.ReconnectTo(LastUrl = defaultHosts.NextLoop(url));
        }
    }

    private bool Validation(string url)
    {
        if (!url.StartsWith(ConnectionManager.WSS, StringComparison.Ordinal))
        {
            return false;
        }
        var host = url.Replace(ConnectionManager.WSS, string.Empty);
        if (host.IsNullOrEmpty())
        {
            return false;
        }
        if (!host.Contains(ConnectionManager.DOT) || host.StartsWith(ConnectionManager.DOT, StringComparison.Ordinal))
        {
            return false;
        }
        return true;
    }

    public bool IsDefault(string url) => !defaultHosts.IsNullOrEmpty() && defaultHosts.Contains(url);

    public bool ConnectTo(string url, Action<ConnectResult> resultCallback)
    {
        if (url.IsNull() || (url = url.Trim()).IsNullOrEmpty())
        {
            return false;
        }
        if (!Urls.Contains(url))
        {
            return false;
        }
        StartCoroutine(TryConnectTo(url, resultCallback));
        return true;
    }

    private IEnumerator TryConnectTo(string url, Action<ConnectResult> resultCallback)
    {
        var ping = new WWW(ConnectionManager.PingUrl);
        yield return ping;
        if (ping.error.IsNull())
        {
            ping = new WWW(ConnectionManager.HTTP + url.Split(new[] { ConnectionManager.SEPARATOR }, StringSplitOptions.None).Last());
            yield return ping;
            if (IsDefault(url) || ping.error.IsNull())
            {
                ConnectionManager.Instance.ReconnectTo(LastUrl = url); // save new host only if them exist
                resultCallback.SafeInvoke(ConnectResult.Ok);
            }
            else
            {
                resultCallback.SafeInvoke(ConnectResult.BadRequest);
            }
        }
        else
        {
            resultCallback.SafeInvoke(ConnectResult.NoInternet);
        }
    }

    public bool AddHost(string url)
    {
        if (url.IsNull() || (url = url.Trim()).IsNullOrEmpty())
        {
            return false;
        }
        if (!Validation(ConnectionManager.WSS + url.Split(new[] { ConnectionManager.SEPARATOR }, StringSplitOptions.None).Last()))
        {
            return false;
        }
        var currentUrls = Urls;
        if (currentUrls.Contains(url))
        {
            return false;
        }
        ArrayTools.Add(ref currentUrls, url);
        Urls = currentUrls;
        return true;
    }

    public bool RemoveHost(string url)
    {
        if (IsDefault(url))
        {
            return false;
        }
        var currentUrls = Urls;
        if (!currentUrls.Contains(url))
        {
            return false;
        }
        ArrayTools.Remove(ref currentUrls, url);
        Urls = currentUrls;
        return true;
    }
}