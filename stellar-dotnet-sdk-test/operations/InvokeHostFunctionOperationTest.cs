using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.operations
{
    [TestClass]
    public class InvokeHostFunctionOperationTest
    {
        private KeyPair sourceAccount =
            KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

        [TestMethod]
        public void TestUploadContractOperation()
        {
            // Arrange
            byte[] wasm = { 0x00, 0x01, 0x02, 0x03, 0x34, 0x45, 0x66, 0x46 };

            var builder = new UploadContractOperation.Builder();
            builder.SetWasm(wasm);
            builder.SetSourceAccount(sourceAccount);

            var operation = builder.Build();

            // Act
            var operationXdrBase64 = operation.ToXdrBase64();
            
            var decodedOperation = UploadContractOperation.FromOperationXdrBase64(operationXdrBase64);
            
            // Assert
            CollectionAssert.AreEqual(
                operation.HostFunction.Wasm,
                decodedOperation.HostFunction.Wasm);
            Assert.AreEqual(
                operation.Auth.Length,
                decodedOperation.Auth.Length);
            Assert.AreEqual(
                sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
    }
}
