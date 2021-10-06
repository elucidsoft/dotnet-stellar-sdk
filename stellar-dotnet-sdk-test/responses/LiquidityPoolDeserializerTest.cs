using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test.responses
{
    public class LiquidityPoolDeserializerTest
    {
        public static void AssertTestData(LiquidityPoolResponse pool)
        {
            Assert.AreEqual(pool.FeeBP, 30);
            Assert.AreEqual(pool.ID, "b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");
            Assert.AreEqual(pool.PagingToken, "b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");

            Assert.AreEqual(pool.Reserves[0].Asset, "native");
            Assert.AreEqual(pool.Reserves[0].Amount, "0.0000000");

            Assert.AreEqual(pool.Reserves[1].Asset, "USDC:GAKMOAANQHJKF5735OYVSQZL6KC3VMFL4LP4ZYY2LWK256TSUG45IEFB");
            Assert.AreEqual(pool.Reserves[1].Amount, "0.0000000");

            Assert.AreEqual(pool.TotalShares, "0.0000000");
            Assert.AreEqual(pool.TotalTrustlines, 2);
            Assert.AreEqual(pool.Type, "constant_product");

            Assert.AreEqual(pool.Links.Operations.Href, "/liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69/operations{?cursor,limit,order}");
            Assert.AreEqual(pool.Links.Self.Href, "liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");
            Assert.AreEqual(pool.Links.Transactions.Href, "/liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69/transactions{?cursor,limit,order}");
        }
    }
}
