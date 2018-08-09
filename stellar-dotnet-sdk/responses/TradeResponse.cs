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
        public TradeResponse(String id, String pagingToken, String ledgerCloseTime, String offerId, bool baseIsSeller, KeyPair baseAccount, String baseAmount, String baseAssetType, String baseAssetCode, String baseAssetIssuer, KeyPair counterAccount, String counterAmount, String counterAssetType, String counterAssetCode, String counterAssetIssuer)
        {
            Id = id;
            PagingToken = pagingToken;
            LedgerCloseTime = ledgerCloseTime;
            OfferId = offerId;
            BaseIsSeller = baseIsSeller;
            BaseAccount = baseAccount;
            BaseAmount = baseAmount;
            BaseAssetType = baseAssetType;
            BaseAssetCode = baseAssetCode;
            BaseAssetIssuer = baseAssetIssuer;
            CounterAccount = counterAccount;
            CounterAmount = counterAmount;
            CounterAssetType = counterAssetType;
            CounterAssetCode = counterAssetCode;
            CounterAssetIssuer = counterAssetIssuer;
        }

        [JsonProperty(PropertyName = "id")] public String Id { get; }

        [JsonProperty(PropertyName = "paging_token")]
        public String PagingToken { get; }

        [JsonProperty(PropertyName = "ledger_close_time")]
        public String LedgerCloseTime { get; }

        [JsonProperty(PropertyName = "offer_id")]
        public String OfferId { get; }

        [JsonProperty(PropertyName = "base_is_seller")]
        public bool BaseIsSeller { get; }

        [JsonProperty(PropertyName = "base_account")]
        public KeyPair BaseAccount { get; }

        [JsonProperty(PropertyName = "base_amount")]
        public String BaseAmount { get; }

        [JsonProperty(PropertyName = "base_asset_type")]
        public String BaseAssetType { get; }

        [JsonProperty(PropertyName = "base_asset_code")]
        public String BaseAssetCode { get; }

        [JsonProperty(PropertyName = "base_asset_issuer")]
        public String BaseAssetIssuer { get; }

        [JsonProperty(PropertyName = "counter_account")]
        public KeyPair CounterAccount { get; }

        [JsonProperty(PropertyName = "counter_amount")]
        public String CounterAmount { get; }

        [JsonProperty(PropertyName = "counter_asset_type")]
        public String CounterAssetType { get; }

        [JsonProperty(PropertyName = "counter_asset_code")]
        public String CounterAssetCode { get; }

        [JsonProperty(PropertyName = "counter_asset_issuer")]
        public String CounterAssetIssuer { get; }

        [JsonProperty(PropertyName = "_links")]
        public TradeResponseLinks Links { get; }


        /// <summary>
        /// Creates and returns a base asset.
        /// </summary>
        public Asset BaseAsset
        {
            get { return Asset.Create(BaseAssetType, BaseAssetCode, BaseAssetIssuer); }
        }

        /// <summary>
        /// Creates and returns a counter asset.
        /// </summary>
        public Asset CountAsset
        {
            get { return Asset.Create(CounterAssetType, CounterAssetCode, CounterAssetIssuer); }
        }
    }

    public class TradeResponseLinks
    {
        public TradeResponseLinks(Link baseLink, Link counterLink, Link operationLink)
        {
            Base = baseLink;
            Counter = counterLink;
            Operation = operationLink;
        }

        [JsonProperty(PropertyName = "base")] public Link Base;

        [JsonProperty(PropertyName = "counter")]
        public Link Counter;

        [JsonProperty(PropertyName = "operation")]
        public Link Operation;
    }
}