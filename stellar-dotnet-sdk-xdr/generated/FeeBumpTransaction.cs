// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct FeeBumpTransaction
    //  {
    //      MuxedAccount feeSource;
    //      int64 fee;
    //      union switch (EnvelopeType type)
    //      {
    //      case ENVELOPE_TYPE_TX:
    //          TransactionV1Envelope v1;
    //      }
    //      innerTx;
    //      union switch (int v)
    //      {
    //      case 0:
    //          void;
    //      }
    //      ext;
    //  };

    //  ===========================================================================
    public class FeeBumpTransaction
    {
        public FeeBumpTransaction() { }
        public MuxedAccount FeeSource { get; set; }
        public Int64 Fee { get; set; }
        public FeeBumpTransactionInnerTx InnerTx { get; set; }
        public FeeBumpTransactionExt Ext { get; set; }

        public static void Encode(XdrDataOutputStream stream, FeeBumpTransaction encodedFeeBumpTransaction)
        {
            MuxedAccount.Encode(stream, encodedFeeBumpTransaction.FeeSource);
            Int64.Encode(stream, encodedFeeBumpTransaction.Fee);
            FeeBumpTransactionInnerTx.Encode(stream, encodedFeeBumpTransaction.InnerTx);
            FeeBumpTransactionExt.Encode(stream, encodedFeeBumpTransaction.Ext);
        }
        public static FeeBumpTransaction Decode(XdrDataInputStream stream)
        {
            FeeBumpTransaction decodedFeeBumpTransaction = new FeeBumpTransaction();
            decodedFeeBumpTransaction.FeeSource = MuxedAccount.Decode(stream);
            decodedFeeBumpTransaction.Fee = Int64.Decode(stream);
            decodedFeeBumpTransaction.InnerTx = FeeBumpTransactionInnerTx.Decode(stream);
            decodedFeeBumpTransaction.Ext = FeeBumpTransactionExt.Decode(stream);
            return decodedFeeBumpTransaction;
        }

        public class FeeBumpTransactionInnerTx
        {
            public FeeBumpTransactionInnerTx() { }

            public EnvelopeType Discriminant { get; set; } = new EnvelopeType();

            public TransactionV1Envelope V1 { get; set; }
            public static void Encode(XdrDataOutputStream stream, FeeBumpTransactionInnerTx encodedFeeBumpTransactionInnerTx)
            {
                stream.WriteInt((int)encodedFeeBumpTransactionInnerTx.Discriminant.InnerValue);
                switch (encodedFeeBumpTransactionInnerTx.Discriminant.InnerValue)
                {
                    case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX:
                        TransactionV1Envelope.Encode(stream, encodedFeeBumpTransactionInnerTx.V1);
                        break;
                }
            }
            public static FeeBumpTransactionInnerTx Decode(XdrDataInputStream stream)
            {
                FeeBumpTransactionInnerTx decodedFeeBumpTransactionInnerTx = new FeeBumpTransactionInnerTx();
                EnvelopeType discriminant = EnvelopeType.Decode(stream);
                decodedFeeBumpTransactionInnerTx.Discriminant = discriminant;
                switch (decodedFeeBumpTransactionInnerTx.Discriminant.InnerValue)
                {
                    case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX:
                        decodedFeeBumpTransactionInnerTx.V1 = TransactionV1Envelope.Decode(stream);
                        break;
                }
                return decodedFeeBumpTransactionInnerTx;
            }

        }
        public class FeeBumpTransactionExt
        {
            public FeeBumpTransactionExt() { }

            public int Discriminant { get; set; } = new int();

            public static void Encode(XdrDataOutputStream stream, FeeBumpTransactionExt encodedFeeBumpTransactionExt)
            {
                stream.WriteInt((int)encodedFeeBumpTransactionExt.Discriminant);
                switch (encodedFeeBumpTransactionExt.Discriminant)
                {
                    case 0:
                        break;
                }
            }
            public static FeeBumpTransactionExt Decode(XdrDataInputStream stream)
            {
                FeeBumpTransactionExt decodedFeeBumpTransactionExt = new FeeBumpTransactionExt();
                int discriminant = stream.ReadInt();
                decodedFeeBumpTransactionExt.Discriminant = discriminant;
                switch (decodedFeeBumpTransactionExt.Discriminant)
                {
                    case 0:
                        break;
                }
                return decodedFeeBumpTransactionExt;
            }

        }
    }
}
