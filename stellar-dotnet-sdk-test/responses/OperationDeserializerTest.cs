using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System.IO;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OperationDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "createAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreateAccountOperationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "createAccount.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateAccountOperationData(back);
        }

        public static void AssertCreateAccountOperationData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is CreateAccountOperationResponse);
            var operation = (CreateAccountOperationResponse)instance;

            Assert.AreEqual(operation.SourceAccount, "GD6WU64OEP5C4LRBH6NK3MHYIA2ADN6K6II6EXPNVUR3ERBXT4AN4ACD");
            Assert.AreEqual(operation.PagingToken, "3936840037961729");
            Assert.AreEqual(operation.Id, 3936840037961729L);

            Assert.AreEqual(operation.Account, "GAR4DDXYNSN2CORG3XQFLAPWYKTUMLZYHYWV4Y2YJJ4JO6ZJFXMJD7PT");
            Assert.AreEqual(operation.StartingBalance, "299454.904954");
            Assert.AreEqual(operation.Funder, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FunderMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FunderMuxedID, 5123456789);

            Assert.IsTrue(operation.TransactionSuccessful);
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
            var json = File.ReadAllText(Path.Combine("testdata/operations", "payment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "payment.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPaymentData(back);
        }

        private static void AssertPaymentData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse)instance;

            AssertPaymentOperationTestData(operation);
        }

        public static void AssertPaymentOperationTestData(PaymentOperationResponse operation)
        {
            Assert.AreEqual(operation.SourceAccount, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.SourceAccountMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.SourceAccountMuxedID, 5123456789);
            
            Assert.AreEqual(operation.Id, 3940808587743233L);

            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FromMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FromMuxedID, 5123456789);
            Assert.AreEqual(operation.To, "GDWNY2POLGK65VVKIH5KQSH7VWLKRTQ5M6ADLJAYC2UEHEBEARCZJWWI");
            Assert.AreEqual(operation.Amount, "100.0");
            Assert.AreEqual(operation.Asset, new AssetTypeNative());

            Assert.IsFalse(operation.TransactionSuccessful);
        }

        [TestMethod]
        public void TestDeserializeNonNativePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "paymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertNonNativePaymentData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeNonNativePaymentOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "paymentNonNative.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertNonNativePaymentData(back);
        }

        private static void AssertNonNativePaymentData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PaymentOperationResponse);
            var operation = (PaymentOperationResponse)instance;

            Assert.AreEqual(operation.From, "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA");
            Assert.AreEqual(operation.To, "GBHUSIZZ7FS2OMLZVZ4HLWJMXQ336NFSXHYERD7GG54NRITDTEWWBBI6");
            Assert.AreEqual(operation.Amount, "1000000000.0");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
        }

        [TestMethod]
        public void TestDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "allowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAllowTrustData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "allowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAllowTrustData(back);
        }

        private static void AssertAllowTrustData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AllowTrustOperationResponse);
            var operation = (AllowTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustor, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Trustee, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.TrusteeMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.TrusteeMuxedID, 5123456789);
            Assert.AreEqual(operation.Authorize, true);
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }

        [TestMethod]
        public void TestDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "changeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertChangeTrustData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "changeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertChangeTrustData(back);
        }

        private static void AssertChangeTrustData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ChangeTrustOperationResponse);
            var operation = (ChangeTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustee, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.TrustorMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.TrustorMuxedID, 5123456789);
            Assert.AreEqual(operation.Limit, "922337203685.4775807");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }

        [TestMethod]
        public void TestDeserializeSetOptionsOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "setOptions.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertSetOptionsData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSetOptionsOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "setOptions.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertSetOptionsData(back);
        }

        private static void AssertSetOptionsData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SetOptionsOperationResponse);
            var operation = (SetOptionsOperationResponse)instance;

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
            var json = File.ReadAllText(Path.Combine("testdata/operations", "setOptionsNonEd25519Key.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertSetOptionsOperationWithNonEd25519KeyData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSetOptionsOperationWithNonEd25519Key()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "setOptionsNonEd25519Key.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertSetOptionsOperationWithNonEd25519KeyData(back);
        }

        private static void AssertSetOptionsOperationWithNonEd25519KeyData(OperationResponse instance)
        {
            Assert.IsTrue(instance is SetOptionsOperationResponse);
            var operation = (SetOptionsOperationResponse)instance;

            Assert.AreEqual(operation.SignerKey, "TBGFYVCU76LJ7GZOCGR4X7DG2NV42JPG5CKRL42LA5FZOFI3U2WU7ZAL");
        }

        [TestMethod]
        public void TestDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "accountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAccountMergeData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "accountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAccountMergeData(back);
        }

        private static void AssertAccountMergeData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountMergeOperationResponse);
            var operation = (AccountMergeOperationResponse)instance;

            Assert.AreEqual(operation.Account, "GD6GKRABNDVYDETEZJQEPS7IBQMERCN44R5RCI4LJNX6BMYQM2KPGGZ2");
            Assert.AreEqual(operation.Into, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.IntoMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.IntoMuxedID, "5123456789");
        }

        [TestMethod]
        public void TestDeserializeManageOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageOfferData(back);
        }

        private static void AssertManageOfferData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageSellOfferOperationResponse);
            var operation = (ManageSellOfferOperationResponse)instance;

            Assert.AreEqual(operation.OfferId, "96052902");
            Assert.AreEqual(operation.Amount, "243.7500000");
            Assert.AreEqual(operation.Price, "8.0850240");
            Assert.AreEqual(operation.SellingAsset,
                Asset.CreateNonNativeAsset("USD", "GDSRCV5VTM3U7Y3L6DFRP3PEGBNQMGOWSRTGSBWX6Z3H6C7JHRI4XFJP"));
            Assert.AreEqual(operation.BuyingAsset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializeManageBuyOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageBuyOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageBuyOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageBuyOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageBuyOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageBuyOfferData(back);
        }

        //Before Horizon 1.0.0 the OfferID in the json was a long.
        [TestMethod]
        public void TestDeserializeManageBuyOfferOperationPre100()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageBuyOfferPre100.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageBuyOfferData(instance);
        }

        //Before Horizon 1.0.0 the OfferID in the json was a long.
        [TestMethod]
        public void TestSerializeDeserializeManageBuyOfferOperationPre100()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageBuyOfferPre100.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageBuyOfferData(back);
        }

        private static void AssertManageBuyOfferData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageBuyOfferOperationResponse);
            var operation = (ManageBuyOfferOperationResponse)instance;

            Assert.AreEqual(operation.OfferId, "1");
            Assert.AreEqual(operation.Amount, "50000.0000000");
            Assert.AreEqual(operation.Price, "0.0463000");
            Assert.AreEqual(operation.BuyingAsset,
                Asset.CreateNonNativeAsset("RMT", "GDEGOXPCHXWFYY234D2YZSPEJ24BX42ESJNVHY5H7TWWQSYRN5ZKZE3N"));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializePathPaymentStrictReceiveOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "pathPaymentStrictReceive.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPathPaymentStrictReceiveData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePathPaymentStrictReceiveOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "pathPaymentStrictReceive.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPathPaymentStrictReceiveData(back);
        }

        private static void AssertPathPaymentStrictReceiveData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PathPaymentStrictReceiveOperationResponse);
            var operation = (PathPaymentStrictReceiveOperationResponse)instance;

            PathPaymentStrictReceiveOperationResponse operationTest = new PathPaymentStrictReceiveOperationResponse(
                "GCXKG6RN4ONIEPCMNFB732A436Z5PNDSRLGWK7GBLCMQLIFO4S7EYWVU",
                "GA5WBPYA5Y4WAEHXWR2UKO2UO4BUGHUQ74EUPKON2QHV4WRHOIRNKKH2",
                "credit_alphanum4", "EUR", "GCQPYGH4K57XBDENKKX55KDTWOTK5WDWRQOH2LHEDX3EKVIQRLMESGBG",
                "10.0",
                "credit_alphanum4", "USD", "GC23QF2HUE52AMXUFUH3AYJAXXGXXV2VHXYYR6EYXETPKDXZSAW67XO4",
                "10.0",
                "10.0",
                new Asset[] { }
            );

            Assert.AreEqual(operation.From, operationTest.From);
            Assert.AreEqual(operation.To, operationTest.To);
            Assert.AreEqual(operation.Amount, operationTest.Amount);
            Assert.AreEqual(operation.SourceMax, operationTest.SourceMax);
            Assert.AreEqual(operation.SourceAmount, operationTest.SourceAmount);
            Assert.AreEqual(operation.DestinationAsset, Asset.CreateNonNativeAsset(operationTest.AssetType, operation.AssetIssuer, operationTest.AssetCode));
            Assert.AreEqual(operation.SourceAsset, Asset.CreateNonNativeAsset(operationTest.SourceAssetType, operation.SourceAssetIssuer, operationTest.SourceAssetCode));
        }

        [TestMethod]
        public void TestDeserializePathPaymentStrictSendOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "pathPaymentStrictSend.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertPathPaymentStrictSendData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializePathPaymentStrictSendOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "pathPaymentStrictSend.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertPathPaymentStrictSendData(back);
        }

        private static void AssertPathPaymentStrictSendData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is PathPaymentStrictSendOperationResponse);
            var operation = (PathPaymentStrictSendOperationResponse)instance;

            PathPaymentStrictSendOperationResponse operationTest = new PathPaymentStrictSendOperationResponse(
                "GCXVEEBWI4YMRK6AFJQSEUBYDQL4PZ24ECAPJE2ZIAPIQZLZIBAX3LIF",
                "GCXVEEBWI4YMRK6AFJQSEUBYDQL4PZ24ECAPJE2ZIAPIQZLZIBAX3LIF",
                "native", "", "",
                "0.0859000",
                "credit_alphanum4", "KIN", "GBDEVU63Y6NTHJQQZIKVTC23NWLQVP3WJ2RI2OTSJTNYOIGICST6DUXR",
                "1000.0000000",
                "0.0859000",
                new Asset[] { }
            );

            Assert.AreEqual(operation.From, operationTest.From);
            Assert.AreEqual(operation.To, operationTest.To);
            Assert.AreEqual(operation.Amount, operationTest.Amount);
            Assert.AreEqual(operation.SourceAmount, operationTest.SourceAmount);
            Assert.AreEqual(operation.DestinationMin, operationTest.DestinationMin);
            Assert.AreEqual(operation.DestinationAsset, Asset.Create(operationTest.AssetType, "", ""));
            Assert.AreEqual(operation.SourceAsset, Asset.CreateNonNativeAsset(operationTest.SourceAssetType, operationTest.SourceAssetIssuer, operationTest.SourceAssetCode));
        }

        [TestMethod]
        public void TestDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "passiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreatePassiveOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "passiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreatePassiveOfferData(back);
        }

        private static void AssertCreatePassiveOfferData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is CreatePassiveOfferOperationResponse);
            var operation = (CreatePassiveOfferOperationResponse)instance;

            Assert.AreEqual(operation.Amount, "11.27827");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("USD", "GDS5JW5E6DRSSN5XK4LW7E6VUMFKKE2HU5WCOVFTO7P2RP7OXVCBLJ3Y"));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }

        [TestMethod]
        public void TestDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "inflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertInflationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeInflationOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "inflation.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertInflationData(back);
        }

        private static void AssertInflationData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is InflationOperationResponse);
            var operation = (InflationOperationResponse)instance;

            Assert.AreEqual(operation.Id, 12884914177L);
        }

        [TestMethod]
        public void TestManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataData(back);
        }

        private static void AssertManageDataData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse)instance;

            Assert.AreEqual(operation.Id, 14336188517191688L);
            Assert.AreEqual(operation.Name, "CollateralValue");
            Assert.AreEqual(operation.Value, "MjAwMA==");
        }

        [TestMethod]
        public void TestDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertManageDataValueEmptyData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeManageDataOperationValueEmpty()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "manageDataValueEmpty.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertManageDataValueEmptyData(back);
        }

        private static void AssertManageDataValueEmptyData(OperationResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is ManageDataOperationResponse);
            var operation = (ManageDataOperationResponse)instance;

            Assert.AreEqual(operation.Value, null);
        }

        [TestMethod]
        public void TestDeserializeUnknownOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "operationUnknown.json"));
            Assert.ThrowsException<JsonSerializationException>(() =>
                JsonSingleton.GetInstance<OperationResponse>(json));
        }

        [TestMethod]
        public void TestDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "bumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertBumpSequenceData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeBumpSequenceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "bumpSequence.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertBumpSequenceData(back);
        }

        private static void AssertBumpSequenceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is BumpSequenceOperationResponse);
            var operation = (BumpSequenceOperationResponse)instance;

            Assert.AreEqual(12884914177L, operation.Id);
            Assert.AreEqual(79473726952833048L, operation.BumpTo);
        }

        //Create Claimable Balance
        [TestMethod]
        public void TestSerializationCreateClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "createClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreateClaimableBalanceData(back);
        }

        [TestMethod]
        public void TestSerializationCreateClaimableBalanceAbsBeforeMaxIntOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "claimableBalanceAbsBeforeMaxInt.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            Assert.IsTrue(instance is CreateClaimableBalanceOperationResponse);
            var operation = (CreateClaimableBalanceOperationResponse)instance;

            Assert.IsNotNull(operation.Claimants[0].Predicate.AbsBefore);
        }

        private static void AssertCreateClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is CreateClaimableBalanceOperationResponse);
            var operation = (CreateClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(213223651414017, operation.Id);
            Assert.AreEqual("GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", operation.Sponsor);
            Assert.AreEqual("native", operation.Asset);
            Assert.AreEqual("1.0000000", operation.Amount);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", operation.Claimants[0].Destination);

            var back = new CreateClaimableBalanceOperationResponse(
                operation.Sponsor, operation.Asset, operation.Amount, operation.Claimants);
            Assert.IsNotNull(back);
        }

        //Claim Claimable Balance
        [TestMethod]
        public void TestSerializationClaimClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "claimClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClaimClaimableBalanceData(back);
        }

        private static void AssertClaimClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClaimClaimableBalanceOperationResponse);
            var operation = (ClaimClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(214525026504705, operation.Id);
            Assert.AreEqual("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7", operation.BalanceID);
            Assert.AreEqual("GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2", operation.Claimant);
            Assert.AreEqual("MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24", operation.ClaimantMuxed);
            Assert.AreEqual(5123456789, operation.ClaimantMuxedID);

            var back = new ClaimClaimableBalanceOperationResponse(operation.BalanceID, operation.Claimant);
            Assert.IsNotNull(back);
        }

        //Begin Sponsoring Future Reserves
        [TestMethod]
        public void TestSerializationBeginSponsoringFutureReservesOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "beginSponsoringFutureReserves.json"));
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

        //End Sponsoring Future Reserves
        [TestMethod]
        public void TestSerializationEndSponsoringFutureReservesOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "endSponsoringFutureReserves.json"));
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
            Assert.AreEqual("MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24", operation.BeginSponsorMuxed);
            Assert.AreEqual(5123456789, operation.BeginSponsorMuxedID);

            var back = new EndSponsoringFutureReservesOperationResponse(operation.BeginSponsor);
            Assert.IsNotNull(back);
        }

        //Revoke Sponsorship Account ID
        [TestMethod]
        public void TestSerializationRevokeSponsorshipAccountIDOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipAccountID.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipAccountIDData(back);
        }

        private static void AssertRevokeSponsorshipAccountIDData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286156491067394, operation.Id);
            Assert.AreEqual("GCLHBHJAYWFT6JA27KEPUQCCGIHUB33HURYAKNWIY4FB7IY3K24PRXET", operation.AccountID);
        }

        //Revoke Sponsorship Claimable Balance
        [TestMethod]
        public void TestSerializationRevokeSponsorshipClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipClaimableBalanceData(back);
        }

        private static void AssertRevokeSponsorshipClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(287054139232258, operation.Id);
            Assert.AreEqual("00000000c582697b67cbec7f9ce64f4dc67bfb2bfd26318bb9f964f4d70e3f41f650b1e6", operation.ClaimableBalanceID);
        }

        //Revoke Sponsorship Data
        [TestMethod]
        public void TestSerializationRevokeSponsorshipDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipDataData(back);
        }

        private static void AssertRevokeSponsorshipDataData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286800736161794, operation.Id);
            Assert.AreEqual("GDHSYF7V3DZRM7Q2HS5J6FHAHNWETMBFMG7DOSWU3GA7OM4KGOPZM3FB", operation.DataAccountID);
            Assert.AreEqual("hello", operation.DataName);
        }

        //Revoke Sponsorship Offer
        [TestMethod]
        public void TestSerializationRevokeSponsorshipOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipOfferData(back);
        }

        private static void AssertRevokeSponsorshipOfferData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286800736161794, operation.Id);
            Assert.IsNull(operation.OfferID);
        }

        //Revoke Sponsorship Signer Key
        [TestMethod]
        public void TestSerializationRevokeSponsorshipSignerKey()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipSignerKey.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipSignerKeyData(back);
        }

        private static void AssertRevokeSponsorshipSignerKeyData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(287363376877570, operation.Id);
            Assert.AreEqual("GAXHU2XHSMTZYAKFCVTULAYUL34BFPPLRVJYZMEOHP7IWPZJKSVY67RJ", operation.SignerAccountID);
            Assert.AreEqual("XAMF7DNTEJY74JPVMGTPZE4LFYTEGBXMGBHNUUMAA7IXMSBGHAMWSND6", operation.SignerKey);
        }

        //Revoke Sponsorship Signer Key
        [TestMethod]
        public void TestSerializationRevokeSponsorshipTrustline()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "revokeSponsorshipTrustline.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipTrustlineData(back);
        }

        private static void AssertRevokeSponsorshipTrustlineData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286500088451074, operation.Id);
            Assert.AreEqual("GDHSYF7V3DZRM7Q2HS5J6FHAHNWETMBFMG7DOSWU3GA7OM4KGOPZM3FB", operation.TrustlineAccountID);
            Assert.AreEqual("XYZ:GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", operation.TrustlineAsset);
        }

        //Clawback
        [TestMethod]
        public void TestClawback()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "clawback.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClawbackData(back);
        }

        private static void AssertClawbackData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClawbackOperationResponse);
            var operation = (ClawbackOperationResponse)instance;

            Assert.AreEqual(3602979345141761, operation.Id);
            Assert.AreEqual(operation.Amount, "1000");
            Assert.AreEqual(operation.AssetCode, "EUR");
            Assert.AreEqual(operation.AssetIssuer, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.AssetType, "credit_alphanum4");
            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FromMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FromMuxedID, 5123456789);
            Assert.AreEqual(operation.Asset.ToQueryParameterEncodedString(), "EUR:GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
        }

        //Clawback Claimable Balance
        [TestMethod]
        public void TestClawbackClaimableBalance()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "clawbackClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClawbackClaimableBalanceData(back);
        }

        private static void AssertClawbackClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClawbackClaimableBalanceOperationResponse);
            var operation = (ClawbackClaimableBalanceOperationResponse)instance;


            Assert.AreEqual(214525026504705, operation.Id);
            Assert.AreEqual(new ClawbackClaimableBalanceOperationResponse("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7").BalanceID, operation.BalanceID);
        }

        //Set Trustline Flags
        [TestMethod]
        public void TestSetTrustlineFlags()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations", "setTrustlineFlags.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertTestSetTrustlineFlagsData(back);
        }

        private static void AssertTestSetTrustlineFlagsData(OperationResponse instance)
        {
            Assert.IsTrue(instance is SetTrustlineFlagsOperationResponse);
            var operation = (SetTrustlineFlagsOperationResponse)instance;
            var operation2 = new SetTrustlineFlagsOperationResponse("credit_alphanum4", "EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM", "GTRUSTORYHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM", new string[1] { "authorized" }, new string[1] { "authorized_to_maintain_liabilites" });

            Assert.AreEqual(operation.AssetType, operation2.AssetType);
            Assert.AreEqual(operation.AssetCode, operation2.AssetCode);
            Assert.AreEqual(operation.AssetIssuer, operation2.AssetIssuer);
            Assert.AreEqual(operation.Trustor, operation2.Trustor);
            Assert.AreEqual(operation.SetFlags[0], operation2.SetFlags[0]);
            Assert.AreEqual(operation.ClearFlags[0], operation2.ClearFlags[0]);
            Assert.AreEqual(operation.Asset, operation2.Asset);
        }
    }
}
