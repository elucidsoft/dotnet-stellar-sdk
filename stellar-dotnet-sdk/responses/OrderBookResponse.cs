using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses;

public class OrderBookResponse
{
    [JsonProperty(PropertyName = "base")]
    [JsonConverter(typeof(AssetDeserializer))]
    public Asset OrderBookBase { get; private set; }

    [JsonProperty(PropertyName = "counter")]
    [JsonConverter(typeof(AssetDeserializer))]
    public Asset Counter { get; private set; }

    [JsonProperty(PropertyName = "asks")] public Row[] Asks { get; private set; }

    [JsonProperty(PropertyName = "bids")] public Row[] Bids { get; private set; }

    public OrderBookResponse(Asset orderBookBase, Asset counter, Row[] asks, Row[] bids)
    {
        OrderBookBase = orderBookBase;
        Counter = counter;
        Asks = asks;
        Bids = bids;
    }

    ///
    /// Represents order book row.
    ///
    public class Row
    {
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        /// <summary>
        ///     The ask/bid price.
        /// </summary>
        [JsonProperty(PropertyName = "price")] public string Price { get; private set; }

        /// <summary>
        ///     The ask/bid price as a ratio.
        /// </summary>
        [JsonProperty(PropertyName = "price_r")]
        public Price PriceR { get; private set; }

        public Row(string amount, string price, Price priceR)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            PriceR = priceR ?? throw new ArgumentNullException(nameof(priceR), "priceR cannot be null");
        }
    }
}