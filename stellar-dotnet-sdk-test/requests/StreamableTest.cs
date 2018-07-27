using Moq;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using System;
using System.Net.Http;

namespace stellar_dotnet_sdk_test.requests
{
    public class StreamableTest<T> where T : class
    {
        private string _json;
        private Action<T> _testAction;

        FakeStreamableRequestBuilder fakeStreamableRequestBuilder;
        Mock<IEventSource> eventSource = new Mock<IEventSource>();

        public StreamableTest(string json, Action<T> testAction)
        {
            _json = json;
            _testAction = testAction;

            fakeStreamableRequestBuilder = new FakeStreamableRequestBuilder(new Uri("https://horizon-testnet.stellar.org"), "test", null, eventSource.Object);
        }

        public string Uri { get => fakeStreamableRequestBuilder.BuildUri().ToString(); }

        public void AssertIsValid()
        {
            var handler = new EventHandler<T>((o, e) =>
            {
                _testAction(e);
            });

            fakeStreamableRequestBuilder.Stream(handler);
            eventSource.Raise(a => a.Message += null, new EventSource.ServerSentEventArgs() { Data = _json });
        }

        public class FakeStreamableRequestBuilder : RequestBuilderStreamable<FakeStreamableRequestBuilder, T>
        {
            public FakeStreamableRequestBuilder(Uri serverUri, string defaultSegment, HttpClient httpClient, IEventSource eventSource)
                : base(serverUri, defaultSegment, httpClient, eventSource)
            {
            }
        }

    }
}
