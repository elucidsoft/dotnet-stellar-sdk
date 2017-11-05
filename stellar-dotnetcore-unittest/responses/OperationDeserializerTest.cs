using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class OperationDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeCreateAccountOperation()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "operationCreateAccount.json"));
            var operation = JsonSingleton.GetInstance<CreateAccountOperationResponse>(json);


        }
    }
}
