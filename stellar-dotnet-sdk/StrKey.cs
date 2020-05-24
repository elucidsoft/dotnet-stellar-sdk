using System;
using System.Collections.Generic;
using System.Linq;

namespace stellar_dotnet_sdk
{
    public class StrKey
    {
        public enum VersionByte : byte
        {
            ACCOUNT_ID = 6 << 3,
            MUXED_ACCOUNT = 12 << 3,
            SEED = 18 << 3,
            PRE_AUTH_TX = 19 << 3,
            SHA256_HASH = 23 << 3
        }

        public static string EncodeStellarAccountId(byte[] data)
        {
            return EncodeCheck(VersionByte.ACCOUNT_ID, data);
        }

        public static string EncodeStellarSecretSeed(byte[] data)
        {
            return EncodeCheck(VersionByte.SEED, data);
        }

        public static byte[] DecodeStellarAccountId(string data)
        {
            return DecodeCheck(VersionByte.ACCOUNT_ID, data);
        }

        public static byte[] DecodeStellarSecretSeed(string data)
        {
            return DecodeCheck(VersionByte.SEED, data);
        }

        public static VersionByte DecodeVersionByte(string encoded)
        {
            var decoded = CheckedBase32Decode(encoded);
            var versionByte = decoded[0];
            if (!Enum.IsDefined(typeof(VersionByte), versionByte))
                throw new FormatException("Version byte is invalid");
            return (VersionByte) versionByte;
        }

        public static string EncodeCheck(VersionByte versionByte, byte[] data)
        {
            var bytes = new List<byte>
            {
                (byte) versionByte
            };

            bytes.AddRange(data);
            var checksum = CalculateChecksum(bytes.ToArray());
            bytes.AddRange(checksum);
            return Base32Encoding.ToString(bytes.ToArray(), options => options.OmitPadding = true);
        }

        public static byte[] DecodeCheck(VersionByte versionByte, string encoded)
        {
            var decoded = CheckedBase32Decode(encoded);
            var decodedVersionByte = decoded[0];

            var payload = new byte[decoded.Length - 2];
            Array.Copy(decoded, 0, payload, 0, payload.Length);

            var data = new byte[payload.Length - 1];
            Array.Copy(payload, 1, data, 0, data.Length);

            var checksum = new byte[2];
            Array.Copy(decoded, decoded.Length - 2, checksum, 0, checksum.Length);

            if (decodedVersionByte != (byte) versionByte)
                throw new FormatException("Version byte is invalid");

            var expectedChecksum = CalculateChecksum(payload);

            if (!expectedChecksum.SequenceEqual(checksum))
                throw new FormatException("Checksum invalid");

            return data;
        }

        protected static byte[] CalculateChecksum(byte[] bytes)
        {
            // This code calculates CRC16-XModem checksum
            // Ported from https://github.com/alexgorbatchev/node-crc
            var crc = 0x0000;
            var count = bytes.Length;
            var i = 0;
            int code;

            while (count > 0)
            {
                code = (int) (uint) crc >> (8 & 0xFF);
                code ^= bytes[i++] & 0xFF;
                code ^= (int) (uint) code >> 4;
                crc = (crc << 8) & 0xFFFF;
                crc ^= code;
                code = (code << 5) & 0xFFFF;
                crc ^= code;
                code = (code << 7) & 0xFFFF;
                crc ^= code;
                count--;
            }

            // little-endian
            return new[]
            {
                (byte) crc,
                (byte) ((uint) crc >> 8)
            };
        }

        public static bool IsValid(VersionByte versionByte, string encoded)
        {
            try
            {
                var decoded = DecodeCheck(versionByte, encoded);
                return decoded.Length == 32;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidEd25519PublicKey(string publicKey)
        {
            return IsValid(VersionByte.ACCOUNT_ID, publicKey);
        }

        public static bool IsValidEd25519SecretSeed(string seed)
        {
            return IsValid(VersionByte.SEED, seed);
        }

        private static byte[] CheckedBase32Decode(string encoded)
        {
            if (encoded.Length == 0)
                throw new ArgumentException("Encoded string is empty");

            foreach (var t in encoded)
                if (t > 127)
                    throw new ArgumentException("Illegal characters in encoded string.");

            return Base32Encoding.ToBytes(encoded);
        }
    }
}