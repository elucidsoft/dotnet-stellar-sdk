using System;
using System.Security.Cryptography;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public static class Util
    {
        public static char[] HEX_ARRAY = "0123456789ABCDEF".ToCharArray();

        public static String BytesToHex(byte[] bytes)
        {
            char[] hexChars = new char[bytes.Length * 2];
            for (int j = 0; j < bytes.Length; j++)
            {
                int v = bytes[j] & 0xFF;
                hexChars[j * 2] = HEX_ARRAY[(uint)v >> 4];
                hexChars[j * 2 + 1] = HEX_ARRAY[v & 0x0F];
            }
            return new String(hexChars);
        }

        public static byte[] HexToBytes(String s)
        {
            int len = s.Length;
            byte[] data = new byte[len / 2];
            for (int i = 0; i < len; i += 2)
            {
                data[i / 2] = (byte)((Convert.ToByte(s[i].ToString(), 16) << 4)
                    + Convert.ToByte(s[i + 1].ToString(), 16));
            }
            return data;
        }

        /// <summary>
        /// Returns SHA-256 hash of data.
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>Sha-256 Hash</returns>
        public static byte[] Hash(byte[] data)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] hash = mySHA256.ComputeHash(data);
            return hash;
        }

        /// <summary>
        /// Pads byte array to length with zeros.
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <param name="length">length</param>
        /// <returns>zero padded byte array</returns>
        public static byte[] PaddedByteArray(byte[] bytes, int length)
        {
            byte[] finalBytes = new byte[length];
            Fill(finalBytes, (byte)0);
            Array.Copy(bytes, 0, finalBytes, 0, bytes.Length);

            return finalBytes;
        }

        /// <summary>
        /// Pads string to length with zeros.
        /// </summary>
        /// <param name="source">string to pad</param>
        /// <param name="length">length</param>
        /// <returns>zero padded byte array</returns>
        public static byte[] PaddedByteArray(string source, int length)
        {
            return PaddedByteArray(Encoding.Default.GetBytes(source), length);
        }

        /// <summary>
        /// Remove zeros from the end of byte array.
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <returns>string with padded zeros removed</returns>
        public static string PaddedByteArrayToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes).Split('\0')[0];
        }

        private static void Fill<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

    }
}



