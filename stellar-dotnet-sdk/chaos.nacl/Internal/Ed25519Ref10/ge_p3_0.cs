namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        public static void ge_p3_0(out GroupElementP3 h)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_0(out h.X);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out h.Y);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out h.Z);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_0(out h.T);
        }
    }
}