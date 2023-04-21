using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.requests
{
    public enum OrderDirection
    {
        ASC,
        DESC
    }

    /// <summary>
    ///     Abstract class for request builders.
    /// </summary>
    public class RequestBuilder<T> where T : class
    {
        private readonly List<string> _segments;
        private bool _segmentsAdded;
        protected UriBuilder UriBuilder;
        private readonly string _serverPathPrefix;

        public static HttpClient HttpClient { get; set; }

        public async Task<TZ> Execute<TZ>(Uri uri, string? jwt = null) where TZ : class
        {
            var responseHandler = new ResponseHandler<TZ>();

            if (jwt != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", jwt);
            }

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        public async Task<TZ> ExecutePost<TZ>(Uri uri, string? jwt = null,
            MultipartFormDataContent? multipartContent = null, object? data = null) where TZ : class
        {
            var responseHandler = new ResponseHandler<TZ>();

            if (jwt != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", jwt);
            }

            HttpContent content;
            if (multipartContent != null)
            {
                content = multipartContent;
            }
            else
            {
                content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }

            var response = await HttpClient.PostAsync(uri, content);
            return await responseHandler.HandleResponse(response);
        }

        public string Uri
        {
            get => BuildUri().ToString();
        }

        public RequestBuilder(Uri serverUri, string defaultSegment, HttpClient httpClient)
        {
            UriBuilder = new UriBuilder(serverUri);
            _segments = new List<string>();

            // Store the required path part of the serverUri
            _serverPathPrefix = UriBuilder.Path;

            if (!string.IsNullOrEmpty(defaultSegment))
                SetSegments(defaultSegment);

            _segmentsAdded = false; //Allow overwriting segments
            HttpClient = httpClient;
        }

        protected RequestBuilder<T> SetSegments(params string[] segments)
        {
            if (_segmentsAdded)
                throw new Exception("URL segments have been already added.");

            _segmentsAdded = true;

            //Remove default segments
            _segments.Clear();

            foreach (var segment in segments)
                _segments.Add(segment);

            return this;
        }

        protected RequestBuilder<T> SetQueryParameters(Dictionary<string, string> queryParams)
        {
            foreach (var param in queryParams)
            {
                UriBuilder.SetQueryParam(param.Key, param.Value);
            }

            return this;
        }

        /// <summary>
        ///     Sets <code>cursor</code> parameter on the request.
        ///     A cursor is a value that points to a specific location in a collection of resources.
        ///     The cursor attribute itself is an opaque value meaning that users should not try to parse it.
        ///     Read https://www.stellar.org/developers/horizon/reference/resources/page.html for more information.
        /// </summary>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public virtual T Cursor(string cursor)
        {
            UriBuilder.SetQueryParam("cursor", cursor);

            return this as T;
        }

        /// <summary>
        ///     Sets <code>limit</code> parameter on the request.
        ///     It defines maximum number of records to return.
        ///     For range and default values check documentation of the endpoint requested.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public virtual T Limit(int number)
        {
            UriBuilder.SetQueryParam("limit", number.ToString());

            return this as T;
        }

        /// <summary>
        ///     Sets order parameter on request.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual T Order(OrderDirection direction)
        {
            UriBuilder.SetQueryParam("order", direction.ToString().ToLower());

            return this as T;
        }

        /// <summary>
        ///     Allows to stream SSE events from horizon.
        ///     Certain endpoints in Horizon can be called in streaming mode using Server-Sent Events.
        ///     This mode will keep the connection to horizon open and horizon will continue to return
        ///     http://www.w3.org/TR/eventsource/
        ///     "https://www.stellar.org/developers/horizon/learn/responses.html
        ///     responses as ledgers close.
        /// </summary>
        /// <param name="listener">
        ///     EventListener implementation with AccountResponse type
        ///     <returns>EventSource object, so you can close() connection when not needed anymore</returns>
        public Uri BuildUri()
        {
            var path = _serverPathPrefix;

            if (_segments.Count > 0)
            {
                foreach (var segment in _segments)
                    path += (path.EndsWith("/") ? string.Empty : "/") + segment;
            }

            UriBuilder.Path = path;

            return UriBuilder.Uri;
        }
    }
}