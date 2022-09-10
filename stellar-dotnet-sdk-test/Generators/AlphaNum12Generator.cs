using System.Linq;
using Fare;
using FsCheck;

namespace stellar_dotnet_sdk_test.Generators
{
    public static class AlphaNum12Generator
    {
        public static Arbitrary<string> Generate()
        {
            var codeLength = Gen.Choose(5, 12);
            var lowerCaseLetter = Gen.Choose('a', 'z');
            var upperCaseLetter = Gen.Choose('A', 'Z');
            var number = Gen.Choose('0', '9');
            var validCharacter =
                Gen.OneOf(lowerCaseLetter, upperCaseLetter, number)
                    .Select(character => (char)character);

            return
                codeLength
                    .SelectMany(length => Gen.ListOf(length, validCharacter))
                    .Select(characters => new string(characters.ToArray()))
                    .ToArbitrary();
        }
    }
}
