using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk
{
    public class Server : IDisposable
    {
        private readonly Uri _serverUri;

        public Server(string uri, HttpClient httpClient)
        {
            HttpClient = httpClient;
            _serverUri = new Uri(uri);
        }

        public Server(string uri)
            : this(uri, new HttpClient())
        {
        }

        public static HttpClient HttpClient { get; set; }

        public AccountsRequestBuilder Accounts => new AccountsRequestBuilder(_serverUri, HttpClient);

        public AssetsRequestBuilder Assets => new AssetsRequestBuilder(_serverUri, HttpClient);

        public EffectsRequestBuilder Effects => new EffectsRequestBuilder(_serverUri, HttpClient);

        public LedgersRequestBuilder Ledgers => new LedgersRequestBuilder(_serverUri, HttpClient);

        public OffersRequestBuilder Offers => new OffersRequestBuilder(_serverUri, HttpClient);

        public OperationsRequestBuilder Operations => new OperationsRequestBuilder(_serverUri, HttpClient);

        public OrderBookRequestBuilder OrderBook => new OrderBookRequestBuilder(_serverUri, HttpClient);
        
        public TradesRequestBuilder Trades => new TradesRequestBuilder(_serverUri, HttpClient);

        public PathsRequestBuilder Paths => new PathsRequestBuilder(_serverUri, HttpClient);

        public PaymentsRequestBuilder Payments => new PaymentsRequestBuilder(_serverUri, HttpClient);

        public TransactionsRequestBuilder Transactions => new TransactionsRequestBuilder(_serverUri, HttpClient);

        public FriendBotRequestBuilder TestNetFriendBot => new FriendBotRequestBuilder(_serverUri, HttpClient);

        public void Dispose()
        {
            HttpClient?.Dispose();
        }

        public TradesAggregationRequestBuilder TradeAggregations => new TradesAggregationRequestBuilder(_serverUri, HttpClient);

        public async Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction)
        {
            var transactionUri = new UriBuilder(_serverUri).SetPath("/transactions").Uri;

            var paramsPairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("tx", transaction.ToEnvelopeXdrBase64())
                };

            var response = await HttpClient.PostAsync(transactionUri, new FormUrlEncodedContent(paramsPairs.ToArray()));
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

            var response = await HttpClient.PostAsync(transactionUri, new FormUrlEncodedContent(paramsPairs.ToArray()));
            if (response.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(responseString);
                return submitTransactionResponse;
            }

            return null;
        }


    }
}