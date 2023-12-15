using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;
namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class ScValTest
    {
        private const string WasmHash = "ZBYoEJT3IaPMMk3FoRmnEQHoDxewPZL+Uor+xWI4uII=";
        [TestMethod]
        public void TestScBool()
        {
            var scBool = new SCBool(true);
            
            // Act
            var scBoolXdrBase64 = scBool.ToXdrBase64();
            var fromXdrBase64ScBool = (SCBool)SCVal.FromXdrBase64(scBoolXdrBase64);

            // Assert
            Assert.AreEqual(scBool.InnerValue, fromXdrBase64ScBool.InnerValue);
        }
       
        [TestMethod]
        public void TestSCContractError()
        {
            var error = new SCContractError(1);
            
            // Action
            var contractErrorXdrBase64 = error.ToXdrBase64();
            var fromXdrBase64ContractError = (SCContractError)SCVal.FromXdrBase64(contractErrorXdrBase64);

            // Assert
            Assert.AreEqual(error.ContractCode, fromXdrBase64ContractError.ContractCode);
        }
        
        [TestMethod]
        public void TestSCWasmVmError()
        {
            var arithDomainError = new SCWasmVmError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCWasmVmError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCWasmVmError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var internalErrorXdrBase64 = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCWasmVmError)SCVal.FromXdrBase64(internalErrorXdrBase64);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCContextError()
        {
            var arithDomainError = new SCContextError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCContextError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCContextError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCContextError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCStorageError()
        {
            var arithDomainError = new SCStorageError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCStorageError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCStorageError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCStorageError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCObjectError()
        {
            var arithDomainError = new SCObjectError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCObjectError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCObjectError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCObjectError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCCryptoError()
        {
            var arithDomainError = new SCCryptoError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCCryptoError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCCryptoError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCCryptoError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCEventsError()
        {
            var arithDomainError = new SCEventsError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCEventsError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCEventsError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCEventsError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCBudgetError()
        {
            var arithDomainError = new SCBudgetError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCBudgetError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCBudgetError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCBudgetError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCValueError()
        {
            var arithDomainError = new SCValueError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCValueError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCValueError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCValueError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestSCAuthError()
        {
            SCAuthError arithDomainError = new SCAuthError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_ARITH_DOMAIN
            };
            var internalError = new SCAuthError
            {
                Code = xdrSDK.SCErrorCode.SCErrorCodeEnum.SCEC_INTERNAL_ERROR
            };
            // Action
            var arithDomainErrorXdrBase64 = arithDomainError.ToXdrBase64();
            var fromXdrBase64ArithDomainError = (SCAuthError)SCVal.FromXdrBase64(arithDomainErrorXdrBase64);

            var xdrBase64InternalError = internalError.ToXdrBase64();
            var fromXdrBase64InternalError = (SCAuthError)SCVal.FromXdrBase64(xdrBase64InternalError);
            
            // Assert
            Assert.AreEqual(arithDomainError.Code, fromXdrBase64ArithDomainError.Code);
            Assert.AreEqual(internalError.Code, fromXdrBase64InternalError.Code);
        }
        
        [TestMethod]
        public void TestScUint32()
        {
            var scUint32 = new SCUint32(1319013123);
            
            // Act
            var scUint32XdrBase64 = scUint32.ToXdrBase64();
            var xdrScUint32 = (SCUint32)SCVal.FromXdrBase64(scUint32XdrBase64);

            // Assert
            Assert.AreEqual(scUint32.InnerValue, xdrScUint32.InnerValue);
        }
        
        [TestMethod]
        public void TestScInt32()
        {
            var scInt32 = new SCInt32(-192049123);
            
            // Act
            var scInt32XdrBase64 = scInt32.ToXdrBase64();
            var fromXdrBase64ScInt32 = (SCInt32)SCVal.FromXdrBase64(scInt32XdrBase64);

            // Assert
            Assert.AreEqual(scInt32.InnerValue, fromXdrBase64ScInt32.InnerValue);
        }
        
        [TestMethod]
        public void TestScUint64()
        {
            var scUint64 = new SCUint64()
            {
                InnerValue = 18446744073709551615
            };
            
            // Act
            var scUint64XdrBase64 = scUint64.ToXdrBase64();
            var fromXdrBase64ScUint64 = (SCUint64)SCVal.FromXdrBase64(scUint64XdrBase64);

            // Assert
            Assert.AreEqual(scUint64.InnerValue, fromXdrBase64ScUint64.InnerValue);
        }
        
        [TestMethod]
        public void TestScInt64()
        {
            var scInt64 = new SCInt64(-9223372036854775807);
            
            // Act
            var scInt64XdrBase64 = scInt64.ToXdrBase64();
            var fromXdrBase64ScInt64 = (SCInt64)SCVal.FromXdrBase64(scInt64XdrBase64);

            // Assert
            Assert.AreEqual(scInt64.InnerValue, fromXdrBase64ScInt64.InnerValue);
        }
        
        [TestMethod]
        public void TestScTimePoint()
        {
            var scTimePoint = new SCTimePoint(18446744073709551615);
            
            // Act
            var scTimePointXdrBase64 = scTimePoint.ToXdrBase64();
            var fromXdrBase64ScTimePoint = (SCTimePoint)SCVal.FromXdrBase64(scTimePointXdrBase64);

            // Assert
            Assert.AreEqual(scTimePoint.InnerValue, fromXdrBase64ScTimePoint.InnerValue);
        }
        
        [TestMethod]
        public void TestScDuration()
        {
            var scDuration = new SCDuration(18446744073709551615);
            
            // Act
            var scDurationXdrBase64 = scDuration.ToXdrBase64();
            var fromXdrBase64ScDuration = (SCDuration)SCVal.FromXdrBase64(scDurationXdrBase64);

            // Assert
            Assert.AreEqual(scDuration.InnerValue, fromXdrBase64ScDuration.InnerValue);
        }
        
        [TestMethod]
        public void TestScUint128()
        {
            var scUint128 = new SCUint128()
            {
                Hi = 18446744073709551615,
                Lo = 1
            };
            
            // Act
            var scUint128XdrBase64 = scUint128.ToXdrBase64();
            var fromXdrBase64ScUint128 = (SCUint128)SCVal.FromXdrBase64(scUint128XdrBase64);

            // Assert
            Assert.AreEqual(scUint128.Hi, fromXdrBase64ScUint128.Hi);
            Assert.AreEqual(scUint128.Lo, fromXdrBase64ScUint128.Lo);
        }
        
        [TestMethod]
        public void TestScInt128()
        {
            var scInt128 = new SCInt128()
            {
                Lo = 18446744073709551615,
                Hi = -9223372036854775807
            };
            
            // Act
            var scInt128XdrBase64 = scInt128.ToXdrBase64();
            var fromXdrBase64ScInt128 = (SCInt128)SCVal.FromXdrBase64(scInt128XdrBase64);

            // Assert
            Assert.AreEqual(scInt128.Lo, fromXdrBase64ScInt128.Lo);
            Assert.AreEqual(scInt128.Hi, fromXdrBase64ScInt128.Hi);
        }
        
        [TestMethod]
        public void TestScUint256()
        {
            var scUint256 = new SCUint256()
            {
                HiHi = 18446744073709551615,
                HiLo = 1,
                LoHi = 18446744073709551614,
                LoLo = 0
            };
            
            // Act
            var scUint256XdrBase64 = scUint256.ToXdrBase64();
            var fromXdrBase64ScUint256 = (SCUint256)SCVal.FromXdrBase64(scUint256XdrBase64);

            // Assert
            Assert.AreEqual(scUint256.HiHi, fromXdrBase64ScUint256.HiHi);
            Assert.AreEqual(scUint256.HiLo, fromXdrBase64ScUint256.HiLo);
            Assert.AreEqual(scUint256.LoHi, fromXdrBase64ScUint256.LoHi);
            Assert.AreEqual(scUint256.LoLo, fromXdrBase64ScUint256.LoLo);
        }
        
        [TestMethod]
        public void TestScInt256()
        {
            var scInt256 = new SCInt256()
            {
                HiHi = -9223372036854775807,
                HiLo = 18446744073709551614,
                LoHi = 18446744073709551615,
                LoLo = 18446744073709551613
            };
            
            // Act
            var scInt256XdrBase64 = scInt256.ToXdrBase64();
            var fromXdrBase64ScInt256 = (SCInt256)SCVal.FromXdrBase64(scInt256XdrBase64);

            // Assert
            Assert.AreEqual(scInt256.HiHi, fromXdrBase64ScInt256.HiHi);
            Assert.AreEqual(scInt256.HiLo, fromXdrBase64ScInt256.HiLo);
            Assert.AreEqual(scInt256.LoHi, fromXdrBase64ScInt256.LoHi);
            Assert.AreEqual(scInt256.LoLo, fromXdrBase64ScInt256.LoLo);
        }
        
        [TestMethod]
        public void TestScBytes()
        {
            byte[] bytes = { 0x00, 0x01, 0x03, 0x03, 0x34, 0x45, 0x66, 0x46 };
            var scBytes = new SCBytes(bytes);
            
            // Act
            var scBytesXdrBase64 = scBytes.ToXdrBase64();
            var fromXdrBase64ScBytes = (SCBytes)SCVal.FromXdrBase64(scBytesXdrBase64);

            // Assert
            CollectionAssert.AreEqual(scBytes.InnerValue, fromXdrBase64ScBytes.InnerValue);
        }
        
        [TestMethod]
        public void TestScBytesEmpty()
        {
            var bytes = Array.Empty<byte>();
            var scBytes = new SCBytes(bytes);
            
            // Act
            var scBytesXdrBase64 = scBytes.ToXdrBase64();
            var fromXdrBase64ScBytes = (SCBytes)SCVal.FromXdrBase64(scBytesXdrBase64);

            // Assert
            CollectionAssert.AreEqual(scBytes.InnerValue, fromXdrBase64ScBytes.InnerValue);
        }
        
        [TestMethod]
        public void TestScString()
        {
            var scString = new SCString("hello world");
            
            // Act
            var scStringXdrBase64 = scString.ToXdrBase64();
            var fromXdrBase64ScString = (SCString)SCVal.FromXdrBase64(scStringXdrBase64);

            // Assert
            Assert.AreEqual(scString.InnerValue, fromXdrBase64ScString.InnerValue);
        }
        
        [TestMethod]
        public void TestEmptyScString()
        {
            var scString = new SCString("");
            
            // Act
            var scStringXdrBase64 = scString.ToXdrBase64();
            var fromXdrBase64ScString = (SCString)SCVal.FromXdrBase64(scStringXdrBase64);

            // Assert
            Assert.AreEqual(scString.InnerValue, fromXdrBase64ScString.InnerValue);
        }
        
        [TestMethod]
        public void TestScSymbol()
        {
            var scSymbol = new SCSymbol("Is this a symbol?");
            
            // Act
            var scSymbolXdrBase64 = scSymbol.ToXdrBase64();
            var fromXdrBase64ScSymbol = (SCSymbol)SCVal.FromXdrBase64(scSymbolXdrBase64);

            // Assert
            Assert.AreEqual(scSymbol.InnerValue, fromXdrBase64ScSymbol.InnerValue);
        }
        
        [TestMethod]
        public void TestScVec()
        {
            var scSymbol = new SCSymbol("Is this a symbol?");
            var scString = new SCString("hello world");
            var scVals = new SCVal[] { scString, scSymbol };
            var scVec = new SCVec(scVals);
            
            // Act
            var scVecXdrBase64 = scVec.ToXdrBase64();
            var fromXdrBase64ScVec = (SCVec)SCVal.FromXdrBase64(scVecXdrBase64);
            
            // Assert
            Assert.AreEqual(scVec.InnerValue.Length, fromXdrBase64ScVec.InnerValue.Length);
            for (var i = 0; i < scVec.InnerValue.Length; i++)
            {
                Assert.AreEqual(scVec.InnerValue[i].ToXdrBase64(), fromXdrBase64ScVec.InnerValue[i].ToXdrBase64());    
            }
        }
        
        [TestMethod]
        public void TestScMap()
        {
            var entry1 = new SCMapEntry()
            {
                Key = new SCString("key 1"),
                Value = new SCBool(false)
            };
            var entry2 = new SCMapEntry()
            {
                Key = new SCUint32(1),
                Value = new SCString("this is value 2")
            };
            var entry3 = new SCMapEntry()
            {
                Key = new SCUint32(1),
                Value = new SCSymbol("$$$")
            };
            var nestedScMap = new SCMap()
            {
                Entries = new[] { entry1, entry2 }
            };
            var entry4 = new SCMapEntry()
            {
                Key = new SCUint32(1),
                Value = nestedScMap
            };

            var mapEntries = new [] { entry1, entry2, entry3, entry3, entry4 };
            var scMap = new SCMap() { Entries = mapEntries };
        
            // Act
            var scMapXdrBase64 = scMap.ToXdrBase64();
            var fromXdrBase64ScMap = (SCMap)SCVal.FromXdrBase64(scMapXdrBase64);
            
            // Assert
            Assert.AreEqual(scMap.Entries.Length, fromXdrBase64ScMap.Entries.Length);
            for (var i = 0; i < scMap.Entries.Length; i++)
            {
                Assert.AreEqual(scMap.Entries[i].Key.ToXdrBase64(), fromXdrBase64ScMap.Entries[i].Key.ToXdrBase64());    
                Assert.AreEqual(scMap.Entries[i].Value.ToXdrBase64(), fromXdrBase64ScMap.Entries[i].Value.ToXdrBase64());
            }
        }
        
        [TestMethod]
        public void TestEmptyScMap()
        {
            var scMap = new SCMap();

            // Act
            var scMapXdrBase64 = scMap.ToXdrBase64();
            var fromXdrBase64ScMap = (SCMap)SCVal.FromXdrBase64(scMapXdrBase64);
            
            // Assert
            Assert.AreEqual(scMap.Entries.Length, fromXdrBase64ScMap.Entries.Length);
            for (var i = 0; i < scMap.Entries.Length; i++)
            {
                Assert.AreEqual(scMap.Entries[i].Key.ToXdrBase64(), fromXdrBase64ScMap.Entries[i].Key.ToXdrBase64());    
                Assert.AreEqual(scMap.Entries[i].Value.ToXdrBase64(), fromXdrBase64ScMap.Entries[i].Value.ToXdrBase64());
            }
        }
     
        [TestMethod]
        public void TestSCLedgerKeyContractInstance()
        {
        }
        
        [TestMethod]
        public void TestSCContractExecutableWasmWithEmptyStorage()
        {
            var contractExecutable = new ContractExecutableWasm(WasmHash);

            var contractInstance = new SCContractInstance
            {
                Executable = contractExecutable
            };
            
            var contractInstanceXdrBase64 = contractInstance.ToXdrBase64();
            var fromXdrBase64ContractInstance = (SCContractInstance)SCVal.FromXdrBase64(contractInstanceXdrBase64);
            
            Assert.AreEqual(((ContractExecutableWasm)contractInstance.Executable).WasmHash, ((ContractExecutableWasm)fromXdrBase64ContractInstance.Executable).WasmHash);
            Assert.AreEqual(contractInstance.Storage.Entries.Length, fromXdrBase64ContractInstance.Storage.Entries.Length);
        }
        
        [TestMethod]
        public void TestSCContractExecutableWasm()
        {
            var contractExecutable = new ContractExecutableWasm(WasmHash);
            
            var entry1 = new SCMapEntry()
            {
                Key = new SCString("key 1"),
                Value = new SCBool(false)
            };
            var entry2 = new SCMapEntry()
            {
                Key = new SCUint32(111),
                Value = new SCString("2nd value")
            };
            var entry3 = new SCMapEntry()
            {
                Key = new SCUint32(1),
                Value = new SCSymbol("&")
            };

            SCMapEntry[] mapEntries = { entry1, entry2, entry3 };
            var scMap = new SCMap() { Entries = mapEntries };

            var contractInstance = new SCContractInstance
            {
                Executable = contractExecutable,
                Storage = scMap
            };
            
            var contractInstanceXdrBase64 = contractInstance.ToXdrBase64();
            var fromXdrBase64ContractInstance = (SCContractInstance)SCVal.FromXdrBase64(contractInstanceXdrBase64);

            Assert.AreEqual(((ContractExecutableWasm)contractInstance.Executable).WasmHash,
                ((ContractExecutableWasm)fromXdrBase64ContractInstance.Executable).WasmHash);
            Assert.AreEqual(contractInstance.Storage.Entries.Length, fromXdrBase64ContractInstance.Storage.Entries.Length);
            for (var i = 0; i < contractInstance.Storage.Entries.Length; i++)
            {
                Assert.AreEqual(contractInstance.Storage.Entries[i].Key.ToXdrBase64(), fromXdrBase64ContractInstance.Storage.Entries[i].Key.ToXdrBase64());    
                Assert.AreEqual(contractInstance.Storage.Entries[i].Value.ToXdrBase64(), fromXdrBase64ContractInstance.Storage.Entries[i].Value.ToXdrBase64());
            }
        }

        [TestMethod]
        public void TestSCContractExecutableStellarAssetWithEmptyStorage()
        {
            var contractInstance = new SCContractInstance
            {
                Executable = new ContractExecutableStellarAsset()
            };
            
            var contractInstanceXdrBase64 = contractInstance.ToXdrBase64();
            var fromXdrBase64ContractInstance = (SCContractInstance)SCVal.FromXdrBase64(contractInstanceXdrBase64);
            
            Assert.AreEqual(contractInstance.Storage.Entries.Length, fromXdrBase64ContractInstance.Storage.Entries.Length);
        }

        [TestMethod]
        public void TestSCContractExecutableStellarAsset()
        {
            var entry1 = new SCMapEntry()
            {
                Key = new SCString("key 1"),
                Value = new SCBool(false)
            };
            var entry2 = new SCMapEntry()
            {
                Key = new SCUint32(111),
                Value = new SCString("2nd value")
            };
            var entry3 = new SCMapEntry()
            {
                Key = new SCUint32(1),
                Value = new SCSymbol("&")
            };

            SCMapEntry[] mapEntries = { entry1, entry2, entry3 };
            var scMap = new SCMap() { Entries = mapEntries };

            var contractInstance = new SCContractInstance
            {
                Executable = new ContractExecutableStellarAsset(),
                Storage = scMap
            };
            
            var contractInstanceXdrBase64 = contractInstance.ToXdrBase64();
            var fromXdrBase64ContractInstance = (SCContractInstance)SCVal.FromXdrBase64(contractInstanceXdrBase64);

            Assert.AreEqual(contractInstance.Storage.Entries.Length,
                fromXdrBase64ContractInstance.Storage.Entries.Length);
            for (var i = 0; i < contractInstance.Storage.Entries.Length; i++)
            {
                Assert.AreEqual(contractInstance.Storage.Entries[i].Key.ToXdrBase64(), fromXdrBase64ContractInstance.Storage.Entries[i].Key.ToXdrBase64());    
                Assert.AreEqual(contractInstance.Storage.Entries[i].Value.ToXdrBase64(), fromXdrBase64ContractInstance.Storage.Entries[i].Value.ToXdrBase64());
            }
        }
        
        [TestMethod]
        public void TestScNonceKey()
        {
            SCNonceKey scNonceKey = new SCNonceKey(-9223372036854775807);
            
            // Act
            var scNonceKeyXdrBase64 = scNonceKey.ToXdrBase64();
            var fromXdrBase64ScNonceKey = (SCNonceKey)SCVal.FromXdrBase64(scNonceKeyXdrBase64);

            // Assert
            Assert.AreEqual(scNonceKey.Nonce, fromXdrBase64ScNonceKey.Nonce);
        }

    }
}