using System;

namespace stellar_dotnetcore_sdk.federation
{
    /// <summary>
    ///     Federation server was not found in stellar.toml file.
    /// </summary>
    internal class NoFederationServerException : Exception
    {
    }
}