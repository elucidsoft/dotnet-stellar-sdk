using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.federation
{
    /// <summary>
    /// Federation server was not found in stellar.toml file.
    /// </summary>
    class NoFederationServerException : Exception
    {
    }
}
