using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventSource4Net;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.accountResponse;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_sdk.requests
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
        public AccountsRequestBuilder(Uri serverUri)
            : base(serverUri, "accounts")
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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
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
        ///     Requests specific uri and returns Page of AccountResponse.
        ///     This method is helpful for getting the next set of results.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Page of AccountResponse</returns>
        public static async Task<Page<AccountResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<AccountResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
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
                if (e.Data == "\"hello\"")
                    return;

                var account = JsonSingleton.GetInstance<AccountResponse>(e.Data);
                listener?.Invoke(this, account);
            };

            return es;
        }

        public async Task<Page<AccountResponse>> Execute()
        {
            return await Execute(BuildUri());
        }

        public override RequestBuilder<AccountsRequestBuilder> Cursor(string cursor)
        {
            base.Cursor(cursor);
            return this;
        }

        public override RequestBuilder<AccountsRequestBuilder> Limit(int number)
        {
            base.Limit(number);
            return this;
        }

        public override RequestBuilder<AccountsRequestBuilder> Order(OrderDirection direction)
        {
            base.Order(direction);
            return this;
        }
    }
}