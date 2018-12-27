using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class LinkTest
    {
        [TestMethod]
        public async Task TestLink()
        {
            var link = new Link<AccountResponse>(
                "https://horizon.stellar.org/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual("horizon.stellar.org", link.Uri.Host);
            Assert.AreEqual("/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7",
                string.Concat(link.Uri.Segments));

            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "account.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            var response = await link.Follow(fakeHttpClient);
            AccountDeserializerTest.AssertTestData(response);
        }

        [TestMethod]
        public void TestTemplatedLink()
        {
             var link = new TemplatedLink<OperationResponse>(
                 "https://horizon.stellar.org/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/operations{?cursor,limit,order}");
            Assert.AreEqual("horizon.stellar.org", link.Uri.Host);
            Assert.AreEqual("/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/operations",
                string.Concat(link.Uri.Segments));
        }

        [TestMethod]
        public void TestTemplatedLinkWithQueryParameters()
        {
             var link = new TemplatedLink<OperationResponse>(
                 "https://horizon.stellar.org/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/operations{?cursor,limit,order}");
             var uri = link.Resolve(new
             {
                 limit = 10,
                 order = OrderDirection.DESC,
                 cursor = "now"
             });

             var query = HttpUtility.ParseQueryString(uri.Query);
             Assert.AreEqual("10", query["limit"]);
             Assert.AreEqual("desc", query["order"]);
             Assert.AreEqual("now", query["cursor"]);
        }

        [TestMethod]
        public void TestTemplatedLinkWithVariable()
        {
            var link = new TemplatedLink<AccountDataResponse>(
                "https://horizon.stellar.org/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/data/{key}");

            var uri = link.Resolve(new
            {
                key = "foobar"
            });

            Assert.AreEqual("https://horizon.stellar.org/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/data/foobar",
                uri.ToString());
        }
    }
}