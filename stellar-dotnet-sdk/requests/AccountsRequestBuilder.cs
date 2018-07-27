using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

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

        /// <summary>
        ///     Requests GET /accounts/{account}
        ///     https://www.stellar.org/developers/horizon/reference/accounts-single.html
        /// </summary>
        /// <param name="account">Account to fetch</param>
        /// <returns></returns>
        public async Task<AccountResponse> Account(KeyPair account)
        {
            SetSegments("accounts", account.AccountId);
            return await Account(BuildUri());
        }
    }
}