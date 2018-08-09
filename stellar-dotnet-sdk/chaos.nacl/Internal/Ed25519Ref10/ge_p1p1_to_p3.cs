namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        /*
        r = p
        */
        public static void ge_p1p1_to_p3(out GroupElementP3 r, ref GroupElementP1P1 p)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out r.X, ref p.X, ref p.T);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out r.Y, ref p.Y, ref p.Z);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out r.Z, ref p.Z, ref p.T);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out r.T, ref p.X, ref p.Y);
        }
    }
}