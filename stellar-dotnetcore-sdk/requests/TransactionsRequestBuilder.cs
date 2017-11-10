using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class TransactionsRequestBuilder: RequestBuilder<TransactionsRequestBuilder>
    {
        public TransactionsRequestBuilder(Uri serverURI) : base(serverURI, "transactions")
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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
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
        /// Requests specific <code>uri</code> and returns {@link Page} of {@link EffectResponse}.
        /// This method is helpful for getting the next set of results.
        /// </Summary>
        public static async Task<Page<TransactionResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<TransactionResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }

        /////<Summary>
        ///// Allows to stream SSE events from horizon.
        ///// Certain endpoints in Horizon can be called in streaming mode using Server-Sent Events.
        ///// This mode will keep the connection to horizon open and horizon will continue to return
        ///// responses as ledgers close.
        ///// <a href="http://www.w3.org/TR/eventsource/" target="_blank">Server-Sent Events</a>
        ///// <a href="https://www.stellar.org/developers/horizon/learn/responses.html" target="_blank">Response Format documentation</a>
        ///// </Summary>
        ///// <param name="listener">EventListener implementation with EffectResponse type</param> 
        ///// <returns>EventSource object, so you can <code>close()</code> connection when not needed anymore</param> 
        //public EventSource Stream(EventHandler<TransactionResponse> listener)
        //{
        //    var es = new EventSource(BuildUri());

        //    es.Message += (sender, e) =>
        //    {
        //        if (e == "\"hello\"")
        //            return;

        //        var account = JsonSingleton.GetInstance<TransactionResponse>(e);
        //        listener?.Invoke(this, account);
        //    };

        //    return es;
        //}

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<TransactionResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        public override TransactionsRequestBuilder Cursor(string token)
        {
            base.Cursor(token);
            return this;
        }


        public override TransactionsRequestBuilder Limit(int number)
        {
            base.Limit(number);
            return this;
        }


        public override TransactionsRequestBuilder Order(OrderDirection direction)
        {
            base.Order(direction);
            return this;
        }
    }
}
