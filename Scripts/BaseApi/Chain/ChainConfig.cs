using System.Collections.Generic;
using CustomTools.Extensions.Core;


namespace Base.Config
{
    public static class ChainConfig
    {
        private class NetworkParameters
        {
            public string CoreAsset { get; private set; }
            public string AddressPrefix { get; private set; }
            public string ChainId { get; private set; }

            public NetworkParameters(string coreAsset, string addressPrefix, string chainId)
            {
                CoreAsset = coreAsset;
                AddressPrefix = addressPrefix;
                ChainId = chainId;
            }
        }


        private const string ECHO = "ECHO";

        private static Dictionary<string, NetworkParameters> networks = new Dictionary<string, NetworkParameters>
        {
            { "EchoRandDev", new NetworkParameters( ECHO, ECHO, "31cde47f6b94908c2b2ed67c3365c7f58bb744dac145ecf6dc0941e1167c34ab" ) }
        };

        private static string coreAsset = ECHO;
        private static string addressPrefix = ECHO;
        private static double expireInSeconds = 15.0;
        private static double expireInSecondsProposal = 24.0 * 60.0 * 60.0;


        public static string CoreAsset => coreAsset;

        public static string AddressPrefix => addressPrefix;

        public static double ExpireInSeconds => expireInSeconds;

        public static double ExpireInSecondsProposal => expireInSecondsProposal;

        public static void SetChainId(string chainId)
        {
            chainId = chainId.OrEmpty();
            var keys = new List<string>(networks.Keys);
            for (var i = 0; i < keys.Count; i++)
            {
                var name = keys[i];
                var network = networks[name];
                if (network.ChainId.Equals(chainId))
                {
                    coreAsset = network.CoreAsset;
                    addressPrefix = network.AddressPrefix;
                    CustomTools.Console.DebugLog(CustomTools.Console.LogWhiteColor("Address prefix:", addressPrefix));
                    return;
                }
            }
            CustomTools.Console.DebugError(CustomTools.Console.LogRedColor("Unknown chain id:", chainId));
        }

        public static void Reset()
        {
            coreAsset = ECHO;
            addressPrefix = ECHO;
            expireInSeconds = 15.0;
            expireInSecondsProposal = 24.0 * 60.0 * 60.0;
        }

        public static void SetPrefix(string prefix = ECHO)
        {
            addressPrefix = prefix;
        }
    }
}