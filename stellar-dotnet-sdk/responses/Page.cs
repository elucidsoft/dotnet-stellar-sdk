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
        [JsonProperty(PropertyName = "records")]
        public List<T> Records { get; private set; }

        [JsonProperty(PropertyName = "links")]
        public PageLinks Links { get; private set; }

        /// <summary>
        ///     The next page of results or null when there is no more results
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> NextPage()
        {
            if (Links.Next == null)
                return null;

            var responseHandler = new ResponseHandler<Page<T>>();
            var uri = new Uri(Links.Next.Href);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }
    }

    /// <summary>
    ///     Links connected to page response.
    /// </summary>
    public class PageLinks
    {
        public PageLinks(Link next, Link prev, Link self)
        {
            Next = next;
            Prev = prev;
            Self = self;
        }

        public Link Next { get; }

        public Link Prev { get; }

        public Link Self { get; }
    }
}