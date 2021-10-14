using Newtonsoft.Json;
using stellar_dotnet_sdk.converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    [JsonConverter(typeof(ReserveJsonConverter))]
    public class Reserve
    {
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        [JsonProperty(PropertyName = "asset")]
        public Asset Asset { get; set; }

        public Reserve() { }

        public Reserve(string amount, Asset asset)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            Asset = asset ?? throw new ArgumentNullException(nameof(amount), "asset cannot be null");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Reserve))
            {
                return false;
            }

            Reserve other = (Reserve)obj;
            return Equals(Asset, other.Asset) && Equals(Amount, other.Amount);
        }

        public override int GetHashCode()
        {
            int hashCode = 1588693772;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Amount);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Asset>.Default.GetHashCode(Asset);
            return hashCode;
        }
    }
}
