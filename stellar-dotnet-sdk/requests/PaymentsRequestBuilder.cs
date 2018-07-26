using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    public class PaymentsRequestBuilder : RequestBuilder<PaymentsRequestBuilder>
    {

        public PaymentsRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "payments", httpClient)
        {

        }


        ///<Summary>
        /// Builds request to <code>GET /accounts/{account}/payments</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/payments-for-account.html">Effects for Account</a>
        /// </Summary>
        /// <param name="account">Account for which to get payments</param> 
        public PaymentsRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "payments");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /ledgers/{ledgerSeq}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/payments-for-ledger.html">Effects for Ledger</a>
        /// </Summary>
        /// <param name="ledgerSeq">Ledger for which to get effects</param> 
        public PaymentsRequestBuilder ForLedger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString(), "payments");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /transactions/{transactionId}/payments</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/payments-for-transaction.html">Effect for Transaction</a>
        /// </Summary>
        /// <param name="transactionId">Transaction ID for which to get payments</param>
        public PaymentsRequestBuilder ForTransaction(string transactionId)
        {
            transactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId), "transactionId cannot be null");
            SetSegments("transactions", transactionId, "payments");
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
        public EventSource Stream(EventHandler<OperationResponse> listener)
        {
            return Stream<OperationResponse>(listener);
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<OperationResponse>> Execute()
        {
            return await Execute<Page<OperationResponse>>(BuildUri());
        }
    }
}
