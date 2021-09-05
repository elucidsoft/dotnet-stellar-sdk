// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  typedef PeerStats PeerStatList<25>;

    //  ===========================================================================
    public class PeerStatList
    {
        public PeerStats[] InnerValue { get; set; } = default(PeerStats[]);

        public PeerStatList() { }

        public PeerStatList(PeerStats[] value)
        {
            InnerValue = value;
        }

        public static void Encode(XdrDataOutputStream stream, PeerStatList encodedPeerStatList)
        {
            int PeerStatListsize = encodedPeerStatList.InnerValue.Length;
            stream.WriteInt(PeerStatListsize);
            for (int i = 0; i < PeerStatListsize; i++)
            {
                PeerStats.Encode(stream, encodedPeerStatList.InnerValue[i]);
            }
        }
        public static PeerStatList Decode(XdrDataInputStream stream)
        {
            PeerStatList decodedPeerStatList = new PeerStatList();
            int PeerStatListsize = stream.ReadInt();
            decodedPeerStatList.InnerValue = new PeerStats[PeerStatListsize];
            for (int i = 0; i < PeerStatListsize; i++)
            {
                decodedPeerStatList.InnerValue[i] = PeerStats.Decode(stream);
            }
            return decodedPeerStatList;
        }
    }
}
