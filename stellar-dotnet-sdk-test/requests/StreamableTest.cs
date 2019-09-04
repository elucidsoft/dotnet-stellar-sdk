using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.requests
{
    public class StreamableTest<T> where T : class
    {
        private readonly string _json;
        private readonly Action<T> _testAction;
        private readonly string _eventId;
        private SSEEventSource _eventSource;
        private FakeStreamableRequestBuilder _requestBuilder;
        private T _received;

        public StreamableTest(string json, Action<T> action, string eventId = null)
        {
            _json = json.Replace("\r\n", "").Replace("\n", "");
            _testAction = action;
            _received = null;
            _eventSource = null;
            _requestBuilder = null;
            _eventId = eventId ?? "1234";
        }

        public async Task Run()
        {
            var fakeHandler = new FakeHttpMessageHandler();
            var stream = $"event: open\ndata: hello\n\nid: {_eventId}\ndata: {_json}\n\n";
            fakeHandler.QueueResponse(FakeResponse.StartsStream(StreamAction.Write(stream)));

            _eventSource = new SSEEventSource(new Uri("http://test.com"),
                builder => builder.MessageHandler(fakeHandler));

            _requestBuilder = new FakeStreamableRequestBuilder(new Uri("https://horizon-testnet.stellar.org"), "test",
                null, _eventSource);
            var handler = new EventHandler<T>((sender, e) =>
            {
                _eventSource.Shutdown();
                _testAction(e);
            });
            var task = _requestBuilder.Stream(handler).Connect();
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5.0));
            var completedTask = await Task.WhenAny(task, timeoutTask).ConfigureAwait(false);
            if (completedTask != task)
            {
                throw new Exception("Task did not complete.");
            }
        }

        public string LastEventId => _eventSource.LastEventId;
        public string Uri => _requestBuilder.BuildUri().ToString();

        public class FakeStreamableRequestBuilder : RequestBuilderStreamable<FakeStreamableRequestBuilder, T>
        {
            public FakeStreamableRequestBuilder(Uri serverUri, string defaultSegment, HttpClient httpClient,
                IEventSource eventSource)
                : base(serverUri, defaultSegment, httpClient, eventSource)
            {
            }
        }
    }
}