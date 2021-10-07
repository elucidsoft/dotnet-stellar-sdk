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
    public class PaymentOperationResponseTest
    {
        //Payment
        [TestMethod]
        public void TestDeserializePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "payment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPaymentOperationTestData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "payment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPaymentOperationTestData(back);
        }

        public static void AssertPaymentOperationTestData(OperationResponse instance)
        {
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.SourceAccountMuxed);
            Assert.IsNull(operation.SourceAccountMuxedID);

            Assert.AreEqual(operation.Id, 3940808587743233L);

            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.FromMuxed);
            Assert.IsNull(operation.FromMuxedID);
            Assert.AreEqual(operation.To, "GDWNY2POLGK65VVKIH5KQSH7VWLKRTQ5M6ADLJAYC2UEHEBEARCZJWWI");
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.Asset, new AssetTypeNative());

            Assert.IsFalse(operation.TransactionSuccessful);
        }

        //Payment Non Native
        [TestMethod]
        public void TestDeserializePaymentOperationNonNative()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "paymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertNonNativePaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePaymentOperationNonNative()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "paymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertNonNativePaymentData(back);
        }

        private static void AssertNonNativePaymentData(OperationResponse instance)
        {
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse)instance;

            Assert.AreEqual(operation.From, "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA");
            Assert.AreEqual(operation.To, "GBHUSIZZ7FS2OMLZVZ4HLWJMXQ336NFSXHYERD7GG54NRITDTEWWBBI6");
            Assert.AreEqual(operation.Amount, "1000000000.0");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
        }

        //Payment (Muxed)
        [TestMethod]
        public void TestDeserializePaymentOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "paymentMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPaymentOperationTestDataMuxed(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePaymentOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/payment", "paymentMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPaymentOperationTestDataMuxed(back);
        }

        public static void AssertPaymentOperationTestDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.SourceAccountMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.SourceAccountMuxedID, 5123456789);

            Assert.AreEqual(operation.Id, 3940808587743233L);

            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FromMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FromMuxedID, 5123456789);
            Assert.AreEqual(operation.To, "GDWNY2POLGK65VVKIH5KQSH7VWLKRTQ5M6ADLJAYC2UEHEBEARCZJWWI");
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.Asset, new AssetTypeNative());

            Assert.IsFalse(operation.TransactionSuccessful);
        }
    }
}
