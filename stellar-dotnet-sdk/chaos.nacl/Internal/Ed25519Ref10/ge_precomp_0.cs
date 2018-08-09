namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        public static void ge_precomp_0(out GroupElementPreComp h)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out h.yplusx);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out h.yminusx);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_0(out h.xy2d);
        }
    }
}