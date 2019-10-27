using System;
using System.IO;
using System.Linq;
using System.Text;

namespace stellar_dotnet_sdk.xdr
{
    /// <summary>
    /// Stream class for Reading XDR Data
    /// </summary>
    public class XdrDataInputStream
    {
        private readonly byte[] _bytes;
        private int _pos;

        /// <summary>
        /// Create the stream from a byte array.
        /// </summary>
        /// <param name="bytes"></param>
        public XdrDataInputStream(byte[] bytes)
        {
            _bytes = bytes;
        }

        /// <summary>
        /// Read single byte from stream.
        /// </summary>
        /// <returns></returns>
        public byte Read()
        {
            var res = _bytes[_pos];
            _pos++;

            return res;
        }

        /// <summary>
        /// Read from Stream and move position.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void Read(byte[] buffer, int offset, int count)
        {
            var result = ReadFixOpaque((uint) count);
            Array.Copy(result, 0, buffer, offset, count);
        }

        /// <summary>
        /// Read a string from stream.
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            return Encoding.UTF8.GetString(ReadVarOpaque(uint.MaxValue));
        }

        /// <summary>
        /// Read array of int from stream.
        /// </summary>
        /// <returns></returns>
        public int[] ReadIntArray()
        {
            var l = ReadInt();
            return ReadIntArray(l);
        }

        private int[] ReadIntArray(int l)
        {
            var arr = new int[l];
            for (var i = 0; i < l; i++)
                arr[i] = ReadInt();
            return arr;
        }

        internal long ReadLong()
        {
            return
                ((long) _bytes[_pos++] << 56) |
                ((long) _bytes[_pos++] << 48) |
                ((long) _bytes[_pos++] << 40) |
                ((long) _bytes[_pos++] << 32) |
                ((long) _bytes[_pos++] << 24) |
                ((long) _bytes[_pos++] << 16) |
                ((long) _bytes[_pos++] << 8) |
                _bytes[_pos++];
        }

        internal ulong ReadULong()
        {
            return
                ((ulong) _bytes[_pos++] << 56) |
                ((ulong) _bytes[_pos++] << 48) |
                ((ulong) _bytes[_pos++] << 40) |
                ((ulong) _bytes[_pos++] << 32) |
                ((ulong) _bytes[_pos++] << 24) |
                ((ulong) _bytes[_pos++] << 16) |
                ((ulong) _bytes[_pos++] << 8) |
                _bytes[_pos++];
        }
        /// <summary>
        /// Read Int32 from Stream
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            return
                (_bytes[_pos++] << 0x18) |
                (_bytes[_pos++] << 0x10) |
                (_bytes[_pos++] << 0x08) |
                _bytes[_pos++];
        }

        /// <summary>
        /// Read UInt from stream
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt()
        {
            return
                ((uint) _bytes[_pos++] << 0x18) |
                ((uint) _bytes[_pos++] << 0x10) |
                ((uint) _bytes[_pos++] << 0x08) |
                _bytes[_pos++];
        }

        private unsafe float ReadSingle()
        {
            var num = ReadInt();
            return *(float*) &num;
        }

        /// <summary>
        /// Read float from stream.
        /// </summary>
        /// <returns></returns>
        public float[] ReadSingleArray()
        {
            var l = ReadInt();
            return ReadSingleArray(l);
        }

        private float[] ReadSingleArray(int l)
        {
            var arr = new float[l];
            for (var i = 0; i < l; i++)
                arr[i] = ReadSingle();

            return arr;
        }

        private unsafe double ReadDouble()
        {
            var num = ReadLong();
            return *(double*) &num;
        }

        /// <summary>
        /// Read double from stream.
        /// </summary>
        /// <returns></returns>
        public double[] ReadDoubleArray()
        {
            var l = ReadInt();
            return ReadDoubleArray(l);
        }

        private double[] ReadDoubleArray(int l)
        {
            var arr = new double[l];
            for (var i = 0; i < l; i++)
                arr[i] = ReadDouble();
            return arr;
        }

        /// <summary>
        /// Return bytes as an Array
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            return _bytes;
        }

        /// <summary>
        /// Read the array with the proper padding applied and check max length.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public byte[] ReadVarOpaque(uint max)
        {
            uint len = CheckedReadLength(max);
            byte[] returnValue = ReadFixOpaque(len);
            return returnValue;
        }

        /// <summary>
        /// Read array with proper padding fixed if needed.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public byte[] ReadFixOpaque(uint len)
        {
            var result = new byte[len];
            Array.Copy(_bytes, _pos, result, 0, (int) len);

            var tail = len % 4u;
            if (tail == 0)
            {
                _pos += (int) len;
                return result;
            }
            var tailLength = (int) (4u - tail);
            var tailBytes = new byte[tailLength];

            Array.Copy(_bytes, _pos + len, tailBytes, 0, tailLength);

            if (tailBytes.Any(a => a != 0))
                throw new IOException("non-zero padding");

            _pos += (int) len + tailLength;

            return result;
        }

        private uint CheckedReadLength(uint max)
        {
            uint len;
            try
            {
                len = ReadUInt();
            }
            catch (SystemException ex)
            {
                throw new FormatException("cant't read 'length'", ex);
            }

            if (len > max)
                throw new FormatException("unexpected length: " + len);

            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max));

            return len;
        }
    }
}
