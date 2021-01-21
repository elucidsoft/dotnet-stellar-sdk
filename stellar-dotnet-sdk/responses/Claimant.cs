using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class Claimant
    {
        [JsonProperty(PropertyName = "destination")]
        public string Destination { get; set; }

        [JsonProperty(PropertyName = "predicate")]
        public Predicate Predicate { get; set; }
    }
}