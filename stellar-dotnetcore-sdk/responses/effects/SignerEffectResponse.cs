using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.effects
{
    public class SignerEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; }

        [JsonProperty(PropertyName = "public_key")]
        public string PublicKey { get; }

        public SignerEffectResponse(int weight, string publicKey)
        {
            Weight = weight;
            PublicKey = publicKey;
        }
    }
}