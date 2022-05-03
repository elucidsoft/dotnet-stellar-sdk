using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class SignedPayloadSignerTest
    {
        [TestMethod]
        public void ItFailsWhenAccoutIDIsNull()
        {
            try
            {
                new SignedPayloadSigner((xdrSDK.AccountID)null, new byte[] { });
                Assert.Fail("Test shouldn't be passing, signer AccountID cannot be null");
            }
            catch (ArgumentNullException e)
            {

            }
        }

        [TestMethod]
        public void ItFailsWhenPayloadLengthTooBig()
        {
            String accountStrKey = "GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ";
            byte[] payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f200102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f2001");
            try
            {
                new SignedPayloadSigner(StrKey.DecodeStellarAccountId(accountStrKey), payload);
                Assert.Fail("Test shouldn't be passing, payload length is over 64");
            }
            catch (ArgumentException e)
            {

            }
        }
    }
}
