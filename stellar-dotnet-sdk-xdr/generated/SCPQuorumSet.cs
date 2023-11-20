// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  struct SCPQuorumSet
//  {
//      uint32 threshold;
//      NodeID validators<>;
//      SCPQuorumSet innerSets<>;
//  };

//  ===========================================================================
public class SCPQuorumSet
{
    public Uint32 Threshold { get; set; }
    public NodeID[] Validators { get; set; }
    public SCPQuorumSet[] InnerSets { get; set; }

    public static void Encode(XdrDataOutputStream stream, SCPQuorumSet encodedSCPQuorumSet)
    {
        Uint32.Encode(stream, encodedSCPQuorumSet.Threshold);
        var validatorssize = encodedSCPQuorumSet.Validators.Length;
        stream.WriteInt(validatorssize);
        for (var i = 0; i < validatorssize; i++) NodeID.Encode(stream, encodedSCPQuorumSet.Validators[i]);
        var innerSetssize = encodedSCPQuorumSet.InnerSets.Length;
        stream.WriteInt(innerSetssize);
        for (var i = 0; i < innerSetssize; i++) Encode(stream, encodedSCPQuorumSet.InnerSets[i]);
    }

    public static SCPQuorumSet Decode(XdrDataInputStream stream)
    {
        var decodedSCPQuorumSet = new SCPQuorumSet();
        decodedSCPQuorumSet.Threshold = Uint32.Decode(stream);
        var validatorssize = stream.ReadInt();
        decodedSCPQuorumSet.Validators = new NodeID[validatorssize];
        for (var i = 0; i < validatorssize; i++) decodedSCPQuorumSet.Validators[i] = NodeID.Decode(stream);
        var innerSetssize = stream.ReadInt();
        decodedSCPQuorumSet.InnerSets = new SCPQuorumSet[innerSetssize];
        for (var i = 0; i < innerSetssize; i++) decodedSCPQuorumSet.InnerSets[i] = Decode(stream);
        return decodedSCPQuorumSet;
    }
}