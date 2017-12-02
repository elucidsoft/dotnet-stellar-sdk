using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class LedgersRequestBuilder : RequestBuilder<LedgersRequestBuilder>
    {
        public LedgersRequestBuilder(Uri serverUri) : base(serverUri, "ledgers")
        {
        }

        /// <summary>
        ///     Requests specific uri and returns LedgerResponse
        ///     This method is helpful for getting the links.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<LedgerResponse> Ledger(Uri uri)
        {
            var responseHandler = new ResponseHandler<LedgerResponse>();
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
        public Task<LedgerResponse> Ledger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString());
            return Ledger(BuildUri());
        }

        ///<Summary>
        /// Requests specific <code>uri</code> and returns {@link Page} of {@link EffectResponse}.
        /// This method is helpful for getting the next set of results.
        /// </Summary>
        public static async Task<Page<LedgerResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<LedgerResponse>>();
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
        public EventSource Stream(EventHandler<LedgerResponse> listener)
        {
            var es = new EventSource(BuildUri());
            es.Message += (sender, e) =>
            {
                if (e.Data == "\"hello\"\r\n")
                    return;

                var account = JsonSingleton.GetInstance<LedgerResponse>(e.Data);
                listener?.Invoke(this, account);
            };

            return es;
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<LedgerResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        public override LedgersRequestBuilder Cursor(string token)
        {
            base.Cursor(token);
            return this;
        }


        public override LedgersRequestBuilder Limit(int number)
        {
            base.Limit(number);
            return this;
        }


        public override LedgersRequestBuilder Order(OrderDirection direction)
        {
            base.Order(direction);
            return this;
        }
    }
}
