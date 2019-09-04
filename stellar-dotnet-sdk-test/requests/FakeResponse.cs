using System.Net.Http;
using System.Threading;

namespace stellar_dotnet_sdk_test.requests
{
    public abstract class FakeResponse
    {
        public abstract HttpResponseMessage MakeResponse(CancellationToken cancellationToken);

        protected FakeResponse()
        {
        }

        public static FakeResponse StartsStream(params StreamAction[] actions)
        {
            return new FakeResponseWithStream(actions);
        }

        public static FakeResponse WithIOError()
        {
            return new FakeResponseWithIOError();
        }
    }
}