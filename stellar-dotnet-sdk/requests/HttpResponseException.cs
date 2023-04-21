using System;

namespace stellar_dotnet_sdk.requests
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public int StatusCode { get; set; }
        public string Body { get; set; }
    }
}