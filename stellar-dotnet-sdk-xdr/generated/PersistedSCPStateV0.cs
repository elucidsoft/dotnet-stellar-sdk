// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  struct PersistedSCPStateV0
//  {
//  	SCPEnvelope scpEnvelopes<>;
//  	SCPQuorumSet quorumSets<>;
//  	StoredTransactionSet txSets<>;
//  };

//  ===========================================================================
public class PersistedSCPStateV0
{
    public SCPEnvelope[] ScpEnvelopes { get; set; }
    public SCPQuorumSet[] QuorumSets { get; set; }
    public StoredTransactionSet[] TxSets { get; set; }

    public static void Encode(XdrDataOutputStream stream, PersistedSCPStateV0 encodedPersistedSCPStateV0)
    {
        var scpEnvelopessize = encodedPersistedSCPStateV0.ScpEnvelopes.Length;
        stream.WriteInt(scpEnvelopessize);
        for (var i = 0; i < scpEnvelopessize; i++)
            SCPEnvelope.Encode(stream, encodedPersistedSCPStateV0.ScpEnvelopes[i]);
        var quorumSetssize = encodedPersistedSCPStateV0.QuorumSets.Length;
        stream.WriteInt(quorumSetssize);
        for (var i = 0; i < quorumSetssize; i++) SCPQuorumSet.Encode(stream, encodedPersistedSCPStateV0.QuorumSets[i]);
        var txSetssize = encodedPersistedSCPStateV0.TxSets.Length;
        stream.WriteInt(txSetssize);
        for (var i = 0; i < txSetssize; i++) StoredTransactionSet.Encode(stream, encodedPersistedSCPStateV0.TxSets[i]);
    }

    public static PersistedSCPStateV0 Decode(XdrDataInputStream stream)
    {
        var decodedPersistedSCPStateV0 = new PersistedSCPStateV0();
        var scpEnvelopessize = stream.ReadInt();
        decodedPersistedSCPStateV0.ScpEnvelopes = new SCPEnvelope[scpEnvelopessize];
        for (var i = 0; i < scpEnvelopessize; i++)
            decodedPersistedSCPStateV0.ScpEnvelopes[i] = SCPEnvelope.Decode(stream);
        var quorumSetssize = stream.ReadInt();
        decodedPersistedSCPStateV0.QuorumSets = new SCPQuorumSet[quorumSetssize];
        for (var i = 0; i < quorumSetssize; i++) decodedPersistedSCPStateV0.QuorumSets[i] = SCPQuorumSet.Decode(stream);
        var txSetssize = stream.ReadInt();
        decodedPersistedSCPStateV0.TxSets = new StoredTransactionSet[txSetssize];
        for (var i = 0; i < txSetssize; i++) decodedPersistedSCPStateV0.TxSets[i] = StoredTransactionSet.Decode(stream);
        return decodedPersistedSCPStateV0;
    }
}