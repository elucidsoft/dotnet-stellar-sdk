﻿using stellar_dotnet_sdk.responses;
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
        public FriendBotRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "friendbot", httpClient)
        {            
        }
        
        public FriendBotRequestBuilder FundAccount(KeyPair account, Network network)
        {
            if (network == null)
            {
                throw new NotSupportedException("FriendBot requires the TESTNET Network to be set explicitly.");
            }

            if (Network.IsPublicNetwork(network))
            {
                throw new NotSupportedException("FriendBot is only supported on the TESTNET Network.");
            }
            UriBuilder.SetQueryParam("addr", account.AccountId);
            return this;
        }
        
        public FriendBotRequestBuilder FundAccount(KeyPair account)
        {
            return FundAccount(account, Network.Current);
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<FriendBotResponse> Execute()
        {
            return await Execute<FriendBotResponse>(BuildUri());
        }
    }
}
