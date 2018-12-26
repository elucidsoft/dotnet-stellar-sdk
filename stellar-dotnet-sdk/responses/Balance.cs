using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account balance.
    /// </summary>
    public class Balance
    {
        [JsonProperty(PropertyName = "asset_issuer")]
        private string _assetIssuer;

        public Balance(string assetType, string assetCode, string assetIssuer, string balance, string limit, string buyingLiabilities, string sellingLiabilities)
        {
            AssetType = assetType ?? throw new ArgumentNullException(nameof(assetType), "assertType cannot be null");
            BalanceString = balance ?? throw new ArgumentNullException(nameof(balance), "balance cannot be null");
            Limit = limit;
            AssetCode = assetCode;
            _assetIssuer = assetIssuer;
            BuyingLiabilities = buyingLiabilities ?? throw new ArgumentNullException(nameof(buyingLiabilities), "buyingLiabilities cannot be null");
            SellingLiabilities = sellingLiabilities ?? throw new ArgumentNullException(nameof(sellingLiabilities), "sellingLiabilities cannot be null");
        }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        [JsonIgnore]
        //This prop is dynamic based on private field serialized above.
        public KeyPair AssetIssuer => KeyPair.FromAccountId(_assetIssuer);

        [JsonIgnore]
        public Asset Asset => Asset.Create(AssetType, AssetCode, _assetIssuer);

        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; private set; }

        [JsonProperty(PropertyName = "balance")]
        public string BalanceString { get; private set; }

        [JsonProperty(PropertyName = "buying_liabilities")]
        public string BuyingLiabilities { get; private set; }

        [JsonProperty(PropertyName = "selling_liabilities")]
        public string SellingLiabilities { get; private set; }
    }
}