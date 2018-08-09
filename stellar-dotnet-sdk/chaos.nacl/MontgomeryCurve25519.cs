using stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10;
using stellar_dotnet_sdk.chaos.nacl.Internal.Salsa;
using FieldOperations = stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations;

namespace stellar_dotnet_sdk.chaos.nacl
{
    // This class is mainly for compatibility with NaCl's Curve25519 implementation
    // If you don't need that compatibility, use Ed25519.KeyExchange
    public static class MontgomeryCurve25519
    {
        private static readonly byte[] Zero16 = new byte[16];

        // hashes like the NaCl paper says instead i.e. HSalsa(x,0)
        internal static void KeyExchangeOutputHashNaCl(byte[] sharedKey, int offset)
        {
            Salsa20.HSalsa20(sharedKey, offset, sharedKey, offset, Zero16, 0);
        }

        internal static void EdwardsToMontgomeryX(out FieldElement montgomeryX, ref FieldElement edwardsY,
            ref FieldElement edwardsZ)
        {
            FieldOperations.fe_add(out var tempX, ref edwardsZ, ref edwardsY);
            FieldOperations.fe_sub(out var tempZ, ref edwardsZ, ref edwardsY);
            FieldOperations.fe_invert(out tempZ, ref tempZ);
            FieldOperations.fe_mul(out montgomeryX, ref tempX, ref tempZ);
        }
    }
}