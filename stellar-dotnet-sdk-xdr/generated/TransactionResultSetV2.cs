// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct TransactionResultSetV2
    //  {
    //      TransactionResultPairV2 results<>;
    //  };

    //  ===========================================================================
    public class TransactionResultSetV2
    {
        public TransactionResultSetV2() { }
        public TransactionResultPairV2[] Results { get; set; }

        public static void Encode(XdrDataOutputStream stream, TransactionResultSetV2 encodedTransactionResultSetV2)
        {
            int resultssize = encodedTransactionResultSetV2.Results.Length;
            stream.WriteInt(resultssize);
            for (int i = 0; i < resultssize; i++)
            {
                TransactionResultPairV2.Encode(stream, encodedTransactionResultSetV2.Results[i]);
            }
        }
        public static TransactionResultSetV2 Decode(XdrDataInputStream stream)
        {
            TransactionResultSetV2 decodedTransactionResultSetV2 = new TransactionResultSetV2();
            int resultssize = stream.ReadInt();
            decodedTransactionResultSetV2.Results = new TransactionResultPairV2[resultssize];
            for (int i = 0; i < resultssize; i++)
            {
                decodedTransactionResultSetV2.Results[i] = TransactionResultPairV2.Decode(stream);
            }
            return decodedTransactionResultSetV2;
        }
    }
}
