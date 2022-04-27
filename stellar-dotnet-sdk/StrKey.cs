using System;
using System.Collections.Generic;
using System.Linq;
using xdr = stellar_dotnet_sdk.xdr;

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
            SHA256_HASH = 23 << 3,
            SIGNED_PAYLOAD = 15 << 3
        }

        public static string EncodeStellarAccountId(byte[] data)
        {
            return EncodeCheck(VersionByte.ACCOUNT_ID, data);
        }

        public static String EncodeSignedPayload(byte[] data)
        {
            try
            {
                var xdrPayloadSigner = new xdr.SignerKey.SignerKeyEd25519SignedPayload();
                xdrPayloadSigner.Payload = data;
                xdrPayloadSigner.Ed25519 = new xdr.Uint256(data);

                xdr.SignerKey.SignerKeyEd25519SignedPayload.Encode(new xdr.XdrDataOutputStream(), xdrPayloadSigner);

                return EncodeCheck(VersionByte.SIGNED_PAYLOAD, data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string EncodeStellarMuxedAccount(byte[] data, ulong id)
        {
            // 8 bytes for 64 bit id + data
            var dataToEncode = new byte[8 + data.Length];
            // Prepend the id in network byte order to the data
            var idBytes = BitConverter.GetBytes(id);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
            }
            Buffer.BlockCopy(idBytes, 0, dataToEncode, 0, 8);
            Buffer.BlockCopy(data, 0, dataToEncode, 8, data.Length);
            return EncodeCheck(VersionByte.MUXED_ACCOUNT, dataToEncode);
        }

        public static string EncodeStellarSecretSeed(byte[] data)
        {
            return EncodeCheck(VersionByte.SEED, data);
        }

        public static byte[] DecodeStellarAccountId(string data)
        {
            return DecodeCheck(VersionByte.ACCOUNT_ID, data);
        }

        public static SignedPayloadSigner DecodeSignedPayload(string data)
        {
            try
            {
                byte[] signedPayloadRaw = DecodeCheck(VersionByte.SIGNED_PAYLOAD, data);

                var xdrPayloadSigner = xdr.SignerKey.SignerKeyEd25519SignedPayload.Decode(new xdr.XdrDataInputStream(signedPayloadRaw));

                return new SignedPayloadSigner(xdrPayloadSigner.Ed25519.InnerValue, xdrPayloadSigner.Payload);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static (ulong, byte[]) DecodeStellarMuxedAccount(string data)
        {
            var bytes = DecodeCheck(VersionByte.MUXED_ACCOUNT, data);
            var keyData = new byte[bytes.Length - 8];
            ulong id;
            Buffer.BlockCopy(bytes, 8, keyData, 0, keyData.Length);

            if (BitConverter.IsLittleEndian)
            {
                var idBuffer = new byte[8];
                Buffer.BlockCopy(bytes, 0, idBuffer, 0, 8);
                Array.Reverse(idBuffer);
                id = BitConverter.ToUInt64(idBuffer, 0);
                return (id, keyData);
            }

            id = BitConverter.ToUInt64(bytes, 0);
            return (id, keyData);
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
            return (VersionByte)versionByte;
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

            if (decodedVersionByte != (byte)versionByte)
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
                code = (int)(uint)crc >> (8 & 0xFF);
                code ^= bytes[i++] & 0xFF;
                code ^= (int)(uint)code >> 4;
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
                // Muxed accounts are encoded as a 64 bit ulong wih 32 bytes of data
                if (versionByte == VersionByte.MUXED_ACCOUNT)
                {
                    return decoded.Length == 40;
                }

                // All other types have 32 bytes of data
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

        public static bool IsValidMuxedAccount(string publicKey)
        {
            return IsValid(VersionByte.MUXED_ACCOUNT, publicKey);
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