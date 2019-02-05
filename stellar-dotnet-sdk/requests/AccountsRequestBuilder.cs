using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk.requests
{
    /// <summary>
    ///     Builds requests connected to accounts.
    /// </summary>
    public class AccountsRequestBuilder : RequestBuilderExecutePageable<AccountsRequestBuilder, AccountResponse>
    {
        /// <summary>
        ///     Builds requests connected to accounts.
        /// </summary>
        /// <param name="serverUri"></param>
        public AccountsRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "accounts", httpClient)
        {
        }

        /// <summary>
        ///     Requests specific uri and returns AccountResponse
        ///     This method is helpful for getting the links.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<AccountResponse> Account(Uri uri)
        {
            var responseHandler = new ResponseHandler<AccountResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        public async Task<AccountDataResponse> AccountData(Uri uri)
        {
            var responseHandler = new ResponseHandler<AccountDataResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        /// <summary>
        ///     Requests GET /accounts/{account}
        ///     https://www.stellar.org/developers/horizon/reference/accounts-single.html
        /// </summary>
        /// <param name="account">Account to fetch</param>
        /// <returns></returns>
        public async Task<AccountResponse> Account(string account)
        {
            SetSegments("accounts", account);
            return await Account(BuildUri());
        }

        /// <summary>
        ///     Requests GET /accounts/{account}/data/{key}
        ///     https://www.stellar.org/developers/horizon/reference/endpoints/data-for-account.html
        /// </summary>
        /// <param name="account">Account to fetch</param>
        /// <param name="key">Key to the data needing retrieval.</param>
        /// <returns></returns>
        public async Task<AccountDataResponse> AccountData(KeyPair account, string key)
        {
            SetSegments("accounts", account.AccountId, "data", key);
            return await AccountData(BuildUri());
        }
    }
}