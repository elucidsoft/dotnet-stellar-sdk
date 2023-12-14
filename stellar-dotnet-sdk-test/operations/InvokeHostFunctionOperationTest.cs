using System;
using System.Security.Cryptography;
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

        private const string WasmHash = "ZBYoEJT3IaPMMk3FoRmnEQHoDxewPZL+Uor+xWI4uII=";
        [TestMethod]
        public void TestCreateEmptySourceAccountContractOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            SCAddress address = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = address,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetContractIDPreimage(contractIdAddressPreimage);
            builder.SetExecutable(contractExecutableWasm);
            var operation = builder.Build();

            // Act
            var operationXdrBase64 = operation.ToXdrBase64();
            var decodedOperation = CreateContractOperation.FromOperationXdrBase64(operationXdrBase64);

            // Assert
            Assert.AreEqual(
                ((SCAccountId)((ContractIDAddressPreimage)operation.HostFunction.ContractIDPreimage).Address)
                .InnerValue,
                ((SCAccountId)((ContractIDAddressPreimage)decodedOperation.HostFunction.ContractIDPreimage).Address)
                .InnerValue);
            CollectionAssert.AreEqual(
                ((ContractIDAddressPreimage)operation.HostFunction.ContractIDPreimage).Salt.InnerValue,
                ((ContractIDAddressPreimage)decodedOperation.HostFunction.ContractIDPreimage).Salt.InnerValue);
            Assert.AreEqual(
                operation.HostFunction.Executable.WasmHash,
                decodedOperation.HostFunction.Executable.WasmHash);
            Assert.IsTrue(operation.Auth.Length == 0);
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }

        [TestMethod]
        public void TestCreateEmptyPreimageContractOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetExecutable(contractExecutableWasm);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Contract ID preimage cannot be null", ex.Message);
        }

        [TestMethod]
        public void TestCreateEmptyExecutableContractOperation()
        {
            SCAddress address = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = address,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetContractIDPreimage(contractIdAddressPreimage);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Executable cannot be null", ex.Message);
        }

        [TestMethod]
        public void TestCreateContractFromAddressOperation()
        {
            SCAddress address = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = address,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetExecutable(contractExecutableWasm);
            builder.SetContractIDPreimage(contractIdAddressPreimage);
            var operation = builder.Build();

            // Act
            var operationXdrBase64 = operation.ToXdrBase64();

            var decodedOperation = CreateContractOperation.FromOperationXdrBase64(operationXdrBase64);

            // Assert
            Assert.AreEqual(
                ((SCAccountId)((ContractIDAddressPreimage)operation.HostFunction.ContractIDPreimage).Address)
                .InnerValue,
                ((SCAccountId)((ContractIDAddressPreimage)decodedOperation.HostFunction.ContractIDPreimage).Address)
                .InnerValue);
            CollectionAssert.AreEqual(
                ((ContractIDAddressPreimage)operation.HostFunction.ContractIDPreimage).Salt.InnerValue,
                ((ContractIDAddressPreimage)decodedOperation.HostFunction.ContractIDPreimage).Salt.InnerValue);
            Assert.AreEqual(
                operation.HostFunction.Executable.WasmHash,
                decodedOperation.HostFunction.Executable.WasmHash);
            Assert.IsTrue(operation.Auth.Length == 0);
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }

        [TestMethod]
        public void TestCreateStellarAssetContractOperation()
        {
            var contractExecutableStellarAsset = new ContractExecutableStellarAsset(WasmHash);
            Asset asset =
                new AssetTypeCreditAlphaNum4("VNDC", "GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");

            var contractIdAssetPreimage = new ContractIDAssetPreimage()
            {
                Asset = asset,
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetExecutable(contractExecutableStellarAsset);
            builder.SetContractIDPreimage(contractIdAssetPreimage);
            var operation = builder.Build();

            // Act
            var operationXdrBase64 = operation.ToXdrBase64();

            var decodedOperation = CreateContractOperation.FromOperationXdrBase64(operationXdrBase64);

            // Assert
            Assert.AreEqual(
                ((AssetTypeCreditAlphaNum4)((ContractIDAssetPreimage)operation.HostFunction.ContractIDPreimage).Asset)
                .Code,
                ((AssetTypeCreditAlphaNum4)((ContractIDAssetPreimage)decodedOperation.HostFunction.ContractIDPreimage)
                    .Asset).Code);
            Assert.AreEqual(
                ((AssetTypeCreditAlphaNum4)((ContractIDAssetPreimage)operation.HostFunction.ContractIDPreimage).Asset)
                .Issuer,
                ((AssetTypeCreditAlphaNum4)((ContractIDAssetPreimage)decodedOperation.HostFunction.ContractIDPreimage)
                    .Asset).Issuer);
            Assert.AreEqual(
                operation.HostFunction.Executable.WasmHash,
                decodedOperation.HostFunction.Executable.WasmHash);
            Assert.IsTrue(operation.Auth.Length == 0);
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }

        [TestMethod]
        public void TestUploadEmptyWasmContractOperation()
        {
            var builder = new UploadContractOperation.Builder();

            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Wasm cannot be null", ex.Message);
        }

        [TestMethod]
        public void TestUploadEmptySourceAccountContractOperation()
        {
            byte[] wasm = { 0x00, 0x01, 0x02, 0x03, 0x34, 0x45, 0x66, 0x46 };

            var builder = new UploadContractOperation.Builder();
            builder.SetWasm(wasm);

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
                operation.SourceAccount?.AccountId,
                decodedOperation.SourceAccount?.AccountId);
        }

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
        
        [TestMethod]
        public void TestInvokeEmptyAddressContractOperation()
        {
            var functionName = new SCSymbol("hello");
            SCString arg = new SCString("world"); 
            SCVal[] args = { arg };
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetFunctionName(functionName);
            builder.SetArgs(args);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Contract address cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestInvokeEmptyFunctionNameContractOperation()
        {
            SCAddress contractId = new SCContractId("CDJ4RICANSXXZ275W2OY2U7RO73HYURBGBRHVW2UUXZNGEBIVBNRKEF7");
            var arg = new SCString("world"); 
            SCVal[] args = { arg };
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetArgs(args);
            builder.SetContractAddress(contractId);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Function name cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestInvokeEmptyAuthEntryContractOperation()
        {
            SCAddress contractId = new SCContractId("CDJ4RICANSXXZ275W2OY2U7RO73HYURBGBRHVW2UUXZNGEBIVBNRKEF7");
            SCSymbol functionName = new SCSymbol("hello");
            SCString arg = new SCString("world"); 
            SCVal[] args = { arg };
         
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetFunctionName(functionName);
            builder.SetArgs(args);
            builder.SetContractAddress(contractId);

            var operation = builder.Build();
            
            // Act
            var operationXdrBase64 = operation.ToXdrBase64();

            var decodedOperation = InvokeContractOperation.FromOperationXdrBase64(operationXdrBase64);
            
            // Assert
            Assert.AreEqual(
                ((SCContractId)operation.HostFunction.ContractAddress).InnerValue,
                ((SCContractId)decodedOperation.HostFunction.ContractAddress).InnerValue);
            Assert.AreEqual(
                operation.HostFunction.FunctionName.InnerValue,
                decodedOperation.HostFunction.FunctionName.InnerValue);
            Assert.AreEqual(
                operation.HostFunction.Args.Length,
                decodedOperation.HostFunction.Args.Length);
            for (var i = 0; i < operation.HostFunction.Args.Length; i++)
            {
                Assert.AreEqual(operation.HostFunction.Args[i].ToXdrBase64(), decodedOperation.HostFunction.Args[i].ToXdrBase64());    
            }
            Assert.AreEqual(
                operation.Auth.Length,
                decodedOperation.Auth.Length);
            
            Assert.AreEqual(
                sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
        
        [TestMethod]
        public void TestInvokeContractOperation()
        {
            SCAddress contractId = new SCContractId("CDJ4RICANSXXZ275W2OY2U7RO73HYURBGBRHVW2UUXZNGEBIVBNRKEF7");
            SCSymbol functionName = new SCSymbol("hello");
            SCString arg = new SCString("world"); 
            SCVal[] args = { arg };
            
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            
            SCAddress accountAddress = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
            
            var contractIDAddressPreimage = new ContractIDAddressPreimage
            {
                Address = accountAddress,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var contractExecutableWasm =
                new ContractExecutableWasm("ZBYoEJT3IaPMMk3FoRmnEQHoDxewPZL+Uor+xWI4uII=");
            
            var createContractHostFunction = new CreateContractHostFunction(contractIDAddressPreimage, contractExecutableWasm);

            var authorizedCreateContractFn = new SorobanAuthorizedCreateContractFunction
            {
                HostFunction = createContractHostFunction
            };
            
            var rootInvocation = new SorobanAuthorizedInvocation
            {
                Function = authorizedCreateContractFn
            };

            var subInvocations = new SorobanAuthorizedInvocation[]
            {
                new()
                {
                    Function = authorizedCreateContractFn,
                    SubInvocations = Array.Empty<SorobanAuthorizedInvocation>()
                }
            };

            rootInvocation.SubInvocations = subInvocations;
            const long nonce = -9223372036854775807;
            const uint signatureExpirationLedger = 1319013123;
            SCString signature = new("Signature");
            var credentials = new SorobanAddressCredentials
            {
                Address = accountAddress,
                Nonce = nonce,
                SignatureExpirationLedger = signatureExpirationLedger,
                Signature = signature
            };

            var authEntry = new SorobanAuthorizationEntry()
            {
                Credentials = credentials,
                RootInvocation = rootInvocation
            };
            
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(sourceAccount);
            builder.SetFunctionName(functionName);
            builder.SetArgs(args);
            builder.SetContractAddress(contractId);
            builder.AddAuth(authEntry);

            var operation = builder.Build();
            
            // Act
            var operationXdrBase64 = operation.ToXdrBase64();

            var decodedOperation = InvokeContractOperation.FromOperationXdrBase64(operationXdrBase64);
            
            // Assert
            Assert.AreEqual(
                ((SCContractId)operation.HostFunction.ContractAddress).InnerValue,
                ((SCContractId)decodedOperation.HostFunction.ContractAddress).InnerValue);
            Assert.AreEqual(
                operation.HostFunction.FunctionName.InnerValue,
                decodedOperation.HostFunction.FunctionName.InnerValue);
            Assert.AreEqual(
                operation.HostFunction.Args.Length,
                decodedOperation.HostFunction.Args.Length);
            for (var i = 0; i < operation.HostFunction.Args.Length; i++)
            {
                Assert.AreEqual(operation.HostFunction.Args[i].ToXdrBase64(), decodedOperation.HostFunction.Args[i].ToXdrBase64());    
            }
            Assert.AreEqual(
                operation.Auth.Length,
                decodedOperation.Auth.Length);
            for (var i = 0; i < operation.Auth.Length; i++)
            {
                Assert.AreEqual(operation.Auth[i].ToXdrBase64(), decodedOperation.Auth[i].ToXdrBase64());
            }
            
            Assert.AreEqual(
                sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
    }
}