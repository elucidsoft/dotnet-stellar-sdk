using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using System.IO;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class RootDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "root.json"));
            var root = JsonSingleton.GetInstance<RootResponse>(json);

            AssertTestData(root);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "root.json"));
            var root = JsonSingleton.GetInstance<RootResponse>(json);
            var serialized = JsonConvert.SerializeObject(root);
            var back = JsonConvert.DeserializeObject<RootResponse>(serialized);

            AssertTestData(back);
        }


        private static void AssertTestData(RootResponse root)
        {
            Assert.AreEqual(root.HorizonVersion, "snapshot-c5fe0ff");
            Assert.AreEqual(root.StellarCoreVersion, "stellar-core 9.2.0 (7561c1d53366ec79b908de533726269e08474f77)");
            Assert.AreEqual(root.HistoryLatestLedger, 18369116);
            Assert.AreEqual(root.HistoryElderLedger, 1);
            Assert.AreEqual(root.CoreLatestLedger, 18369117);
            Assert.AreEqual(root.NetworkPassphrase, "Public Global Stellar Network ; September 2015");
            Assert.AreEqual(root.ProtocolVersion, 9);
            Assert.AreEqual(root.CurrentProtocolVersion, 10);
            Assert.AreEqual(root.CoreSupportedProtocolVersion, 11);
        }
    }
}