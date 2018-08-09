namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        public static int ge_frombytes_negate_vartime(out GroupElementP3 h, byte[] data, int offset)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_frombytes(out h.Y, data, offset);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out h.Z);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out var u, ref h.Y);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out var v, ref u, ref stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.LookupTables.D);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out u, ref u, ref h.Z); /* u = y^2-1 */
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out v, ref v, ref h.Z); /* v = dy^2+1 */

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out var v3, ref v);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out v3, ref v3, ref v); /* v3 = v^3 */
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out h.X, ref v3);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.X, ref h.X, ref v);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.X, ref h.X, ref u); /* x = uv^7 */

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_pow22523(out h.X, ref h.X); /* x = (uv^7)^((q-5)/8) */
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.X, ref h.X, ref v3);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.X, ref h.X, ref u); /* x = uv^3(uv^7)^((q-5)/8) */

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out var vxx, ref h.X);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out vxx, ref vxx, ref v);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out var check, ref vxx, ref u); /* vx^2-u */
            if (stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_isnonzero(ref check) != 0)
            {
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out check, ref vxx, ref u); /* vx^2+u */
                if (stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_isnonzero(ref check) != 0)
                {
                    h = default(GroupElementP3);
                    return -1;
                }

                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.X, ref h.X, ref stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.LookupTables.Sqrtm1);
            }

            if (stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_isnegative(ref h.X) == data[offset + 31] >> 7)
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_neg(out h.X, ref h.X);

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out h.T, ref h.X, ref h.Y);
            return 0;
        }
    }
}