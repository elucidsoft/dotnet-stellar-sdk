using System;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    public class OrderBookResponse
    {
        [JsonProperty(PropertyName = "base")]
        public Asset OrderBookBase { get; private set; }
        [JsonProperty(PropertyName = "counter")]
        public Asset Counter { get; private set; }
        [JsonProperty(PropertyName = "asks")]
        public Row[] Asks { get; private set; }
        [JsonProperty(PropertyName = "bids")]
        public Row[] Bids { get; private set; }

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
            public String Amount { get; private set; }
            [JsonProperty(PropertyName = "price")]
            public String Price { get; private set; }
            [JsonProperty(PropertyName = "price_r")]
            public Price PriceR { get; private set; }

            public Row(String amount, String price, Price priceR)
            {
                this.Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                this.Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
                this.PriceR = priceR ?? throw new ArgumentNullException(nameof(priceR), "priceR cannot be null");
            }
        }
    }
}
