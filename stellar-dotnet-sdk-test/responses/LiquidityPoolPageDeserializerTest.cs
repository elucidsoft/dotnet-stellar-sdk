using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;
using XDR = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test.responses
{
    public class LiquidityPoolPageDeserializerTest
    {
        public static void AssertTestData(Page<LiquidityPoolResponse> poolsPage)
        {
            Assert.AreEqual(poolsPage.Records[1].FeeBP, 30);
            Assert.AreEqual(poolsPage.Records[1].ID.ToString(), "b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");
            Assert.AreEqual(poolsPage.Records[1].PagingToken, "b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");

            Assert.AreEqual(poolsPage.Records[1].Reserves[0].Asset.CanonicalName(), "native");
            Assert.AreEqual(poolsPage.Records[1].Reserves[0].Amount, "0.0000000");

            Assert.AreEqual(poolsPage.Records[1].Reserves[1].Asset.CanonicalName(), "USDC:GAKMOAANQHJKF5735OYVSQZL6KC3VMFL4LP4ZYY2LWK256TSUG45IEFB");
            Assert.AreEqual(poolsPage.Records[1].Reserves[1].Amount, "0.0000000");

            Assert.AreEqual(poolsPage.Records[1].TotalShares, "0.0000000");
            Assert.AreEqual(poolsPage.Records[1].TotalTrustlines, "2");
            Assert.AreEqual(poolsPage.Records[1].Type, XDR.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT);

            Assert.AreEqual(poolsPage.Records[1].Links.Operations.Href, "https://horizon-testnet.stellar.org/liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69/operations{?cursor,limit,order}");
            Assert.AreEqual(poolsPage.Records[1].Links.Self.Href, "https://horizon-testnet.stellar.org/liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69");
            Assert.AreEqual(poolsPage.Records[1].Links.Transactions.Href, "https://horizon-testnet.stellar.org/liquidity_pools/b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69/transactions{?cursor,limit,order}");
        }
    }
}
