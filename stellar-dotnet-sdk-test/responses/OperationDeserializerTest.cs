using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OperationDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationCreateAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreateAccountOperationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationCreateAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateAccountOperationData(back);
        }

        public static void AssertCreateAccountOperationData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is CreateAccountOperationResponse);
            var operation = (CreateAccountOperationResponse) instance;

            Assert.AreEqual(operation.SourceAccount, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");
            Assert.AreEqual(operation.PagingToken, "3936840037961729");
            Assert.AreEqual(operation.Id, 3936840037961729L);
            Assert.IsNotNull(operation.TransactionSuccessful);

            Assert.AreEqual(operation.Account, "GAR4DDXYNSN2CORG3XQFLAPWYKTUMLZYHYWV4Y2YJJ4JO6ZJFXMJD7PT");
            Assert.AreEqual(operation.StartingBalance, "299454.904954");
            Assert.AreEqual(operation.Funder, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");

            Assert.AreEqual(operation.Links.Effects.Href, "/operations/3936840037961729/effects{?cursor,limit,order}");
            Assert.AreEqual(operation.Links.Precedes.Href, "/operations?cursor=3936840037961729&order=asc");
            Assert.AreEqual(operation.Links.Self.Href, "/operations/3936840037961729");
            Assert.AreEqual(operation.Links.Succeeds.Href, "/operations?cursor=3936840037961729&order=desc");
            Assert.AreEqual(operation.Links.Transaction.Href,
                "/transactions/75608563ae63757ffc0650d84d1d13c0f3cd4970a294a2a6b43e3f454e0f9e6d");
        }

        [TestMethod]
        public void TestDeserializePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPayment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPayment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPaymentData(back);
        }

        private static void AssertPaymentData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse) instance;

            AssertPaymentOperationTestData(operation);
        }

        public static void AssertPaymentOperationTestData(PaymentOperationResponse operation)
        {
            Assert.AreEqual(operation.SourceAccount, "GB6NVEN5HSUBKMYCE5ZOWSK5K23TBWRUQLZY3KNMXUZ3AQ2ESC4MY4AQ");
            Assert.AreEqual(operation.Id, 3940808587743233L);
            Assert.AreEqual(operation.TransactionSuccessful, false);

            Assert.AreEqual(operation.From, "GB6NVEN5HSUBKMYCE5ZOWSK5K23TBWRUQLZY3KNMXUZ3AQ2ESC4MY4AQ");
            Assert.AreEqual(operation.To, "GDWNY2POLGK65VVKIH5KQSH7VWLKRTQ5M6ADLJAYC2UEHEBEARCZJWWI");
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.Asset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializeNonNativePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPaymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertNonNativePaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeNonNativePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPaymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertNonNativePaymentData(back);
        }

        private static void AssertNonNativePaymentData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse) instance;

            Assert.AreEqual(operation.TransactionSuccessful, true);

            Assert.AreEqual(operation.From, "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA");
            Assert.AreEqual(operation.To, "GBHUSIZZ7FS2OMLZVZ4HLWJMXQ336NFSXHYERD7GG54NRITDTEWWBBI6");
            Assert.AreEqual(operation.Amount, "1000000000.0");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
        }

        [TestMethod]
        public void TestDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationAllowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAllowTrustData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationAllowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAllowTrustData(back);
        }

        private static void AssertAllowTrustData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AllowTrustOperationResponse);
            var operation = (AllowTrustOperationResponse) instance;

            Assert.AreEqual(operation.Trustee, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Authorize, true);
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }

        [TestMethod]
        public void TestDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationChangeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertChangeTrustData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationChangeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertChangeTrustData(back);
        }

        private static void AssertChangeTrustData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ChangeTrustOperationResponse);
            var operation = (ChangeTrustOperationResponse) instance;

            Assert.AreEqual(operation.Trustee, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Limit, "922337203685.4775807");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }

        [TestMethod]
        public void TestDeserializeSetOptionsOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationSetOptions.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertSetOptionsData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSetOptionsOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationSetOptions.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertSetOptionsData(back);
        }

        private static void AssertSetOptionsData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SetOptionsOperationResponse);
            var operation = (SetOptionsOperationResponse) instance;

            Assert.AreEqual(operation.SignerKey, "GD3ZYXVC7C3ECD5I4E5NGPBFJJSULJ6HJI2FBHGKYFV34DSIWB4YEKJZ");
            Assert.AreEqual(operation.SignerWeight, 1);
            Assert.AreEqual(operation.HomeDomain, "stellar.org");
            Assert.AreEqual(operation.InflationDestination, "GBYWSY4NPLLPTP22QYANGTT7PEHND64P4D4B6LFEUHGUZRVYJK2H4TBE");
            Assert.AreEqual(operation.LowThreshold, 1);
            Assert.AreEqual(operation.MedThreshold, 2);
            Assert.AreEqual(operation.HighThreshold, 3);
            Assert.AreEqual(operation.MasterKeyWeight, 4);
            Assert.AreEqual(operation.SetFlags[0], "auth_required_flag");
            Assert.AreEqual(operation.ClearFlags[0], "auth_revocable_flag");
        }

        [TestMethod]
        public void TestDeserializeSetOptionsOperationWithNonEd25519Key()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationSetOptionsNonEd25519Key.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertSetOptionsOperationWithNonEd25519KeyData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSetOptionsOperationWithNonEd25519Key()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationSetOptionsNonEd25519Key.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertSetOptionsOperationWithNonEd25519KeyData(back);
        }

        private static void AssertSetOptionsOperationWithNonEd25519KeyData(OperationResponse instance)
        {
            Assert.IsTrue(instance is SetOptionsOperationResponse);
            var operation = (SetOptionsOperationResponse) instance;

            Assert.AreEqual(operation.SignerKey, "TBGFYVCU76LJ7GZOCGR4X7DG2NV42JPG5CKRL42LA5FZOFI3U2WU7ZAL");
        }

        [TestMethod]
        public void TestDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationAccountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAccountMergeData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationAccountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAccountMergeData(back);
        }

        private static void AssertAccountMergeData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountMergeOperationResponse);
            var operation = (AccountMergeOperationResponse) instance;

            Assert.AreEqual(operation.Account, "GD6GKRABNDVYDETEZJQEPS7IBQMERCN44R5RCI4LJNX6BMYQM2KPGGZ2");
            Assert.AreEqual(operation.Into, "GAZWSWPDQTBHFIPBY4FEDFW2J6E2LE7SZHJWGDZO6Q63W7DBSRICO2KN");
        }

        [TestMethod]
        public void TestDeserializeManageOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageOfferData(back);
        }

        private static void AssertManageOfferData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageOfferOperationResponse);
            var operation = (ManageOfferOperationResponse) instance;

            Assert.AreEqual(operation.OfferId, 0);
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("CNY", "GAZWSWPDQTBHFIPBY4FEDFW2J6E2LE7SZHJWGDZO6Q63W7DBSRICO2KN"));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializePathPaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPathPayment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPathPaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePathPaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPathPayment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPathPaymentData(back);
        }

        private static void AssertPathPaymentData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PathPaymentOperationResponse);
            var operation = (PathPaymentOperationResponse) instance;

            Assert.AreEqual(operation.From, "GCXKG6RN4ONIEPCMNFB732A436Z5PNDSRLGWK7GBLCMQLIFO4S7EYWVU");
            Assert.AreEqual(operation.To, "GA5WBPYA5Y4WAEHXWR2UKO2UO4BUGHUQ74EUPKON2QHV4WRHOIRNKKH2");
            Assert.AreEqual(operation.Amount, "10.0");
            Assert.AreEqual(operation.SourceMax, "100.0");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GCQPYGH4K57XBDENKKX55KDTWOTK5WDWRQOH2LHEDX3EKVIQRLMESGBG"));
            Assert.AreEqual(operation.SendAsset, Asset.CreateNonNativeAsset("USD", "GC23QF2HUE52AMXUFUH3AYJAXXGXXV2VHXYYR6EYXETPKDXZSAW67XO4"));
        }

        [TestMethod]
        public void TestDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPassiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreatePassiveOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPassiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreatePassiveOfferData(back);
        }

        private static void AssertCreatePassiveOfferData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is CreatePassiveOfferOperationResponse);
            var operation = (CreatePassiveOfferOperationResponse) instance;

            Assert.AreEqual(operation.Amount, "11.27827");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("USD", "GDS5JW5E6DRSSN5XK4LW7E6VUMFKKE2HU5WCOVFTO7P2RP7OXVCBLJ3Y"));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationInflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertInflationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationInflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertInflationData(back);
        }

        private static void AssertInflationData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is InflationOperationResponse);
            var operation = (InflationOperationResponse) instance;

            Assert.AreEqual(operation.Id, 12884914177L);
        }

        [TestMethod]
        public void TestManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataData(back);
        }

        private static void AssertManageDataData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse) instance;

            Assert.AreEqual(operation.Id, 14336188517191688L);
            Assert.AreEqual(operation.Name, "CollateralValue");
            Assert.AreEqual(operation.Value, "MjAwMA==");
        }

        [TestMethod]
        public void TestDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataValueEmptyData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationManageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataValueEmptyData(back);
        }

        private static void AssertManageDataValueEmptyData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse) instance;

            Assert.AreEqual(operation.Value, null);
        }

        [TestMethod]
        public void TestDeserializeUnknownOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationUnknown.json"));
            Assert.ThrowsException<JsonSerializationException>(() =>
                JsonSingleton.GetInstance<OperationResponse>(json));
        }

        [TestMethod]
        public void TestDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationBumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertBumpSequenceData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationBumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertBumpSequenceData(back);
        }

        private static void AssertBumpSequenceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is BumpSequenceOperationResponse);
            var operation = (BumpSequenceOperationResponse) instance;

            Assert.AreEqual(12884914177L, operation.Id);
            Assert.AreEqual(79473726952833048L, operation.BumpTo);
        }
    }
}
