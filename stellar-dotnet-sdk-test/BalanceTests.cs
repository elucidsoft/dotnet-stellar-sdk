using FakeItEasy;
using FluentAssertions;
using FsCheck;
using FsCheck.NUnit;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using System;
using stellar_dotnet_sdk_test.Generators;

namespace stellar_dotnet_sdk_test
{
    [NUnit.Framework.TestFixture]
    public class BalanceTests
    {
        private Balance SUT { get; set; }

        #region Constructor Tests

        [NUnit.Framework.TestCase(true, false)]
        [NUnit.Framework.TestCase(false, true)]
        public void Constructor_LiquidityPool(bool expectedIsAuthorized, bool expectedIsAuthorizedToMaintainLiabilities)
        {
            const string ExpectedLiquidityPoolId = "1c80ecd9cc567ef5301683af3ca7c2deeba7d519275325549f22514076396469";
            const string ExpectedAssetType = "liquidity_pool_shares";
            const string ExpectedLimit = "1.0";
            const string ExpectedBalance = "2.0";

            // setup
            this.SUT = new Balance(
                ExpectedAssetType,
                null,
                null,
                ExpectedBalance,
                ExpectedLimit,
                null,
                null,
                expectedIsAuthorized,
                expectedIsAuthorizedToMaintainLiabilities,
                ExpectedLiquidityPoolId);

            // expected
            this.SUT.Asset
                .Should().BeNull();

            this.SUT.AssetType
                .Should().Be(ExpectedAssetType);

            this.SUT.Limit
                .Should().Be(ExpectedLimit);

            this.SUT.BalanceString
                .Should().Be(ExpectedBalance);

            this.SUT.LiquidityPoolId
                .Should().Be(ExpectedLiquidityPoolId);

            this.SUT.AssetCode
                .Should().BeNull();

            this.SUT.AssetIssuer
                .Should().BeNull();

            this.SUT.BuyingLiabilities
                .Should().BeNull();

            this.SUT.SellingLiabilities
                .Should().BeNull();

            this.SUT.IsAuthorized
                .Should().Be(expectedIsAuthorized);

            this.SUT.IsAuthorizedToMaintainLiabilities
                .Should().Be(expectedIsAuthorizedToMaintainLiabilities);
        }

        [NUnit.Framework.TestCase("ABC4", true, false, AssetTypeCreditAlphaNum4.RestApiType, typeof(AssetTypeCreditAlphaNum4))]
        [NUnit.Framework.TestCase("ABC12", false, true, AssetTypeCreditAlphaNum12.RestApiType, typeof(AssetTypeCreditAlphaNum12))]
        public void Constructor_CreditAlphaNum(
            string expectedAssetCode,
            bool expectedIsAuthorized,
            bool expectedIsAuthorizedToMaintainLiabilities,
            string expectedAssetType,
            Type expectedAssetDataType)
        {
            const string ExpectedLimit = "1.0";
            const string ExpectedBalance = "2.0";
            const string ExpectedAssetIssuer = "Expected Asset Issuer";
            const string ExpectedBuyingLiabilities = "3.0";
            const string ExpectedSellingLiabilities = "4.0";

            // setup
            this.SUT = new Balance(
                expectedAssetType,
                expectedAssetCode,
                ExpectedAssetIssuer,
                ExpectedBalance,
                ExpectedLimit,
                ExpectedBuyingLiabilities,
                ExpectedSellingLiabilities,
                expectedIsAuthorized,
                expectedIsAuthorizedToMaintainLiabilities,
                null);

            // expected
            this.SUT.Asset
                .Should().BeOfType(expectedAssetDataType);

            this.SUT.AssetCode
                .Should().Be(expectedAssetCode);

            this.SUT.AssetIssuer
                .Should().Be(ExpectedAssetIssuer);

            this.SUT.AssetType
                .Should().Be(expectedAssetType);

            this.SUT.Limit
                .Should().Be(ExpectedLimit);

            this.SUT.BalanceString
                .Should().Be(ExpectedBalance);

            this.SUT.LiquidityPoolId
                .Should().BeNull();

            this.SUT.BuyingLiabilities
                .Should().Be(ExpectedBuyingLiabilities);

            this.SUT.SellingLiabilities
                .Should().Be(ExpectedSellingLiabilities);

            this.SUT.IsAuthorized
                .Should().Be(expectedIsAuthorized);

            this.SUT.IsAuthorizedToMaintainLiabilities
                .Should().Be(expectedIsAuthorizedToMaintainLiabilities);
        }

        #endregion Constructor Tests

        #region Asset Tests

        [Property(Arbitrary = new[] { typeof(AlphaNum4Generator) })]
        public Property Asset_AlphaNum4(string assetCode)
        {
            // setup
            this.SUT = new Balance(
                AssetTypeCreditAlphaNum4.RestApiType,
                assetCode,
                A.Dummy<string>(),
                "0.0",
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<bool>(),
                A.Dummy<bool>(),
                null);

            return (this.SUT.Asset is AssetTypeCreditAlphaNum4).ToProperty();
        }

        [Property(Arbitrary = new[] { typeof(AlphaNum12Generator) })]
        public Property Asset_AlphaNum12(string assetCode)
        {
            // setup
            this.SUT = new Balance(
                AssetTypeCreditAlphaNum12.RestApiType,
                assetCode,
                A.Dummy<string>(),
                "0.0",
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<bool>(),
                A.Dummy<bool>(),
                null);

            return (this.SUT.Asset is AssetTypeCreditAlphaNum12).ToProperty();
        }

        [NUnit.Framework.Test]
        public void Asset_Native()
        {
            // setup
            this.SUT = new Balance(
                AssetTypeNative.RestApiType,
                null,
                A.Dummy<string>(),
                "0.0",
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<bool>(),
                A.Dummy<bool>(),
                null);

            // actual 
            var actual = this.SUT.Asset;

            // expected
            actual
                .Should().BeOfType<AssetTypeNative>();
        }

        [NUnit.Framework.Test]
        public void Asset_LiquidityPool()
        {
            const string ExpectedLiquidityPoolId = "1c80ecd9cc567ef5301683af3ca7c2deeba7d519275325549f22514076396469";

            // setup
            this.SUT = new Balance(
                "liquidity_pool_shares",
                A.Dummy<string>(),
                A.Dummy<string>(),
                "0.0",
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<string>(),
                A.Dummy<bool>(),
                A.Dummy<bool>(),
                ExpectedLiquidityPoolId);

            // actual 
            var actual = this.SUT.Asset;

            // expected
            actual
                .Should().BeNull();
        }

        #endregion Asset Tests
    }
}
