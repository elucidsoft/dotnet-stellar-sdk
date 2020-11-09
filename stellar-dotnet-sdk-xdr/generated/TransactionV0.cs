// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================
//  struct TransactionV0
//  {
//      uint256 sourceAccountEd25519;
//      uint32 fee;
//      SequenceNumber seqNum;
//      TimeBounds* timeBounds;
//      Memo memo;
//      Operation operations<MAX_OPS_PER_TX>;
//      union switch (int v)
//      {
//      case 0:
//          void;
//      }
//      ext;
//  };
//  ===========================================================================
    public class TransactionV0
    {
        public TransactionV0()
        {
        }

        public Uint256 SourceAccountEd25519 { get; set; }
        public Uint32 Fee { get; set; }
        public SequenceNumber SeqNum { get; set; }
        public TimeBounds TimeBounds { get; set; }
        public Memo Memo { get; set; }
        public Operation[] Operations { get; set; }
        public TransactionV0Ext Ext { get; set; }

        public static void Encode(XdrDataOutputStream stream, TransactionV0 encodedTransactionV0)
        {
            Uint256.Encode(stream, encodedTransactionV0.SourceAccountEd25519);
            Uint32.Encode(stream, encodedTransactionV0.Fee);
            SequenceNumber.Encode(stream, encodedTransactionV0.SeqNum);
            if (encodedTransactionV0.TimeBounds != null)
            {
                stream.WriteInt(1);
                TimeBounds.Encode(stream, encodedTransactionV0.TimeBounds);
            }
            else
            {
                stream.WriteInt(0);
            }

            Memo.Encode(stream, encodedTransactionV0.Memo);
            int operationssize = encodedTransactionV0.Operations.Length;
            stream.WriteInt(operationssize);
            for (int i = 0; i < operationssize; i++)
            {
                Operation.Encode(stream, encodedTransactionV0.Operations[i]);
            }

            TransactionV0Ext.Encode(stream, encodedTransactionV0.Ext);
        }

        public static TransactionV0 Decode(XdrDataInputStream stream)
        {
            TransactionV0 decodedTransactionV0 = new TransactionV0();
            decodedTransactionV0.SourceAccountEd25519 = Uint256.Decode(stream);
            decodedTransactionV0.Fee = Uint32.Decode(stream);
            decodedTransactionV0.SeqNum = SequenceNumber.Decode(stream);
            int TimeBoundsPresent = stream.ReadInt();
            if (TimeBoundsPresent != 0)
            {
                decodedTransactionV0.TimeBounds = TimeBounds.Decode(stream);
            }

            decodedTransactionV0.Memo = Memo.Decode(stream);
            int operationssize = stream.ReadInt();
            decodedTransactionV0.Operations = new Operation[operationssize];
            for (int i = 0; i < operationssize; i++)
            {
                decodedTransactionV0.Operations[i] = Operation.Decode(stream);
            }

            decodedTransactionV0.Ext = TransactionV0Ext.Decode(stream);
            return decodedTransactionV0;
        }

        public class TransactionV0Ext
        {
            public TransactionV0Ext()
            {
            }

            public int Discriminant { get; set; } = new int();

            public static void Encode(XdrDataOutputStream stream, TransactionV0Ext encodedTransactionV0Ext)
            {
                stream.WriteInt((int) encodedTransactionV0Ext.Discriminant);
                switch (encodedTransactionV0Ext.Discriminant)
                {
                    case 0:
                        break;
                }
            }

            public static TransactionV0Ext Decode(XdrDataInputStream stream)
            {
                TransactionV0Ext decodedTransactionV0Ext = new TransactionV0Ext();
                int discriminant = stream.ReadInt();
                decodedTransactionV0Ext.Discriminant = discriminant;
                switch (decodedTransactionV0Ext.Discriminant)
                {
                    case 0:
                        break;
                }

                return decodedTransactionV0Ext;
            }
        }
    }
}