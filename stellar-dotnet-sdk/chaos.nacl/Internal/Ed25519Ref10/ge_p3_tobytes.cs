namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        public static void ge_p3_tobytes(byte[] s, int offset, ref GroupElementP3 h)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_invert(out var recip, ref h.Z);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out var x, ref h.X, ref recip);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out var y, ref h.Y, ref recip);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_tobytes(s, offset, ref y);
            s[offset + 31] ^= (byte) (stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_isnegative(ref x) << 7);
        }
    }
}