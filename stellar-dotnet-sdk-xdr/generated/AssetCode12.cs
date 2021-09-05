// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  typedef opaque AssetCode12[12];

    //  ===========================================================================
    public class AssetCode12
    {
        public byte[] InnerValue { get; set; } = default(byte[]);

        public AssetCode12() { }

        public AssetCode12(byte[] value)
        {
            InnerValue = value;
        }

        public static void Encode(XdrDataOutputStream stream, AssetCode12 encodedAssetCode12)
        {
            int AssetCode12size = encodedAssetCode12.InnerValue.Length;
            stream.Write(encodedAssetCode12.InnerValue, 0, AssetCode12size);
        }
        public static AssetCode12 Decode(XdrDataInputStream stream)
        {
            AssetCode12 decodedAssetCode12 = new AssetCode12();
            int AssetCode12size = 12;
            decodedAssetCode12.InnerValue = new byte[AssetCode12size];
            stream.Read(decodedAssetCode12.InnerValue, 0, AssetCode12size);
            return decodedAssetCode12;
        }
    }
}
