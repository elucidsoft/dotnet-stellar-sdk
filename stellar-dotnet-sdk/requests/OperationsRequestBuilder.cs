using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.requests
{
    public class OperationsRequestBuilder : RequestBuilderStreamable<OperationsRequestBuilder, OperationResponse>
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
        public OperationsRequestBuilder ForAccount(string account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "account cannot be null");

            SetSegments("accounts", account, "operations");

            return this;
        }

        /// <summary>
        ///     Builds request to GET /ledgers/{ledgerSeq}/operations
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
        ///     Set <code>include_failed</code> flag to include operations of failed transactions.
        /// </summary>
        /// <param name="includeFailed">Set to true to include operations of failed transactions in results</param>
        /// <returns>
        ///     <see cref="OperationsRequestBuilder" />
        /// </returns>
        public OperationsRequestBuilder IncludeFailed(bool includeFailed)
        {
            UriBuilder.SetQueryParam("include_failed", includeFailed.ToString().ToLowerInvariant());
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
    }
}