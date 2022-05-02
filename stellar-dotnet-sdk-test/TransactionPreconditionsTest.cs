using Microsoft.VisualStudio.TestTools.UnitTesting;
using xdrSDK = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class TransactionPreconditionsTest
    {
        [TestMethod]
        public void ItConvertsFromXdr()
        {
            var preconditions = new xdrSDK.Preconditions();
            preconditions.Discriminant.InnerValue = xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_V2;

            var preconditionsV2 = new xdrSDK.PreconditionsV2();
            preconditionsV2.ExtraSigners = new xdrSDK.SignerKey[0];
            preconditionsV2.MinSeqAge = new xdrSDK.Duration(new xdrSDK.Uint64(2L));
            preconditionsV2.LedgerBounds = new xdrSDK.LedgerBounds();
            preconditionsV2.LedgerBounds.MinLedger = new xdrSDK.Uint32(1);
            preconditionsV2.LedgerBounds.MaxLedger = new xdrSDK.Uint32(2);
            preconditionsV2.MinSeqNum = new xdrSDK.SequenceNumber(new xdrSDK.Int64(4L));
            preconditionsV2.MinSeqLedgerGap = new xdrSDK.Uint32(0);
            preconditions.V2 = preconditionsV2;

            var stream = new xdrSDK.XdrDataOutputStream();
            xdrSDK.Preconditions.Encode(stream, preconditions);
            preconditions = xdrSDK.Preconditions.Decode(new xdrSDK.XdrDataInputStream(stream.ToArray()));

            var transactionPreconditions = TransactionPreconditions.FromXDR(preconditions);

            Assert.AreEqual(transactionPreconditions.MinSeqAge, 2UL);
            Assert.AreEqual(transactionPreconditions.LedgerBounds.MinLedger, 1U);
            Assert.AreEqual(transactionPreconditions.LedgerBounds.MaxLedger, 2U);
            Assert.AreEqual(transactionPreconditions.MinSeqNumber, 4L);
            Assert.AreEqual(transactionPreconditions.MinSeqLedgerGap, 0U);
        }

        [TestMethod]
        public void ItRoundTripsFromV2ToV1IfOnlyTimeboundsPresent()
        {
            var preconditions = new xdrSDK.Preconditions();
            preconditions.Discriminant.InnerValue = xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_V2;

            var preconditionsV2 = new xdrSDK.PreconditionsV2();

            var xdrTimeBounds = new xdrSDK.TimeBounds();
            xdrTimeBounds.MinTime = new xdrSDK.TimePoint(new xdrSDK.Uint64(1L));
            xdrTimeBounds.MaxTime = new xdrSDK.TimePoint(new xdrSDK.Uint64(2L));
            preconditionsV2.TimeBounds = xdrTimeBounds;
            preconditionsV2.MinSeqLedgerGap = new xdrSDK.Uint32(0);
            preconditionsV2.MinSeqAge = new xdrSDK.Duration(new xdrSDK.Uint64(0L));
            preconditionsV2.ExtraSigners = new xdrSDK.SignerKey[0];
            preconditions.V2 = preconditionsV2;

            var stream = new xdrSDK.XdrDataOutputStream();
            xdrSDK.Preconditions.Encode(stream, preconditions);
            preconditions = xdrSDK.Preconditions.Decode(new xdrSDK.XdrDataInputStream(stream.ToArray()));

            TransactionPreconditions transactionPreconditions = TransactionPreconditions.FromXDR(preconditions);
            Assert.AreEqual(transactionPreconditions.TimeBounds, new TimeBounds(1L, 2L));

            preconditions = transactionPreconditions.ToXDR();
            
            Assert.AreEqual(preconditions.Discriminant.InnerValue, xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_TIME);
            Assert.AreEqual(preconditions.TimeBounds.MinTime.InnerValue.InnerValue, xdrTimeBounds.MinTime.InnerValue.InnerValue);
            Assert.AreEqual(preconditions.TimeBounds.MaxTime.InnerValue.InnerValue, xdrTimeBounds.MaxTime.InnerValue.InnerValue);
            Assert.IsNull(preconditions.V2);
        }
    }
}
