using System;

namespace stellar_dotnetcore_sdk.federation
{
    /// <inheritdoc />
    /// <summary>
    ///     Federation server is invalid (malformed URL, not HTTPS, etc.)
    /// </summary>
    public class FederationServerInvalidException : Exception
    {
        public override string Message => "Federation server is invalid (malformed URL, not HTTPS, etc.";
    }
}