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
    public class UnknownOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeUnknownOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/unknownOperation", "unknownOperation.json"));
            Assert.ThrowsException<JsonSerializationException>(() =>
                JsonSingleton.GetInstance<OperationResponse>(json));
        }
    }
}
