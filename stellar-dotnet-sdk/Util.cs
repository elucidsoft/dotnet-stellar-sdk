using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace stellar_dotnet_sdk
{
    public static class Util
    {
        public static char[] HexArray = "0123456789ABCDEF".ToCharArray();

        public static string BytesToHex(byte[] bytes)
        {
            var hexChars = new char[bytes.Length * 2];
            for (var j = 0; j < bytes.Length; j++)
            {
                var v = bytes[j] & 0xFF;
                hexChars[j * 2] = HexArray[(uint) v >> 4];
                hexChars[(j * 2) + 1] = HexArray[v & 0x0F];
            }

            return new string(hexChars);
        }

        public static byte[] HexToBytes(string s)
        {
            var len = s.Length;
            var data = new byte[len / 2];
            for (var i = 0; i < len; i += 2)
                data[i / 2] = (byte) ((Convert.ToByte(s[i].ToString(), 16) << 4)
                                      + Convert.ToByte(s[i + 1].ToString(), 16));
            return data;
        }

        /// <summary>
        ///     Returns SHA-256 hash of data.
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>Sha-256 Hash</returns>
        public static byte[] Hash(byte[] data)
        {
            var mySHA256 = SHA256.Create();
            var hash = mySHA256.ComputeHash(data);
            return hash;
        }

        /// <summary>
        ///     Pads byte array to length with zeros.
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <param name="length">length</param>
        /// <returns>zero padded byte array</returns>
        public static byte[] PaddedByteArray(byte[] bytes, int length)
        {
            var finalBytes = new byte[length];
            Fill(finalBytes, (byte) 0);
            Array.Copy(bytes, 0, finalBytes, 0, bytes.Length);

            return finalBytes;
        }

        /// <summary>
        ///     Pads string to length with zeros.
        /// </summary>
        /// <param name="source">string to pad</param>
        /// <param name="length">length</param>
        /// <returns>zero padded byte array</returns>
        public static byte[] PaddedByteArray(string source, int length)
        {
            return PaddedByteArray(Encoding.Default.GetBytes(source), length);
        }

        /// <summary>
        ///     Remove zeros from the end of byte array.
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <returns>string with padded zeros removed</returns>
        public static string PaddedByteArrayToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes).Split('\0')[0];
        }

        public static bool IsIdentical(this byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            return !a1.Where((t, i) => t != a2[i]).Any();
        }


        public static void Fill<T>(this T[] arr, T value)
        {
            for (var i = 0; i < arr.Length; i++)
                arr[i] = value;
        }

        public static int ComputeByteArrayHash(params byte[] data)
        {
            unchecked
            {
                const int p = 16777619;
                var hash = data.Aggregate((int) 2166136261, (current, t) => (current ^ t) * p);

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }
    }
}