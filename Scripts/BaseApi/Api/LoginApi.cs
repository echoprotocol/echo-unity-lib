using Base.Requests;
using RSG;


namespace Base.Api.Database
{
    public sealed class LoginApi : ApiId
    {
        public const int ID = 1;

        private LoginApi(ISender sender) : base(ID, sender) { }

        public static LoginApi Create(ISender sender) => new LoginApi(sender);

        public IPromise<bool> Login(string userName, string password)
        {
            return new Promise<bool>((resolve, reject) =>
            {
#if ECHO_DEBUG
                var debug = false;
#else
                var debug = false;
#endif
                var methodName = "login";
                var parameters = new Parameters { Id.Value, methodName, new object[] { userName, password } };
                DoRequest(GenerateNewId(), parameters, resolve, reject, methodName, debug);
            });
        }
    }
}