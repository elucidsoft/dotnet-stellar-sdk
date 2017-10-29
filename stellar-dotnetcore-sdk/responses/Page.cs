using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.responses.page
{
    /// <summary>
    ///  Represents page of objects.
    ///  https://www.stellar.org/developers/horizon/reference/resources/page.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : Response
    {
        [JsonProperty(PropertyName = "records")]
        public List<T> Records { get; private set; }

        [JsonProperty(PropertyName = "links")]
        public Links Links { get; private set; }

        public Page() { }

        /// <summary>
        /// The next page of results or null when there is no more results
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> NextPage()
        {
            if (Links.Next == null)
                return null;

            ResponseHandler<Page<T>> responseHandler = new ResponseHandler<Page<T>>();
            Uri uri = new Uri(Links.Next.Href);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }
    }

    /// <summary>
    /// Links connected to page response.
    /// </summary>
    public class Links
    {
        public Links(Link next, Link prev, Link self)
        {
            Next = next;
            Prev = prev;
            Self = self;
        }

        public Link Next { get; private set; }

        public Link Prev { get; private set; }

        public Link Self { get; private set; }
    }
}
