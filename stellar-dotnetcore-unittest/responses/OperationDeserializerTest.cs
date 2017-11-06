using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class OperationDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationCreateAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            
            Assert.IsTrue(instance is CreateAccountOperationResponse);
            var operation = (CreateAccountOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount.AccountId, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");
            Assert.AreEqual(operation.PagingToken, "3936840037961729");
            Assert.AreEqual(operation.Id, 3936840037961729L);

            Assert.AreEqual(operation.Account.AccountId, "GAR4DDXYNSN2CORG3XQFLAPWYKTUMLZYHYWV4Y2YJJ4JO6ZJFXMJD7PT");
            Assert.AreEqual(operation.StartingBalance, "299454.904954");
            Assert.AreEqual(operation.Funder.AccountId, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");

            Assert.AreEqual(operation.Links.Effects.Href, "/operations/3936840037961729/effects{?cursor,limit,order}");
            Assert.AreEqual(operation.Links.Precedes.Href, "/operations?cursor=3936840037961729&order=asc");
            Assert.AreEqual(operation.Links.Self.Href, "/operations/3936840037961729");
            Assert.AreEqual(operation.Links.Succeeds.Href, "/operations?cursor=3936840037961729&order=desc");
            Assert.AreEqual(operation.Links.Transaction.Href, "/transactions/75608563ae63757ffc0650d84d1d13c0f3cd4970a294a2a6b43e3f454e0f9e6d");

        }

        //TODO: TestDeserializePaymentOperation

        //TODO TestDeserializeAllowTrustOperation

        //TODO: TestDeserializeChangeTrustOperation

        //TODO: TestDeserializeSetOperationsOperation

        //TODO: TestDeserializeAccountMergeOperation

        //TODO: TestDeserializeManageOfferOperation

        //TODO: TestDeserializePathPaymentOperation

        //TODO: TestDeserializeCreatePassiveOfferOperation

        //TODO: TestDeserializeInflationOperation

        //TODO: TestDeserializeManageDataOperation

        //TODO: TestDeserializeManageDataOperationValueEmpty
    }
}
