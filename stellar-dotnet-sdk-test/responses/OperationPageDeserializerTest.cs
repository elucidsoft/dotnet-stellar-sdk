using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OperationPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPage.json"));
            var operationsPage = JsonSingleton.GetInstance<Page<OperationResponse>>(json);

            AssertTestData(operationsPage);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPage.json"));
            var operationsPage = JsonSingleton.GetInstance<Page<OperationResponse>>(json);
            var serialized = JsonConvert.SerializeObject(operationsPage, new OperationDeserializer());
            var back = JsonConvert.DeserializeObject<Page<OperationResponse>>(serialized, new OperationDeserializer());

            AssertTestData(back);
        }

        public static void AssertTestData(Page<OperationResponse> operationsPage)
        {
            var createAccountOperation = (CreateAccountOperationResponse) operationsPage.Records[0];
            Assert.AreEqual(createAccountOperation.StartingBalance, "10000.0");
            Assert.AreEqual(createAccountOperation.PagingToken, "3717508943056897");
            Assert.AreEqual(createAccountOperation.Account, "GDFH4NIYMIIAKRVEJJZOIGWKXGQUF3XHJG6ZM6CEA64AMTVDN44LHOQE");
            Assert.AreEqual(createAccountOperation.Funder, "GBS43BF24ENNS3KPACUZVKK2VYPOZVBQO2CISGZ777RYGOPYC2FT6S3K");

            var paymentOperation = (PaymentOperationResponse) operationsPage.Records[4];
            Assert.AreEqual(paymentOperation.Amount, "10.123");
            Assert.AreEqual(paymentOperation.Asset, new AssetTypeNative());
            Assert.AreEqual(paymentOperation.From, "GCYK67DDGBOANS6UODJ62QWGLEB2A7JQ3XUV25HCMLT7CI23PMMK3W6R");
            Assert.AreEqual(paymentOperation.To, "GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H");
        }
    }
}
