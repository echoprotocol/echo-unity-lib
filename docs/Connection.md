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

For manage different node, you can use NodeManager. NodeManager.defaultHosts contains default node list. Also NodeManager save last connected node and all using nodes at PlayerPrefs.

```c#
private void Start() => InitConnection();

private void InitConnection()
{
    var url = SelecteUrl;
    if (url.IsNull() || (url = url.Trim()).IsNullOrEmpty())
    {
        return;
    }
    if (!Urls.Contains(url))
    {
        return;
    }
    if (IsDefault(SelecteUrl))
    {
        SelecteUrl = defaultHosts.NextLoop(SelecteUrl);
    }
    ConnectionManager.Instance.ReconnectTo(SelecteUrl);
    ConnectionManager.OnConnectionAttemptsDone -= ConnectionAttemptsDone;
    ConnectionManager.OnConnectionAttemptsDone += ConnectionAttemptsDone;
}
```
