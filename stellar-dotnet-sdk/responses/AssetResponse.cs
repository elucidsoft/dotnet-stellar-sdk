using Newtonsoft.Json;
using System;

namespace stellar_dotnet_sdk.responses
{
    public class AssetResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "_links")]
        public AssetResponseLinks Links { get; set; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; set; }

        [JsonProperty(PropertyName = "accounts")]
        public AssetAccounts Accounts { get; set; }

        [JsonProperty(PropertyName = "balances")]
        public AssetBalances Balances { get; set; }

        [JsonProperty(PropertyName = "ammount")]
        public string Amount { get; set; }

        [JsonProperty(PropertyName = "claimable_balances_amount")]
        public string ClaimableBalancesAmount { get; set; }

        [JsonProperty(PropertyName = "num_accounts")]
        public long NumAccounts { get; set; }

        [JsonProperty(PropertyName = "num_claimable_balances")]
        public int NumClaimableBalances { get; set; }

        [JsonProperty(PropertyName = "flags")]
        public AssetResponseFlags Flags { get; set; }

        /// <summary>
        /// The number of liquidity pools trading this asset
        /// </summary>
        [JsonProperty(PropertyName = "num_liquidity_pools")]
        public int NumLiquidityPools { get; set; }

        /// <summary>
        /// The amount of this asset held in liquidity pools
        /// </summary>
        [JsonProperty(PropertyName = "liquidity_pools_amount")]
        public string LiquidityPoolsAmount { get; set; }

        /// <summary>
        /// Describe asset accounts
        /// </summary>
        public class AssetAccounts
        {
            [JsonProperty(PropertyName = "authorized")]
            public int Authorized { get; set; }

            [JsonProperty(PropertyName = "authorized_to_maintain_liabilities")]
            public int AuthorizedToMaintainLiabilities { get; set; }

            [JsonProperty(PropertyName = "unauthorized")]
            public int Unauthorized { get; set; }

            public AssetAccounts(int authorized, int authorizedToMaintainLiabilities, int unauthorized)
            {
                Authorized = authorized;
                AuthorizedToMaintainLiabilities = authorizedToMaintainLiabilities;
                Unauthorized = unauthorized;
            }
        }

        /// <summary>
        /// Describe asset balances
        /// </summary>
        public class AssetBalances
        {
            [JsonProperty(PropertyName = "authorized")]
            public string Authorized { get; set; }

            [JsonProperty(PropertyName = "authorized_to_maintain_liabilities")]
            public string AuthorizedToMaintainLiabilities { get; set; }

            [JsonProperty(PropertyName = "unauthorized")]
            public string Unauthorized { get; set; }

            public AssetBalances(string authorized, string authorizedToMaintainLiabilities, string unauthorized)
            {
                Authorized = authorized;
                AuthorizedToMaintainLiabilities = authorizedToMaintainLiabilities;
                Unauthorized = unauthorized;
            }
        }

        public Asset Asset
        {
            get { return Asset.Create(AssetType, AssetCode, AssetIssuer); }
        }

        public AssetResponse(string assetType, string assetCode, string assetIssuer, string pagingToken, string amount, int numAccounts, AssetResponseFlags flags, AssetResponseLinks links)
        {
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            PagingToken = pagingToken;
            Amount = amount;
            NumAccounts = numAccounts;
            Flags = flags;
            Links = links;
        }
    }
}