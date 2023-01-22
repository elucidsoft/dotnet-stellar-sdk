// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  typedef string SCSymbol<10>;

    //  ===========================================================================
    public class SCSymbol
    {
        public String InnerValue { get; set; } = default(String);

        public SCSymbol() { }

        public SCSymbol(String value)
        {
            InnerValue = value;
        }

        public static void Encode(XdrDataOutputStream stream, SCSymbol encodedSCSymbol)
        {
            stream.WriteString(encodedSCSymbol.InnerValue);
        }
        public static SCSymbol Decode(XdrDataInputStream stream)
        {
            SCSymbol decodedSCSymbol = new SCSymbol();
            decodedSCSymbol.InnerValue = stream.ReadString();
            return decodedSCSymbol;
        }
    }
}
