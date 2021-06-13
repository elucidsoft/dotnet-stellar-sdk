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
    public class BumpSequenceOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/bumpSequence", "bumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertBumpSequenceData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/bumpSequence", "bumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertBumpSequenceData(back);
        }

        private static void AssertBumpSequenceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is BumpSequenceOperationResponse);
            var operation = (BumpSequenceOperationResponse)instance;

            Assert.AreEqual(12884914177L, operation.Id);
            Assert.AreEqual(79473726952833048L, operation.BumpTo);
        }
    }
}
