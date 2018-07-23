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
    public class AccountsRequestBuilder : RequestBuilder<AccountsRequestBuilder>
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

        /// <summary>
        ///     llows to stream SSE events from horizon.
        ///     Certain endpoints in Horizon can be called in streaming mode using Server-Sent Events.
        ///     This mode will keep the connection to horizon open and horizon will continue to return
        ///     http://www.w3.org/TR/eventsource/
        ///     "https://www.stellar.org/developers/horizon/learn/responses.html
        ///     responses as ledgers close.
        /// </summary>
        /// <param name="listener">
        ///     EventListener implementation with AccountResponse type
        ///     <returns>EventSource object, so you can close() connection when not needed anymore</returns>
        public EventSource Stream(EventHandler<AccountResponse> listener)
        {
            var es = new EventSource(BuildUri());
            es.Message += (sender, e) =>
            {
                if (e.Data == "\"hello\"\r\n")
                    return;

                var account = JsonSingleton.GetInstance<AccountResponse>(e.Data);
                listener?.Invoke(this, account);
            };

            return es;
        }

        public async Task<Page<AccountResponse>> Execute()
        {
            return await Execute<Page<AccountResponse>>(BuildUri());
        }
    }
}