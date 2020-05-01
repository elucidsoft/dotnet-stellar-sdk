using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public interface IAccountId
    {
        xdr.MuxedAccount MuxedAccount { get; }
        KeyPair SigningKey { get; }
        byte[] PublicKey { get; }
        string Address { get; }
        string AccountId { get; }
        bool IsMuxedAccount { get; }
    }
}