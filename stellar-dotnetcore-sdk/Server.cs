using stellar_dotnetcore_sdk.requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class Server
    {
        private Uri _serverUri;
        private HttpClient _httpClient;

        public Server(string uri)
        {
            _httpClient = new HttpClient();

            try
            {
                _serverUri = new Uri(uri);
            }
            catch(UriFormatException)
            {
                throw;
            }
        }

        public AccountsRequestBuilder Accounts => new AccountsRequestBuilder(_serverUri);

        //TODO: Implement the rest of this class, has many many dependencies...
    }
}
