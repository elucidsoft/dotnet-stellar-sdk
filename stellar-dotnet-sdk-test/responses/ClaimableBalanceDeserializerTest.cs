using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class ClaimableBalanceDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "claimableBalance.json"));
            var claimableBalance = JsonSingleton.GetInstance<ClaimableBalanceResponse>(json);
            AssertTestData(claimableBalance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "claimableBalance.json"));
            var claimableBalance = JsonConvert.DeserializeObject<ClaimableBalanceResponse>(json);

            var serialized = JsonConvert.SerializeObject(claimableBalance);
            var back = JsonConvert.DeserializeObject<ClaimableBalanceResponse>(serialized);

            Assert.AreEqual(claimableBalance.LastModifiedLedger, back.LastModifiedLedger);
            Assert.AreEqual(claimableBalance.Asset, back.Asset);
            Assert.AreEqual(claimableBalance.Sponsor, back.Sponsor);
        }

        public static void AssertTestData(ClaimableBalanceResponse claimableBalance)
        {
            Assert.AreEqual("00000000c582697b67cbec7f9ce64f4dc67bfb2bfd26318bb9f964f4d70e3f41f650b1e6", claimableBalance.Id);
            Assert.AreEqual("native", claimableBalance.Asset);
            Assert.AreEqual("GB5N4275ETC6A77K4DTDL3EFAQMN66PC7UITDUZUBM7Y6LDJP7EYSGOB", claimableBalance.Sponsor);
            Assert.AreEqual(66835, claimableBalance.LastModifiedLedger);
            Assert.AreEqual("66835-00000000c582697b67cbec7f9ce64f4dc67bfb2bfd26318bb9f964f4d70e3f41f650b1e6", claimableBalance.PagingToken);
            
            Assert.AreEqual(1, claimableBalance.Claimants.Length);
            var claimant = claimableBalance.Claimants[0];
            Assert.AreEqual("GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", claimant.Destination);
            Assert.AreEqual(true, claimant.Predicate.Unconditional);
        }
    }
}