namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class FieldOperations
    {
        /*
        h = f * g
        Can overlap h with f or g.

        Preconditions:
           |f| bounded by 1.65*2^26,1.65*2^25,1.65*2^26,1.65*2^25,etc.
           |g| bounded by 1.65*2^26,1.65*2^25,1.65*2^26,1.65*2^25,etc.

        Postconditions:
           |h| bounded by 1.01*2^25,1.01*2^24,1.01*2^25,1.01*2^24,etc.
        */

        /*
        Notes on implementation strategy:

        Using schoolbook multiplication.
        Karatsuba would save a little in some cost models.

        Most multiplications by 2 and 19 are 32-bit precomputations;
        cheaper than 64-bit postcomputations.

        There is one remaining multiplication by 19 in the carry chain;
        one *19 precomputation can be merged into this,
        but the resulting data flow is considerably less clean.

        There are 12 carries below.
        10 of them are 2-way parallelizable and vectorizable.
        Can get away with 11 carries, but then data flow is much deeper.

        With tighter constraints on inputs can squeeze carries into int32.
        */

        internal static void fe_mul(out FieldElement h, ref FieldElement f, ref FieldElement g)
        {
            var f0 = f.x0;
            var f1 = f.x1;
            var f2 = f.x2;
            var f3 = f.x3;
            var f4 = f.x4;
            var f5 = f.x5;
            var f6 = f.x6;
            var f7 = f.x7;
            var f8 = f.x8;
            var f9 = f.x9;
            var g0 = g.x0;
            var g1 = g.x1;
            var g2 = g.x2;
            var g3 = g.x3;
            var g4 = g.x4;
            var g5 = g.x5;
            var g6 = g.x6;
            var g7 = g.x7;
            var g8 = g.x8;
            var g9 = g.x9;
            var g1_19 = 19 * g1; /* 1.959375*2^29 */
            var g2_19 = 19 * g2; /* 1.959375*2^30; still ok */
            var g3_19 = 19 * g3;
            var g4_19 = 19 * g4;
            var g5_19 = 19 * g5;
            var g6_19 = 19 * g6;
            var g7_19 = 19 * g7;
            var g8_19 = 19 * g8;
            var g9_19 = 19 * g9;
            var f1_2 = 2 * f1;
            var f3_2 = 2 * f3;
            var f5_2 = 2 * f5;
            var f7_2 = 2 * f7;
            var f9_2 = 2 * f9;
            var f0g0 = f0 * (long) g0;
            var f0g1 = f0 * (long) g1;
            var f0g2 = f0 * (long) g2;
            var f0g3 = f0 * (long) g3;
            var f0g4 = f0 * (long) g4;
            var f0g5 = f0 * (long) g5;
            var f0g6 = f0 * (long) g6;
            var f0g7 = f0 * (long) g7;
            var f0g8 = f0 * (long) g8;
            var f0g9 = f0 * (long) g9;
            var f1g0 = f1 * (long) g0;
            var f1g1_2 = f1_2 * (long) g1;
            var f1g2 = f1 * (long) g2;
            var f1g3_2 = f1_2 * (long) g3;
            var f1g4 = f1 * (long) g4;
            var f1g5_2 = f1_2 * (long) g5;
            var f1g6 = f1 * (long) g6;
            var f1g7_2 = f1_2 * (long) g7;
            var f1g8 = f1 * (long) g8;
            var f1g9_38 = f1_2 * (long) g9_19;
            var f2g0 = f2 * (long) g0;
            var f2g1 = f2 * (long) g1;
            var f2g2 = f2 * (long) g2;
            var f2g3 = f2 * (long) g3;
            var f2g4 = f2 * (long) g4;
            var f2g5 = f2 * (long) g5;
            var f2g6 = f2 * (long) g6;
            var f2g7 = f2 * (long) g7;
            var f2g8_19 = f2 * (long) g8_19;
            var f2g9_19 = f2 * (long) g9_19;
            var f3g0 = f3 * (long) g0;
            var f3g1_2 = f3_2 * (long) g1;
            var f3g2 = f3 * (long) g2;
            var f3g3_2 = f3_2 * (long) g3;
            var f3g4 = f3 * (long) g4;
            var f3g5_2 = f3_2 * (long) g5;
            var f3g6 = f3 * (long) g6;
            var f3g7_38 = f3_2 * (long) g7_19;
            var f3g8_19 = f3 * (long) g8_19;
            var f3g9_38 = f3_2 * (long) g9_19;
            var f4g0 = f4 * (long) g0;
            var f4g1 = f4 * (long) g1;
            var f4g2 = f4 * (long) g2;
            var f4g3 = f4 * (long) g3;
            var f4g4 = f4 * (long) g4;
            var f4g5 = f4 * (long) g5;
            var f4g6_19 = f4 * (long) g6_19;
            var f4g7_19 = f4 * (long) g7_19;
            var f4g8_19 = f4 * (long) g8_19;
            var f4g9_19 = f4 * (long) g9_19;
            var f5g0 = f5 * (long) g0;
            var f5g1_2 = f5_2 * (long) g1;
            var f5g2 = f5 * (long) g2;
            var f5g3_2 = f5_2 * (long) g3;
            var f5g4 = f5 * (long) g4;
            var f5g5_38 = f5_2 * (long) g5_19;
            var f5g6_19 = f5 * (long) g6_19;
            var f5g7_38 = f5_2 * (long) g7_19;
            var f5g8_19 = f5 * (long) g8_19;
            var f5g9_38 = f5_2 * (long) g9_19;
            var f6g0 = f6 * (long) g0;
            var f6g1 = f6 * (long) g1;
            var f6g2 = f6 * (long) g2;
            var f6g3 = f6 * (long) g3;
            var f6g4_19 = f6 * (long) g4_19;
            var f6g5_19 = f6 * (long) g5_19;
            var f6g6_19 = f6 * (long) g6_19;
            var f6g7_19 = f6 * (long) g7_19;
            var f6g8_19 = f6 * (long) g8_19;
            var f6g9_19 = f6 * (long) g9_19;
            var f7g0 = f7 * (long) g0;
            var f7g1_2 = f7_2 * (long) g1;
            var f7g2 = f7 * (long) g2;
            var f7g3_38 = f7_2 * (long) g3_19;
            var f7g4_19 = f7 * (long) g4_19;
            var f7g5_38 = f7_2 * (long) g5_19;
            var f7g6_19 = f7 * (long) g6_19;
            var f7g7_38 = f7_2 * (long) g7_19;
            var f7g8_19 = f7 * (long) g8_19;
            var f7g9_38 = f7_2 * (long) g9_19;
            var f8g0 = f8 * (long) g0;
            var f8g1 = f8 * (long) g1;
            var f8g2_19 = f8 * (long) g2_19;
            var f8g3_19 = f8 * (long) g3_19;
            var f8g4_19 = f8 * (long) g4_19;
            var f8g5_19 = f8 * (long) g5_19;
            var f8g6_19 = f8 * (long) g6_19;
            var f8g7_19 = f8 * (long) g7_19;
            var f8g8_19 = f8 * (long) g8_19;
            var f8g9_19 = f8 * (long) g9_19;
            var f9g0 = f9 * (long) g0;
            var f9g1_38 = f9_2 * (long) g1_19;
            var f9g2_19 = f9 * (long) g2_19;
            var f9g3_38 = f9_2 * (long) g3_19;
            var f9g4_19 = f9 * (long) g4_19;
            var f9g5_38 = f9_2 * (long) g5_19;
            var f9g6_19 = f9 * (long) g6_19;
            var f9g7_38 = f9_2 * (long) g7_19;
            var f9g8_19 = f9 * (long) g8_19;
            var f9g9_38 = f9_2 * (long) g9_19;
            var h0 = f0g0 + f1g9_38 + f2g8_19 + f3g7_38 + f4g6_19 + f5g5_38 + f6g4_19 + f7g3_38 + f8g2_19 + f9g1_38;
            var h1 = f0g1 + f1g0 + f2g9_19 + f3g8_19 + f4g7_19 + f5g6_19 + f6g5_19 + f7g4_19 + f8g3_19 + f9g2_19;
            var h2 = f0g2 + f1g1_2 + f2g0 + f3g9_38 + f4g8_19 + f5g7_38 + f6g6_19 + f7g5_38 + f8g4_19 + f9g3_38;
            var h3 = f0g3 + f1g2 + f2g1 + f3g0 + f4g9_19 + f5g8_19 + f6g7_19 + f7g6_19 + f8g5_19 + f9g4_19;
            var h4 = f0g4 + f1g3_2 + f2g2 + f3g1_2 + f4g0 + f5g9_38 + f6g8_19 + f7g7_38 + f8g6_19 + f9g5_38;
            var h5 = f0g5 + f1g4 + f2g3 + f3g2 + f4g1 + f5g0 + f6g9_19 + f7g8_19 + f8g7_19 + f9g6_19;
            var h6 = f0g6 + f1g5_2 + f2g4 + f3g3_2 + f4g2 + f5g1_2 + f6g0 + f7g9_38 + f8g8_19 + f9g7_38;
            var h7 = f0g7 + f1g6 + f2g5 + f3g4 + f4g3 + f5g2 + f6g1 + f7g0 + f8g9_19 + f9g8_19;
            var h8 = f0g8 + f1g7_2 + f2g6 + f3g5_2 + f4g4 + f5g3_2 + f6g2 + f7g1_2 + f8g0 + f9g9_38;
            var h9 = f0g9 + f1g8 + f2g7 + f3g6 + f4g5 + f5g4 + f6g3 + f7g2 + f8g1 + f9g0;

            /*
            |h0| <= (1.65*1.65*2^52*(1+19+19+19+19)+1.65*1.65*2^50*(38+38+38+38+38))
              i.e. |h0| <= 1.4*2^60; narrower ranges for h2, h4, h6, h8
            |h1| <= (1.65*1.65*2^51*(1+1+19+19+19+19+19+19+19+19))
              i.e. |h1| <= 1.7*2^59; narrower ranges for h3, h5, h7, h9
            */

            var carry0 = (h0 + (1 << 25)) >> 26;
            h1 += carry0;
            h0 -= carry0 << 26;
            var carry4 = (h4 + (1 << 25)) >> 26;
            h5 += carry4;
            h4 -= carry4 << 26;
            /* |h0| <= 2^25 */
            /* |h4| <= 2^25 */
            /* |h1| <= 1.71*2^59 */
            /* |h5| <= 1.71*2^59 */

            var carry1 = (h1 + (1 << 24)) >> 25;
            h2 += carry1;
            h1 -= carry1 << 25;
            var carry5 = (h5 + (1 << 24)) >> 25;
            h6 += carry5;
            h5 -= carry5 << 25;
            /* |h1| <= 2^24; from now on fits into int32 */
            /* |h5| <= 2^24; from now on fits into int32 */
            /* |h2| <= 1.41*2^60 */
            /* |h6| <= 1.41*2^60 */

            var carry2 = (h2 + (1 << 25)) >> 26;
            h3 += carry2;
            h2 -= carry2 << 26;
            var carry6 = (h6 + (1 << 25)) >> 26;
            h7 += carry6;
            h6 -= carry6 << 26;
            /* |h2| <= 2^25; from now on fits into int32 unchanged */
            /* |h6| <= 2^25; from now on fits into int32 unchanged */
            /* |h3| <= 1.71*2^59 */
            /* |h7| <= 1.71*2^59 */

            var carry3 = (h3 + (1 << 24)) >> 25;
            h4 += carry3;
            h3 -= carry3 << 25;
            var carry7 = (h7 + (1 << 24)) >> 25;
            h8 += carry7;
            h7 -= carry7 << 25;
            /* |h3| <= 2^24; from now on fits into int32 unchanged */
            /* |h7| <= 2^24; from now on fits into int32 unchanged */
            /* |h4| <= 1.72*2^34 */
            /* |h8| <= 1.41*2^60 */

            carry4 = (h4 + (1 << 25)) >> 26;
            h5 += carry4;
            h4 -= carry4 << 26;
            var carry8 = (h8 + (1 << 25)) >> 26;
            h9 += carry8;
            h8 -= carry8 << 26;
            /* |h4| <= 2^25; from now on fits into int32 unchanged */
            /* |h8| <= 2^25; from now on fits into int32 unchanged */
            /* |h5| <= 1.01*2^24 */
            /* |h9| <= 1.71*2^59 */

            var carry9 = (h9 + (1 << 24)) >> 25;
            h0 += carry9 * 19;
            h9 -= carry9 << 25;
            /* |h9| <= 2^24; from now on fits into int32 unchanged */
            /* |h0| <= 1.1*2^39 */

            carry0 = (h0 + (1 << 25)) >> 26;
            h1 += carry0;
            h0 -= carry0 << 26;
            /* |h0| <= 2^25; from now on fits into int32 unchanged */
            /* |h1| <= 1.01*2^24 */

            h.x0 = (int) h0;
            h.x1 = (int) h1;
            h.x2 = (int) h2;
            h.x3 = (int) h3;
            h.x4 = (int) h4;
            h.x5 = (int) h5;
            h.x6 = (int) h6;
            h.x7 = (int) h7;
            h.x8 = (int) h8;
            h.x9 = (int) h9;
        }
    }
}