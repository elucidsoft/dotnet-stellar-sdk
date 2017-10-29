using stellar_dotnetcore_sdk.responses.accountResponse;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    /// <summary>
    /// Builds requests connected to accounts.
    /// </summary>
    public class AccountsRequestBuilder : RequestBuilder
    {

        public AccountsRequestBuilder(Uri serverUri)
            : base(serverUri, "accounts")
        {

        }

        public async Task<AccountResponse> Account(Uri uri)
        {
            var responseHandler = new ResponseHandler<AccountResponse>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }

        public async Task<AccountResponse> Account(KeyPair account)
        {
            throw new NotImplementedException();
        }
    }
}
