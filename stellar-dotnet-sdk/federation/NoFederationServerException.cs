using System;

namespace stellar_dotnet_sdk.federation
{
    /// <summary>
    ///     Federation server was not found in stellar.toml file.
    /// </summary>
    internal class NoFederationServerException : Exception
    {
    }
}