#### Before use echo instances you should connect to node.
You need do this once, before start, and after force disconnect (if you want to continue work).
Lib provide reconnecting socket it can reconnect automatically after network disconnect or node disconnect, you can tune **ConnectionManager.tryConnectCount** to set maximum count of retries and **ConnectionManager.delayBetweenTryConnect** to set delay betweeen reconnection attempts.

```c#
private void Update()
{
    if (autoUpConnection)
    {
        UpConnection();
    }
    if (sendByUpdate && CanSend)
    {
        ConnectionManager.DoAll(requestBuffer);
    }
}
    
public void UpConnection()
{
    if (ConnectionManager.ReadyState == WebSocketState.Closed || ConnectionManager.ReadyState == WebSocketState.Closing)
    {
        ConnectionManager.Instance.InitConnect();
    }
}
```

For manage different nodes, you can use [NodeManager](Scripts/Management/NodeManager.cs). **NodeManager.defaultHosts** contains default node list. Also NodeManager save last connected node and all using nodes at PlayerPrefs.

```c#
public sealed class NodeManager : CustomTools.Singleton.SingletonMonoBehaviour<NodeManager>
{

    ...

        private void Start() => InitConnection();

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ConnectionManager.OnConnectionAttemptsDone -= ConnectionAttemptsDone;
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
        
    ...
    
}
```
