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
    public class EndSponsoringFutureReservesOperationResponseTest
    {
        //End Sponsoring Future Reserves
        [TestMethod]
        public void TestSerializationEndSponsoringFutureReservesOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/endSponsoringFutureReserves", "endSponsoringFutureReserves.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertEndSponsoringFutureReservesData(back);
        }

        private static void AssertEndSponsoringFutureReservesData(OperationResponse instance)
        {
            Assert.IsTrue(instance is EndSponsoringFutureReservesOperationResponse);
            var operation = (EndSponsoringFutureReservesOperationResponse)instance;

            Assert.AreEqual(215542933753859, operation.Id);
            Assert.AreEqual("GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2", operation.BeginSponsor);
            Assert.AreEqual("", operation.BeginSponsorMuxed);
            Assert.IsNull(operation.BeginSponsorMuxedID);

            var back = new EndSponsoringFutureReservesOperationResponse(operation.BeginSponsor);
            Assert.IsNotNull(back);
        }

        //End Sponsoring Future Reserves (Muxed)
        [TestMethod]
        public void TestSerializationEndSponsoringFutureReservesOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/endSponsoringFutureReserves", "endSponsoringFutureReservesMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertEndSponsoringFutureReservesDataMuxed(back);
        }

        private static void AssertEndSponsoringFutureReservesDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is EndSponsoringFutureReservesOperationResponse);
            var operation = (EndSponsoringFutureReservesOperationResponse)instance;

            Assert.AreEqual(215542933753859, operation.Id);
            Assert.AreEqual("GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2", operation.BeginSponsor);
            Assert.AreEqual("MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24", operation.BeginSponsorMuxed);
            Assert.AreEqual(5123456789, operation.BeginSponsorMuxedID);

            var back = new EndSponsoringFutureReservesOperationResponse(operation.BeginSponsor);
            Assert.IsNotNull(back);
        }
    }
}
