using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace stellar_dotnet_sdk.chaos.nacl
{
    public static class CryptoBytes
    {
        public static bool ConstantTimeEquals(byte[] x, int xOffset, byte[] y, int yOffset, int length)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (xOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(xOffset), "xOffset < 0");
            if (y == null)
                throw new ArgumentNullException(nameof(y));
            if (yOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(yOffset), "yOffset < 0");
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "length < 0");
            if (x.Length - xOffset < length)
                throw new ArgumentException("xOffset + length > x.Length");
            if (y.Length - yOffset < length)
                throw new ArgumentException("yOffset + length > y.Length");

            return InternalConstantTimeEquals(x, xOffset, y, yOffset, length) != 0;
        }

        private static uint InternalConstantTimeEquals(IReadOnlyList<byte> x, int xOffset, IReadOnlyList<byte> y,
            int yOffset, int length)
        {
            var differentbits = 0;
            for (var i = 0; i < length; i++)
                differentbits |= x[xOffset + i] ^ y[yOffset + i];
            return 1 & (unchecked((uint) differentbits - 1) >> 8);
        }

        public static void Wipe(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            InternalWipe(data, 0, data.Length);
        }

        // Secure wiping is hard
        // * the GC can move around and copy memory
        //   Perhaps this can be avoided by using unmanaged memory or by fixing the position of the array in memory
        // * Swap files and error dumps can contain secret information
        //   It seems possible to lock memory in RAM, no idea about error dumps
        // * Compiler could optimize out the wiping if it knows that data won't be read back
        //   I hope this is enough, suppressing inlining
        //   but perhaps `RtlSecureZeroMemory` is needed
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void InternalWipe(byte[] data, int offset, int count)
        {
            Array.Clear(data, offset, count);
        }

    }
}