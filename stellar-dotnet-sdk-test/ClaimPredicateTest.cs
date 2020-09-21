using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class ClaimPredicateTest
    {
        [TestMethod]
        public void TestClaimPredicateBeforeAbsoluteTime()
        {
            var predicate = ClaimPredicate.BeforeAbsoluteTime(1600720493);
            var xdr = predicate.ToXdr();

            var parsed = (ClaimPredicateBeforeAbsoluteTime) ClaimPredicate.FromXdr(xdr);

            Assert.AreEqual(1600720493, parsed.DateTime.ToUnixTimeSeconds());
        }

        [TestMethod]
        public void TestClaimPredicateBeforeRelativeTime()
        {
            var predicate = ClaimPredicate.BeforeRelativeTime(120);
            var xdr = predicate.ToXdr();

            var parsed = (ClaimPredicateBeforeRelativeTime) ClaimPredicate.FromXdr(xdr);

            Assert.AreEqual(120.0, parsed.Duration.TotalSeconds);
        }

        [TestMethod]
        public void TestClaimPredicateNot()
        {
            var predicate = ClaimPredicate.Not(ClaimPredicate.BeforeRelativeTime(120));
            var xdr = predicate.ToXdr();

            var parsed = (ClaimPredicateNot) ClaimPredicate.FromXdr(xdr);

            Assert.IsNotNull(parsed.Predicate);
        }

        [TestMethod]
        public void TestClaimPredicateAnd()
        {
            var predicate = ClaimPredicate.And(
                ClaimPredicate.BeforeRelativeTime(120),
                ClaimPredicate.BeforeRelativeTime(240));
            var xdr = predicate.ToXdr();

            var parsed = (ClaimPredicateAnd) ClaimPredicate.FromXdr(xdr);

            Assert.IsNotNull(parsed.LeftPredicate);
            Assert.IsNotNull(parsed.RightPredicate);
        }

        [TestMethod]
        public void TestClaimPredicateOr()
        {
            var predicate = ClaimPredicate.Or(
                ClaimPredicate.BeforeRelativeTime(120),
                ClaimPredicate.BeforeRelativeTime(240));
            var xdr = predicate.ToXdr();

            var parsed = (ClaimPredicateOr) ClaimPredicate.FromXdr(xdr);

            Assert.IsNotNull(parsed.LeftPredicate);
            Assert.IsNotNull(parsed.RightPredicate);
        }        
    }
}