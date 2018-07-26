using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.requests
{
    public class OperationsRequestBuilder : RequestBuilder<OperationsRequestBuilder>
    {
        /// <summary>
        ///     Builds requests connected to operations.
        /// </summary>
        /// <param name="serverUri"></param>
        public OperationsRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "operations", httpClient)
        {
        }

        /// <summary>
        ///     Requests specific uri and returns <see cref="OperationResponse" />.
        ///     This method is helpful for getting the links.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>
        ///     <see cref="Task{OperationResponse}" />
        /// </returns>
        public async Task<OperationResponse> Operation(Uri uri)
        {
            var responseHandler = new ResponseHandler<OperationResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
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
            var es = new EventSource(BuildUri());

            es.Message += (sender, e) =>
            {
                if (e.Data == "\"hello\"\r\n" || e.Data == "\"hello\"\n")
                    return;

                var account = JsonSingleton.GetInstance<OperationResponse>(e.Data);
                listener?.Invoke(this, account);
            };

            return es;
        }

        /// <summary>
        ///     Requests GET /operations/{operationId}
        ///     See: https://www.stellar.org/developers/horizon/reference/operations-single.html
        /// </summary>
        /// <param name="operationId">Operation to fetch</param>
        /// <returns>
        ///     <see cref="OperationsRequestBuilder" />
        /// </returns>
        /// <exception cref="HttpRequestException"></exception>
        public OperationsRequestBuilder Operation(long operationId)
        {
            SetSegments("operations", operationId.ToString());
            return this;
        }

        /// <summary>
        ///     Builds request to GET /accounts/{account}/operations
        ///     See: https://www.stellar.org/developers/horizon/reference/operations-for-account.html
        /// </summary>
        /// <param name="account">Account for which to get operations</param>
        /// <returns>
        ///     <see cref="OperationsRequestBuilder" />
        /// </returns>
        /// <exception cref="HttpRequestException"></exception>
        public OperationsRequestBuilder ForAccount(KeyPair account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "account cannot be null");

            SetSegments("accounts", account.AccountId, "operations");

            return this;
        }

        /// <summary>
        ///     uilds request to GET /ledgers/{ledgerSeq}/operations
        ///     See: https://www.stellar.org/developers/horizon/reference/operations-for-ledger.html
        /// </summary>
        /// <param name="ledgerSeq">Ledger for which to get operations</param>
        /// <returns>
        ///     <see cref="OperationsRequestBuilder" />
        /// </returns>
        public OperationsRequestBuilder ForLedger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString(), "operations");

            return this;
        }

        /// <summary>
        ///     Builds request to GET /transactions/{transactionId}/operations
        ///     See: https://www.stellar.org/developers/horizon/reference/operations-for-transaction.html
        /// </summary>
        /// <param name="transactionId">Transaction ID for which to get operations</param>
        /// <returns>
        ///     <see cref="OperationsRequestBuilder" />
        /// </returns>
        public OperationsRequestBuilder ForTransaction(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentNullException(nameof(transactionId), "transactionId cannot be null");

            SetSegments("transactions", transactionId, "operations");

            return this;
        }

        /// <summary>
        ///     Build and execute request.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{Page{OperationResponse}}" />
        /// </returns>
        /// <exception cref="TooManyRequestsException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Page<OperationResponse>> Execute()
        {
            return await Execute<Page<OperationResponse>>(BuildUri());
        }
    }
}