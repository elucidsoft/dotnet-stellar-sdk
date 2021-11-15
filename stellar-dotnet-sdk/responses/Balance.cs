using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account balance.
    /// </summary>
    public class Balance
    {
        public Balance(string assetType, string assetCode, string assetIssuer, string balance, string limit, string buyingLiabilities, string sellingLiabilities, bool isAuthorized, bool isAuthorizedToMaintainLiabilities, string liquidityPoolId)
        {
            AssetType = assetType ?? throw new ArgumentNullException(nameof(assetType), "assertType cannot be null");
            BalanceString = balance ?? throw new ArgumentNullException(nameof(balance), "balance cannot be null");
            Limit = limit;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;

            if (assetType != "liquidity_pool_shares")
            {
                BuyingLiabilities = buyingLiabilities ?? throw new ArgumentNullException(nameof(buyingLiabilities), "buyingLiabilities cannot be null");
                SellingLiabilities = sellingLiabilities ?? throw new ArgumentNullException(nameof(sellingLiabilities), "sellingLiabilities cannot be null");
            }

            IsAuthorized = isAuthorized;
            IsAuthorizedToMaintainLiabilities = isAuthorizedToMaintainLiabilities;

            if (assetType == "liquidity_pool_shares")
            {
                LiquidityPoolId = liquidityPoolId ?? throw new ArgumentNullException(nameof(liquidityPoolId));
            }
        }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        [JsonIgnore]
        public Asset Asset => Asset.Create(AssetType, AssetCode, AssetIssuer);

        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; private set; }

        [JsonProperty(PropertyName = "balance")]
        public string BalanceString { get; private set; }

        [JsonProperty(PropertyName = "buying_liabilities")]
        public string BuyingLiabilities { get; private set; }

        [JsonProperty(PropertyName = "selling_liabilities")]
        public string SellingLiabilities { get; private set; }

        [JsonProperty(PropertyName = "is_authorized")]
        public bool IsAuthorized { get; private set; }

        [JsonProperty(PropertyName = "is_authorized_to_maintain_liabilities")]
        public bool IsAuthorizedToMaintainLiabilities { get; private set; }

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public string LiquidityPoolId { get; private set; }
    }
}
