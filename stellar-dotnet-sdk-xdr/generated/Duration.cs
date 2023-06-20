// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  typedef uint64 Duration;

    //  ===========================================================================
    public class Duration
    {
        public Uint64 InnerValue { get; set; } = default(Uint64);

        public Duration() { }

        public Duration(Uint64 value)
        {
            InnerValue = value;
        }

        public static void Encode(XdrDataOutputStream stream, Duration encodedDuration)
        {
            Uint64.Encode(stream, encodedDuration.InnerValue);
        }
        public static Duration Decode(XdrDataInputStream stream)
        {
            Duration decodedDuration = new Duration();
            decodedDuration.InnerValue = Uint64.Decode(stream);
            return decodedDuration;
        }
    }
}