namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        private static byte Equal(byte b, byte c)
        {
            var ub = b;
            var uc = c;
            var x = (byte) (ub ^ uc); /* 0: yes; 1..255: no */
            uint y = x; /* 0: yes; 1..255: no */
            unchecked
            {
                y -= 1;
            } /* 4294967295: yes; 0..254: no */

            y >>= 31; /* 1: yes; 0: no */
            return (byte) y;
        }

        private static byte Negative(sbyte b)
        {
            var x = unchecked((ulong) b); /* 18446744073709551361..18446744073709551615: yes; 0..255: no */
            x >>= 63; /* 1: yes; 0: no */
            return (byte) x;
        }

        private static void Cmov(ref GroupElementPreComp t, ref GroupElementPreComp u, byte b)
        {
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cmov(ref t.yplusx, ref u.yplusx, b);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cmov(ref t.yminusx, ref u.yminusx, b);
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_cmov(ref t.xy2d, ref u.xy2d, b);
        }

        private static void Select(out GroupElementPreComp t, int pos, sbyte b)
        {
            GroupElementPreComp minust;
            var bnegative = Negative(b);
            var babs = (byte) (b - ((-bnegative & b) << 1));

            ge_precomp_0(out t);
            var table = stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.LookupTables.Base[pos];
            Cmov(ref t, ref table[0], Equal(babs, 1));
            Cmov(ref t, ref table[1], Equal(babs, 2));
            Cmov(ref t, ref table[2], Equal(babs, 3));
            Cmov(ref t, ref table[3], Equal(babs, 4));
            Cmov(ref t, ref table[4], Equal(babs, 5));
            Cmov(ref t, ref table[5], Equal(babs, 6));
            Cmov(ref t, ref table[6], Equal(babs, 7));
            Cmov(ref t, ref table[7], Equal(babs, 8));
            minust.yplusx = t.yminusx;
            minust.yminusx = t.yplusx;
            stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.FieldOperations.fe_neg(out minust.xy2d, ref t.xy2d);
            Cmov(ref t, ref minust, bnegative);
        }

        /*
        h = a * B
        where a = a[0]+256*a[1]+...+256^31 a[31]
        B is the Ed25519 base point (x,4/5) with x positive.

        Preconditions:
          a[31] <= 127
        */

        public static void GeScalarmultBase(out GroupElementP3 h, byte[] a, int offset)
        {
            var e = new sbyte[64];
            GroupElementP1P1 r;
            GroupElementPreComp t;
            int i;

            for (i = 0; i < 32; ++i)
            {
                e[2 * i + 0] = (sbyte) ((a[offset + i] >> 0) & 15);
                e[2 * i + 1] = (sbyte) ((a[offset + i] >> 4) & 15);
            }
            /* each e[i] is between 0 and 15 */
            /* e[63] is between 0 and 7 */

            sbyte carry = 0;
            for (i = 0; i < 63; ++i)
            {
                e[i] += carry;
                carry = (sbyte) (e[i] + 8);
                carry >>= 4;
                e[i] -= (sbyte) (carry << 4);
            }

            e[63] += carry;
            /* each e[i] is between -8 and 8 */

            ge_p3_0(out h);
            for (i = 1; i < 64; i += 2)
            {
                Select(out t, i / 2, e[i]);
                ge_madd(out r, ref h, ref t);
                ge_p1p1_to_p3(out h, ref r);
            }

            ge_p3_dbl(out r, ref h);
            ge_p1p1_to_p2(out var s, ref r);
            ge_p2_dbl(out r, ref s);
            ge_p1p1_to_p2(out s, ref r);
            ge_p2_dbl(out r, ref s);
            ge_p1p1_to_p2(out s, ref r);
            ge_p2_dbl(out r, ref s);
            ge_p1p1_to_p3(out h, ref r);

            for (i = 0; i < 64; i += 2)
            {
                Select(out t, i / 2, e[i]);
                ge_madd(out r, ref h, ref t);
                ge_p1p1_to_p3(out h, ref r);
            }
        }
    }
}