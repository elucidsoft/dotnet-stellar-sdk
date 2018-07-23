using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(ledger.Hash, "686bb246db89b099cd3963a4633eb5e4315d89dfd3c00594c80b41a483847bfa");
            Assert.AreEqual(ledger.PagingToken, "3860428274794496");
            Assert.AreEqual(ledger.PrevHash, "50c8695eb32171a19858413e397cc50b504ceacc819010bdf8ff873aff7858d7");
            Assert.AreEqual(ledger.Sequence, 898826);
            Assert.AreEqual(ledger.TransactionCount, 1);
            Assert.AreEqual(ledger.OperationCount, 2);
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
