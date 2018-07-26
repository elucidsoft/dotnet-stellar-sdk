using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    public class TransactionsRequestBuilder : RequestBuilder<TransactionsRequestBuilder>
    {
        public TransactionsRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "transactions", httpClient)
        {

        }

        /// <summary>
        ///     Requests specific uri and returns LedgerResponse
        ///     This method is helpful for getting the links.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> Transaction(Uri uri)
        {
            var responseHandler = new ResponseHandler<TransactionResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        ///<summary>
        /// Requests <code>GET /ledgers/{ledgerSeq}</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/ledgers-single.html">Ledger Details</a>
        ///</summary>
        ///<param name="ledgerSeq">Ledger to fetch</param>
        public Task<TransactionResponse> Transaction(string transactionId)
        {
            SetSegments("transactions", transactionId);
            return Transaction(this.BuildUri());
        }

        ///<Summary>
        /// Builds request to <code>GET /accounts/{account}/transactions</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/payments-for-account.html">Transactions for Account</a>
        /// </Summary>
        /// <param name="account">Account for which to get transactions</param> 
        public TransactionsRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "transactions");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /ledgers/{ledgerSeq}/transactions</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/payments-for-ledger.html">Transactions for Ledger</a>
        /// </Summary>
        /// <param name="ledgerSeq">Ledger for which to get transactions</param> 
        public TransactionsRequestBuilder ForLedger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString(), "transactions");
            return this;
        }

        ///<Summary>
        /// Allows to stream SSE events from horizon.
        /// Certain endpoints in Horizon can be called in streaming mode using Server-Sent Events.
        /// This mode will keep the connection to horizon open and horizon will continue to return
        /// responses as ledgers close.
        /// <a href="http://www.w3.org/TR/eventsource/" target="_blank">Server-Sent Events</a>
        /// <a href="https://www.stellar.org/developers/horizon/learn/responses.html" target="_blank">Response Format documentation</a>
        /// </Summary>
        /// <param name="listener">EventListener implementation with EffectResponse type</param> 
        /// <returns>EventSource object, so you can <code>close()</code> connection when not needed anymore</param> 
        public EventSource Stream(EventHandler<TransactionResponse> listener)
        {
            return Stream<TransactionResponse>(listener);
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<TransactionResponse>> Execute()
        {
            return await Execute<Page<TransactionResponse>>(BuildUri());
        }
    }
}
