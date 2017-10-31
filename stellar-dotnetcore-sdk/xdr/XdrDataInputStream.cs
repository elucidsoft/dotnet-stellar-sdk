using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.xdr
{
    public class XdrDataInputStream : BinaryReader
    {
        public XdrDataInputStream(MemoryStream xdrInputStream)
           : base(new XdrInputStream(xdrInputStream))
        {
        }


        public override string ReadString()
        {
            int l = Read();
            byte[] bytes = new byte[l];
            Read(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes);
        }

        public int[] ReadIntArray()
        {
            int l = ReadInt32();
            return ReadIntArray(l);
        }

        private int[] ReadIntArray(int l)
        {
            int[] arr = new int[l];
            for (int i = 0; i < l; i++)
            {
                arr[i] = ReadInt32();
            }
            return arr;
        }

        public uint ReadInt()
        {
            return ReadUInt32();
        }

        public float[] ReadSingleArray()
        {
            int l = ReadInt32();
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

        public double[] ReadDoubleArray()
        {
            int l = ReadInt32();
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
    }

    public class XdrInputStream : MemoryStream
    {
        int _count = 0;
        MemoryStream _memoryStream;

        public XdrInputStream(MemoryStream ms) 
        {
            _memoryStream = ms;
        }

        public int Read()
        {
            var buffer = _memoryStream.GetBuffer();
            int read = _memoryStream.ReadByte();
            if (read >= 0)
                _count++;

            return read;
        }

        public int Read(byte[] b)
        {
            return _memoryStream.Read(b, 0, b.Length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = _memoryStream.Read(buffer, offset, count);
            _count += read;
            Pad();

            return read;
        }

        public override bool CanWrite => false;

        public override bool CanSeek => false;

        public override bool CanRead => true;

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
                int b = Read();
                if (b != 0)
                {
                    throw new IOException("non-zero padding");
                }
            }
        }
    }
}
