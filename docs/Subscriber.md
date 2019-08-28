#### Subscriber
This library provides subscriber module to notify subscribers about changes.

[EchoApiManager](Scripts/Management/EchoApiManager.cs) automaticaly do subscribe (notice chanel and update block) after Database API initialized.

```c#
public sealed class EchoApiManager : CustomTools.Singleton.SingletonMonoBehaviour<EchoApiManager>, ISender
{

    ...
    
    private IPromise DatabaseApiInitialized(DatabaseApi api)
    {
        return api.GetChainId().Then(SetChainId).Then(() => Repository.SubscribeToNotice(api).Then(() => Repository.SubscribeToDynamicGlobalProperties(api).Then(() =>
        {
            OnDatabaseApiInitialized.SafeInvoke(api);
            return Promise.Resolved();
        })));
    }
    
    ...
    
}
```

The [Repository](Scripts/BaseApi/Responses.cs) contains last getted/changed objects.
