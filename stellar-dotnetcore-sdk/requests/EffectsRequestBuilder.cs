using EventSource4Net;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.effects;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class EffectsRequestBuilder : RequestBuilder<EffectsRequestBuilder>
    {
        public EffectsRequestBuilder(Uri serverURI) : base(serverURI, "effects")
        {
        }

        ///<Summary>
        /// Builds request to <code>GET /accounts/{account}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-account.html">Effects for Account</a>
        /// </Summary>
        /// <param name="account">Account for which to get effects</param> 
        public EffectsRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /ledgers/{ledgerSeq}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-ledger.html">Effects for Ledger</a>
        /// </Summary>
        /// <param name="ledgerSeq">Ledger for which to get effects</param> 
        public EffectsRequestBuilder ForLedger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString(), "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /transactions/{transactionId}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-transaction.html">Effect for Transaction</a>
        /// </Summary>
        /// <param name="transactionId">Transaction ID for which to get effects</param>
        public EffectsRequestBuilder ForTransaction(String transactionId)
        {
            transactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId), "transactionId cannot be null");
            SetSegments("transactions", transactionId, "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /operation/{operationId}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-operation.html">Effect for Operation</a>
        /// </Summary>
        /// <param name="operationId">Operation ID for which to get effects</param>
        public EffectsRequestBuilder ForOperation(long operationId)
        {
            SetSegments("operations", operationId.ToString(), "effects");
            return this;
        }

        ///<Summary>
        /// Requests specific <code>uri</code> and returns {@link Page} of {@link EffectResponse}.
        /// This method is helpful for getting the next set of results.
        /// </Summary>
        public static async Task<Page<EffectResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<EffectResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
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
        public EventSource Stream(EventHandler<EffectResponse> listener)
        {
            var es = new EventSource(BuildUri());
            es.Message += (sender, e) =>
            {
                if (e.Data == "\"hello\"")
                    return;

                var account = JsonSingleton.GetInstance<EffectResponse>(e.Data);
                listener?.Invoke(this, account);
            };

            return es;
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<EffectResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        public override RequestBuilder<EffectsRequestBuilder> Cursor(String token)
        {
            base.Cursor(token);
            return this;
        }


        public override RequestBuilder<EffectsRequestBuilder> Limit(int number)
        {
            base.Limit(number);
            return this;
        }


        public override RequestBuilder<EffectsRequestBuilder> Order(OrderDirection direction)
        {
            base.Order(direction);
            return this;
        }
    }
}
