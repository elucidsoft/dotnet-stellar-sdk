using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnetcore_sdk.xdr
{
    public class XdrDataOutputStream : BinaryWriter
    {
        public XdrDataOutputStream()
        {

        }

        public XdrDataOutputStream(Stream output) :
            base(output)
        {
        }

        public XdrDataOutputStream(Stream output, Encoding encoding) :
            base(output, encoding)
        {
        }

        public XdrDataOutputStream(Stream output, Encoding encoding, bool leaveOpen) :
            base(output, encoding, leaveOpen)
        {
        }

        protected XdrDataOutputStream()
        {
        }

        public void WriteString(string s)
        {
            byte[] chars = Encoding.UTF8.GetBytes(s);
            Write(chars.Length);
            Write(chars);
        }

        public void WriteIntArray(int[] a)
        {
            Write(a.Length);
            WriteIntArray(a, a.Length);
        }

        private void WriteIntArray(int[] a, int l)
        {
            for (int i = 0; i < l; i++)
            {
                Write(a[i]);
            }
        }

        public void WriteSingleArray(Single[] a)
        {
            Write(a.Length);
            WriteSingleArray(a, a.Length);
        }

        private void WriteSingleArray(float[] a, int l)
        {
            for (int i = 0; i < l; i++)
            {
                Write(a[i]);
            }
        }

        public void WriteDoubleArray(double[] a)
        {
            Write(a.Length);
            WriteDoubleArray(a, a.Length);
        }

        private void WriteDoubleArray(double[] a, int l)
        {
            for (int i = 0; i < l; i++)
            {
                Write(a[i]);
            }
        }
    }

    public class XdrOutputStream : MemoryStream
    {
        private int _count = 0;

        public XdrOutputStream(byte[] buffer) :
            base(buffer)
        {
        }

        public void Write(int b)
        {
            // > The byte to be written is the eight low-order bits of the argument b.
            // > The 24 high-order bits of b are ignored.

            WriteByte((byte)(Convert.ToByte(b) & 0xff));
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            _count += count;
            Pad();
        }

        public void Write(byte[] b)
        {
            Write(b, 0, b.Length);
        }

        private void Pad()
        {
            int pad = 0;
            int mod = _count % 4;
            if (mod > 0)
            {
                pad = 4 - mod;
            }
            while (pad-- > 0)
            {
                Write(0);
            }
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;
    }
}
