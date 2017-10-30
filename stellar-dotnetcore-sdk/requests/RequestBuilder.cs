using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.requests
{
    public enum OrderDirection { ASC, DESC }

    /// <summary>
    /// Abstract class for request builders.
    /// </summary>
    public class RequestBuilder<T>
    {
       

        protected UriBuilder _uriBuilder;
        private List<string> _segments;
        private Boolean _segmentsAdded;

        public RequestBuilder(Uri serverUri, string defaultSegment)
        {
            _uriBuilder = new UriBuilder(serverUri);
            _segments = new List<string>();

            if(!String.IsNullOrEmpty(defaultSegment))
            {
                SetSegments(defaultSegment);
            }

            _segmentsAdded = false;  //Allow overwriting segments
        }

        protected RequestBuilder<T> SetSegments(params string[] segments)
        {
            if (_segmentsAdded)
                throw new Exception("URL segments have been already added.");

            _segmentsAdded = true;

            //Remove default segments
            _segments.Clear();

            foreach(var segment in segments)
            {
                _segments.Add(segment);
            }

            return this;
        }

        /// <summary>
        /// Sets <code>cursor</code> parameter on the request. 
        /// A cursor is a value that points to a specific location in a collection of resources. 
        /// The cursor attribute itself is an opaque value meaning that users should not try to parse it.
        /// Read https://www.stellar.org/developers/horizon/reference/resources/page.html for more information.
        /// </summary>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public virtual RequestBuilder<T> Cursor(string cursor)
        {
            _uriBuilder.SetQueryParam("cursor", cursor);

            return this;
        }

        /// <summary>
        ///  Sets <code>limit</code> parameter on the request.
        ///  It defines maximum number of records to return. 
        ///  For range and default values check documentation of the endpoint requested.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public virtual RequestBuilder<T> Limit(int number)
        {
            _uriBuilder.SetQueryParam("limit", number.ToString());

            return this;
        }

        /// <summary>
        /// Sets order parameter on request.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual RequestBuilder<T> Order(OrderDirection direction)
        {
            _uriBuilder.SetQueryParam("order", direction.ToString().ToLower());

            return this;
        }

        public Uri BuildUri()
        {
            if(_segments.Count > 0)
            {
                string path = "";

                foreach(string segment in _segments)
                {
                    path += "/" + segment;
                }

                _uriBuilder.Path = path;

                try
                {
                    return _uriBuilder.Uri;
                }
                catch(UriFormatException)
                {
                    throw;
                }
            }

            throw new NotSupportedException("No segments defined.");
        }

      
    }
}
