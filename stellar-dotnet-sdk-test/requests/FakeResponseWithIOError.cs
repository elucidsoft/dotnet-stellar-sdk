using System.Net.Http;
using System.Threading;

namespace stellar_dotnet_sdk_test.requests
{
    internal class FakeResponseWithIOError : FakeResponse
    {
        public override HttpResponseMessage MakeResponse(CancellationToken cancellationToken)
        {
            throw new HttpRequestException("Unit Test Exception Message");
        }
    }
}