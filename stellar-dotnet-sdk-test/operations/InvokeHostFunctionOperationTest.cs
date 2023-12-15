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
        private readonly KeyPair _sourceAccount =
            KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

        private const string WasmHash = "ZBYoEJT3IaPMMk3FoRmnEQHoDxewPZL+Uor+xWI4uII=";
        private readonly SCAddress _accountAddress = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
        private readonly SCAddress _contractAddress = new SCContractId("CDJ4RICANSXXZ275W2OY2U7RO73HYURBGBRHVW2UUXZNGEBIVBNRKEF7");
        private readonly SCSymbol _functionName = new("hello");
        private readonly SCVal[] _args = { new SCString("world"), new SCBytes(new byte[] { 0x00, 0x01, 0x02 }) };
        private const long Nonce = -9223372036854775807;
        private const uint SignatureExpirationLedger = 1319013123;
        private readonly SCString _signature = new("Signature");
        
        private SorobanAddressCredentials InitCredentials()
        {
            return new SorobanAddressCredentials
            {
                Address = _accountAddress,
                Nonce = Nonce,
                SignatureExpirationLedger = SignatureExpirationLedger,
                Signature = _signature
            };
        }

        private SorobanAuthorizationEntry InitAuthEntry()
        {
            var authorizedContractFn = new SorobanAuthorizedContractFunction
            {
                HostFunction = new InvokeContractHostFunction(_contractAddress, _functionName, _args)
            };

            var rootInvocation = new SorobanAuthorizedInvocation
            {
                Function = authorizedContractFn,
                SubInvocations = new SorobanAuthorizedInvocation[]
                {
                    new()
                    {
                        Function = authorizedContractFn,
                        SubInvocations = Array.Empty<SorobanAuthorizedInvocation>()
                    }
                }
            };

            return new SorobanAuthorizationEntry
            {
                RootInvocation = rootInvocation,
                Credentials = InitCredentials()
            };    
        }
        
        [TestMethod]
        public void TestCreateEmptySourceAccountContractOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = _accountAddress,
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
                ((ContractExecutableWasm)operation.HostFunction.Executable).WasmHash,
                ((ContractExecutableWasm)decodedOperation.HostFunction.Executable).WasmHash);
            Assert.IsTrue(operation.Auth.Length == 0);
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }

        [TestMethod]
        public void TestCreateEmptyPreimageContractOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetExecutable(contractExecutableWasm);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Contract ID preimage cannot be null", ex.Message);
        }

        [TestMethod]
        public void TestCreateEmptyExecutableContractOperation()
        {
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = _accountAddress,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetContractIDPreimage(contractIdAddressPreimage);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Executable cannot be null", ex.Message);
        }

        [TestMethod]
        public void TestCreateEmptyAuthEntryContractFromAddressOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = _accountAddress,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
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
                ((ContractExecutableWasm)operation.HostFunction.Executable).WasmHash,
                ((ContractExecutableWasm)decodedOperation.HostFunction.Executable).WasmHash);
            Assert.AreEqual(operation.Auth.Length, decodedOperation.Auth.Length);
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }
        
        [TestMethod]
        public void TestCreateContractFromAddressOperation()
        {
            var contractExecutableWasm = new ContractExecutableWasm(WasmHash);
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            var contractIdAddressPreimage = new ContractIDAddressPreimage
            {
                Address = _accountAddress,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetExecutable(contractExecutableWasm);
            builder.SetContractIDPreimage(contractIdAddressPreimage);
            builder.SetAuth(new[] { InitAuthEntry() });
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
                ((ContractExecutableWasm)operation.HostFunction.Executable).WasmHash,
                ((ContractExecutableWasm)decodedOperation.HostFunction.Executable).WasmHash);
            Assert.AreEqual(
                operation.Auth.Length,
                decodedOperation.Auth.Length);
            for (var i = 0; i < operation.Auth.Length; i++)
            {
                Assert.AreEqual(operation.Auth[i].ToXdrBase64(), decodedOperation.Auth[i].ToXdrBase64());
            }
            Assert.AreEqual(operation.SourceAccount?.AccountId, decodedOperation.SourceAccount?.AccountId);
        }

        [TestMethod]
        public void TestCreateStellarAssetContractOperation()
        {
            var builder = new CreateContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetExecutable(new ContractExecutableStellarAsset());
            builder.SetContractIDPreimage(new ContractIDAssetPreimage()
            {
                Asset =
                    new AssetTypeCreditAlphaNum4("VNDC",
                        "GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP"),
            });
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
        public void TestUploadEmptyAuthEntryContractOperation()
        {
            // Arrange
            byte[] wasm = { 0x00, 0x01, 0x02, 0x03, 0x34, 0x45, 0x66, 0x46 };

            var builder = new UploadContractOperation.Builder();
            builder.SetWasm(wasm);
            builder.SetSourceAccount(_sourceAccount);

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
                _sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
        
        [TestMethod]
        public void TestUploadContractOperation()
        {
            // Arrange
            byte[] wasm = { 0x00, 0x01, 0x02, 0x03, 0x34, 0x45, 0x66, 0x46 };

            var builder = new UploadContractOperation.Builder();
            builder.SetWasm(wasm);
            builder.SetSourceAccount(_sourceAccount);
            var authEntry = InitAuthEntry();
            builder.AddAuth(authEntry);
            builder.AddAuth(authEntry);
            builder.RemoveAuth(authEntry);
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
            for (var i = 0; i < operation.Auth.Length; i++)
            {
                Assert.AreEqual(operation.Auth[i].ToXdrBase64(), decodedOperation.Auth[i].ToXdrBase64());
            }
            Assert.AreEqual(
                _sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
        
        [TestMethod]
        public void TestInvokeEmptyAddressContractOperation()
        {
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetFunctionName(_functionName);
            builder.SetArgs(_args);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Contract address cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestInvokeEmptyFunctionNameContractOperation()
        {
            var arg = new SCString("world"); 
            SCVal[] args = { arg };
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetArgs(args);
            builder.SetContractAddress(_contractAddress);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => builder.Build());
            Assert.AreEqual("Function name cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestInvokeEmptyAuthEntryContractOperation()
        {
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetFunctionName(_functionName);
            builder.SetArgs(_args);
            builder.SetContractAddress(_contractAddress);

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
                _sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
        
        [TestMethod]
        public void TestInvokeContractOperation()
        {
            var builder = new InvokeContractOperation.Builder();
            builder.SetSourceAccount(_sourceAccount);
            builder.SetFunctionName(_functionName);
            builder.SetArgs(_args);
            builder.SetContractAddress(_contractAddress);
            builder.AddAuth(InitAuthEntry());

            var operation = builder.Build();
            
            // Act
            var operationXdrBase64 = operation.ToXdrBase64();

            var decodedOperation = InvokeContractOperation.FromOperationXdrBase64(operationXdrBase64);
            
            // Assert
            Assert.AreEqual(
                operation.HostFunction.ContractAddress.ToXdrBase64(),
                decodedOperation.HostFunction.ContractAddress.ToXdrBase64());
            Assert.AreEqual(
                operation.HostFunction.FunctionName.ToXdrBase64(),
                decodedOperation.HostFunction.FunctionName.ToXdrBase64());
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
                _sourceAccount.AccountId,
                decodedOperation.SourceAccount!.AccountId);
        }
    }
}