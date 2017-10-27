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


    }
}
