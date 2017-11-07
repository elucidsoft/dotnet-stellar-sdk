using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class OperationPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationPage.json"));
            var operationsPage = JsonSingleton.GetInstance<Page<OperationResponse>>(json);

            CreateAccountOperationResponse createAccountOperation = (CreateAccountOperationResponse) operationsPage.Records[0];
            Assert.AreEqual(createAccountOperation.StartingBalance, "10000.0");
            Assert.AreEqual(createAccountOperation.PagingToken, "3717508943056897");
            Assert.AreEqual(createAccountOperation.Account.AccountId, "GDFH4NIYMIIAKRVEJJZOIGWKXGQUF3XHJG6ZM6CEA64AMTVDN44LHOQE");
            Assert.AreEqual(createAccountOperation.Funder.AccountId, "GBS43BF24ENNS3KPACUZVKK2VYPOZVBQO2CISGZ777RYGOPYC2FT6S3K");

            PaymentOperationResponse paymentOperation = (PaymentOperationResponse) operationsPage.Records[4];
            Assert.AreEqual(paymentOperation.Amount, "10.123");
            Assert.AreEqual(paymentOperation.Asset, new AssetTypeNative());
            Assert.AreEqual(paymentOperation.From.AccountId, "GCYK67DDGBOANS6UODJ62QWGLEB2A7JQ3XUV25HCMLT7CI23PMMK3W6R");
            Assert.AreEqual(paymentOperation.To.AccountId, "GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H");
        }
    }
}
