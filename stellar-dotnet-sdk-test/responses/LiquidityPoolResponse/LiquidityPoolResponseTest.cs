using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using XDR = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class LiquidityPoolResponseTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/LiquidityPoolResponse", "Data.json"));
            var instance = JsonSingleton.GetInstance<LiquidityPoolResponse>(json);

            Assert.AreEqual(new LiquidityPoolID("67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9"), instance.ID);
            Assert.AreEqual("113725249324879873", instance.PagingToken);
            Assert.AreEqual(30, instance.FeeBP);
            Assert.AreEqual(XDR.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, instance.Type);
            Assert.AreEqual("300", instance.TotalTrustlines);
            Assert.AreEqual("5000.0000000", instance.TotalShares);

            Assert.AreEqual("1000.0000005", instance.Reserves[0].Amount);
            Assert.AreEqual("EURT:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", instance.Reserves[0].Asset.CanonicalName());

            Assert.AreEqual("2000.0000000", instance.Reserves[1].Amount);
            Assert.AreEqual("PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", instance.Reserves[1].Asset.CanonicalName());

            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/effects{?cursor,limit,order}", instance.Links.Effects.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/operations{?cursor,limit,order}", instance.Links.Operations.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9", instance.Links.Self.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/transactions{?cursor,limit,order}", instance.Links.Transactions.Href);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/LiquidityPoolResponse", "Data.json"));
            var instance = JsonSingleton.GetInstance<LiquidityPoolResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<LiquidityPoolResponse>(serialized);

            var parsed = (LiquidityPoolResponse)back;
            Assert.AreEqual(new LiquidityPoolID("67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9"), parsed.ID);
            Assert.AreEqual("113725249324879873", parsed.PagingToken);
            Assert.AreEqual(30, parsed.FeeBP);
            Assert.AreEqual(XDR.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, parsed.Type);
            Assert.AreEqual("300", parsed.TotalTrustlines);
            Assert.AreEqual("5000.0000000", parsed.TotalShares);

            Assert.AreEqual("1000.0000005", parsed.Reserves[0].Amount);
            Assert.AreEqual("EURT:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", parsed.Reserves[0].Asset.CanonicalName());

            Assert.AreEqual("2000.0000000", parsed.Reserves[1].Amount);
            Assert.AreEqual("PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", parsed.Reserves[1].Asset.CanonicalName());

            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/effects{?cursor,limit,order}", parsed.Links.Effects.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/operations{?cursor,limit,order}", parsed.Links.Operations.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9", parsed.Links.Self.Href);
            Assert.AreEqual("/liquidity_pools/67260c4c1807b262ff851b0a3fe141194936bb0215b2f77447f1df11998eabb9/transactions{?cursor,limit,order}", parsed.Links.Transactions.Href);
        }

        [TestMethod]
        public void TestReserveEquality()
        {
            LiquidityPoolResponse.Reserve a = new LiquidityPoolResponse.Reserve("2000.0000000", Asset.Create("PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S"));
            LiquidityPoolResponse.Reserve b = new LiquidityPoolResponse.Reserve("2000.0000000", Asset.Create("PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S"));
            LiquidityPoolResponse.Reserve c = new LiquidityPoolResponse.Reserve("1000.0000005", Asset.Create("PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S"));
            LiquidityPoolResponse.Reserve d = new LiquidityPoolResponse.Reserve("2000.0000000", Asset.Create("EURT:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S"));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(c, d);
        }
    }
}
