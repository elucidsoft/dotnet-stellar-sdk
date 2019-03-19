using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class BumpSequenceResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAALAAAAAAAAAAA=", typeof(BumpSequenceSuccess), true);
        }

        [TestMethod]
        public void TestBadSeq()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAL/////wAAAAA=", typeof(BumpSequenceBadSeq), false);
        }
    }
}