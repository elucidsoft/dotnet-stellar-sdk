using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
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

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
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

        [TestMethod]
        public void TestDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationAllowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AllowTrustOperationResponse);
            var operation = (AllowTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustee.AccountId, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor.AccountId, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Authorize, true);
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM")));
        }

        [TestMethod]
        public void TestDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationChangeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ChangeTrustOperationResponse);
            var operation = (ChangeTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustee.AccountId, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor.AccountId, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Limit, "922337203685.4775807");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM")));
        }

        //TODO: TestDeserializeSetOperationsOperation

        [TestMethod]
        public void TestDeserializeAccountMergeOperation()
        {

            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationAccountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountMergeOperationResponse);
            var operation = (AccountMergeOperationResponse)instance;

            Assert.AreEqual(operation.Account.AccountId, "GD6GKRABNDVYDETEZJQEPS7IBQMERCN44R5RCI4LJNX6BMYQM2KPGGZ2");
            Assert.AreEqual(operation.Into.AccountId, "GAZWSWPDQTBHFIPBY4FEDFW2J6E2LE7SZHJWGDZO6Q63W7DBSRICO2KN");
        }

        [TestMethod]
        public void TestDeserializeManageOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationManageOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageOfferOperationResponse);
            var operation = (ManageOfferOperationResponse)instance;

            Assert.AreEqual(operation.OfferId, 0);
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("CNY", KeyPair.FromAccountId("GAZWSWPDQTBHFIPBY4FEDFW2J6E2LE7SZHJWGDZO6Q63W7DBSRICO2KN")));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        //TODO: TestDeserializePathPaymentOperation

        [TestMethod]
        public void TestDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationPassiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is CreatePassiveOfferOperationResponse);
            var operation = (CreatePassiveOfferOperationResponse)instance;

            Assert.AreEqual(operation.Amount, "11.27827");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("USD", KeyPair.FromAccountId("GDS5JW5E6DRSSN5XK4LW7E6VUMFKKE2HU5WCOVFTO7P2RP7OXVCBLJ3Y")));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        //TODO: TestDeserializeInflationOperation

        //TODO: TestDeserializeManageDataOperation

        //TODO: TestDeserializeManageDataOperationValueEmpty
    }
}
