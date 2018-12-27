using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class PathsPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "pathsPage.json"));
            var pathsPage = JsonSingleton.GetInstance<Page<PathResponse>>(json);

            AssertTestData(pathsPage);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "pathsPage.json"));
            var pathsPage = JsonSingleton.GetInstance<Page<PathResponse>>(json);
            var serialized = JsonConvert.SerializeObject(pathsPage);
            var back = JsonConvert.DeserializeObject<Page<PathResponse>>(serialized);

            AssertTestData(back);
        }

        public static void AssertTestData(Page<PathResponse> pathsPage)
        {
            Assert.IsNull(pathsPage.NextPage());
            Assert.IsNull(pathsPage.PreviousPage());

            Assert.AreEqual(pathsPage.Records[0].DestinationAmount, "20.0000000");
            Assert.AreEqual(pathsPage.Records[0].DestinationAsset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GDSBCQO34HWPGUGQSP3QBFEXVTSR2PW46UIGTHVWGWJGQKH3AFNHXHXN")));
            Assert.AreEqual(pathsPage.Records[0].Path.Count, 0);
            Assert.AreEqual(pathsPage.Records[0].SourceAmount, "30.0000000");
            Assert.AreEqual(pathsPage.Records[0].SourceAsset,
                Asset.CreateNonNativeAsset("USD",
                    KeyPair.FromAccountId("GDSBCQO34HWPGUGQSP3QBFEXVTSR2PW46UIGTHVWGWJGQKH3AFNHXHXN")));

            Assert.AreEqual(pathsPage.Records[1].DestinationAmount, "50.0000000");
            Assert.AreEqual(pathsPage.Records[1].DestinationAsset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GBFMFKDUFYYITWRQXL4775CVUV3A3WGGXNJUAP4KTXNEQ2HG7JRBITGH")));
            Assert.AreEqual(pathsPage.Records[1].Path.Count, 1);
            Assert.AreEqual(pathsPage.Records[1].Path[0],
                Asset.CreateNonNativeAsset("GBP",
                    KeyPair.FromAccountId("GDSBCQO34HWPGUGQSP3QBFEXVTSR2PW46UIGTHVWGWJGQKH3AFNHXHXN")));
            Assert.AreEqual(pathsPage.Records[1].SourceAmount, "60.0000000");
            Assert.AreEqual(pathsPage.Records[1].SourceAsset,
                Asset.CreateNonNativeAsset("USD",
                    KeyPair.FromAccountId("GBRAOXQDNQZRDIOK64HZI4YRDTBFWNUYH3OIHQLY4VEK5AIGMQHCLGXI")));

            Assert.AreEqual(pathsPage.Records[2].DestinationAmount, "200.0000000");
            Assert.AreEqual(pathsPage.Records[2].DestinationAsset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GBRCOBK7C7UE72PB5JCPQU3ZI45ZCEM7HKQ3KYV3YD3XB7EBOPBEDN2G")));
            Assert.AreEqual(pathsPage.Records[2].Path.Count, 2);
            Assert.AreEqual(pathsPage.Records[2].Path[0],
                Asset.CreateNonNativeAsset("GBP",
                    KeyPair.FromAccountId("GAX7B3ZT3EOZW5POAMV4NGPPKCYUOYW2QQDIAF23JAXF72NMGRYPYOPM")));
            Assert.AreEqual(pathsPage.Records[2].Path[1],
                Asset.CreateNonNativeAsset("PLN",
                    KeyPair.FromAccountId("GACWIA2XGDFWWN3WKPX63JTK4S2J5NDPNOIVYMZY6RVTS7LWF2VHZLV3")));
            Assert.AreEqual(pathsPage.Records[2].SourceAmount, "300.0000000");
            Assert.AreEqual(pathsPage.Records[2].SourceAsset,
                Asset.CreateNonNativeAsset("USD",
                    KeyPair.FromAccountId("GC7J5IHS3GABSX7AZLRINXWLHFTL3WWXLU4QX2UGSDEAIAQW2Q72U3KH")));
        }
    }
}