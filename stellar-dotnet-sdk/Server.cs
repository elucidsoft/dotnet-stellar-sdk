using stellar_dotnet_sdk.federation;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk
{
    public class Server : IDisposable
    {
        private readonly Uri _serverUri;
        private readonly HttpClient _httpClient;
        private readonly bool _ownHttpClient;

        private const string ClientNameHeader = "X-Client-Name";
        private const string ClientVersionHeader = "X-Client-Version";
        private const string AccountRequiresMemo = "MQ=="; // "1" in base64. See SEP0029
        private const string AccountRequiresMemoKey = "config.memo_required";

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

        public ClaimableBalancesRequestBuilder ClaimableBalances => new ClaimableBalancesRequestBuilder(_serverUri, _httpClient);
        public EffectsRequestBuilder Effects => new EffectsRequestBuilder(_serverUri, _httpClient);

        public LedgersRequestBuilder Ledgers => new LedgersRequestBuilder(_serverUri, _httpClient);

        public OffersRequestBuilder Offers => new OffersRequestBuilder(_serverUri, _httpClient);

        public OperationsRequestBuilder Operations => new OperationsRequestBuilder(_serverUri, _httpClient);

        public FeeStatsRequestBuilder FeeStats => new FeeStatsRequestBuilder(_serverUri, _httpClient);

        public OrderBookRequestBuilder OrderBook => new OrderBookRequestBuilder(_serverUri, _httpClient);

        public TradesRequestBuilder Trades => new TradesRequestBuilder(_serverUri, _httpClient);

        [Obsolete("Paths is deprecated in Horizon v1.0.0. Use PathStrictReceive.")]
        public PathsRequestBuilder Paths => new PathsRequestBuilder(_serverUri, _httpClient);

        public PathStrictSendRequestBuilder PathStrictSend => new PathStrictSendRequestBuilder(_serverUri, _httpClient);

        public PathStrictReceiveRequestBuilder PathStrictReceive => new PathStrictReceiveRequestBuilder(_serverUri, _httpClient);

        public PaymentsRequestBuilder Payments => new PaymentsRequestBuilder(_serverUri, _httpClient);

        public TransactionsRequestBuilder Transactions => new TransactionsRequestBuilder(_serverUri, _httpClient);

        public FriendBotRequestBuilder TestNetFriendBot => new FriendBotRequestBuilder(_serverUri, _httpClient);

        public TradesAggregationRequestBuilder TradeAggregations => new TradesAggregationRequestBuilder(_serverUri, _httpClient);
        public LiquidityPoolsRequestBuilder LiquidityPools => new LiquidityPoolsRequestBuilder(_serverUri, _httpClient);

        /// <summary>
        /// Submit a transaction to the network.
        ///
        /// This method will check if any of the destination accounts require a memo.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction)
        {
            var options = new SubmitTransactionOptions { SkipMemoRequiredCheck = false };
            return SubmitTransaction(transaction.ToEnvelopeXdrBase64(), options);
        }

        /// <summary>
        /// Submit a transaction to the network.
        ///
        /// This method will check if any of the destination accounts require a memo.  Change the SkipMemoRequiredCheck
        /// options to change this behaviour.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction, SubmitTransactionOptions options)
        {
            return SubmitTransaction(transaction.ToEnvelopeXdrBase64(), options);
        }

        /// <summary>
        /// Submit a transaction to the network.
        ///
        /// This method will check if any of the destination accounts require a memo.
        /// </summary>
        /// <param name="transactionEnvelopeBase64"></param>
        /// <returns></returns>
        public Task<SubmitTransactionResponse> SubmitTransaction(string transactionEnvelopeBase64)
        {
            var options = new SubmitTransactionOptions { SkipMemoRequiredCheck = false };
            return SubmitTransaction(transactionEnvelopeBase64, options);
        }

        public Task<SubmitTransactionResponse> SubmitTransaction(FeeBumpTransaction feeBump)
        {
            var options = new SubmitTransactionOptions { FeeBumpTransaction = true };
            return SubmitTransaction(feeBump.ToEnvelopeXdrBase64(), options);
        }

        public Task<SubmitTransactionResponse> SubmitTransaction(FeeBumpTransaction feeBump, SubmitTransactionOptions options)
        {
            options.FeeBumpTransaction = true;
            return SubmitTransaction(feeBump.ToEnvelopeXdrBase64(), options);
        }

        /// <summary>
        /// Submit a transaction to the network.
        ///
        /// This method will check if any of the destination accounts require a memo.  Change the SkipMemoRequiredCheck
        /// options to change this behaviour.
        /// </summary>
        /// <param name="transactionEnvelopeBase64"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<SubmitTransactionResponse> SubmitTransaction(string transactionEnvelopeBase64, SubmitTransactionOptions options)
        {
            if (!options.SkipMemoRequiredCheck)
            {
                TransactionBase tx;

                if (options.FeeBumpTransaction)
                {
                    tx = FeeBumpTransaction.FromEnvelopeXdr(transactionEnvelopeBase64);
                }
                else
                {
                    tx = Transaction.FromEnvelopeXdr(transactionEnvelopeBase64);
                }

                await CheckMemoRequired(tx);
            }

            var transactionUri = new UriBuilder(_serverUri).SetPath("/transactions").Uri;

            var paramsPairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tx", transactionEnvelopeBase64)
            };

            var response = await _httpClient.PostAsync(transactionUri, new FormUrlEncodedContent(paramsPairs.ToArray()));

            if (options.EnsureSuccess && !response.IsSuccessStatusCode)
            {
                string responseString = string.Empty;
                if (response.Content != null)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                }

                throw new ConnectionErrorException($"Status code ({response.StatusCode}) is not success.{(!string.IsNullOrEmpty(responseString) ? " Content: " + responseString : "")}");
            }

            if (response.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(responseString);
                return submitTransactionResponse;
            }

            return null;
        }

        /// <summary>
        /// Check whether any of the destination accounts require a memo.
        ///
        /// This method implements the checks defined in
        /// <a href="https://github.com/stellar/stellar-protocol/blob/master/ecosystem/sep-0029.md">SEP0029</a>.
        /// It will sequantially load each destination account and check if it has the data field
        /// <c>config.memo_required</c> set to <c>"MQ=="</c>.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="AccountRequiresMemoException"></exception>
        public async Task CheckMemoRequired(TransactionBase transaction)
        {
            var tx = GetTransactionToCheck(transaction);

            if (tx.Memo != null && !Equals(tx.Memo, Memo.None()))
            {
                return;
            }

            var destinations = new HashSet<string>();

            foreach (var operation in tx.Operations)
            {
                if (!IsPaymentOperation(operation))
                {
                    continue;
                }

                // If it's a muxed account it already contains the memo.
                var destinationKey = PaymentOperationDestination(operation);
                if (destinationKey.IsMuxedAccount)
                {
                    continue;
                }

                var destination = destinationKey.Address;

                if (destinations.Contains(destination))
                {
                    continue;
                }

                destinations.Add(destination);

                try
                {
                    var account = await Accounts.Account(destination);
                    if (!account.Data.ContainsKey(AccountRequiresMemoKey))
                    {
                        continue;
                    }

                    if (account.Data[AccountRequiresMemoKey] == AccountRequiresMemo)
                    {
                        throw new AccountRequiresMemoException("Account requires memo", destination, operation);
                    }
                }
                catch (HttpResponseException ex)
                {
                    if (ex.StatusCode != 404)
                        throw;
                }
            }
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

        private Transaction GetTransactionToCheck(TransactionBase transaction)
        {
            switch (transaction)
            {
                case FeeBumpTransaction feeBump:
                    return feeBump.InnerTransaction;
                case Transaction tx:
                    return tx;
                default:
                    throw new ArgumentException($"Invalid transaction of type {transaction.GetType().Name}");
            }
        }

        private bool IsPaymentOperation(Operation op)
        {
            switch (op)
            {
                case PaymentOperation _:
                case PathPaymentStrictSendOperation _:
                case PathPaymentStrictReceiveOperation _:
                case AccountMergeOperation _:
                    return true;
                default:
                    return false;
            }
        }

        private IAccountId PaymentOperationDestination(Operation op)
        {
            switch (op)
            {
                case PaymentOperation p:
                    return p.Destination;
                case PathPaymentStrictSendOperation p:
                    return p.Destination;
                case PathPaymentStrictReceiveOperation p:
                    return p.Destination;
                case AccountMergeOperation p:
                    return p.Destination;
                default:
                    throw new ArgumentException("Expected payment operation.");
            }
        }

    }
}
