using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class PredicateDeserializerTest
    {
        [TestMethod]
        public void TestPredicateDeserialize()
        {
            var json = "{\"and\":[{\"or\":[{\"relBefore\":12},{\"absBefore\":\"2020-08-26T11:15:39Z\"}]},{\"not\":{\"unconditional\":true}}]}";
            var predicate = JsonConvert.DeserializeObject<Predicate>(json);
            var claimPredicate = predicate.ToClaimPredicate();

            var andPredicate = (ClaimPredicateAnd) claimPredicate;
            Assert.IsNotNull(andPredicate);
            
            var orPredicate = (ClaimPredicateOr) andPredicate.LeftPredicate;
            Assert.IsNotNull(orPredicate);
            var notPredicate = (ClaimPredicateNot) andPredicate.RightPredicate;
            Assert.IsNotNull(notPredicate);

            var relBefore = (ClaimPredicateBeforeRelativeTime) orPredicate.LeftPredicate;
            Assert.IsNotNull(relBefore);
            var absBefore = (ClaimPredicateBeforeAbsoluteTime) orPredicate.RightPredicate;
            Assert.IsNotNull(absBefore);

            var unconditional = (ClaimPredicateUnconditional) notPredicate.Predicate;
            Assert.IsNotNull(unconditional);
        }
    }
}