using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class PathsRequestBuilderTest
    {
        [TestMethod]
        public void TestAccounts()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Paths
                    .DestinationAccount(KeyPair.FromAccountId("GB24QI3BJNKBY4YNJZ2I37HFIYK56BL2OURFML76X46RQQKDLVT7WKJF"))
                    .SourceAccount(KeyPair.FromAccountId("GD4KO3IOYYWIYVI236Y35K2DU6VNYRH3BPNFJSH57J5BLLCQHBIOK3IN"))
                    .DestinationAmount("20.50")
                    .DestinationAsset(Asset.CreateNonNativeAsset("USD", KeyPair.FromAccountId("GAYSHLG75RPSMXWJ5KX7O7STE6RSZTD6NE4CTWAXFZYYVYIFRUVJIBJH")))
                    .Cursor("13537736921089")
                    .Limit(200)
                    .Order(OrderDirection.ASC)
                    .BuildUri();


                Assert.AreEqual("https://horizon-testnet.stellar.org/paths?" +
                                "destination_account=GB24QI3BJNKBY4YNJZ2I37HFIYK56BL2OURFML76X46RQQKDLVT7WKJF&" +
                                "source_account=GD4KO3IOYYWIYVI236Y35K2DU6VNYRH3BPNFJSH57J5BLLCQHBIOK3IN&" +
                                "destination_amount=20.50&" +
                                "destination_asset_type=credit_alphanum4&" +
                                "destination_asset_code=USD&" +
                                "destination_asset_issuer=GAYSHLG75RPSMXWJ5KX7O7STE6RSZTD6NE4CTWAXFZYYVYIFRUVJIBJH&" +
                                "cursor=13537736921089&" +
                                "limit=200&" +
                                "order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestPathsExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "pathsPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var account = await server.Paths
                    .SourceAccount(KeyPair.FromAccountId("GD4KO3IOYYWIYVI236Y35K2DU6VNYRH3BPNFJSH57J5BLLCQHBIOK3IN"))
                    .DestinationAccount(KeyPair.FromAccountId("GB24QI3BJNKBY4YNJZ2I37HFIYK56BL2OURFML76X46RQQKDLVT7WKJF"))
                    .DestinationAmount("20")
                    .Execute();

                PathsPageDeserializerTest.AssertTestData(account);
            }
        }
    }
}