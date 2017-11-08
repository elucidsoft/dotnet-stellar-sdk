using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses;

namespace stellar_dotnetcore_sdk
{
    public class Server : IDisposable
    {
        private readonly Uri _serverUri;

        public Server(string uri)
        {
            HttpClient = new HttpClient();
            _serverUri = new Uri(uri);
        }

        public HttpClient HttpClient { get; set; }

        public AccountsRequestBuilder Accounts => new AccountsRequestBuilder(_serverUri);

        public EffectsRequestBuilder Effects => new EffectsRequestBuilder(_serverUri);

        public LedgersRequestBuilder Ledgers => new LedgersRequestBuilder(_serverUri);

        public OffersRequestBuilder Offers => new OffersRequestBuilder(_serverUri);

        //TODO: Operations

        public OrderBookRequestBuilder OrderBook => new OrderBookRequestBuilder(_serverUri);

        //TODO: Trades

        public PathsRequestBuilder Paths => new PathsRequestBuilder(_serverUri);

        public PaymentsRequestBuilder Payments => new PaymentsRequestBuilder(_serverUri);

        public TransactionsRequestBuilder Transactions => new TransactionsRequestBuilder(_serverUri);

        public void Dispose()
        {
            HttpClient?.Dispose();
        }

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
    }
}