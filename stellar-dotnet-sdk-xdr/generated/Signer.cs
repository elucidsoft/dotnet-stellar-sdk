// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct Signer
    //  {
    //      SignerKey key;
    //      uint32 weight; // really only need 1 byte
    //  };

    //  ===========================================================================
    public class Signer
    {
        public Signer() { }
        public SignerKey Key { get; set; }
        public Uint32 Weight { get; set; }

        public static void Encode(XdrDataOutputStream stream, Signer encodedSigner)
        {
            SignerKey.Encode(stream, encodedSigner.Key);
            Uint32.Encode(stream, encodedSigner.Weight);
        }
        public static Signer Decode(XdrDataInputStream stream)
        {
            Signer decodedSigner = new Signer();
            decodedSigner.Key = SignerKey.Decode(stream);
            decodedSigner.Weight = Uint32.Decode(stream);
            return decodedSigner;
        }
    }
}
