using System.Linq;
using FakeItEasy;
using Fare;
using FsCheck;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test.Generators
{
    public static class KeyPairGenerator
    {
        public static Arbitrary<KeyPair> Generate() =>
            Gen.ListOf(32, Arb.Default.Byte().Generator)
                .Select(seed => KeyPair.FromSecretSeed(seed.ToArray())).ToArbitrary();
    }
}