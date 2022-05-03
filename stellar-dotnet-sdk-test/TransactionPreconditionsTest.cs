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

        [TestMethod]
        public void ItConvertsToV2Xdr()
        {
            var payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f20");
            var signerKey = new xdrSDK.SignerKey();
            signerKey.Discriminant.InnerValue = xdrSDK.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_ED25519_SIGNED_PAYLOAD;
            signerKey.Ed25519SignedPayload = new xdrSDK.SignerKey.SignerKeyEd25519SignedPayload();
            signerKey.Ed25519SignedPayload.Ed25519 = new xdrSDK.Uint256(StrKey.DecodeStellarAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR"));
            signerKey.Ed25519SignedPayload.Payload = payload;

            TransactionPreconditions preconditions = new TransactionPreconditions();
            preconditions.TimeBounds = new TimeBounds(1, 2);
            preconditions.MinSeqNumber = 3;
            preconditions.ExtraSigners = new List<xdrSDK.SignerKey> { signerKey, signerKey, signerKey };
            
            var xdr = preconditions.ToXDR();

            var stream = new xdrSDK.XdrDataOutputStream();
            xdrSDK.Preconditions.Encode(stream, xdr);
            xdr = xdrSDK.Preconditions.Decode(new xdrSDK.XdrDataInputStream(stream.ToArray()));

            Assert.AreEqual(xdr.Discriminant.InnerValue, xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_V2);
            Assert.AreEqual(xdr.V2.TimeBounds.MinTime.InnerValue.InnerValue, 1UL);
            Assert.AreEqual(xdr.V2.TimeBounds.MaxTime.InnerValue.InnerValue, 2UL);
            Assert.AreEqual(xdr.V2.MinSeqNum.InnerValue.InnerValue, 3L);
            Assert.AreEqual(xdr.V2.MinSeqLedgerGap.InnerValue, 0U);
            Assert.AreEqual(xdr.V2.MinSeqAge.InnerValue.InnerValue, 0UL);
            Assert.AreEqual(xdr.V2.ExtraSigners.Length, 3);
        }

        [TestMethod]
        public void ItConvertsOnlyTimeBoundsXdr()
        {
            TransactionPreconditions preconditions = new TransactionPreconditions();
            preconditions.TimeBounds = new TimeBounds(1, 2);

            var xdr = preconditions.ToXDR();

            var stream = new xdrSDK.XdrDataOutputStream();
            xdrSDK.Preconditions.Encode(stream, xdr);
            xdr = xdrSDK.Preconditions.Decode(new xdrSDK.XdrDataInputStream(stream.ToArray()));

            Assert.AreEqual(xdr.Discriminant.InnerValue, xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_TIME);
            Assert.AreEqual(xdr.TimeBounds.MinTime.InnerValue.InnerValue, 1UL);
            Assert.AreEqual(xdr.TimeBounds.MaxTime.InnerValue.InnerValue, 2UL);
            Assert.IsNull(xdr.V2);
        }

        [TestMethod]
        public void ItConvertsNullTimeBoundsXDR()
        {
            TransactionPreconditions preconditions = new TransactionPreconditions();
            var xdr = preconditions.ToXDR();

            var stream = new xdrSDK.XdrDataOutputStream();
            xdrSDK.Preconditions.Encode(stream, xdr);
            xdr = xdrSDK.Preconditions.Decode(new xdrSDK.XdrDataInputStream(stream.ToArray()));

            Assert.AreEqual(xdr.Discriminant.InnerValue, xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_NONE);
            Assert.IsNull(xdr.TimeBounds);
        }

        [TestMethod]
        public void ItChecksValidityWhenTimebounds()
        {
            TransactionPreconditions preconditions = new TransactionPreconditions();
            preconditions.TimeBounds = new TimeBounds(1, 2);
            preconditions.IsValid();
        }

        [TestMethod]
        public void ItChecksNonValidityOfTimeBounds()
        {
            var preconditions = new TransactionPreconditions();
            try
            {
                preconditions.IsValid();
                Assert.Fail();
            }
            catch (Exception e) 
            { 
            }
        }

        [TestMethod]
        public void ItChecksNonValidityOfExtraSignersSize()
        {
            var preconditions = new TransactionPreconditions();
            preconditions.TimeBounds = new TimeBounds(1, 2);
            preconditions.ExtraSigners = new List<xdrSDK.SignerKey>() { new xdrSDK.SignerKey(), new xdrSDK.SignerKey(), new xdrSDK.SignerKey() };
            try
            {
                preconditions.IsValid();
                Assert.Fail();
            }
            catch (Exception e) 
            { 
            }
        }

        [TestMethod]
        public void ItChecksValidityWhenNoTimeboundsSet()
        {
            TransactionPreconditions preconditions = new TransactionPreconditions();
            try
            {
                preconditions.IsValid();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Invalid preconditions, must define timebounds");
            }
        }

        [TestMethod]
        public void ItChecksV2Status()
        {
            var preconditions = new xdrSDK.Preconditions();
            preconditions.Discriminant.InnerValue = xdrSDK.PreconditionType.PreconditionTypeEnum.PRECOND_V2;
            var preconditionsV2 = new xdrSDK.PreconditionsV2();

            preconditionsV2.ExtraSigners = new xdrSDK.SignerKey[0] { };
            preconditionsV2.MinSeqAge = new xdrSDK.Duration(new xdrSDK.Uint64(2L));
            preconditionsV2.LedgerBounds = new xdrSDK.LedgerBounds();
            preconditionsV2.LedgerBounds.MinLedger = new xdrSDK.Uint32(1);
            preconditionsV2.LedgerBounds.MaxLedger = new xdrSDK.Uint32(2);
            preconditionsV2.MinSeqNum = new xdrSDK.SequenceNumber(new xdrSDK.Int64(4L));

            preconditions.V2 = preconditionsV2;

            TransactionPreconditions transactionPreconditions = TransactionPreconditions.FromXDR(preconditions);
            Assert.IsTrue(transactionPreconditions.HasV2());
        }
    }
}
