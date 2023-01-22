// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

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
        public PersistedSCPStateV0() { }
        public SCPEnvelope[] ScpEnvelopes { get; set; }
        public SCPQuorumSet[] QuorumSets { get; set; }
        public StoredTransactionSet[] TxSets { get; set; }

        public static void Encode(XdrDataOutputStream stream, PersistedSCPStateV0 encodedPersistedSCPStateV0)
        {
            int scpEnvelopessize = encodedPersistedSCPStateV0.ScpEnvelopes.Length;
            stream.WriteInt(scpEnvelopessize);
            for (int i = 0; i < scpEnvelopessize; i++)
            {
                SCPEnvelope.Encode(stream, encodedPersistedSCPStateV0.ScpEnvelopes[i]);
            }
            int quorumSetssize = encodedPersistedSCPStateV0.QuorumSets.Length;
            stream.WriteInt(quorumSetssize);
            for (int i = 0; i < quorumSetssize; i++)
            {
                SCPQuorumSet.Encode(stream, encodedPersistedSCPStateV0.QuorumSets[i]);
            }
            int txSetssize = encodedPersistedSCPStateV0.TxSets.Length;
            stream.WriteInt(txSetssize);
            for (int i = 0; i < txSetssize; i++)
            {
                StoredTransactionSet.Encode(stream, encodedPersistedSCPStateV0.TxSets[i]);
            }
        }
        public static PersistedSCPStateV0 Decode(XdrDataInputStream stream)
        {
            PersistedSCPStateV0 decodedPersistedSCPStateV0 = new PersistedSCPStateV0();
            int scpEnvelopessize = stream.ReadInt();
            decodedPersistedSCPStateV0.ScpEnvelopes = new SCPEnvelope[scpEnvelopessize];
            for (int i = 0; i < scpEnvelopessize; i++)
            {
                decodedPersistedSCPStateV0.ScpEnvelopes[i] = SCPEnvelope.Decode(stream);
            }
            int quorumSetssize = stream.ReadInt();
            decodedPersistedSCPStateV0.QuorumSets = new SCPQuorumSet[quorumSetssize];
            for (int i = 0; i < quorumSetssize; i++)
            {
                decodedPersistedSCPStateV0.QuorumSets[i] = SCPQuorumSet.Decode(stream);
            }
            int txSetssize = stream.ReadInt();
            decodedPersistedSCPStateV0.TxSets = new StoredTransactionSet[txSetssize];
            for (int i = 0; i < txSetssize; i++)
            {
                decodedPersistedSCPStateV0.TxSets[i] = StoredTransactionSet.Decode(stream);
            }
            return decodedPersistedSCPStateV0;
        }
    }
}
