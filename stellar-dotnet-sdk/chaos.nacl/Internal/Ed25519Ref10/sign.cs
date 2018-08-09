using System;

namespace stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10
{
    internal static partial class Ed25519Operations
    {
        /*public static void crypto_sign(
          byte[] sm, out int smlen,
           byte[] m, int mlen,
           byte[] sk
        )
        {
            byte[] az = new byte[64];
            byte[] r = new byte[64];
            byte[] hram = new byte[64];
            GroupElementP3 R;
            int i;

            Helpers.crypto_hash_sha512(az, sk, 0, 32);
            az[0] &= 248;
            az[31] &= 63;
            az[31] |= 64;

            smlen = mlen + 64;
            for (i = 0; i < mlen; ++i) sm[64 + i] = m[i];
            for (i = 0; i < 32; ++i) sm[32 + i] = az[32 + i];
            Helpers.crypto_hash_sha512(r, sm, 32, mlen + 32);
            for (i = 0; i < 32; ++i) sm[32 + i] = sk[32 + i];

            ScalarOperations.sc_reduce(r);
            GroupOperations.ge_scalarmult_base(out R, r, 0);
            GroupOperations.ge_p3_tobytes(sm, 0, ref R);

            Helpers.crypto_hash_sha512(hram, sm, 0, mlen + 64);
            ScalarOperations.sc_reduce(hram);
            var sm32 = new byte[32];
            Array.Copy(sm, 32, sm32, 0, 32);
            ScalarOperations.sc_muladd(sm32, hram, az, r);
            Array.Copy(sm32, 0, sm, 32, 32);
        }*/

        public static void crypto_sign2(
            byte[] sig, int sigoffset,
            byte[] m, int moffset, int mlen,
            byte[] sk, int skoffset)
        {
            var hasher = new Sha512();
            {
                hasher.Update(sk, skoffset, 32);
                var az = hasher.Finish();
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.ScalarOperations.ScClamp(az, 0);

                hasher.Init();
                hasher.Update(az, 32, 32);
                hasher.Update(m, moffset, mlen);
                var r = hasher.Finish();

                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.ScalarOperations.ScReduce(r);
                GroupElementP3 R;
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.GroupOperations.GeScalarmultBase(out R, r, 0);
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.GroupOperations.ge_p3_tobytes(sig, sigoffset, ref R);

                hasher.Init();
                hasher.Update(sig, sigoffset, 32);
                hasher.Update(sk, skoffset + 32, 32);
                hasher.Update(m, moffset, mlen);
                var hram = hasher.Finish();

                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.ScalarOperations.ScReduce(hram);
                var s = new byte[32];
                Array.Copy(sig, sigoffset + 32, s, 0, 32);
                stellar_dotnet_sdk.chaos.nacl.Internal.Ed25519Ref10.ScalarOperations.ScMulAdd(s, hram, az, r);
                Array.Copy(s, 0, sig, sigoffset + 32, 32);
                CryptoBytes.Wipe(s);
            }
        }
    }
}