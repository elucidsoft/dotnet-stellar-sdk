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
    public class InflationOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/inflation", "inflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertInflationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/inflation", "inflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertInflationData(back);
        }

        private static void AssertInflationData(OperationResponse instance)
        {
            Assert.IsTrue(instance is InflationOperationResponse);
            var operation = (InflationOperationResponse)instance;

            Assert.AreEqual(operation.Id, 12884914177L);
        }
    }
}
