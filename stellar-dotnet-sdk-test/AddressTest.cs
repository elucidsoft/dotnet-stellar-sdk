using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void TestInvalidAccountId()
        {
            const string invalidAccountId = "Invalidid";
            try
            {
                new SCAccountId(invalidAccountId);
            }
            catch (InvalidOperationException exception)
            {
                Assert.AreEqual("Invalid account id", exception.Message);
            }
        }
        
        [TestMethod]
        public void TestInvalidContractId()
        {
            const string invalidContractId = "Invalidid";
            try
            {
                new SCContractId(invalidContractId);
            }
            catch (InvalidOperationException exception)
            {
                Assert.AreEqual("Invalid contract id", exception.Message);
            }
        }
        
        [TestMethod]
        public void TestAccountId()
        {
            var scAccountId = new SCAccountId("GCZFMH32MF5EAWETZTKF3ZV5SEVJPI53UEMDNSW55WBR75GMZJU4U573");
            
            // Act
            var scAccountIdXdrBase64 = scAccountId.ToXdrBase64();
            var fromXdrBase64ScAccountId = (SCAccountId)SCVal.FromXdrBase64(scAccountIdXdrBase64);

            // Assert
            Assert.AreEqual(scAccountId.InnerValue, fromXdrBase64ScAccountId.InnerValue);
        }
        
        [TestMethod]
        public void TestContractId()
        {
            var scContractId = new SCContractId("CAC2UYJQMC4ISUZ5REYB2AMDC44YKBNZWG4JB6N6GBL66CEKQO3RDSAB");
            
            // Act
            var scContractIdXdrBase64 = scContractId.ToXdrBase64();
            var fromXdrBase64ScContractId = (SCContractId)SCVal.FromXdrBase64(scContractIdXdrBase64);

            // Assert
            Assert.AreEqual(scContractId.InnerValue, fromXdrBase64ScContractId.InnerValue);
        }
    }
}