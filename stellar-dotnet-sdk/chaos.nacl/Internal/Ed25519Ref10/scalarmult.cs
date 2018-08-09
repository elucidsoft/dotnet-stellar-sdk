namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    public static class MontgomeryOperations
    {
        internal static void ScalarMult(
            out FieldElement q,
            byte[] n, int noffset,
            ref FieldElement p)
        {
            var e = new byte[32];
            uint i;
            int pos;

            for (i = 0; i < 32; ++i)
                e[i] = n[noffset + i];
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.ScalarOperations.ScClamp(e, 0);
            var x1 = p;
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out var x2);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_0(out var z2);
            var x3 = x1;
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_1(out var z3);

            uint swap = 0;
            for (pos = 254; pos >= 0; --pos)
            {
                var b = (uint) (e[pos / 8] >> (pos & 7));
                b &= 1;
                swap ^= b;
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cswap(ref x2, ref x3, swap);
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cswap(ref z2, ref z3, swap);
                swap = b;
                /* qhasm: fe X2 */

                /* qhasm: fe Z2 */

                /* qhasm: fe X3 */

                /* qhasm: fe Z3 */

                /* qhasm: fe X4 */

                /* qhasm: fe Z4 */

                /* qhasm: fe X5 */

                /* qhasm: fe Z5 */

                /* qhasm: fe A */

                /* qhasm: fe B */

                /* qhasm: fe C */

                /* qhasm: fe D */

                /* qhasm: fe E */

                /* qhasm: fe AA */

                /* qhasm: fe BB */

                /* qhasm: fe DA */

                /* qhasm: fe CB */

                /* qhasm: fe t0 */

                /* qhasm: fe t1 */

                /* qhasm: fe t2 */

                /* qhasm: fe t3 */

                /* qhasm: fe t4 */

                /* qhasm: enter ladder */

                /* qhasm: D = X3-Z3 */
                /* asm 1: fe_sub(>D=fe#5,<X3=fe#3,<Z3=fe#4); */
                /* asm 2: fe_sub(>D=tmp0,<X3=x3,<Z3=z3); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out var tmp0, ref x3, ref z3);

                /* qhasm: B = X2-Z2 */
                /* asm 1: fe_sub(>B=fe#6,<X2=fe#1,<Z2=fe#2); */
                /* asm 2: fe_sub(>B=tmp1,<X2=x2,<Z2=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out var tmp1, ref x2, ref z2);

                /* qhasm: A = X2+Z2 */
                /* asm 1: fe_add(>A=fe#1,<X2=fe#1,<Z2=fe#2); */
                /* asm 2: fe_add(>A=x2,<X2=x2,<Z2=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out x2, ref x2, ref z2);

                /* qhasm: C = X3+Z3 */
                /* asm 1: fe_add(>C=fe#2,<X3=fe#3,<Z3=fe#4); */
                /* asm 2: fe_add(>C=z2,<X3=x3,<Z3=z3); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out z2, ref x3, ref z3);

                /* qhasm: DA = D*A */
                /* asm 1: fe_mul(>DA=fe#4,<D=fe#5,<A=fe#1); */
                /* asm 2: fe_mul(>DA=z3,<D=tmp0,<A=x2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out z3, ref tmp0, ref x2);

                /* qhasm: CB = C*B */
                /* asm 1: fe_mul(>CB=fe#2,<C=fe#2,<B=fe#6); */
                /* asm 2: fe_mul(>CB=z2,<C=z2,<B=tmp1); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out z2, ref z2, ref tmp1);

                /* qhasm: BB = B^2 */
                /* asm 1: fe_sq(>BB=fe#5,<B=fe#6); */
                /* asm 2: fe_sq(>BB=tmp0,<B=tmp1); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out tmp0, ref tmp1);

                /* qhasm: AA = A^2 */
                /* asm 1: fe_sq(>AA=fe#6,<A=fe#1); */
                /* asm 2: fe_sq(>AA=tmp1,<A=x2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out tmp1, ref x2);

                /* qhasm: t0 = DA+CB */
                /* asm 1: fe_add(>t0=fe#3,<DA=fe#4,<CB=fe#2); */
                /* asm 2: fe_add(>t0=x3,<DA=z3,<CB=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out x3, ref z3, ref z2);

                /* qhasm: assign x3 to t0 */

                /* qhasm: t1 = DA-CB */
                /* asm 1: fe_sub(>t1=fe#2,<DA=fe#4,<CB=fe#2); */
                /* asm 2: fe_sub(>t1=z2,<DA=z3,<CB=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out z2, ref z3, ref z2);

                /* qhasm: X4 = AA*BB */
                /* asm 1: fe_mul(>X4=fe#1,<AA=fe#6,<BB=fe#5); */
                /* asm 2: fe_mul(>X4=x2,<AA=tmp1,<BB=tmp0); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out x2, ref tmp1, ref tmp0);

                /* qhasm: E = AA-BB */
                /* asm 1: fe_sub(>E=fe#6,<AA=fe#6,<BB=fe#5); */
                /* asm 2: fe_sub(>E=tmp1,<AA=tmp1,<BB=tmp0); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sub(out tmp1, ref tmp1, ref tmp0);

                /* qhasm: t2 = t1^2 */
                /* asm 1: fe_sq(>t2=fe#2,<t1=fe#2); */
                /* asm 2: fe_sq(>t2=z2,<t1=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out z2, ref z2);

                /* qhasm: t3 = a24*E */
                /* asm 1: fe_mul121666(>t3=fe#4,<E=fe#6); */
                /* asm 2: fe_mul121666(>t3=z3,<E=tmp1); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul121666(out z3, ref tmp1);

                /* qhasm: X5 = t0^2 */
                /* asm 1: fe_sq(>X5=fe#3,<t0=fe#3); */
                /* asm 2: fe_sq(>X5=x3,<t0=x3); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_sq(out x3, ref x3);

                /* qhasm: t4 = BB+t3 */
                /* asm 1: fe_add(>t4=fe#5,<BB=fe#5,<t3=fe#4); */
                /* asm 2: fe_add(>t4=tmp0,<BB=tmp0,<t3=z3); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_add(out tmp0, ref tmp0, ref z3);

                /* qhasm: Z5 = X1*t2 */
                /* asm 1: fe_mul(>Z5=fe#4,x1,<t2=fe#2); */
                /* asm 2: fe_mul(>Z5=z3,x1,<t2=z2); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out z3, ref x1, ref z2);

                /* qhasm: Z4 = E*t4 */
                /* asm 1: fe_mul(>Z4=fe#2,<E=fe#6,<t4=fe#5); */
                /* asm 2: fe_mul(>Z4=z2,<E=tmp1,<t4=tmp0); */
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out z2, ref tmp1, ref tmp0);

                /* qhasm: return */
            }

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cswap(ref x2, ref x3, swap);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cswap(ref z2, ref z3, swap);

            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_invert(out z2, ref z2);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_mul(out x2, ref x2, ref z2);
            q = x2;
            CryptoBytes.Wipe(e);
        }
    }
}