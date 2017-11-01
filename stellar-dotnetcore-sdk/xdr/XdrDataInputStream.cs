using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.xdr
{
    public class XdrDataInputStream
    {
        //private readonly MemoryStream _memoryStream;
        private readonly byte[] _bytes;
        private int _pos = 0;

        //public byte[] Buffer => _memoryStream.ToArray();

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
            int l = ReadInt();
            return ReadIntArray(l);
        }

        private int[] ReadIntArray(int l)
        {
            int[] arr = new int[l];
            for (int i = 0; i < l; i++)
            {
                arr[i] = ReadInt();
            }
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
                (long)_bytes[_pos++];
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
                (uint)_bytes[_pos++];
        }

        private unsafe float ReadSingle()
        {
            int num = ReadInt();
            return *(float*)(&num);
        }

        public float[] ReadSingleArray()
        {
            int l = ReadInt();
            return ReadSingleArray(l);
        }

        private float[] ReadSingleArray(int l)
        {
            float[] arr = new float[l];
            for (int i = 0; i < l; i++)
            {
                arr[i] = ReadSingle();
            }

            return arr;
        }

        private unsafe double ReadDouble()
        {
            long num = ReadLong();
            return *(double*)(&num);
        }

        public double[] ReadDoubleArray()
        {
            int l = ReadInt();
            return ReadDoubleArray(l);
        }

        private double[] ReadDoubleArray(int l)
        {
            double[] arr = new double[l];
            for (int i = 0; i < l; i++)
            {
                arr[i] = ReadDouble();
            }
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
            byte[] result = new byte[len];
            Read(result, 0, (int)len);

            uint tail = len % 4u;
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

    //public class XdrInputStream : MemoryStream
    //{
    //    int _count = 0;
    //    MemoryStream _memoryStream;

    //    public XdrInputStream(MemoryStream ms) 
    //    {
    //        _memoryStream = ms;
    //    }

    //    public int Read()
    //    {
    //        int read = _memoryStream.ReadByte();
    //        if (read >= 0)
    //            _count++;

    //        return read;
    //    }

    //    public override int ReadByte()
    //    {
    //        int read = Read(_memoryStream.ToArray(), 0, 1);
    //        return read;
    //    }

    //    public int Read(byte[] b)
    //    {
    //        return Read(b, 0, b.Length);
    //    }

    //    public override int Read(byte[] buffer, int offset, int count)
    //    {
    //        int read = _memoryStream.Read(buffer, offset, count);
    //        _count += read;
    //        Pad();

    //        return read;
    //    }

    //    public override bool CanWrite => false;

    //    public override bool CanSeek => false;

    //    public override bool CanRead => true;

    //    private void Pad()
    //    {
    //        int pad = 0;
    //        int mod = _count % 4;
    //        if (mod > 0)
    //        {
    //            pad = 4 - mod;
    //        }

    //        while (pad-- > 0)
    //        {
    //            int b = Read();
    //            if (b != 0)
    //            {
    //                throw new IOException("non-zero padding");
    //            }
    //        }
    //    }
    //}
}
