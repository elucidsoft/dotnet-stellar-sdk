using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.xdr;
using Asset = stellar_dotnet_sdk.Asset;
using Claimant = stellar_dotnet_sdk.Claimant;
using ClaimPredicate = stellar_dotnet_sdk.ClaimPredicate;
using LedgerKey = stellar_dotnet_sdk.LedgerKey;
using Operation = stellar_dotnet_sdk.Operation;
using Price = stellar_dotnet_sdk.Price;
using Signer = stellar_dotnet_sdk.Signer;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class OperationTest
    {
        [TestMethod]
        [Obsolete]
        public void TestCreateAccountOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");

            const string startingAmount = "1000";
            var operation = new CreateAccountOperation.Builder(destination, startingAmount)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (CreateAccountOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(10000000000L, xdr.Body.CreateAccountOp.StartingBalance.InnerValue);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(startingAmount, parsedOperation.StartingBalance);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAACVAvkAA==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestPaymentOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");

            Asset asset = new AssetTypeNative();
            var amount = "1000";

            var operation = new PaymentOperation.Builder(destination, asset, amount)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (PaymentOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(10000000000L, xdr.Body.PaymentOp.Amount.InnerValue);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.IsTrue(parsedOperation.Asset is AssetTypeNative);
            Assert.AreEqual(amount, parsedOperation.Amount);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAEAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAAAAAAAlQL5AA=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestPathPaymentStrictReceiveOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");
            // GCGZLB3X2B3UFOFSHHQ6ZGEPEX7XYPEH6SBFMIV74EUDOFZJA3VNL6X4
            var issuer = KeyPair.FromSecretSeed("SBOBVZUN6WKVMI6KIL2GHBBEETEV6XKQGILITNH6LO6ZA22DBMSDCPAG");

            // GAVAQKT2M7B4V3NN7RNNXPU5CWNDKC27MYHKLF5UNYXH4FNLFVDXKRSV
            var pathIssuer1 = KeyPair.FromSecretSeed("SALDLG5XU5AEJWUOHAJPSC4HJ2IK3Z6BXXP4GWRHFT7P7ILSCFFQ7TC5");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var pathIssuer2 = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset sendAsset = new AssetTypeNative();
            var sendMax = "0.0001";
            Asset destAsset = new AssetTypeCreditAlphaNum4("USD", issuer.AccountId);
            var destAmount = "0.0001";
            Asset[] path = { new AssetTypeCreditAlphaNum4("USD", pathIssuer1.AccountId), new AssetTypeCreditAlphaNum12("TESTTEST", pathIssuer2.AccountId) };

            var operation = new PathPaymentStrictReceiveOperation.Builder(
                    sendAsset, sendMax, destination, destAsset, destAmount)
                .SetPath(path)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (PathPaymentStrictReceiveOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(1000L, xdr.Body.PathPaymentStrictReceiveOp.SendMax.InnerValue);
            Assert.AreEqual(1000L, xdr.Body.PathPaymentStrictReceiveOp.DestAmount.InnerValue);
            Assert.IsTrue(parsedOperation.SendAsset is AssetTypeNative);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(sendMax, parsedOperation.SendMax);
            Assert.IsTrue(parsedOperation.DestAsset is AssetTypeCreditAlphaNum4);
            Assert.AreEqual(destAmount, parsedOperation.DestAmount);
            Assert.AreEqual(path.Length, parsedOperation.Path.Length);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAIAAAAAAAAAAAAAA+gAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAABVVNEAAAAAACNlYd30HdCuLI54eyYjyX/fDyH9IJWIr/hKDcXKQbq1QAAAAAAAAPoAAAAAgAAAAFVU0QAAAAAACoIKnpnw8rtrfxa276dFZo1C19mDqWXtG4ufhWrLUd1AAAAAlRFU1RURVNUAAAAAAAAAABE/ttVl8BLV0csW/xgXtbXOVf1lMyDluMiafl0IDVFIg==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestPathPaymentStrictReceiveEmptyPathOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");
            // GCGZLB3X2B3UFOFSHHQ6ZGEPEX7XYPEH6SBFMIV74EUDOFZJA3VNL6X4
            var issuer = KeyPair.FromSecretSeed("SBOBVZUN6WKVMI6KIL2GHBBEETEV6XKQGILITNH6LO6ZA22DBMSDCPAG");

            // GAVAQKT2M7B4V3NN7RNNXPU5CWNDKC27MYHKLF5UNYXH4FNLFVDXKRSV
            var unused1 = KeyPair.FromSecretSeed("SALDLG5XU5AEJWUOHAJPSC4HJ2IK3Z6BXXP4GWRHFT7P7ILSCFFQ7TC5");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var unused = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset sendAsset = new AssetTypeNative();
            var sendMax = "0.0001";
            Asset destAsset = new AssetTypeCreditAlphaNum4("USD", issuer.AccountId);
            var destAmount = "0.0001";

            var operation = new PathPaymentStrictReceiveOperation.Builder(
                    sendAsset, sendMax, destination, destAsset, destAmount)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (PathPaymentStrictReceiveOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(1000L, xdr.Body.PathPaymentStrictReceiveOp.SendMax.InnerValue);
            Assert.AreEqual(1000L, xdr.Body.PathPaymentStrictReceiveOp.DestAmount.InnerValue);
            Assert.IsTrue(parsedOperation.SendAsset is AssetTypeNative);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(sendMax, parsedOperation.SendMax);
            Assert.IsTrue(parsedOperation.DestAsset is AssetTypeCreditAlphaNum4);
            Assert.AreEqual(destAmount, parsedOperation.DestAmount);
            Assert.AreEqual(0, parsedOperation.Path.Length);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAIAAAAAAAAAAAAAA+gAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAABVVNEAAAAAACNlYd30HdCuLI54eyYjyX/fDyH9IJWIr/hKDcXKQbq1QAAAAAAAAPoAAAAAA==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestPathPaymentStrictSendOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");
            // GCGZLB3X2B3UFOFSHHQ6ZGEPEX7XYPEH6SBFMIV74EUDOFZJA3VNL6X4
            var issuer = KeyPair.FromSecretSeed("SBOBVZUN6WKVMI6KIL2GHBBEETEV6XKQGILITNH6LO6ZA22DBMSDCPAG");

            // GAVAQKT2M7B4V3NN7RNNXPU5CWNDKC27MYHKLF5UNYXH4FNLFVDXKRSV
            var pathIssuer1 = KeyPair.FromSecretSeed("SALDLG5XU5AEJWUOHAJPSC4HJ2IK3Z6BXXP4GWRHFT7P7ILSCFFQ7TC5");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var pathIssuer2 = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset sendAsset = new AssetTypeNative();
            var sendAmount = "0.0001";
            Asset destAsset = new AssetTypeCreditAlphaNum4("USD", issuer.AccountId);
            var destMin = "0.0001";
            Asset[] path = { new AssetTypeCreditAlphaNum4("USD", pathIssuer1.AccountId), new AssetTypeCreditAlphaNum12("TESTTEST", pathIssuer2.AccountId) };

            var operation = new PathPaymentStrictSendOperation.Builder(
                    sendAsset, sendAmount, destination, destAsset, destMin)
                .SetPath(path)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (PathPaymentStrictSendOperation)Operation.FromXdr(xdr);

            Assert.IsTrue(parsedOperation.SendAsset is AssetTypeNative);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(sendAmount, parsedOperation.SendAmount);
            Assert.IsTrue(parsedOperation.DestAsset is AssetTypeCreditAlphaNum4);
            Assert.AreEqual(destMin, parsedOperation.DestMin);
            Assert.AreEqual(path.Length, parsedOperation.Path.Length);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAA0AAAAAAAAAAAAAA+gAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAABVVNEAAAAAACNlYd30HdCuLI54eyYjyX/fDyH9IJWIr/hKDcXKQbq1QAAAAAAAAPoAAAAAgAAAAFVU0QAAAAAACoIKnpnw8rtrfxa276dFZo1C19mDqWXtG4ufhWrLUd1AAAAAlRFU1RURVNUAAAAAAAAAABE/ttVl8BLV0csW/xgXtbXOVf1lMyDluMiafl0IDVFIg==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestPathPaymentStrictSendEmptyPathOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");
            // GCGZLB3X2B3UFOFSHHQ6ZGEPEX7XYPEH6SBFMIV74EUDOFZJA3VNL6X4
            var issuer = KeyPair.FromSecretSeed("SBOBVZUN6WKVMI6KIL2GHBBEETEV6XKQGILITNH6LO6ZA22DBMSDCPAG");

            // GAVAQKT2M7B4V3NN7RNNXPU5CWNDKC27MYHKLF5UNYXH4FNLFVDXKRSV
            var unused1 = KeyPair.FromSecretSeed("SALDLG5XU5AEJWUOHAJPSC4HJ2IK3Z6BXXP4GWRHFT7P7ILSCFFQ7TC5");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var unused = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset sendAsset = new AssetTypeNative();
            var sendAmount = "0.0001";
            Asset destAsset = new AssetTypeCreditAlphaNum4("USD", issuer.AccountId);
            var destMin = "0.0001";

            var operation = new PathPaymentStrictSendOperation.Builder(
                    sendAsset, sendAmount, destination, destAsset, destMin)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (PathPaymentStrictSendOperation)Operation.FromXdr(xdr);

            Assert.IsTrue(parsedOperation.SendAsset is AssetTypeNative);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(sendAmount, parsedOperation.SendAmount);
            Assert.IsTrue(parsedOperation.DestAsset is AssetTypeCreditAlphaNum4);
            Assert.AreEqual(destMin, parsedOperation.DestMin);
            Assert.AreEqual(0, parsedOperation.Path.Length);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAA0AAAAAAAAAAAAAA+gAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAABVVNEAAAAAACNlYd30HdCuLI54eyYjyX/fDyH9IJWIr/hKDcXKQbq1QAAAAAAAAPoAAAAAA==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestChangeTrustOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            stellar_dotnet_sdk.ChangeTrustAsset asset = stellar_dotnet_sdk.ChangeTrustAsset.Create(new AssetTypeNative());
            var limit = ChangeTrustOperation.MaxLimit;

            var operation = new ChangeTrustOperation.Builder(asset, limit)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (ChangeTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(long.MaxValue, xdr.Body.ChangeTrustOp.Limit.InnerValue);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.IsTrue(((stellar_dotnet_sdk.ChangeTrustAsset.Wrapper)parsedOperation.Asset).Asset is AssetTypeNative);
            Assert.AreEqual(limit, parsedOperation.Limit);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAYAAAAAf/////////8=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestChangeTrustOperationNoLimit()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            stellar_dotnet_sdk.ChangeTrustAsset asset = stellar_dotnet_sdk.ChangeTrustAsset.Create(new AssetTypeNative());

            var operation = new ChangeTrustOperation.Builder(asset)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (ChangeTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(long.MaxValue, xdr.Body.ChangeTrustOp.Limit.InnerValue);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.IsTrue(((stellar_dotnet_sdk.ChangeTrustAsset.Wrapper)parsedOperation.Asset).Asset is AssetTypeNative);
            Assert.AreEqual(ChangeTrustOperation.MaxLimit, parsedOperation.Limit);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAYAAAAAf/////////8=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestAllowTrustOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var trustor = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");

            const string assetCode = "USDA";

            var operation = new AllowTrustOperation.Builder(trustor, assetCode, true, true)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (AllowTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(trustor.AccountId, parsedOperation.Trustor.AccountId);
            Assert.AreEqual(assetCode, parsedOperation.AssetCode);

            Assert.AreEqual(OperationThreshold.Low, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAcAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAABVVNEQQAAAAE=",
                operation.ToXdrBase64());

            TestAllowTrustOperationAuthorize(source, trustor, assetCode);
        }

        [Obsolete]
        private static void TestAllowTrustOperationAuthorize(KeyPair source, KeyPair trustor, string assetCode)
        {
            AllowTrustOperation operation = null;
            stellar_dotnet_sdk.xdr.Operation xdr = null;
            AllowTrustOperation parsedOperation = null;

            //Authorize: true, MaintainLiabilities: false -> true, false
            operation = new AllowTrustOperation.Builder(trustor, assetCode, true, false)
               .SetSourceAccount(source)
               .Build();

            xdr = operation.ToXdr();
            parsedOperation = (AllowTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(true, parsedOperation.Authorize);
            Assert.AreEqual(false, parsedOperation.AuthorizeToMaintainLiabilities);

            //Authorize: false, MaintainLiabilities: true -> false, true
            operation = new AllowTrustOperation.Builder(trustor, assetCode, false, true)
               .SetSourceAccount(source)
               .Build();

            xdr = operation.ToXdr();
            parsedOperation = (AllowTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(false, parsedOperation.Authorize);
            Assert.AreEqual(true, parsedOperation.AuthorizeToMaintainLiabilities);

            //Authorize: true, MaintainLiabilities: true -> true, false
            operation = new AllowTrustOperation.Builder(trustor, assetCode, true, true)
               .SetSourceAccount(source)
               .Build();

            xdr = operation.ToXdr();
            parsedOperation = (AllowTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(true, parsedOperation.Authorize);
            Assert.AreEqual(false, parsedOperation.AuthorizeToMaintainLiabilities);

            //Authorize: false, MaintainLiabilities: false -> false, false
            operation = new AllowTrustOperation.Builder(trustor, assetCode, false, false)
                .SetSourceAccount(source)
                .Build();

            xdr = operation.ToXdr();
            parsedOperation = (AllowTrustOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(false, parsedOperation.Authorize);
            Assert.AreEqual(false, parsedOperation.AuthorizeToMaintainLiabilities);
        }
 
        [TestMethod]
        public void TestManageOfferOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var issuer = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset selling = new AssetTypeNative();
            var buying = Asset.CreateNonNativeAsset("USD", issuer.AccountId);
            var amount = "0.00001";
            var price = "0.85334384"; // n=5333399 d=6250000
            var priceObj = Price.FromString(price);
            long offerId = 1;

            var operation = new ManageSellOfferOperation.Builder(selling, buying, amount, price)
                .SetOfferId(offerId)
                .SetSourceAccount(source)
                .Build();

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAMAAAAAAAAAAVVTRAAAAAAARP7bVZfAS1dHLFv8YF7W1zlX9ZTMg5bjImn5dCA1RSIAAAAAAAAAZABRYZcAX14QAAAAAAAAAAE=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestManageSellOfferOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var issuer = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset selling = new AssetTypeNative();
            var buying = Asset.CreateNonNativeAsset("USD", issuer.AccountId);
            var amount = "0.00001";
            var price = "0.85334384"; // n=5333399 d=6250000
            var priceObj = Price.FromString(price);
            long offerId = 1;

            var operation = new ManageSellOfferOperation.Builder(selling, buying, amount, price)
                .SetOfferId(offerId)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (ManageSellOfferOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(100L, xdr.Body.ManageSellOfferOp.Amount.InnerValue);
            Assert.IsTrue(parsedOperation.Selling is AssetTypeNative);
            Assert.IsTrue(parsedOperation.Buying is AssetTypeCreditAlphaNum4);
            Assert.IsTrue(parsedOperation.Buying.Equals(buying));
            Assert.AreEqual(amount, parsedOperation.Amount);
            Assert.AreEqual(price, parsedOperation.Price);
            Assert.AreEqual(priceObj.Numerator, 5333399);
            Assert.AreEqual(priceObj.Denominator, 6250000);
            Assert.AreEqual(offerId, parsedOperation.OfferId);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAMAAAAAAAAAAVVTRAAAAAAARP7bVZfAS1dHLFv8YF7W1zlX9ZTMg5bjImn5dCA1RSIAAAAAAAAAZABRYZcAX14QAAAAAAAAAAE=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestManageBuyOfferOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var issuer = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset selling = new AssetTypeNative();
            var buying = Asset.CreateNonNativeAsset("USD", issuer.AccountId);
            var amount = "0.00001";
            var price = "0.85334384"; // n=5333399 d=6250000
            var priceObj = Price.FromString(price);
            long offerId = 1;

            var operation = new ManageBuyOfferOperation.Builder(selling, buying, amount, price)
                .SetOfferId(offerId)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (ManageBuyOfferOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(100L, xdr.Body.ManageBuyOfferOp.BuyAmount.InnerValue);
            Assert.IsTrue(parsedOperation.Selling is AssetTypeNative);
            Assert.IsTrue(parsedOperation.Buying is AssetTypeCreditAlphaNum4);
            Assert.IsTrue(parsedOperation.Buying.Equals(buying));
            Assert.AreEqual(amount, parsedOperation.BuyAmount);
            Assert.AreEqual(price, parsedOperation.Price);
            Assert.AreEqual(priceObj.Numerator, 5333399);
            Assert.AreEqual(priceObj.Denominator, 6250000);
            Assert.AreEqual(offerId, parsedOperation.OfferId);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAwAAAAAAAAAAVVTRAAAAAAARP7bVZfAS1dHLFv8YF7W1zlX9ZTMg5bjImn5dCA1RSIAAAAAAAAAZABRYZcAX14QAAAAAAAAAAE=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestCreatePassiveOfferOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var issuer = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset selling = new AssetTypeNative();
            var buying = Asset.CreateNonNativeAsset("USD", issuer.AccountId);
            var amount = "0.00001";
            var price = "2.93850088"; // n=36731261 d=12500000
            var priceObj = Price.FromString(price);

            var operation = new CreatePassiveSellOfferOperation.Builder(selling, buying, amount, price)
                .SetSourceAccount(source)
                .Build();

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAQAAAAAAAAAAVVTRAAAAAAARP7bVZfAS1dHLFv8YF7W1zlX9ZTMg5bjImn5dCA1RSIAAAAAAAAAZAIweX0Avrwg",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestCreatePassiveSellOfferOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var issuer = KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ");

            Asset selling = new AssetTypeNative();
            var buying = Asset.CreateNonNativeAsset("USD", issuer.AccountId);
            var amount = "0.00001";
            var price = "2.93850088"; // n=36731261 d=12500000
            var priceObj = Price.FromString(price);

            var operation = new CreatePassiveSellOfferOperation.Builder(selling, buying, amount, price)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (CreatePassiveSellOfferOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(100L, xdr.Body.CreatePassiveSellOfferOp.Amount.InnerValue);
            Assert.IsTrue(parsedOperation.Selling is AssetTypeNative);
            Assert.IsTrue(parsedOperation.Buying is AssetTypeCreditAlphaNum4);
            Assert.IsTrue(parsedOperation.Buying.Equals(buying));
            Assert.AreEqual(amount, parsedOperation.Amount);
            Assert.AreEqual(price, parsedOperation.Price);
            Assert.AreEqual(priceObj.Numerator, 36731261);
            Assert.AreEqual(priceObj.Denominator, 12500000);
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAQAAAAAAAAAAVVTRAAAAAAARP7bVZfAS1dHLFv8YF7W1zlX9ZTMg5bjImn5dCA1RSIAAAAAAAAAZAIweX0Avrwg",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestAccountMergeOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var destination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");

            var operation = new AccountMergeOperation.Builder(destination)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (AccountMergeOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(destination.AccountId, parsedOperation.Destination.AccountId);
            Assert.AreEqual(OperationThreshold.High, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAgAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxA=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestManageDataOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var operation = new ManageDataOperation.Builder("test", new byte[] { 0, 1, 2, 3, 4 })
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ManageDataOperation)Operation.FromXdr(xdr);

            Assert.AreEqual("test", parsedOperation.Name);
            Assert.IsTrue(new byte[] { 0, 1, 2, 3, 4 }.SequenceEqual(parsedOperation.Value));
            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAoAAAAEdGVzdAAAAAEAAAAFAAECAwQAAAA=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestManageDataOperationEmptyValue()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var operation = new ManageDataOperation.Builder("test", null)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ManageDataOperation)Operation.FromXdr(xdr);

            Assert.AreEqual("test", parsedOperation.Name);
            Assert.AreEqual(null, parsedOperation.Value);

            Assert.AreEqual("AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAoAAAAEdGVzdAAAAAA=",
                operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestToXdrAmount()
        {
            Assert.AreEqual(0L, Operation.ToXdrAmount("0"));
            Assert.AreEqual(1L, Operation.ToXdrAmount("0.0000001"));
            Assert.AreEqual(10000000L, Operation.ToXdrAmount("1"));
            Assert.AreEqual(11234567L, Operation.ToXdrAmount("1.1234567"));
            Assert.AreEqual(729912843007381L, Operation.ToXdrAmount("72991284.3007381"));
            Assert.AreEqual(729912843007381L, Operation.ToXdrAmount("72991284.30073810"));
            Assert.AreEqual(1014016711446800155L, Operation.ToXdrAmount("101401671144.6800155"));
            Assert.AreEqual(9223372036854775807L, Operation.ToXdrAmount("922337203685.4775807"));

            try
            {
                Operation.ToXdrAmount("0.00000001");
                Assert.Fail();
            }
            catch (ArithmeticException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                Operation.ToXdrAmount("72991284.30073811");
                Assert.Fail();
            }
            catch (ArithmeticException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Obsolete]
        public void TestBumpSequence()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var operation = new BumpSequenceOperation.Builder(156L)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (BumpSequenceOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(156L, parsedOperation.BumpTo);
            Assert.AreEqual(OperationThreshold.Low, parsedOperation.Threshold);

            Assert.AreEqual("AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAsAAAAAAAAAnA==", operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestInflationOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var operation = new InflationOperation.Builder()
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (InflationOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAk=", operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestCreateClaimableBalanceOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            // GAS4V4O2B7DW5T7IQRPEEVCRXMDZESKISR7DVIGKZQYYV3OSQ5SH5LVP
            var destination = KeyPair.FromSecretSeed("SBMSVD4KKELKGZXHBUQTIROWUAPQASDX7KEJITARP4VMZ6KLUHOGPTYW");

            var asset = new AssetTypeNative();
            var claimant = new Claimant
            {
                Destination = destination,
                Predicate = ClaimPredicate.Not(ClaimPredicate.BeforeRelativeTime(TimeSpan.FromHours(7.0))),
            };

            var operation = new CreateClaimableBalanceOperation.Builder(asset, "123.45", new[] { claimant })
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (CreateClaimableBalanceOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAAA4AAAAAAAAAAEmU+aAAAAABAAAAAAAAAAAlyvHaD8duz+iEXkJUUbsHkklIlH46oMrMMYrt0odkfgAAAAMAAAABAAAABQAAAAAAAGJw", operation.ToXdrBase64());
        }

        /// <summary>
        /// The API didn't previously did not support the balance id type within the balance id (expected 32 bytes rather than the full 36).
        /// This tests that we can still pass in the 32 bytes for compatability and use the default type (0).
        /// </summary>
        [TestMethod]
        [Obsolete]
        public void TestClaimClaimableBalanceWithLegacyByteIdOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            var balanceId = Enumerable.Repeat((byte)0x07, 32).ToArray();
            var operation = new ClaimClaimableBalanceOperation.Builder(balanceId)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ClaimClaimableBalanceOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAAA8AAAAABwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwc=", operation.ToXdrBase64());
        }

        /// <summary>
        /// Claim a claimable balance using the byte representation of the balance id.
        /// </summary>
        [TestMethod]
        [Obsolete]
        public void TestClaimClaimableBalanceWithByteIdOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            // Prepend type bytes (first four bytes are the balance id type)
            var balanceId = Enumerable.Repeat((byte)0x07, 32).Prepend((byte)0).Prepend((byte)0).Prepend((byte)0).Prepend((byte)0).ToArray();
            var operation = new ClaimClaimableBalanceOperation.Builder(balanceId)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ClaimClaimableBalanceOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAAA8AAAAABwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwcHBwc=", operation.ToXdrBase64());
        }

        /// <summary>
        /// Claim a claimable balance using the string representation of the balance id.
        /// </summary>
        [TestMethod]
        [Obsolete]
        public void TestClaimClaimableBalanceWithStringIdOperationValid()
        {
            var balanceId = "000000006d6a0c142516a9cc7885a85c5aba3a1f4af5181cf9e7a809ac7ae5e4a58c825f";
            var accountId = KeyPair.FromAccountId("GABTTS6N4CT7AUN4LD7IFIUMRD5PSMCW6QTLIQNEFZDEI6ZQVUCQMCLN");
            var operation = new ClaimClaimableBalanceOperation.Builder(balanceId).SetSourceAccount(accountId).Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (ClaimClaimableBalanceOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);
            CollectionAssert.AreEqual(operation.BalanceId, parsedOperation.BalanceId);

            Assert.AreEqual(
                    "AAAAAQAAAAADOcvN4KfwUbxY/oKijIj6+TBW9Ca0QaQuRkR7MK0FBgAAAA8AAAAAbWoMFCUWqcx4hahcWro6H0r1GBz556gJrHrl5KWMgl8=",
                    operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestClaimClaimableBalanceOperationInvalidEmptyBalanceId()
        {
            var balanceId = "";
            var accountId = KeyPair.FromAccountId("GABTTS6N4CT7AUN4LD7IFIUMRD5PSMCW6QTLIQNEFZDEI6ZQVUCQMCLN");

            Assert.ThrowsException<ArgumentException>(() =>
                new ClaimClaimableBalanceOperation.Builder(balanceId).SetSourceAccount(accountId).Build());
        }

        /// <summary>
        /// The first 4 bytes of the balance id are the balance id type, these are required. The default is 0x00000000.
        /// </summary>
        [TestMethod]
        public void TestClaimClaimableBalanceOperationInvalidClaimableBalanceIDTypeMissing()
        {
            var balanceId = "6d6a0c142516a9cc7885a85c5aba3a1f4af5181cf9e7a809ac7ae5e4a58c825f";
            var accountId = KeyPair.FromAccountId("GABTTS6N4CT7AUN4LD7IFIUMRD5PSMCW6QTLIQNEFZDEI6ZQVUCQMCLN");

            Assert.ThrowsException<ArgumentException>(() =>
                new ClaimClaimableBalanceOperation.Builder(balanceId).SetSourceAccount(accountId).Build());
        }

        /// <summary>
        /// The last 32 bytes of the balance id are the balance id body, this is required.
        /// </summary>
        [TestMethod]
        public void TestClaimClaimableBalanceOperationInvalidClaimableBalanceIDBodyMissing()
        {
            var balanceId = "00000000";
            var accountId = KeyPair.FromAccountId("GABTTS6N4CT7AUN4LD7IFIUMRD5PSMCW6QTLIQNEFZDEI6ZQVUCQMCLN");

            Assert.ThrowsException<ArgumentException>(() =>
                new ClaimClaimableBalanceOperation.Builder(balanceId).SetSourceAccount(accountId).Build());
        }

        [TestMethod]
        [Obsolete]
        public void TestBeginSponsoringFutureReservesOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            // GAS4V4O2B7DW5T7IQRPEEVCRXMDZESKISR7DVIGKZQYYV3OSQ5SH5LVP
            var sponsored = KeyPair.FromSecretSeed("SBMSVD4KKELKGZXHBUQTIROWUAPQASDX7KEJITARP4VMZ6KLUHOGPTYW");

            var operation = new BeginSponsoringFutureReservesOperation.Builder(sponsored)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (BeginSponsoringFutureReservesOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAABAAAAAAJcrx2g/Hbs/ohF5CVFG7B5JJSJR+OqDKzDGK7dKHZH4=", operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestEndSponsoringFutureReservesOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            var operation = new EndSponsoringFutureReservesOperation.Builder()
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (EndSponsoringFutureReservesOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAABE=", operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestRevokeLedgerEntrySponsorshipOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            // GAS4V4O2B7DW5T7IQRPEEVCRXMDZESKISR7DVIGKZQYYV3OSQ5SH5LVP
            var otherAccount = KeyPair.FromSecretSeed("SBMSVD4KKELKGZXHBUQTIROWUAPQASDX7KEJITARP4VMZ6KLUHOGPTYW");

            var ledgerKey = LedgerKey.Account(otherAccount);
            var operation = new RevokeLedgerEntrySponsorshipOperation.Builder(ledgerKey)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (RevokeLedgerEntrySponsorshipOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAABIAAAAAAAAAAAAAAAAlyvHaD8duz+iEXkJUUbsHkklIlH46oMrMMYrt0odkfg==", operation.ToXdrBase64());
        }

        [TestMethod]
        [Obsolete]
        public void TestRevokeSignerSponsorshipOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            // GAS4V4O2B7DW5T7IQRPEEVCRXMDZESKISR7DVIGKZQYYV3OSQ5SH5LVP
            var otherAccount = KeyPair.FromSecretSeed("SBMSVD4KKELKGZXHBUQTIROWUAPQASDX7KEJITARP4VMZ6KLUHOGPTYW");

            var signerKey = Signer.Ed25519PublicKey(otherAccount);

            var operation = new RevokeSignerSponsorshipOperation.Builder(otherAccount, signerKey)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (RevokeSignerSponsorshipOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual("AAAAAQAAAADg3G3hclysZlFitS+s5zWyiiJD5B0STWy5LXCj6i5yxQAAABIAAAABAAAAACXK8doPx27P6IReQlRRuweSSUiUfjqgyswxiu3Sh2R+AAAAACXK8doPx27P6IReQlRRuweSSUiUfjqgyswxiu3Sh2R+", operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestFromXdrAmount()
        {
            Assert.AreEqual("0", Operation.FromXdrAmount(0L));
            Assert.AreEqual("0.0000001", Operation.FromXdrAmount(1L));
            Assert.AreEqual("1", Operation.FromXdrAmount(10000000L));
            Assert.AreEqual("1.1234567", Operation.FromXdrAmount(11234567L));
            Assert.AreEqual("72991284.3007381", Operation.FromXdrAmount(729912843007381L));
            Assert.AreEqual("101401671144.6800155", Operation.FromXdrAmount(1014016711446800155L));
            Assert.AreEqual("922337203685.4775807", Operation.FromXdrAmount(9223372036854775807L));
        }

        [TestMethod]
        [Obsolete]
        public void TestClawbackOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            var operation = new ClawbackOperation.Builder(Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"), "1000", KeyPair.FromAccountId("GCFRHRU5YRI3IN3IMRMYGWWEG2PX2B6MYH2RJW7NEDE2PTYPISPT3RU7"))
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ClawbackOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(operation.Amount, parsedOperation.Amount);
            Assert.AreEqual(operation.Asset, parsedOperation.Asset);
            Assert.AreEqual(operation.From.AccountId, parsedOperation.From.AccountId);
        }


        [TestMethod]
        [Obsolete]
        public void TestClawbackClaimableBalanceOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            var operation = new ClawbackClaimableBalanceOperation.Builder("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7")
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (ClawbackClaimableBalanceOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(operation.BalanceId.Length, parsedOperation.BalanceId.Length);
        }

        [TestMethod]
        public void TestClawbackClaimableBalanceOperationLengthNotCorrect()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            try
            {
                var operation = new ClawbackClaimableBalanceOperation.Builder(new byte[34])
                    .SetSourceAccount(source)
                    .Build();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, new ArgumentException("Must be 36 bytes long", "balanceId").Message);
            }
        }

        [TestMethod]
        [Obsolete]
        public void TestSetTrustlineFlagsOperation()
        {
            // GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3
            var source = KeyPair.FromSecretSeed("SBPQUZ6G4FZNWFHKUWC5BEYWF6R52E3SEP7R3GWYSM2XTKGF5LNTWW4R");

            var operation = new SetTrustlineFlagsOperation.Builder(Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"), KeyPair.Random(), 1, 1)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();

            var parsedOperation = (SetTrustlineFlagsOperation)Operation.FromXdr(xdr);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(operation.Asset, parsedOperation.Asset);
            Assert.AreEqual(operation.Trustor.AccountId, parsedOperation.Trustor.AccountId);
            Assert.AreEqual(operation.SetFlags, parsedOperation.SetFlags);
            Assert.AreEqual(operation.ClearFlags, parsedOperation.ClearFlags);
        }
    }
}