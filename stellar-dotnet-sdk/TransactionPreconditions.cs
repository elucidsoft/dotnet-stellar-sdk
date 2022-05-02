using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class TransactionPreconditions
    {
        public static long MAX_EXTRA_SIGNERS_COUNT = 2;
        public static ulong TIMEOUT_INFINITE = 0;

        public LedgerBounds LedgerBounds { get; set; }
        public ulong MinSeqAge { get; set; }
        public long? MinSeqNumber { get; set; }
        public uint MinSeqLedgerGap { get; set; }
        public List<xdr.SignerKey> ExtraSigners { get; set; }
        public TimeBounds TimeBounds { get; set; }

        public void IsValid()
        {
            if (TimeBounds == null)
            {
                throw new FormatException("Invalid preconditions, must define timebounds");
            }

            if (ExtraSigners?.Count > MAX_EXTRA_SIGNERS_COUNT)
            {
                throw new FormatException("Invalid preconditions, too many extra signers, can only have up to " + MAX_EXTRA_SIGNERS_COUNT);
            }
        }

        public bool HasV2()
        {
            return (LedgerBounds != null ||
                    (MinSeqLedgerGap > 0) ||
                    (MinSeqAge > 0) ||
                    MinSeqNumber != null ||
                    ExtraSigners != null && ExtraSigners.Count != 0);
        }

        public static TransactionPreconditions FromXDR(xdr.Preconditions preconditions)
        {
            TransactionPreconditions transactionPreconditions = new TransactionPreconditions();

            if (preconditions.Discriminant.InnerValue == xdr.PreconditionType.PreconditionTypeEnum.PRECOND_V2)
            {
                //Time Bounds
                if (preconditions.V2.TimeBounds != null)
                {
                    transactionPreconditions.TimeBounds = new TimeBounds(preconditions.V2.TimeBounds.MinTime.InnerValue.InnerValue, preconditions.V2.TimeBounds.MaxTime.InnerValue.InnerValue);
                }

                //Extra Signers
                if (preconditions.V2.ExtraSigners != null && preconditions.V2.ExtraSigners.Length > 0)
                {
                    transactionPreconditions.ExtraSigners = preconditions.V2.ExtraSigners.ToList();
                }

                //Min Seq Age
                if (preconditions.V2.MinSeqAge != null)
                {
                    transactionPreconditions.MinSeqAge = preconditions.V2.MinSeqAge.InnerValue.InnerValue;
                }

                //Ledger Bounds
                if (preconditions.V2.LedgerBounds != null)
                {
                    transactionPreconditions.LedgerBounds = LedgerBounds.FromXdr(preconditions.V2.LedgerBounds);
                }

                //Min Seq Num
                if (preconditions.V2.MinSeqNum != null)
                {
                    transactionPreconditions.MinSeqNumber = preconditions.V2.MinSeqNum.InnerValue.InnerValue;
                }

                //Min Seq Ledger Gap
                if (preconditions.V2.MinSeqLedgerGap != null)
                {
                    transactionPreconditions.MinSeqLedgerGap = preconditions.V2.MinSeqLedgerGap.InnerValue;
                }
            }
            else
            {
                if (preconditions.TimeBounds != null)
                {
                    transactionPreconditions.TimeBounds = new TimeBounds(preconditions.TimeBounds.MinTime.InnerValue.InnerValue, preconditions.TimeBounds.MaxTime.InnerValue.InnerValue);
                }
            }

            return transactionPreconditions;
        }

        public xdr.Preconditions ToXDR()
        {
            var preconditions = new xdr.Preconditions();

            if (HasV2())
            {
                preconditions.Discriminant.InnerValue = xdr.PreconditionType.PreconditionTypeEnum.PRECOND_V2;

                var preconditionsV2 = new xdr.PreconditionsV2();
                preconditions.V2 = preconditionsV2;

                preconditionsV2.ExtraSigners = new xdr.SignerKey[0];
                preconditionsV2.MinSeqAge = new xdr.Duration(new xdr.Uint64(MinSeqAge));

                if (LedgerBounds != null)
                {
                    var ledgerBoundsXDR = new xdr.LedgerBounds();
                    ledgerBoundsXDR.MinLedger = new xdr.Uint32(LedgerBounds.MinLedger);
                    ledgerBoundsXDR.MaxLedger = new xdr.Uint32(LedgerBounds.MaxLedger);

                    preconditionsV2.LedgerBounds = ledgerBoundsXDR;
                }

                if (MinSeqNumber != null)
                {
                    preconditionsV2.MinSeqNum = new xdr.SequenceNumber(new xdr.Int64(MinSeqNumber.Value));
                }

                preconditionsV2.MinSeqLedgerGap = new xdr.Uint32(MinSeqLedgerGap);

                if (TimeBounds != null)
                {
                    preconditionsV2.TimeBounds = TimeBounds.ToXdr();
                }
            }
            else
            {
                if (TimeBounds == null)
                {
                    preconditions.Discriminant.InnerValue = xdr.PreconditionType.PreconditionTypeEnum.PRECOND_NONE;
                }
                else
                {
                    preconditions.Discriminant.InnerValue = xdr.PreconditionType.PreconditionTypeEnum.PRECOND_TIME;
                    preconditions.TimeBounds = TimeBounds.ToXdr();
                }
            }

            return preconditions;
        }
    }
}