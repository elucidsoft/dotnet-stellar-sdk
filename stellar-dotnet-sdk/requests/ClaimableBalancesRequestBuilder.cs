using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk.requests
{
    public class ClaimableBalancesRequestBuilder : RequestBuilderExecutePageable<ClaimableBalancesRequestBuilder, ClaimableBalanceResponse>
    {
        public ClaimableBalancesRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "claimable_balances", httpClient)
        {
        }
        

        public ClaimableBalancesRequestBuilder ForAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("asset", asset.CanonicalName());
            return this;
        }

        public ClaimableBalancesRequestBuilder ForClaimant(KeyPair claimant)
        {
            UriBuilder.SetQueryParam("claimant", claimant.Address);
            return this;
        }        
        
        public ClaimableBalancesRequestBuilder ForSponsor(KeyPair sponsor)
        {
            UriBuilder.SetQueryParam("sponsor", sponsor.Address);
            return this;
        }

        public async Task<ClaimableBalanceResponse> ClaimableBalance(Uri uri)
        {
            var responseHandler = new ResponseHandler<ClaimableBalanceResponse>();
            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);                
        }
        
        public Task<ClaimableBalanceResponse> ClaimableBalance(string balanceId)
        {
            SetSegments("claimable_balances", balanceId);
            return ClaimableBalance(BuildUri());
        }
    }
}