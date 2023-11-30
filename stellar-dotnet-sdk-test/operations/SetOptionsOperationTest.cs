using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;
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

            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = SetOptionsOperation.FromOperationXdrBase64(operationXdrBase64);

            Assert.AreEqual(inflationDestination.AccountId, decodedOperation.InflationDestination.AccountId);
            Assert.AreEqual(1U, decodedOperation.ClearFlags);
            Assert.AreEqual(1U, decodedOperation.SetFlags);
            Assert.AreEqual(1U, decodedOperation.MasterKeyWeight);
            Assert.AreEqual(2U, decodedOperation.LowThreshold);
            Assert.AreEqual(3U, decodedOperation.MediumThreshold);
            Assert.AreEqual(4U, decodedOperation.HighThreshold);
            Assert.AreEqual(homeDomain, decodedOperation.HomeDomain);
            Assert.AreEqual(signer.Discriminant.InnerValue, decodedOperation.Signer.Discriminant.InnerValue);
            CollectionAssert.AreEqual(signer.Ed25519.InnerValue, decodedOperation.Signer.Ed25519.InnerValue);
            Assert.AreEqual(1U, decodedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, decodedOperation.SourceAccount.AccountId);
            Assert.AreEqual(OperationThreshold.High, decodedOperation.Threshold);

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

            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = SetOptionsOperation.FromOperationXdrBase64(operationXdrBase64);

            Assert.AreEqual(null, decodedOperation.InflationDestination);
            Assert.AreEqual(null, decodedOperation.ClearFlags);
            Assert.AreEqual(null, decodedOperation.SetFlags);
            Assert.AreEqual(null, decodedOperation.MasterKeyWeight);
            Assert.AreEqual(null, decodedOperation.LowThreshold);
            Assert.AreEqual(null, decodedOperation.MediumThreshold);
            Assert.AreEqual(null, decodedOperation.HighThreshold);
            Assert.AreEqual(homeDomain, decodedOperation.HomeDomain);
            Assert.AreEqual(null, decodedOperation.Signer);
            Assert.AreEqual(null, decodedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, decodedOperation.SourceAccount.AccountId);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAtzdGVsbGFyLm9yZwAAAAAA",
                operationXdrBase64);
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

            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = SetOptionsOperation.FromOperationXdrBase64(operationXdrBase64);

            Assert.AreEqual(null, decodedOperation.InflationDestination);
            Assert.AreEqual(null, decodedOperation.ClearFlags);
            Assert.AreEqual(null, decodedOperation.SetFlags);
            Assert.AreEqual(null, decodedOperation.MasterKeyWeight);
            Assert.AreEqual(null, decodedOperation.LowThreshold);
            Assert.AreEqual(null, decodedOperation.MediumThreshold);
            Assert.AreEqual(null, decodedOperation.HighThreshold);
            Assert.AreEqual(null, decodedOperation.HomeDomain);
            Assert.IsTrue(hash.SequenceEqual(decodedOperation.Signer.HashX.InnerValue));
            Assert.AreEqual(10U, decodedOperation.SignerWeight);
            Assert.AreEqual(source.AccountId, decodedOperation.SourceAccount.AccountId);

            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAACbpRqMkaQAfCYSk/n3xIl4fCoHfKqxF34ht2iuvSYEJQAAAAK",
                operationXdrBase64);
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

            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = SetOptionsOperation.FromOperationXdrBase64(operationXdrBase64);

            Assert.AreEqual(operation.InflationDestination, decodedOperation.InflationDestination);
            Assert.AreEqual(operation.ClearFlags, decodedOperation.ClearFlags);
            Assert.AreEqual(operation.SetFlags, decodedOperation.SetFlags);
            Assert.AreEqual(operation.MasterKeyWeight, decodedOperation.MasterKeyWeight);
            Assert.AreEqual(operation.LowThreshold, decodedOperation.LowThreshold);
            Assert.AreEqual(operation.MediumThreshold, decodedOperation.MediumThreshold);
            Assert.AreEqual(operation.HighThreshold, decodedOperation.HighThreshold);
            Assert.AreEqual(operation.HomeDomain, decodedOperation.HomeDomain);
            Assert.IsTrue(transaction.Hash().SequenceEqual(decodedOperation.Signer.PreAuthTx.InnerValue));
            Assert.AreEqual(operation.SignerWeight, decodedOperation.SignerWeight);
            Assert.AreEqual(operation.SourceAccount.AccountId, decodedOperation.SourceAccount.AccountId);
        }

        [TestMethod]
        public void TestPayloadSignerKey()
        {
            // Arrange
            const string payloadSignerStrKey = "GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ";
            
            var builder = new SetOptionsOperation.Builder();

            var payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f20");
            var signedPayloadSigner = new SignedPayloadSigner(StrKey.DecodeStellarAccountId(payloadSignerStrKey), payload);
            var signerKey = Signer.SignedPayload(signedPayloadSigner);

            builder.SetSigner(signerKey, 1);
            builder.SetSourceAccount(source);

            var operation = builder.Build();
            
            // Act
            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = SetOptionsOperation.FromOperationXdrBase64(operationXdrBase64);

            // Assert
            // verify serialized xdr emitted with signed payload
            Assert.AreEqual(
                "AAAAAQAAAAC7JAuE3XvquOnbsgv2SRztjuk4RoBVefQ0rlrFMMQvfAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAADPww0v5OtDZlx0EzMkPcFURyDiq2XNKSi+w16A/x/6JoAAAAgAQIDBAUGBwgJCgsMDQ4PEBESExQVFhcYGRobHB0eHyAAAAAB",
                operationXdrBase64);
            
            // verify round trip between xdr and pojo
            Assert.AreEqual(source.AccountId, decodedOperation.SourceAccount.AccountId);
            CollectionAssert.AreEqual(signedPayloadSigner.SignerAccountID.InnerValue.Ed25519.InnerValue, decodedOperation.Signer.Ed25519SignedPayload.Ed25519.InnerValue);
            CollectionAssert.AreEqual(signedPayloadSigner.Payload, decodedOperation.Signer.Ed25519SignedPayload.Payload);
        }
    }
}
