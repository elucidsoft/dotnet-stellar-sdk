using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account signers.
    /// </summary>
    public class Signer
    {
        public Signer(string accountId, int? weight)
        {
            AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId), "accountId cannot be null");
            Weight = weight ?? throw new ArgumentNullException(nameof(weight), "weight cannot be null");
        }

        [JsonProperty(PropertyName = "public_key")]
        public string AccountId { get; private set; }

        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; private set; }
    }
}