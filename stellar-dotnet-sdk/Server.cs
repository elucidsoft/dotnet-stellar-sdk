using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk
{
    public class Server : IDisposable
    {
        private readonly Uri _serverUri;
        private readonly HttpClient _httpClient;
        private readonly bool _ownHttpClient;

        private const string ClientNameHeader = "X-Client-Name";
        private const string ClientVersionHeader = "X-Client-Version";

        public Server(string uri, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serverUri = new Uri(uri);
            _ownHttpClient = false;
        }

        public Server(string uri)
            : this(uri, CreateHttpClient())
        {
            _ownHttpClient = true;
        }

        public void Dispose()
        {
            if (_ownHttpClient)
            {
                _httpClient?.Dispose();
            }
        }

        public RootResponse Root()
        {
            ResponseHandler<RootResponse> responseHandler = new ResponseHandler<RootResponse>();

            var response = _httpClient.GetAsync(_serverUri).Result;

            return responseHandler.HandleResponse(response).Result;
        }

        public AccountsRequestBuilder Accounts => new AccountsRequestBuilder(_serverUri, _httpClient);

        public AssetsRequestBuilder Assets => new AssetsRequestBuilder(_serverUri, _httpClient);

        public EffectsRequestBuilder Effects => new EffectsRequestBuilder(_serverUri, _httpClient);

        public LedgersRequestBuilder Ledgers => new LedgersRequestBuilder(_serverUri, _httpClient);

        public OffersRequestBuilder Offers => new OffersRequestBuilder(_serverUri, _httpClient);

        public OperationsRequestBuilder Operations => new OperationsRequestBuilder(_serverUri, _httpClient);

        public OperationFeeStatsRequestBuilder OperationFeeStats => new OperationFeeStatsRequestBuilder(_serverUri, _httpClient);

        public OrderBookRequestBuilder OrderBook => new OrderBookRequestBuilder(_serverUri, _httpClient);

        public TradesRequestBuilder Trades => new TradesRequestBuilder(_serverUri, _httpClient);

        public PathsRequestBuilder Paths => new PathsRequestBuilder(_serverUri, _httpClient);

        public PaymentsRequestBuilder Payments => new PaymentsRequestBuilder(_serverUri, _httpClient);

        public TransactionsRequestBuilder Transactions => new TransactionsRequestBuilder(_serverUri, _httpClient);

        public FriendBotRequestBuilder TestNetFriendBot => new FriendBotRequestBuilder(_serverUri, _httpClient);

        public TradesAggregationRequestBuilder TradeAggregations => new TradesAggregationRequestBuilder(_serverUri, _httpClient);

        public async Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction)
        {
            var transactionUri = new UriBuilder(_serverUri).SetPath("/transactions").Uri;

            var paramsPairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tx", transaction.ToEnvelopeXdrBase64())
            };

            var response = await _httpClient.PostAsync(transactionUri, new FormUrlEncodedContent(paramsPairs.ToArray()));
            if (response.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(responseString);
                return submitTransactionResponse;
            }

            return null;
        }

        public async Task<SubmitTransactionResponse> SubmitTransaction(string transactionEnvelopeBase64)
        {
            var transactionUri = new UriBuilder(_serverUri).SetPath("/transactions").Uri;

            var paramsPairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tx", transactionEnvelopeBase64)
            };

            var response = await _httpClient.PostAsync(transactionUri, new FormUrlEncodedContent(paramsPairs.ToArray()));
            if (response.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(responseString);
                return submitTransactionResponse;
            }

            return null;
        }

        public static HttpClient CreateHttpClient()
        {
            return CreateHttpClient(new HttpClientHandler());
        }

        public static HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            var httpClient = new HttpClient(handler);
            var assembly = Assembly.GetAssembly(typeof(Server)).GetName();
            httpClient.DefaultRequestHeaders.Add(ClientNameHeader, assembly.Name);
            httpClient.DefaultRequestHeaders.Add(ClientVersionHeader, assembly.Version.ToString());
            return httpClient;
        }
    }
}