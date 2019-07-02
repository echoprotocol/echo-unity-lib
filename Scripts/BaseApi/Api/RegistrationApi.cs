using Base.Keys.EDDSA;
using Base.Requests;
using RSG;


namespace Base.Api.Database
{
    public sealed class RegistrationApi : ApiId
    {
        private RegistrationApi(ISender sender) : base(null, sender) { }

        public static RegistrationApi Create(ISender sender) => new RegistrationApi(sender);

        public IPromise<RegistrationApi> Init()
        {
            return new Promise<int>((resolve, reject) =>
            {
#if ECHO_DEBUG
                var debug = false;
#else
                var debug = false;
#endif
                var methodName = "registration";
                var parameters = new Parameters { LoginApi.ID, methodName, new object[0] };
                DoRequest(GenerateNewId(), parameters, resolve, reject, methodName, debug);
            }).Then(apiId => (RegistrationApi)Init(apiId));
        }

        public IPromise RegisterAccount(string name, PublicKey active, PublicKey echorandKey, System.Action callback) // todo
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
                    var methodName = "register_account";
                    var title = methodName + " " + requestId;
                    var parameters = new Parameters { Id.Value, methodName, new object[] { name, active, echorandKey } };
                    DoRequestVoid(requestId, parameters, resolve, reject, title, debug);
                });
            }
            return Init().Then(api => api.RegisterAccount(name, active, echorandKey, callback));
        }
    }
}


