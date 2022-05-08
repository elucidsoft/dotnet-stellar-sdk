using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;
using System;
using System.Linq;
using System.Text;

namespace stellar_dotnet_sdk_test.operations
{
    [TestClass]
    public class SetOptionsOperationTest
    {
        public KeyPair source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

        [TestMethod]
        public void TestSetOptionsOperation()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");
            // GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR
            var inflationDestination = KeyPair.FromSecretSeed("SDHZGHURAYXKU2KMVHPOXI6JG2Q4BSQUQCEOY72O3QQTCLR2T455PMII");
            // GBCP5W2VS7AEWV2HFRN7YYC623LTSV7VSTGIHFXDEJU7S5BAGVCSETRR
            var signer = Signer.Ed25519PublicKey(KeyPair.FromSecretSeed("SA64U7C5C7BS5IHWEPA7YWFN3Z6FE5L6KAMYUIT4AQ7KVTVLD23C6HEZ"));

            var clearFlags = 1;
            var setFlags = 1;
            var masterKeyWeight = 1;
            var lowThreshold = 2;
            var mediumThreshold = 3;
            var highThreshold = 4;
            var homeDomain = "stellar.org";
            var signerWeight = 1;

            var operation = new SetOptionsOperation.Builder()
                .SetInflationDestination(inflationDestination)
                .SetClearFlags(clearFlags)
                .SetSetFlags(setFlags)
                .SetMasterKeyWeight(masterKeyWeight)
                .SetLowThreshold(lowThreshold)
                .SetMediumThreshold(mediumThreshold)
                .SetHighThreshold(highThreshold)
                .SetHomeDomain(homeDomain)
                .SetSigner(signer, signerWeight)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (SetOptionsOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(inflationDestination.AccountId, parsedOperation.InflationDestination.AccountId);
            Assert.AreEqual(1U, parsedOperation.ClearFlags);
            Assert.AreEqual(1U, parsedOperation.SetFlags);
            Assert.AreEqual(1U, parsedOperation.MasterKeyWeight);
            Assert.AreEqual(2U, parsedOperation.LowThreshold);
            Assert.AreEqual(3U, parsedOperation.MediumThreshold);
            Assert.AreEqual(4U, parsedOperation.HighThreshold);
            Assert.AreEqual(homeDomain, parsedOperation.HomeDomain);
            Assert.AreEqual(signer.Discriminant.InnerValue, parsedOperation.Signer.Discriminant.InnerValue);
            Assert.AreEqual(signer.Ed25519.InnerValue, parsedOperation.Signer.Ed25519.InnerValue);
            Assert.AreEqual(1U, parsedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(OperationThreshold.High, parsedOperation.Threshold);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAABAAAAAO3gUmG83C+VCqO6FztuMtXJF/l7grZA7MjRzqdZ9W8QAAAAAQAAAAEAAAABAAAAAQAAAAEAAAABAAAAAQAAAAIAAAABAAAAAwAAAAEAAAAEAAAAAQAAAAtzdGVsbGFyLm9yZwAAAAABAAAAAET+21WXwEtXRyxb/GBe1tc5V/WUzIOW4yJp+XQgNUUiAAAAAQ==",
                operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestSetOptionsOperationSingleField()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var homeDomain = "stellar.org";

            var operation = new SetOptionsOperation.Builder()
                .SetHomeDomain(homeDomain)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (SetOptionsOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(null, parsedOperation.InflationDestination);
            Assert.AreEqual(null, parsedOperation.ClearFlags);
            Assert.AreEqual(null, parsedOperation.SetFlags);
            Assert.AreEqual(null, parsedOperation.MasterKeyWeight);
            Assert.AreEqual(null, parsedOperation.LowThreshold);
            Assert.AreEqual(null, parsedOperation.MediumThreshold);
            Assert.AreEqual(null, parsedOperation.HighThreshold);
            Assert.AreEqual(homeDomain, parsedOperation.HomeDomain);
            Assert.AreEqual(null, parsedOperation.Signer);
            Assert.AreEqual(null, parsedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAtzdGVsbGFyLm9yZwAAAAAA",
                operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestSetOptionsOperationSignerSha256()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var preimage = Encoding.UTF8.GetBytes("stellar.org");
            var hash = Util.Hash(preimage);

            var operation = new SetOptionsOperation.Builder()
                .SetSigner(Signer.Sha256Hash(hash), 10)
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (SetOptionsOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(null, parsedOperation.InflationDestination);
            Assert.AreEqual(null, parsedOperation.ClearFlags);
            Assert.AreEqual(null, parsedOperation.SetFlags);
            Assert.AreEqual(null, parsedOperation.MasterKeyWeight);
            Assert.AreEqual(null, parsedOperation.LowThreshold);
            Assert.AreEqual(null, parsedOperation.MediumThreshold);
            Assert.AreEqual(null, parsedOperation.HighThreshold);
            Assert.AreEqual(null, parsedOperation.HomeDomain);
            Assert.IsTrue(hash.SequenceEqual(parsedOperation.Signer.HashX.InnerValue));
            Assert.AreEqual(10U, parsedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAACbpRqMkaQAfCYSk/n3xIl4fCoHfKqxF34ht2iuvSYEJQAAAAK",
                operation.ToXdrBase64());
        }

        [TestMethod]
        public void TestSetOptionsOperationPreAuthTxSigner()
        {
            Network.UseTestNetwork();

            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var sequenceNumber = 2908908335136768L;
            var account = new Account(source.AccountId, sequenceNumber);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddPreconditions(new TransactionPreconditions() { TimeBounds = new stellar_dotnet_sdk.TimeBounds(0, TransactionPreconditions.TIMEOUT_INFINITE) })
                .Build();

            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var opSource = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var operation = new SetOptionsOperation.Builder()
                .SetSigner(Signer.PreAuthTx(transaction), 10)
                .SetSourceAccount(opSource)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (SetOptionsOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(operation.InflationDestination, parsedOperation.InflationDestination);
            Assert.AreEqual(operation.ClearFlags, parsedOperation.ClearFlags);
            Assert.AreEqual(operation.SetFlags, parsedOperation.SetFlags);
            Assert.AreEqual(operation.MasterKeyWeight, parsedOperation.MasterKeyWeight);
            Assert.AreEqual(operation.LowThreshold, parsedOperation.LowThreshold);
            Assert.AreEqual(operation.MediumThreshold, parsedOperation.MediumThreshold);
            Assert.AreEqual(operation.HighThreshold, parsedOperation.HighThreshold);
            Assert.AreEqual(operation.HomeDomain, parsedOperation.HomeDomain);
            Assert.IsTrue(transaction.Hash().SequenceEqual(parsedOperation.Signer.PreAuthTx.InnerValue));
            Assert.AreEqual(operation.SignerWeight, parsedOperation.SignerWeight);
            Assert.AreEqual(operation.SourceAccount.AccountId, parsedOperation.SourceAccount.AccountId);
        }

        [TestMethod]
        public void TestPayloadSignerKey()
        {
            SetOptionsOperation.Builder builder = new SetOptionsOperation.Builder();
            String payloadSignerStrKey = "GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ";

            byte[] payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f20");
            SignedPayloadSigner signedPayloadSigner = new SignedPayloadSigner(StrKey.DecodeStellarAccountId(payloadSignerStrKey), payload);
            xdrSDK.SignerKey signerKey = Signer.SignedPayload(signedPayloadSigner);

            builder.SetSigner(signerKey, 1);
            builder.SetSourceAccount(source);

            SetOptionsOperation operation = builder.Build();

            xdrSDK.Operation xdrOperation = operation.ToXdr();
            SetOptionsOperation parsedOperation = (SetOptionsOperation)Operation.FromXdr(xdrOperation);

            // verify round trip between xdr and pojo
            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(signedPayloadSigner.SignerAccountID.InnerValue.Ed25519, parsedOperation.Signer.Ed25519SignedPayload.Ed25519);
            Assert.IsTrue(signedPayloadSigner.Payload.SequenceEqual(parsedOperation.Signer.Ed25519SignedPayload.Payload));

            // verify serialized xdr emitted with signed payload
            Assert.AreEqual("AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                            "AAAAAAAAAAAAAAAEAAAADPww0v5OtDZlx0EzMkPcFURyDiq2XNKSi+w16A/x/6JoAAAAgAQIDBAUGBwgJCgsMDQ4PEBES" +
                            "ExQVFhcYGRobHB0eHyAAAAAB",
                            operation.ToXdrBase64());
        }
    }
}
