using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class RequestBuilderStreamableTest
    {
        private readonly Uri _uri = new Uri("http://test.com");

        [TestMethod]
        public async Task TestHelloStream()
        {
            // Check we skip the first message with "hello" data
            var fakeHandler = new FakeHttpMessageHandler();
            var stream = "event: open\ndata: hello\n\ndata: foobar\n\n";
            fakeHandler.QueueResponse(FakeResponse.StartsStream(StreamAction.Write(stream)));

            using (var eventSource = new SSEEventSource(_uri, builder => builder.MessageHandler(fakeHandler)))
            {

                string dataReceived = null;
                eventSource.Message += (sender, args) =>
                {
                    dataReceived = args.Data;
                    eventSource.Shutdown();
                };

                await eventSource.Connect();

                Assert.AreEqual("foobar", dataReceived);
            }
        }

        [TestMethod]
        public async Task TestStreamErrorEvent()
        {
            var fakeHandler = new FakeHttpMessageHandler();
            fakeHandler.QueueResponse(FakeResponse.WithIOError());
            fakeHandler.QueueResponse(FakeResponse.WithIOError());
            fakeHandler.QueueResponse(FakeResponse.StartsStream());

            using (var eventSource = new SSEEventSource(_uri, builder => builder.MessageHandler(fakeHandler)))
            {
                var errorCount = 0;
                eventSource.Error += (sender, args) =>
                {
                    errorCount += 1;
                    if (errorCount >= 2)
                    {
                        eventSource.Shutdown();
                    }
                };
                await eventSource.Connect();
                Assert.AreEqual(2, errorCount);
            }
        }
    }
}