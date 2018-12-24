using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class Link
    {
        public Link(string href, bool templated)
        {
            Href = href;
            Templated = templated;
        }

        [JsonProperty(PropertyName = "href")] public string Href { get; set; }

        [JsonProperty(PropertyName = "templated")]
        public bool Templated { get; set; }

        [JsonIgnore]
        public Uri Uri
        {
            get
            {
                try
                {
                    return new Uri(Href);
                }
                catch (UriFormatException)
                {
                    throw;
                }
            }
        }

        public bool IsTemplated()
        {
            return Templated;
        }
    }
}