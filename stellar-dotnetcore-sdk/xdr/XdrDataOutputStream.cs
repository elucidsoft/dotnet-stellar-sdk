using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.xdr
{
    public class XdrDataOutputStream
    {
        private readonly List<byte> _bytes;

        private readonly byte[][] _tails =
        {
            null,
            new byte[] {0x00},
            new byte[] {0x00, 0x00},
            new byte[] {0x00, 0x00, 0x00}
        };

        public XdrDataOutputStream()
        {
            _bytes = new List<byte>();
        }

        public void Write(byte b)
        {
            _bytes.Add(b);
        }

        public void Write(byte[] bytes)
        {
            Write(bytes, 0, bytes.Length);
        }

        public void Write(byte[] bytes, int offset, int count)
        {
            var newBytes = new byte[count];
            Array.Copy(bytes, offset, newBytes, 0, count);

            _bytes.AddRange(newBytes);

            Padd((uint)count);

        }

        public void WriteString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            WriteVarOpaque((uint) bytes.Length, bytes);
        }

        public void WriteIntArray(int[] a)
        {
            WriteInt(a.Length);
            WriteIntArray(a, a.Length);
        }

        private void WriteIntArray(int[] a, int l)
        {
            for (var i = 0; i < l; i++)
                WriteInt(a[i]);
        }

        public void WriteLong(long v)
        {
            Write((byte) ((v >> 56) & 0xff));
            Write((byte) ((v >> 48) & 0xff));
            Write((byte) ((v >> 40) & 0xff));
            Write((byte) ((v >> 32) & 0xff));
            Write((byte) ((v >> 24) & 0xff));
            Write((byte) ((v >> 16) & 0xff));
            Write((byte) ((v >> 8) & 0xff));
            Write((byte) (v & 0xff));
        }

        public void WriteInt(int i)
        {
            Write((byte) ((i >> 0x18) & 0xff));
            Write((byte) ((i >> 0x10) & 0xff));
            Write((byte) ((i >> 8) & 0xff));
            Write((byte) (i & 0xff));
        }

        public void WriteUInt(uint i)
        {
            Write((byte) ((i >> 0x18) & 0xff));
            Write((byte) ((i >> 0x10) & 0xff));
            Write((byte) ((i >> 8) & 0xff));
            Write((byte) (i & 0xff));
        }

        private unsafe void WriteSingle(float v)
        {
            WriteInt(*(int*) &v);
        }

        public void WriteSingleArray(float[] a)
        {
            WriteInt(a.Length);
            WriteSingleArray(a, a.Length);
        }

        private void WriteSingleArray(float[] a, int l)
        {
            for (var i = 0; i < l; i++)
                WriteSingle(a[i]);
        }

        private unsafe void WriteDouble(double v)
        {
            WriteLong(*(long*) &v);
        }

        public void WriteDoubleArray(double[] a)
        {
            WriteInt(a.Length);
            WriteDoubleArray(a, a.Length);
        }

        private void WriteDoubleArray(double[] a, int l)
        {
            for (var i = 0; i < l; i++)
                WriteDouble(a[i]);
        }

        public byte[] ToArray()
        {
            return _bytes.ToArray();
        }

        public void WriteVarOpaque(uint max, byte[] v)
        {
            var len = (uint) v.LongLength;
            if (len > max)
                throw new FormatException("unexpected length: " + len);

            try
            {
                WriteUInt(len);
            }
            catch (SystemException ex)
            {
                throw new FormatException("can't write length", ex);
            }
            NoCheckWriteFixOpaque(len, v);
        }


        private void NoCheckWriteFixOpaque(uint len, byte[] v)
        {
            try
            {
                Write(v);
            }
            catch (SystemException ex)
            {
                throw new FormatException("can't write byte array", ex);
            }
        }

        private void Padd(uint length)
        {
            var tail = length % 4u;
            if (tail != 0)
            {
                var padd = _tails[4u - tail];

                for (int i = 0; i < padd.Length; i++)
                {
                    Write(padd[i]);
                }
            }
        }
    }
}