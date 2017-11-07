using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using System.IO;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class SubmitTransactionResponseTest
    {
        [TestMethod]
        public void TestDeserializeTransactionFailureResponse()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "submitTransactionTransactionFailure.json"));
            var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(json);

            Assert.AreEqual(submitTransactionResponse.IsSuccess(), false);
            Assert.AreEqual(submitTransactionResponse.EnvelopeXdr, "AAAAAKpmDL6Z4hvZmkTBkYpHftan4ogzTaO4XTB7joLgQnYYAAAAZAAAAAAABeoyAAAAAAAAAAEAAAAAAAAAAQAAAAAAAAABAAAAAD3sEVVGZGi/NoC3ta/8f/YZKMzyi9ZJpOi0H47x7IqYAAAAAAAAAAAF9eEAAAAAAAAAAAA=");
            Assert.AreEqual(submitTransactionResponse.ResultXdr, "AAAAAAAAAAD////4AAAAAA==");
            Assert.AreEqual(submitTransactionResponse.SubmitTransactionResponseExtras.ExtrasResultCodes.TransactionResultCode, "tx_no_source_account");
        }

        [TestMethod]
        public void TestDeserializeOperationFailureResponse()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "submitTransactionOperationFailure.json"));
            var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(json);

            Assert.AreEqual(submitTransactionResponse.IsSuccess(), false);
            Assert.AreEqual(submitTransactionResponse.EnvelopeXdr, "AAAAAF2O0axA67+p2jMunG6G188kDSHIvqQ13d9l29YCSA/uAAAAZAAvvc0AAAABAAAAAAAAAAEAAAAAAAAAAQAAAAAAAAABAAAAAD3sEVVGZGi/NoC3ta/8f/YZKMzyi9ZJpOi0H47x7IqYAAAAAAAAAAAF9eEAAAAAAAAAAAECSA/uAAAAQFuZVAjftHa+JZes1VxSk8naOfjjAz9V86mY1AZf8Ik6PtTsBpDsCfG57EYsq4jWyZcT+vhXyWsw5evF1ELqMw4=");
            Assert.AreEqual(submitTransactionResponse.ResultXdr, "AAAAAAAAAGT/////AAAAAQAAAAAAAAAB////+wAAAAA=");
            Assert.AreEqual(submitTransactionResponse.SubmitTransactionResponseExtras.ExtrasResultCodes.TransactionResultCode, "tx_failed");
            Assert.AreEqual(submitTransactionResponse.SubmitTransactionResponseExtras.ExtrasResultCodes.OperationsResultCodes[0], "op_no_destination");
        }

        [TestMethod]
        public void TestDeserializeSuccessResponse()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "submitTransactionSuccess.json"));
            var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(json);

            Assert.AreEqual(submitTransactionResponse.IsSuccess(), true);
            Assert.AreEqual(submitTransactionResponse.Hash, "ee14b93fcd31d4cfe835b941a0a8744e23a6677097db1fafe0552d8657bed940");
            Assert.AreEqual(submitTransactionResponse.Ledger, 3128812L);
            Assert.AreEqual(submitTransactionResponse.EnvelopeXdr, "AAAAADSMMRmQGDH6EJzkgi/7PoKhphMHyNGQgDp2tlS/dhGXAAAAZAAT3TUAAAAwAAAAAAAAAAAAAAABAAAAAAAAAAMAAAABSU5SAAAAAAA0jDEZkBgx+hCc5IIv+z6CoaYTB8jRkIA6drZUv3YRlwAAAAFVU0QAAAAAADSMMRmQGDH6EJzkgi/7PoKhphMHyNGQgDp2tlS/dhGXAAAAAAX14QAAAAAKAAAAAQAAAAAAAAAAAAAAAAAAAAG/dhGXAAAAQLuStfImg0OeeGAQmvLkJSZ1MPSkCzCYNbGqX5oYNuuOqZ5SmWhEsC7uOD9ha4V7KengiwNlc0oMNqBVo22S7gk=");
            Assert.AreEqual(submitTransactionResponse.ResultXdr, "AAAAAAAAAGQAAAAAAAAAAQAAAAAAAAADAAAAAAAAAAAAAAAAAAAAADSMMRmQGDH6EJzkgi/7PoKhphMHyNGQgDp2tlS/dhGXAAAAAAAAAPEAAAABSU5SAAAAAAA0jDEZkBgx+hCc5IIv+z6CoaYTB8jRkIA6drZUv3YRlwAAAAFVU0QAAAAAADSMMRmQGDH6EJzkgi/7PoKhphMHyNGQgDp2tlS/dhGXAAAAAAX14QAAAAAKAAAAAQAAAAAAAAAAAAAAAA==");
            Assert.AreEqual(submitTransactionResponse.GetOfferIdFromResult(0), 241L);
        }

        [TestMethod]
        public void TestDeserializeNoOfferID()
        {

            var json = File.ReadAllText(Path.Combine("responses", "testdata", "submitTransactionNoOfferId.json"));
            var submitTransactionResponse = JsonSingleton.GetInstance<SubmitTransactionResponse>(json);

            Assert.AreEqual(submitTransactionResponse.IsSuccess(), true);
            Assert.IsNull(submitTransactionResponse.GetOfferIdFromResult(0));
        }

    }
}
