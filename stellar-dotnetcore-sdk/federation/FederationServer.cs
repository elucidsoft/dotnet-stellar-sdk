using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nett;

namespace stellar_dotnetcore_sdk.federation
{
    /// <summary>
    /// FederationServer handles a network connection to a
    /// federation server (https://www.stellar.org/developers/learn/concepts/federation.html)
    /// instance and exposes an interface for requests to that instance.
    /// 
    /// For resolving a stellar address without knowing which federation server
    /// to query use Federation#resolve(String).
    /// See Federation docs: https://www.stellar.org/developers/learn/concepts/federation.html
    /// </summary>
    public class FederationServer
    {
        private Uri _serverUri;
        private string _domain;
        private static HttpClient _httpClient;

        public FederationServer(Uri serverUri, string domain)
        {
            if (serverUri.Scheme != "https")
                throw new FederationServerInvalidException();

            _serverUri = serverUri;

            if (Uri.CheckHostName(domain) == UriHostNameType.Unknown)
                throw new ArgumentException("Invalid internet domain name supplied.", nameof(domain));

            _domain = domain;
        }

        public FederationServer(string serverUri, string domain)
            : this(new Uri(serverUri), domain)
        {

        }

        /// <summary>
        /// reates a <see cref="FederationServer"/> instance for a given domain.
        /// It tries to find a federation server URL in stellar.toml file.
        /// See: https://www.stellar.org/developers/learn/concepts/stellar-toml.html
        /// </summary>
        /// <param name="domain">Domain to find a federation server for</param>
        /// <returns><see cref="FederationServer"/></returns>
        public static async Task<FederationServer> CreateForDomain(string domain)
        {
            StringBuilder uriBuilder = new StringBuilder();
            uriBuilder.Append("https://");
            uriBuilder.Append(domain);
            uriBuilder.Append("/.well-known/stellar.toml");
            var stellarTomUri = new Uri(uriBuilder.ToString());

            TomlTable stellarToml;
            try
            {
                _httpClient = new HttpClient();
                var response = await _httpClient.GetAsync(stellarTomUri, HttpCompletionOption.ResponseContentRead);

                if ((int) response.StatusCode >= 300)
                {
                    throw new StellarTomlNotFoundInvalidException();
                }

                var responseToml = await response.Content.ReadAsStringAsync();
                stellarToml = Toml.ReadString(responseToml);
            }
            catch (HttpRequestException e)
            {
                throw new ConnectionErrorException(e.Message);
            }

            string federationServer = stellarToml["FEDERATION_SERVER"].ToString();
            if(String.IsNullOrWhiteSpace(federationServer))
                throw new NoFederationServerException();

            return new FederationServer(federationServer, domain);
        }

        //TODO: ResolveAddress

        //TODO: ServerUri

        //TODO: Domain

        //TODO: HttpClient
    }
}
