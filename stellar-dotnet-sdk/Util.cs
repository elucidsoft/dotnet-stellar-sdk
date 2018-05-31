using System;
using System.Security.Cryptography;
using System.Text;

namespace stellar_dotnet_sdk
{
    public static class Util
    {
        public static char[] HEX_ARRAY = "0123456789ABCDEF".ToCharArray();

        public static string BytesToHex(byte[] bytes)
        {
            var hexChars = new char[bytes.Length * 2];
            for (var j = 0; j < bytes.Length; j++)
            {
                var v = bytes[j] & 0xFF;
                hexChars[j * 2] = HEX_ARRAY[(uint) v >> 4];
                hexChars[j * 2 + 1] = HEX_ARRAY[v & 0x0F];
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

            for (var i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;
            return true;
        }


        public static void Fill<T>(this T[] arr, T value)
        {
            for (var i = 0; i < arr.Length; i++)
                arr[i] = value;
        }
    }
}