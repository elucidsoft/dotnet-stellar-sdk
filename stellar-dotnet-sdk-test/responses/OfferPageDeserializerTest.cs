using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OfferPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "offerPage.json"));
            var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);

            AssertTestData(offerResponsePage);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "offerPage.json"));
            var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);
            var serialized = JsonConvert.SerializeObject(offerResponsePage);
            var back = JsonConvert.DeserializeObject<Page<OfferResponse>>(serialized);

            AssertTestData(back);
        }

        public static void AssertTestData(Page<OfferResponse> offerResponsePage)
        {
            Assert.AreEqual(offerResponsePage.Records[0].Id, 241);
            Assert.AreEqual(offerResponsePage.Records[0].Seller.AccountId,
                "GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD");
            Assert.AreEqual(offerResponsePage.Records[0].PagingToken, "241");
            Assert.AreEqual(offerResponsePage.Records[0].Selling,
                Asset.CreateNonNativeAsset("INR",
                    KeyPair.FromAccountId("GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD")));
            Assert.AreEqual(offerResponsePage.Records[0].Buying,
                Asset.CreateNonNativeAsset("USD",
                    KeyPair.FromAccountId("GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD")));
            Assert.AreEqual(offerResponsePage.Records[0].Amount, "10.0000000");
            Assert.AreEqual(offerResponsePage.Records[0].Price, "11.0000000");

            Assert.AreEqual(offerResponsePage.Links.Next.Href,
                "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=asc&limit=10&cursor=241");
            Assert.AreEqual(offerResponsePage.Links.Prev.Href,
                "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=desc&limit=10&cursor=241");
            Assert.AreEqual(offerResponsePage.Links.Self.Href,
                "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=asc&limit=10&cursor=");
        }
    }
}