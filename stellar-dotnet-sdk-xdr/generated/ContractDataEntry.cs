// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct ContractDataEntry {
    //      Hash contractID;
    //      SCVal key;
    //      SCVal val;
    //  };

    //  ===========================================================================
    public class ContractDataEntry
    {
        public ContractDataEntry() { }
        public Hash ContractID { get; set; }
        public SCVal Key { get; set; }
        public SCVal Val { get; set; }

        public static void Encode(XdrDataOutputStream stream, ContractDataEntry encodedContractDataEntry)
        {
            Hash.Encode(stream, encodedContractDataEntry.ContractID);
            SCVal.Encode(stream, encodedContractDataEntry.Key);
            SCVal.Encode(stream, encodedContractDataEntry.Val);
        }
        public static ContractDataEntry Decode(XdrDataInputStream stream)
        {
            ContractDataEntry decodedContractDataEntry = new ContractDataEntry();
            decodedContractDataEntry.ContractID = Hash.Decode(stream);
            decodedContractDataEntry.Key = SCVal.Decode(stream);
            decodedContractDataEntry.Val = SCVal.Decode(stream);
            return decodedContractDataEntry;
        }
    }
}
