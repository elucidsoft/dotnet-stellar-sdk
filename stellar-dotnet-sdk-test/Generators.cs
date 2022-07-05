using Fare;
using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace stellar_dotnet_sdk_test
{
    public static class AlphaNum4Generator
    {
        public static Arbitrary<string> Generate() 
        {
            var regexGenerator = new Xeger("([a-zA-Z0-9]){1,4}");

            return Gen.Sized(assetCodeLength => Gen.Elements(Enumerable.Range(1, assetCodeLength).Select(_ => regexGenerator.Generate()))).ToArbitrary();
        }
    }

    public static class AlphaNum12Generator
    {
        public static Arbitrary<string> Generate()
        {
            var regexGenerator = new Xeger("([a-zA-Z0-9]){5,12}");

            return Gen.Sized(assetCodeLength => Gen.Elements(Enumerable.Range(1, assetCodeLength).Select(_ => regexGenerator.Generate()))).ToArbitrary();
        }
    }
}
