using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using stellar_dotnet_sdk.requests;

namespace stellar_dotnet_sdk.responses.page
{
    /// <summary>
    ///     Represents page of objects.
    ///     https://www.stellar.org/developers/horizon/reference/resources/page.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : Response
    {
        public class EmbeddedRecords
        {
            [JsonProperty(PropertyName = "records")]
            public List<T> Records { get; private set; }
        }

        [JsonProperty(PropertyName = "_embedded")]
        public EmbeddedRecords Embedded { get; set; }

        public List<T> Records => Embedded.Records;

        [JsonProperty(PropertyName = "_links")]
        public PageLinks<T> Links { get; private set; }

        /// <summary>
        ///     The previous page of results or null when there is no more results
        /// </summary>
        /// <returns></returns>
        public Task<Page<T>> PreviousPage() => Links.Prev?.Follow();

        /// <summary>
        ///     The next page of results or null when there is no more results
        /// </summary>
        /// <returns></returns>
        public Task<Page<T>> NextPage() => Links.Next?.Follow();
    }
}