using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using SCAddress = stellar_dotnet_sdk.SCAddress;
using SCString = stellar_dotnet_sdk.SCString;
using SorobanAddressCredentials = stellar_dotnet_sdk.SorobanAddressCredentials;
using SorobanAuthorizationEntry = stellar_dotnet_sdk.SorobanAuthorizationEntry;
using SorobanAuthorizedInvocation = stellar_dotnet_sdk.SorobanAuthorizedInvocation;
using SorobanCredentials = stellar_dotnet_sdk.SorobanCredentials;
using xdrSDK = stellar_dotnet_sdk.xdr;
using ContractExecutable = stellar_dotnet_sdk.ContractExecutable;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AuthTest
    {
        private readonly SCAddress _accountAddress = new SCAccountId("GAEBBKKHGCAD53X244CFGTVEKG7LWUQOAEW4STFHMGYHHFS5WOQZZTMP");
        private const long Nonce = -9223372036854775807;
        private const uint SignatureExpirationLedger = 1319013123;
        private readonly SCString _signature = new("Signature");

        private readonly ContractExecutable _contractExecutableWasm =
            new ContractExecutableWasm("ZBYoEJT3IaPMMk3FoRmnEQHoDxewPZL+Uor+xWI4uII=");

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

        private ContractIDAddressPreimage InitContractIDAddressPreimage()
        {
            var random32Bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(random32Bytes);
            
            return new ContractIDAddressPreimage
            {
                Address = _accountAddress,
                Salt = new xdrSDK.Uint256(random32Bytes)
            };
        }
        
        [TestMethod]
        public void TestAccountAddressSorobanAddressCredentials()
        {
            var sorobanCredentials = InitCredentials();
            
            // Act
            var credentialsXdrBase64 = sorobanCredentials.ToXdrBase64();
            var decodedCredentials = (SorobanAddressCredentials)SorobanCredentials.FromXdrBase64(credentialsXdrBase64);
            
            // Assert
            Assert.AreEqual(((SCAccountId)sorobanCredentials.Address).InnerValue, ((SCAccountId)decodedCredentials.Address).InnerValue);
            Assert.AreEqual(sorobanCredentials.Nonce, decodedCredentials.Nonce);
            Assert.AreEqual(sorobanCredentials.Signature.ToXdrBase64(), decodedCredentials.Signature.ToXdrBase64());
            Assert.AreEqual(sorobanCredentials.SignatureExpirationLedger, decodedCredentials.SignatureExpirationLedger);
        }
        
        [TestMethod]
        public void TestEmptyAddressSorobanAddressCredentials()
        {
            var sorobanCredentials = InitCredentials();
            sorobanCredentials.Address = null;
            
            var ex = Assert.ThrowsException<InvalidOperationException>(() => sorobanCredentials.ToXdrBase64());
            Assert.AreEqual("Address cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestContractAddressSorobanAddressCredentials()
        {
            var contractAddress = new SCContractId("CAC2UYJQMC4ISUZ5REYB2AMDC44YKBNZWG4JB6N6GBL66CEKQO3RDSAB");
            var sorobanCredentials = InitCredentials();
            sorobanCredentials.Address = contractAddress;
            
            // Act
            var credentialsXdrBase64 = sorobanCredentials.ToXdrBase64();
            var decodedCredentials = (SorobanAddressCredentials)SorobanCredentials.FromXdrBase64(credentialsXdrBase64);
            
            // Assert
            Assert.AreEqual(((SCContractId)sorobanCredentials.Address).InnerValue, ((SCContractId)decodedCredentials.Address).InnerValue);
            Assert.AreEqual(sorobanCredentials.Nonce, decodedCredentials.Nonce);
            Assert.AreEqual(((SCString)sorobanCredentials.Signature).InnerValue, ((SCString)decodedCredentials.Signature).InnerValue);
            Assert.AreEqual(sorobanCredentials.SignatureExpirationLedger, decodedCredentials.SignatureExpirationLedger);
        }
        
        [TestMethod]
        public void TestEmptySignatureSorobanAddressCredentials()
        {
            var sorobanCredentials = InitCredentials();
            sorobanCredentials.Signature = null;
            
            var ex = Assert.ThrowsException<InvalidOperationException>(() => sorobanCredentials.ToXdrBase64());
            Assert.AreEqual("Signature cannot be null", ex.Message);
        }
        
        [TestMethod]
        public void TestZeroSignatureExpirationLedgerSorobanAddressCredentials()
        {
            var sorobanCredentials = InitCredentials();
            sorobanCredentials.SignatureExpirationLedger = 0;
            
            // Act 
            var credentialsXdrBase64 = sorobanCredentials.ToXdrBase64();
            var decodedCredentials = (SorobanAddressCredentials)SorobanCredentials.FromXdrBase64(credentialsXdrBase64);
            
            // Assert
            Assert.AreEqual(((SCAccountId)sorobanCredentials.Address).InnerValue, ((SCAccountId)decodedCredentials.Address).InnerValue);
            Assert.AreEqual(sorobanCredentials.Nonce, decodedCredentials.Nonce);
            Assert.AreEqual(((SCString)sorobanCredentials.Signature).ToXdrBase64(), ((SCString)decodedCredentials.Signature).ToXdrBase64());
            Assert.AreEqual(sorobanCredentials.SignatureExpirationLedger, decodedCredentials.SignatureExpirationLedger);
        }
        
        [TestMethod]
        public void TestZeroNonceSorobanAddressCredentials()
        {
            var sorobanCredentials = InitCredentials();
            sorobanCredentials.Nonce = 0;
            // Act 
            var credentialsXdrBase64 = sorobanCredentials.ToXdrBase64();
            var decodedCredentials = (SorobanAddressCredentials)SorobanCredentials.FromXdrBase64(credentialsXdrBase64);
            
            // Assert
            Assert.AreEqual(((SCAccountId)sorobanCredentials.Address).InnerValue, ((SCAccountId)decodedCredentials.Address).InnerValue);
            Assert.AreEqual(sorobanCredentials.Nonce, decodedCredentials.Nonce);
            Assert.AreEqual(((SCString)sorobanCredentials.Signature).ToXdrBase64(), ((SCString)decodedCredentials.Signature).ToXdrBase64());
            Assert.AreEqual(sorobanCredentials.SignatureExpirationLedger, decodedCredentials.SignatureExpirationLedger);
        }

        [TestMethod]
        public void TestEmptySubInvocationsSorobanAuthorizationEntry()
        {
            var rootInvocation = new SorobanAuthorizedInvocation
            {
                Function = new SorobanAuthorizedCreateContractFunction
                {
                    HostFunction =
                        new CreateContractHostFunction(InitContractIDAddressPreimage(), _contractExecutableWasm)
                },
                SubInvocations = Array.Empty<SorobanAuthorizedInvocation>()
            };
            
            var authEntry = new SorobanAuthorizationEntry
            {
                RootInvocation = rootInvocation,
                Credentials = InitCredentials()
            };

            var authEntryXdrBase64 = authEntry.ToXdrBase64();
            var decodedAuthEntry = SorobanAuthorizationEntry.FromXdrBase64(authEntryXdrBase64);
            
            TestEqualInvocations(authEntry.RootInvocation, decodedAuthEntry.RootInvocation);
            
            Assert.AreEqual(authEntry.Credentials.ToXdrBase64(), decodedAuthEntry.Credentials.ToXdrBase64());
        }
        
        [TestMethod]
        public void TestAuthorizedCreateContractSorobanAuthorizationEntry()
        {
            var authorizedCreateContractFn = new SorobanAuthorizedCreateContractFunction
            {
                HostFunction = new CreateContractHostFunction(InitContractIDAddressPreimage(), _contractExecutableWasm)
            };

            var rootInvocation = new SorobanAuthorizedInvocation
            {
                Function = authorizedCreateContractFn,
                SubInvocations = new SorobanAuthorizedInvocation[]
                {
                    new()
                    {
                        Function = authorizedCreateContractFn,
                        SubInvocations = Array.Empty<SorobanAuthorizedInvocation>()
                    }
                }
            };

            var authEntry = new SorobanAuthorizationEntry
            {
                RootInvocation = rootInvocation,
                Credentials = InitCredentials()
            };

            var authEntryXdrBase64 = authEntry.ToXdrBase64();
            var decodedAuthEntry = SorobanAuthorizationEntry.FromXdrBase64(authEntryXdrBase64);
       
            TestEqualInvocations(authEntry.RootInvocation, decodedAuthEntry.RootInvocation);
            
            Assert.AreEqual(authEntry.Credentials.ToXdrBase64(), decodedAuthEntry.Credentials.ToXdrBase64());
        }

        [TestMethod]
        public void TestAuthorizedContractSorobanAuthorizationEntry()
        {
            var contractAddress = new SCContractId("CDJ4RICANSXXZ275W2OY2U7RO73HYURBGBRHVW2UUXZNGEBIVBNRKEF7");
            var authorizedContractFn = new SorobanAuthorizedContractFunction
            {
                HostFunction = new InvokeContractHostFunction(contractAddress, new SCSymbol("hello"),
                    new SCVal[] { new SCBool(false), new SCString("world") })
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

            var authEntry = new SorobanAuthorizationEntry
            {
                RootInvocation = rootInvocation,
                Credentials = InitCredentials()
            };

            var authEntryXdrBase64 = authEntry.ToXdrBase64();
            var decodedAuthEntry = SorobanAuthorizationEntry.FromXdrBase64(authEntryXdrBase64);
       
            TestEqualInvocations(authEntry.RootInvocation, decodedAuthEntry.RootInvocation);
            
            Assert.AreEqual(authEntry.Credentials.ToXdrBase64(), decodedAuthEntry.Credentials.ToXdrBase64());
        }
        
        private void TestEqualInvocations(SorobanAuthorizedInvocation expected, SorobanAuthorizedInvocation actual)
        {
            Assert.AreEqual(expected.Function.GetType(), actual.Function.GetType());
            switch (expected.Function)
            {
                case SorobanAuthorizedContractFunction contractFn:
                    Assert.AreEqual(contractFn.HostFunction.ToXdrBase64(),
                        ((SorobanAuthorizedContractFunction)actual.Function).HostFunction.ToXdrBase64());
                    break;
                case SorobanAuthorizedCreateContractFunction createContractFn:
                    Assert.AreEqual(createContractFn.HostFunction.ToXdrBase64(),
                        ((SorobanAuthorizedCreateContractFunction)actual.Function).HostFunction.ToXdrBase64());
                    break;
            }
            Assert.AreEqual(expected.SubInvocations.Length, actual.SubInvocations.Length);
            for (var i = 0; i < expected.SubInvocations.Length; i++)
            {
                TestEqualInvocations(expected.SubInvocations[0], actual.SubInvocations[0]);
            }
        }
    }
}