using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.effects;
using stellar_dotnetcore_sdk.responses.operations;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class EffectDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeAccountCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "effectAccountCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreatedEffectResponse);
            var operation = (AccountCreatedEffectResponse)instance;


        }
    }
}
