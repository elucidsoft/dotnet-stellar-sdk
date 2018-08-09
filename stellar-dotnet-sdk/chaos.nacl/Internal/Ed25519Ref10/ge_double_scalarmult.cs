namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class GroupOperations
    {
        private static void Slide(sbyte[] r, byte[] a)
        {
            int i;
            int b;

            for (i = 0; i < 256; ++i)
                r[i] = (sbyte) (1 & (a[i >> 3] >> (i & 7)));

            for (i = 0; i < 256; ++i)
                if (r[i] != 0)
                    for (b = 1; b <= 6 && i + b < 256; ++b)
                        if (r[i + b] != 0)
                        {
                            if (r[i] + (r[i + b] << b) <= 15)
                            {
                                r[i] += (sbyte) (r[i + b] << b);
                                r[i + b] = 0;
                            }
                            else if (r[i] - (r[i + b] << b) >= -15)
                            {
                                r[i] -= (sbyte) (r[i + b] << b);
                                int k;
                                for (k = i + b; k < 256; ++k)
                                {
                                    if (r[k] == 0)
                                    {
                                        r[k] = 1;
                                        break;
                                    }

                                    r[k] = 0;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
        }

        /*
        r = a * A + b * B
        where a = a[0]+256*a[1]+...+256^31 a[31].
        and b = b[0]+256*b[1]+...+256^31 b[31].
        B is the Ed25519 base point (x,4/5) with x positive.
        */

        public static void ge_double_scalarmult_vartime(out GroupElementP2 r, byte[] a, ref GroupElementP3 A, byte[] b)
        {
            var Bi = stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.LookupTables.Base2;
            var aslide = new sbyte[256];
            var bslide = new sbyte[256];
            var ai = new GroupElementCached[8]; /* A,3A,5A,7A,9A,11A,13A,15A */
            int i;

            Slide(aslide, a);
            Slide(bslide, b);

            GroupOperations.ge_p3_to_cached(out ai[0], ref A);
            GroupOperations.ge_p3_dbl(out var t, ref A);
            GroupOperations.ge_p1p1_to_p3(out var A2, ref t);
            ge_add(out t, ref A2, ref ai[0]);
            GroupOperations.ge_p1p1_to_p3(out var u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[1], ref u);
            ge_add(out t, ref A2, ref ai[1]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[2], ref u);
            ge_add(out t, ref A2, ref ai[2]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[3], ref u);
            ge_add(out t, ref A2, ref ai[3]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[4], ref u);
            ge_add(out t, ref A2, ref ai[4]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[5], ref u);
            ge_add(out t, ref A2, ref ai[5]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[6], ref u);
            ge_add(out t, ref A2, ref ai[6]);
            GroupOperations.ge_p1p1_to_p3(out u, ref t);
            GroupOperations.ge_p3_to_cached(out ai[7], ref u);

            GroupOperations.ge_p2_0(out r);

            for (i = 255; i >= 0; --i)
                if (aslide[i] != 0 || bslide[i] != 0)
                    break;

            for (; i >= 0; --i)
            {
                GroupOperations.ge_p2_dbl(out t, ref r);

                if (aslide[i] > 0)
                {
                    GroupOperations.ge_p1p1_to_p3(out u, ref t);
                    ge_add(out t, ref u, ref ai[aslide[i] / 2]);
                }
                else if (aslide[i] < 0)
                {
                    GroupOperations.ge_p1p1_to_p3(out u, ref t);
                    GroupOperations.GeSub(out t, ref u, ref ai[-aslide[i] / 2]);
                }

                if (bslide[i] > 0)
                {
                    GroupOperations.ge_p1p1_to_p3(out u, ref t);
                    GroupOperations.ge_madd(out t, ref u, ref Bi[bslide[i] / 2]);
                }
                else if (bslide[i] < 0)
                {
                    GroupOperations.ge_p1p1_to_p3(out u, ref t);
                    GroupOperations.ge_msub(out t, ref u, ref Bi[-bslide[i] / 2]);
                }

                GroupOperations.ge_p1p1_to_p2(out r, ref t);
            }
        }
    }
}