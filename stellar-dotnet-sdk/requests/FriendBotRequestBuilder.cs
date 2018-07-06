using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    public class FriendBotRequestBuilder : RequestBuilder<FriendBotRequestBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUri"></param>
        public FriendBotRequestBuilder(Uri serverUri) : base(serverUri, "friendbot")
        {
            if (!Network.IsTestNetwork())
            {
                throw new NotSupportedException("FriendBot is only supported on the TESTNET Network.");
            }
        }
        public async Task<FriendBotResponse> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<FriendBotResponse>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }

        public async Task<FriendBotResponse> Execute()
        {
            return await Execute(BuildUri());
        }

        public FriendBotRequestBuilder FundAccount(KeyPair account)
        {
            _uriBuilder.SetQueryParam("addr", account.AccountId);
            return this;
        }
    }
}
