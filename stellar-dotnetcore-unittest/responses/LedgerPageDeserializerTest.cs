using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class LedgerPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "ledgerPage.json"));
            var ledgersPage = JsonSingleton.GetInstance<Page<LedgerResponse>>(json);

            Assert.AreEqual(ledgersPage.Records[0].Hash, "f6fc9a29b5ecec90348e17a10a60e019c5cb8ea24f66a1063e92c13391b09b8d");
            Assert.AreEqual(ledgersPage.Records[0].PagingToken, "3862202096287744");
            Assert.AreEqual(ledgersPage.Records[9].Hash, "8552b7d130e602ed068bcfec729b3056d0c8b94d77f06d91cd1ec8323c2d6177");
            Assert.AreEqual(ledgersPage.Links.Next.Href, "/ledgers?order=desc&limit=10&cursor=3862163441582080");
            Assert.AreEqual(ledgersPage.Links.Prev.Href, "/ledgers?order=asc&limit=10&cursor=3862202096287744");
            Assert.AreEqual(ledgersPage.Links.Self.Href, "/ledgers?order=desc&limit=10&cursor=");
        }

    }
}
