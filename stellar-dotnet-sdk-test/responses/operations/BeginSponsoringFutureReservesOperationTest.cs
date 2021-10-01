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
    public class BeginSponsoringFutureReservesOperationTest
    {
        //Begin Sponsoring Future Reserves
        [TestMethod]
        public void TestSerializationBeginSponsoringFutureReservesOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/beginSponsoringFutureReserves", "beginSponsoringFutureReserves.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertBeginSponsoringFutureReservesData(back);
        }

        private static void AssertBeginSponsoringFutureReservesData(OperationResponse instance)
        {
            Assert.IsTrue(instance is BeginSponsoringFutureReservesOperationResponse);
            var operation = (BeginSponsoringFutureReservesOperationResponse)instance;

            Assert.AreEqual(215542933753857, operation.Id);
            Assert.AreEqual("GAXHU2XHSMTZYAKFCVTULAYUL34BFPPLRVJYZMEOHP7IWPZJKSVY67RJ", operation.SponsoredID);

            var back = new BeginSponsoringFutureReservesOperationResponse(operation.SponsoredID);
            Assert.IsNotNull(back);
        }
    }
}
