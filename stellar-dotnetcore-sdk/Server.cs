using System;
using System.Net.Http;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk
{
    public class Server
    {
        private HttpClient _httpClient;
        private readonly Uri _serverUri;

        public Server(string uri)
        {
            _httpClient = new HttpClient();

            try
            {
                _serverUri = new Uri(uri);
            }
            catch (UriFormatException)
            {
                throw;
            }
        }

        public AccountsRequestBuilder Accounts => new AccountsRequestBuilder(_serverUri);

        //TODO: Implement the rest of this class, has many many dependencies...
    }
}