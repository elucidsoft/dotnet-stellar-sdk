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
    public class CreateClaimableBalanceOperationResponseTest
    {
        //Create Claimable Balance
        [TestMethod]
        public void TestSerializationCreateClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createClaimableBalance", "createClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateClaimableBalanceData(back);
        }

        [TestMethod]
        public void TestSerializationCreateClaimableBalanceAbsBeforeMaxIntOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createClaimableBalance", "createClaimableBalanceAbsBeforeMaxInt.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            Assert.IsTrue(instance is CreateClaimableBalanceOperationResponse);
            var operation = (CreateClaimableBalanceOperationResponse)instance;

            Assert.IsNotNull(operation.Claimants[0].Predicate.AbsBefore);
        }

        private static void AssertCreateClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is CreateClaimableBalanceOperationResponse);
            var operation = (CreateClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(213223651414017, operation.Id);
            Assert.AreEqual("GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", operation.Sponsor);
            Assert.AreEqual("native", operation.Asset);
            Assert.AreEqual("1.0000000", operation.Amount);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", operation.Claimants[0].Destination);

            var back = new CreateClaimableBalanceOperationResponse(operation.Sponsor, operation.Asset, operation.Amount, operation.Claimants);
            Assert.IsNotNull(back);
        }
    }
}
