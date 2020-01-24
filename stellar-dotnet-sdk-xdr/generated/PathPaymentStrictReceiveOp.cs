// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;
namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================
    //  struct PathPaymentStrictReceiveOp
    //  {
    //      Asset sendAsset; // asset we pay with
    //      int64 sendMax;   // the maximum amount of sendAsset to
    //                       // send (excluding fees).
    //                       // The operation will fail if can't be met
    //  
    //      AccountID destination; // recipient of the payment
    //      Asset destAsset;       // what they end up with
    //      int64 destAmount;      // amount they end up with
    //  
    //      Asset path<5>; // additional hops it must go through to get there
    //  };
    //  ===========================================================================
    public class PathPaymentStrictReceiveOp
    {
        public PathPaymentStrictReceiveOp() { }
        public Asset SendAsset { get; set; }
        public Int64 SendMax { get; set; }
        public AccountID Destination { get; set; }
        public Asset DestAsset { get; set; }
        public Int64 DestAmount { get; set; }
        public Asset[] Path { get; set; }

        public static void Encode(XdrDataOutputStream stream, PathPaymentStrictReceiveOp encodedPathPaymentStrictReceiveOp)
        {
            Asset.Encode(stream, encodedPathPaymentStrictReceiveOp.SendAsset);
            Int64.Encode(stream, encodedPathPaymentStrictReceiveOp.SendMax);
            AccountID.Encode(stream, encodedPathPaymentStrictReceiveOp.Destination);
            Asset.Encode(stream, encodedPathPaymentStrictReceiveOp.DestAsset);
            Int64.Encode(stream, encodedPathPaymentStrictReceiveOp.DestAmount);
            int pathsize = encodedPathPaymentStrictReceiveOp.Path.Length;
            stream.WriteInt(pathsize);
            for (int i = 0; i < pathsize; i++)
            {
                Asset.Encode(stream, encodedPathPaymentStrictReceiveOp.Path[i]);
            }
        }
        public static PathPaymentStrictReceiveOp Decode(XdrDataInputStream stream)
        {
            PathPaymentStrictReceiveOp decodedPathPaymentStrictReceiveOp = new PathPaymentStrictReceiveOp();
            decodedPathPaymentStrictReceiveOp.SendAsset = Asset.Decode(stream);
            decodedPathPaymentStrictReceiveOp.SendMax = Int64.Decode(stream);
            decodedPathPaymentStrictReceiveOp.Destination = AccountID.Decode(stream);
            decodedPathPaymentStrictReceiveOp.DestAsset = Asset.Decode(stream);
            decodedPathPaymentStrictReceiveOp.DestAmount = Int64.Decode(stream);
            int pathsize = stream.ReadInt();
            decodedPathPaymentStrictReceiveOp.Path = new Asset[pathsize];
            for (int i = 0; i < pathsize; i++)
            {
                decodedPathPaymentStrictReceiveOp.Path[i] = Asset.Decode(stream);
            }
            return decodedPathPaymentStrictReceiveOp;
        }
    }
}
