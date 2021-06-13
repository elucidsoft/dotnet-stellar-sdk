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
    public class ManageDataOperationResponseTest
    {
        //Manage Data
        [TestMethod]
        public void TestManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/manageData", "manageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/manageData", "manageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataData(back);
        }

        private static void AssertManageDataData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse)instance;

            Assert.AreEqual(operation.Id, 14336188517191688L);
            Assert.AreEqual(operation.Name, "CollateralValue");
            Assert.AreEqual(operation.Value, "MjAwMA==");
        }

        //Manage Data Value Empty
        [TestMethod]
        public void TestDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/manageData", "manageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataValueEmptyData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/manageData", "manageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataValueEmptyData(back);
        }

        private static void AssertManageDataValueEmptyData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse)instance;

            Assert.AreEqual(operation.Value, null);
        }
    }
}
