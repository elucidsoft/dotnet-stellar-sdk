using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class CreateAccountOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createAccount", "createAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreateAccountOperationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createAccount", "createAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateAccountOperationData(back);
        }

        public static void AssertCreateAccountOperationData(OperationResponse instance)
        {
            Assert.IsTrue(instance is CreateAccountOperationResponse);
            var operation = (CreateAccountOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");
            Assert.AreEqual(operation.PagingToken, "3936840037961729");
            Assert.AreEqual(operation.Id, 3936840037961729L);

            Assert.AreEqual(operation.Account, "GAR4DDXYNSN2CORG3XQFLAPWYKTUMLZYHYWV4Y2YJJ4JO6ZJFXMJD7PT");
            Assert.AreEqual(operation.StartingBalance, "299454.904954");
            Assert.AreEqual(operation.Funder, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.FunderMuxed);
            Assert.IsNull(operation.FunderMuxedID);

            Assert.IsTrue(operation.TransactionSuccessful);
            Assert.AreEqual(operation.Links.Effects.Href, "/operations/3936840037961729/effects{?cursor,limit,order}");
            Assert.AreEqual(operation.Links.Precedes.Href, "/operations?cursor=3936840037961729&order=asc");
            Assert.AreEqual(operation.Links.Self.Href, "/operations/3936840037961729");
            Assert.AreEqual(operation.Links.Succeeds.Href, "/operations?cursor=3936840037961729&order=desc");
            Assert.AreEqual(operation.Links.Transaction.Href,
                "/transactions/75608563ae63757ffc0650d84d1d13c0f3cd4970a294a2a6b43e3f454e0f9e6d");
        }

        [TestMethod]
        public void TestDeserializeCreateAccountOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createAccount", "createAccountMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreateAccountOperationDataMuxed(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreateAccountOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createAccount", "createAccountMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateAccountOperationDataMuxed(back);
        }

        public static void AssertCreateAccountOperationDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is CreateAccountOperationResponse);
            var operation = (CreateAccountOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");
            Assert.AreEqual(operation.PagingToken, "3936840037961729");
            Assert.AreEqual(operation.Id, 3936840037961729L);

            Assert.AreEqual(operation.Account, "GAR4DDXYNSN2CORG3XQFLAPWYKTUMLZYHYWV4Y2YJJ4JO6ZJFXMJD7PT");
            Assert.AreEqual(operation.StartingBalance, "299454.904954");
            Assert.AreEqual(operation.Funder, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FunderMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FunderMuxedID, 5123456789UL);

            Assert.IsTrue(operation.TransactionSuccessful);
            Assert.AreEqual(operation.Links.Effects.Href, "/operations/3936840037961729/effects{?cursor,limit,order}");
            Assert.AreEqual(operation.Links.Precedes.Href, "/operations?cursor=3936840037961729&order=asc");
            Assert.AreEqual(operation.Links.Self.Href, "/operations/3936840037961729");
            Assert.AreEqual(operation.Links.Succeeds.Href, "/operations?cursor=3936840037961729&order=desc");
            Assert.AreEqual(operation.Links.Transaction.Href,
                "/transactions/75608563ae63757ffc0650d84d1d13c0f3cd4970a294a2a6b43e3f454e0f9e6d");
        }
    }
}
