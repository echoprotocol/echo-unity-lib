using System;
using System.Collections.Generic;
using Base.Responses;
using Newtonsoft.Json;


namespace Base.Requests
{
    public sealed class RequestIdentificator
    {
        public const int OPEN_ID = -1;
        public const int CLOSE_ID = -2;
        public const int INVALID_ID = 0;

        private int currentId;


        public RequestIdentificator()
        {
            currentId = INVALID_ID;
        }

        public RequestIdentificator(int startId)
        {
            currentId = startId;
        }

        public int GenerateNewId() => ++currentId;

        public int OpenId => OPEN_ID;

        public int CloseId => CLOSE_ID;
    }


    public class Request
    {
        private const string METHOD_NAME = "call";

        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; private set; } = "2.0";
        [JsonProperty("id")]
        public int RequestId { get; private set; }
        [JsonProperty("method")]
        public string MethodName { get; private set; }
        [JsonProperty("params")]
        public Parameters Parameters { get; private set; }
        [JsonIgnore]
        public string Title { get; private set; }
        [JsonIgnore]
        public bool Debug { get; private set; }


        public Request(int requestId, Parameters parameters, string title, bool debug)
        {
            MethodName = METHOD_NAME;
            Parameters = parameters ?? new Parameters();
            RequestId = requestId;
            Title = title;
            Debug = debug;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public void PrintDebugLog()
        {
            CustomTools.Console.DebugLog(CustomTools.Console.LogYellowColor(Title), CustomTools.Console.LogGreenColor("--->>>"), CustomTools.Console.LogWhiteColor(ToString()));
        }
    }


    public class RequestAction : Request
    {
        [JsonIgnore]
        public Action<Response> Callback { get; private set; }

        public RequestAction(int requestId, Parameters parameters, Action<Response> callback, string title, bool debug) : base(requestId, parameters, title, debug)
        {
            Callback = callback;
        }
    }


    public class Parameters : List<object> { }
}