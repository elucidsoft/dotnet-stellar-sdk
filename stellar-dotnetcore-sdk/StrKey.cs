using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class StrKey
    {
        public class VersionByte
        {
            public const byte ACCOUNT_ID = (6 << 3);    //G
            public const byte SEED = (18 << 3);         //S
            public const byte PRE_AUTH_TX = (19 << 3);  //T
            public const byte SHA256_HASH = (23 << 3);  //X


            public VersionByte(byte value) => Value = value;

            public byte Value { get; set; }

        }

        public static string EncodeStellarAccountId(byte[] data)
        {
            throw new NotImplementedException();
        }


        public static string EncodeCheck(VersionByte versionByte, byte[] data)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(versionByte.Value);
            bytes.AddRange(data);

            byte[] checksum = CalculateChecksum(bytes.ToArray());
            bytes.AddRange(checksum);
            return Base32Encoding.ToString(bytes.ToArray());
        }

        public static byte[] DecodeCheck(VersionByte versionByte, string encoded)
        {
            for (int i = 0; i < encoded.Length; i++)
            {
                if (encoded[i] > 127)
                {
                    throw new ArgumentException("Illegal characters in encoded char array.");
                }
            }

            byte[] decoded = Base32Encoding.ToBytes(encoded);
            byte decodedVersionByte = decoded[0];

            byte[] payload = new byte[decoded.Length - 2];
            Array.Copy(decoded, 0, payload, 0, payload.Length);

            byte[] data = new byte[payload.Length - 1];
            Array.Copy(payload, 1, data, 0, data.Length);

            byte[] checksum = new byte[2];
            Array.Copy(decoded, decoded.Length - 2, checksum, 0, checksum.Length);

            if (decodedVersionByte != versionByte.Value)
            {
                throw new FormatException("Version byte is invalid");
            }

            byte[] expectedChecksum = CalculateChecksum(payload);

            if (!expectedChecksum.SequenceEqual(checksum))
            {
                throw new FormatException("Checksum invalid");
            }

            return data;
        }

        protected static byte[] CalculateChecksum(byte[] bytes)
        {
            // This code calculates CRC16-XModem checksum
            // Ported from https://github.com/alexgorbatchev/node-crc
            int crc = 0x0000;
            int count = bytes.Length;
            int i = 0;
            int code;

            while (count > 0)
            {
                code = (int)((uint)crc) >> (8 & 0xFF);
                code ^= bytes[i++] & 0xFF;
                code ^= (int)((uint)code) >> 4;
                crc = crc << 8 & 0xFFFF;
                crc ^= code;
                code = code << 5 & 0xFFFF;
                crc ^= code;
                code = code << 7 & 0xFFFF;
                crc ^= code;
                count--;
            }

            // little-endian
            return new byte[] {
            (byte)crc,
            (byte)((uint)crc >> 8)};
        }
    }
}
