using System;
using System.Text;

namespace stellar_dotnetcore_sdk.xdr
{
    public class XdrDataInputStream
    {
        private readonly byte[] _bytes;
        private int _pos;

        public XdrDataInputStream(byte[] bytes)
        {
            _bytes = bytes;
        }

        public byte Read()
        {
            var res = _bytes[_pos];
            _pos++;

            return res;
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            Array.Copy(_bytes, _pos, buffer, offset, count);
            _pos += count;
        }

        public string ReadString()
        {
            return Encoding.UTF8.GetString(ReadVarOpaque(uint.MaxValue));
        }

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
                ((long)_bytes[_pos++] << 56) |
                ((long)_bytes[_pos++] << 48) |
                ((long)_bytes[_pos++] << 40) |
                ((long)_bytes[_pos++] << 32) |
                ((long)_bytes[_pos++] << 24) |
                ((long)_bytes[_pos++] << 16) |
                ((long)_bytes[_pos++] << 8) |
                _bytes[_pos++];
        }

        public int ReadInt()
        {
            return
                (_bytes[_pos++] << 0x18) |
                (_bytes[_pos++] << 0x10) |
                (_bytes[_pos++] << 0x08) |
                _bytes[_pos++];
        }

        public uint ReadUInt()
        {
            return
                ((uint)_bytes[_pos++] << 0x18) |
                ((uint)_bytes[_pos++] << 0x10) |
                ((uint)_bytes[_pos++] << 0x08) |
                _bytes[_pos++];
        }

        private unsafe float ReadSingle()
        {
            var num = ReadInt();
            return *(float*)&num;
        }

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
            return *(double*)&num;
        }

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

        public byte[] ToArray()
        {
            return _bytes;
        }

        public byte[] ReadVarOpaque(uint max)
        {
            return ReadFixOpaque(CheckedReadLength(max));
        }

        public byte[] ReadFixOpaque(uint len)
        {
            var result = new byte[len];
            Read(result, 0, (int)len);

            var tail = len % 4u;
            if (tail != 0)
                Read(_bytes, 0, (int)(4u - tail));

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

            return len;
        }
    }
}