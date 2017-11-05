using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.federation
{
    public static class Federation
    {
        public static async Task<FederationResponse> Resolve(string value)
        {
            var tokens = Regex.Split(value, "\\*");
            if (tokens.Length == 1)
                return new FederationResponse(null, value, null, null);

            if (tokens.Length == 2)
            {
                var domain = tokens[1];
                var server = await FederationServer.CreateForDomain(domain);
                return await server.ResolveAddress(value);
            }

            throw new MalformedAddressException();
        }
    }
}