using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class PathPaymentStrictSendOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializePathPaymentStrictSendOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/pathPaymentStrictSend", "pathPaymentStrictSend.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPathPaymentStrictSendData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePathPaymentStrictSendOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/pathPaymentStrictSend", "pathPaymentStrictSend.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPathPaymentStrictSendData(back);
        }

        private static void AssertPathPaymentStrictSendData(OperationResponse instance)
        {
            Assert.IsTrue(instance is PathPaymentStrictSendOperationResponse);
            var operation = (PathPaymentStrictSendOperationResponse)instance;

            PathPaymentStrictSendOperationResponse operationTest = new PathPaymentStrictSendOperationResponse(
                "GCXVEEBWI4YMRK6AFJQSEUBYDQL4PZ24ECAPJE2ZIAPIQZLZIBAX3LIF",
                "GCXVEEBWI4YMRK6AFJQSEUBYDQL4PZ24ECAPJE2ZIAPIQZLZIBAX3LIF",
                "native", "", "",
                "0.0859000",
                "credit_alphanum4", "KIN", "GBDEVU63Y6NTHJQQZIKVTC23NWLQVP3WJ2RI2OTSJTNYOIGICST6DUXR",
                "1000.0000000",
                "0.0859000",
                new Asset[] { }
            );

            Assert.AreEqual(operation.From, operationTest.From);
            Assert.AreEqual(operation.To, operationTest.To);
            Assert.AreEqual(operation.Amount, operationTest.Amount);
            Assert.AreEqual(operation.SourceAmount, operationTest.SourceAmount);
            Assert.AreEqual(operation.DestinationMin, operationTest.DestinationMin);
            Assert.AreEqual(operation.DestinationAsset, Asset.Create(operationTest.AssetType, "", ""));
            Assert.AreEqual(operation.SourceAsset, Asset.CreateNonNativeAsset(operationTest.SourceAssetCode, operationTest.SourceAssetIssuer));
        }
    }
}
