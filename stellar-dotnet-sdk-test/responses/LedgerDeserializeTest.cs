using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class LedgerDeserializeTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "ledger.json"));
            var ledger = JsonSingleton.GetInstance<LedgerResponse>(json);

            AssertTestData(ledger);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "ledger.json"));
            var ledger = JsonSingleton.GetInstance<LedgerResponse>(json);
            var serialized = JsonConvert.SerializeObject(ledger);
            var back = JsonConvert.DeserializeObject<LedgerResponse>(serialized);

            AssertTestData(back);
        }

        [TestMethod]
        public void TestSerializeDeserializeNullFailedTransactionCount()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "ledgerNullFailedTransactionCount.json"));
            var ledger = JsonSingleton.GetInstance<LedgerResponse>(json);
            var serialized = JsonConvert.SerializeObject(ledger);
            var back = JsonConvert.DeserializeObject<LedgerResponse>(serialized);

            Assert.AreEqual(ledger.Hash, "7f7cc428fa2b5f17fea0dba3bdbd36972f3dff4fae9345cc1f013b1133bbf7c4");
            Assert.AreEqual(ledger.PagingToken, "2147483648000");
            Assert.AreEqual(ledger.PrevHash, "29a54d2641d0051e4748d1ed1c9e53bd3634b2aaa823fb709341b93328c6d313");
            Assert.AreEqual(ledger.Sequence, 500);
            Assert.AreEqual(ledger.SuccessfulTransactionCount, 0);
            Assert.AreEqual(ledger.FailedTransactionCount, null);
            Assert.AreEqual(ledger.OperationCount, 0);
        }

        public static void AssertTestData(LedgerResponse ledger)
        {
            Assert.AreEqual(ledger.Hash, "686bb246db89b099cd3963a4633eb5e4315d89dfd3c00594c80b41a483847bfa");
            Assert.AreEqual(ledger.PagingToken, "3860428274794496");
            Assert.AreEqual(ledger.PrevHash, "50c8695eb32171a19858413e397cc50b504ceacc819010bdf8ff873aff7858d7");
            Assert.AreEqual(ledger.Sequence, 898826);
            Assert.AreEqual(ledger.SuccessfulTransactionCount, 3);
            Assert.AreEqual(ledger.FailedTransactionCount, 2);
            Assert.AreEqual(ledger.OperationCount, 10);
            Assert.AreEqual(ledger.ClosedAt, "2015-11-19T21:35:59Z");
            Assert.AreEqual(ledger.TotalCoins, "101343867604.8975480");
            Assert.AreEqual(ledger.FeePool, "1908.2248818");
            Assert.AreEqual(ledger.BaseFee, 100);
            Assert.AreEqual(ledger.BaseReserve, "10.0000000");
            Assert.AreEqual(ledger.MaxTxSetSize, 50);
            Assert.AreEqual(ledger.Links.Effects.Href, "/ledgers/898826/effects{?cursor,limit,order}");
            Assert.AreEqual(ledger.Links.Operations.Href, "/ledgers/898826/operations{?cursor,limit,order}");
            Assert.AreEqual(ledger.Links.Self.Href, "/ledgers/898826");
            Assert.AreEqual(ledger.Links.Transactions.Href, "/ledgers/898826/transactions{?cursor,limit,order}");
        }
    }
}