using Newtonsoft.Json;
using System;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Represents trades response.
    /// See: https://www.stellar.org/developers/horizon/reference/endpoints/trades.html
    /// <seealso cref="TradesRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class TradeResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "id")] 
        public string Id { get; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; }

        [JsonProperty(PropertyName = "ledger_close_time")]
        public string LedgerCloseTime { get; }

        [JsonProperty(PropertyName = "offer_id")]
        public string OfferId { get; }

        [JsonProperty(PropertyName = "base_is_seller")]
        public bool BaseIsSeller { get; }

        [JsonProperty(PropertyName = "base_account")]
        public string BaseAccount { get; }

        [JsonProperty(PropertyName = "base_liquidity_pool_id")]
        public LiquidityPoolID BaseLiquidityPoolID { get; set; }

        [JsonProperty(PropertyName = "base_offer_id")]
        public string BaseOfferId { get; }

        [JsonProperty(PropertyName = "base_amount")]
        public string BaseAmount { get; }

        [JsonProperty(PropertyName = "base_asset_type")]
        public string BaseAssetType { get; }

        [JsonProperty(PropertyName = "base_asset_code")]
        public string BaseAssetCode { get; }

        [JsonProperty(PropertyName = "base_asset_issuer")]
        public string BaseAssetIssuer { get; }

        [JsonProperty(PropertyName = "counter_account")]
        public string CounterAccount { get; }

        [JsonProperty(PropertyName = "counter_liquidity_pool_id")]
        public LiquidityPoolID CounterLiquidityPoolID { get; }

        [JsonProperty(PropertyName = "counter_offer_id")]
        public string CounterOfferId { get; }

        [JsonProperty(PropertyName = "counter_amount")]
        public string CounterAmount { get; }

        [JsonProperty(PropertyName = "counter_asset_type")]
        public string CounterAssetType { get; }

        [JsonProperty(PropertyName = "counter_asset_code")]
        public string CounterAssetCode { get; }

        [JsonProperty(PropertyName = "counter_asset_issuer")]
        public string CounterAssetIssuer { get; }

        [JsonProperty(PropertyName = "price")]
        public TradePrice Price { get; }

        [JsonProperty(PropertyName = "_links")]
        public TradeResponseLinks Links { get; }

        /// <summary>
        /// Creates and returns a base asset.
        /// </summary>
        public Asset BaseAsset => Asset.Create(BaseAssetType, BaseAssetCode, BaseAssetIssuer);

        /// <summary>
        /// Creates and returns a counter asset.
        /// </summary>
        public Asset CountAsset => Asset.Create(CounterAssetType, CounterAssetCode, CounterAssetIssuer);

        public TradeResponse(string id, string pagingToken, string ledgerCloseTime, string offerId, bool baseIsSeller, string baseAccount, LiquidityPoolID baseLiquidityPoolID, string baseOfferId, string baseAmount, string baseAssetType, string baseAssetCode, string baseAssetIssuer, string counterAccount, LiquidityPoolID counterLiquidityPoolID, string counterOfferId, string counterAmount, string counterAssetType, string counterAssetCode, string counterAssetIssuer, TradePrice price)
        {
            Id = id;
            PagingToken = pagingToken;
            LedgerCloseTime = ledgerCloseTime;
            OfferId = offerId;
            BaseIsSeller = baseIsSeller;
            BaseAccount = baseAccount;
            BaseLiquidityPoolID = baseLiquidityPoolID;
            BaseOfferId = baseOfferId;
            BaseAmount = baseAmount;
            BaseAssetType = baseAssetType;
            BaseAssetCode = baseAssetCode;
            BaseAssetIssuer = baseAssetIssuer;
            CounterAccount = counterAccount;
            CounterLiquidityPoolID = counterLiquidityPoolID;
            CounterOfferId = counterOfferId;
            CounterAmount = counterAmount;
            CounterAssetType = counterAssetType;
            CounterAssetCode = counterAssetCode;
            CounterAssetIssuer = counterAssetIssuer;
            Price = price;
        }
    }
}
