using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;

namespace stellar_dotnetcore_sdk.requests
{
    public class ResponseHandler<T> where T : class
    {
        public async Task<T> HandleResponse(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            if ((int) statusCode == 429)
            {
                var retryAfter = int.Parse(response.Headers.GetValues("Retry-After").First());
                throw new TooManyRequestsException(retryAfter);
            }

            if ((int) statusCode >= 300)
                throw new HttpResponseException((int) statusCode, response.ReasonPhrase);

            if (string.IsNullOrWhiteSpace(content))
                throw new ClientProtocolException("Response contains no content");

            var responseObj = JsonSingleton.GetInstance<T>(content);

            if (responseObj is Response)
            {
                var responseInstance = responseObj as Response;
                responseInstance.SetHeaders(response.Headers);
            }

            return responseObj;
        }
    }
}