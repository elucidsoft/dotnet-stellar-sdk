// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct SurveyRequestMessage
    //  {
    //      NodeID surveyorPeerID;
    //      NodeID surveyedPeerID;
    //      uint32 ledgerNum;
    //      Curve25519Public encryptionKey;
    //      SurveyMessageCommandType commandType;
    //  };

    //  ===========================================================================
    public class SurveyRequestMessage
    {
        public SurveyRequestMessage() { }
        public NodeID SurveyorPeerID { get; set; }
        public NodeID SurveyedPeerID { get; set; }
        public Uint32 LedgerNum { get; set; }
        public Curve25519Public EncryptionKey { get; set; }
        public SurveyMessageCommandType CommandType { get; set; }

        public static void Encode(XdrDataOutputStream stream, SurveyRequestMessage encodedSurveyRequestMessage)
        {
            NodeID.Encode(stream, encodedSurveyRequestMessage.SurveyorPeerID);
            NodeID.Encode(stream, encodedSurveyRequestMessage.SurveyedPeerID);
            Uint32.Encode(stream, encodedSurveyRequestMessage.LedgerNum);
            Curve25519Public.Encode(stream, encodedSurveyRequestMessage.EncryptionKey);
            SurveyMessageCommandType.Encode(stream, encodedSurveyRequestMessage.CommandType);
        }
        public static SurveyRequestMessage Decode(XdrDataInputStream stream)
        {
            SurveyRequestMessage decodedSurveyRequestMessage = new SurveyRequestMessage();
            decodedSurveyRequestMessage.SurveyorPeerID = NodeID.Decode(stream);
            decodedSurveyRequestMessage.SurveyedPeerID = NodeID.Decode(stream);
            decodedSurveyRequestMessage.LedgerNum = Uint32.Decode(stream);
            decodedSurveyRequestMessage.EncryptionKey = Curve25519Public.Decode(stream);
            decodedSurveyRequestMessage.CommandType = SurveyMessageCommandType.Decode(stream);
            return decodedSurveyRequestMessage;
        }
    }
}
