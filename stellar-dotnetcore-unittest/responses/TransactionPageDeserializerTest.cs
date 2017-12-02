using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System.IO;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class TransactionPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "transactionPageDeserialize.json"));
            var transactionsPage = JsonSingleton.GetInstance<Page<TransactionResponse>>(json);

            Assert.AreEqual(transactionsPage.Records[0].SourceAccount.AccountId, "GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual(transactionsPage.Records[0].PagingToken, "12884905984");
            Assert.AreEqual(transactionsPage.Records[0].Links.Account.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual(transactionsPage.Records[9].SourceAccount.AccountId, "GAENIE5LBJIXLMJIAJ7225IUPA6CX7EGHUXRX5FLCZFFAQSG2ZUYSWFK");

            Assert.AreEqual(transactionsPage.Links.Next.Href, "/transactions?order=asc&limit=10&cursor=81058917781504");
            Assert.AreEqual(transactionsPage.Links.Prev.Href, "/transactions?order=desc&limit=10&cursor=12884905984");
            Assert.AreEqual(transactionsPage.Links.Self.Href, "/transactions?order=asc&limit=10&cursor=");
        }
    }
}
