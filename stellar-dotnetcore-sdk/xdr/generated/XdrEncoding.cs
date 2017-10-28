// Copyright ExM
// Licensed as LGPL
// Source at git://github.com/ExM/OncRpc.git

using System;
using System.Text;

namespace stellar_dotnetcore_sdk.xdr
{
	public static class XdrEncoding
	{
		/// <summary>
		/// Decodes the Int32.
		/// http://tools.ietf.org/html/rfc4506#section-4.1
		/// </summary>
		public static int DecodeInt32(IByteReader r)
		{
			return
				(r.Read() << 0x18) |
				(r.Read() << 0x10) |
				(r.Read() << 0x08) |
				r.Read();
		}

		/// <summary>
		/// Encodes the Int32.
		/// http://tools.ietf.org/html/rfc4506#section-4.1
		/// </summary>
		public static void EncodeInt32(int v, IByteWriter w)
		{
			w.Write((byte)((v >> 0x18) & 0xff));
			w.Write((byte)((v >> 0x10) & 0xff));
			w.Write((byte)((v >> 8) & 0xff));
			w.Write((byte)(v & 0xff));
		}

		/// <summary>
		/// Decodes the UInt32.
		/// http://tools.ietf.org/html/rfc4506#section-4.2
		/// </summary>
		public static uint DecodeUInt32(IByteReader r)
		{
			return
				((uint)r.Read() << 0x18) |
				((uint)r.Read() << 0x10) |
				((uint)r.Read() << 0x08) |
				(uint)r.Read();
		}

		/// <summary>
		/// Encodes the UInt32.
		/// http://tools.ietf.org/html/rfc4506#section-4.2
		/// </summary>
		public static void EncodeUInt32(uint v, IByteWriter w)
		{
			w.Write((byte)((v >> 0x18) & 0xff));
			w.Write((byte)((v >> 0x10) & 0xff));
			w.Write((byte)((v >> 8) & 0xff));
			w.Write((byte)(v & 0xff));
		}

		/// <summary>
		/// Decodes the Int64.
		/// http://tools.ietf.org/html/rfc4506#section-4.5
		/// </summary>
		public static long DecodeInt64(IByteReader r)
		{
			return
				((long)r.Read() << 56) |
				((long)r.Read() << 48) |
				((long)r.Read() << 40) |
				((long)r.Read() << 32) |
				((long)r.Read() << 24) |
				((long)r.Read() << 16) |
				((long)r.Read() << 8) |
				(long)r.Read();
		}

		/// <summary>
		/// Encodes the Int64.
		/// http://tools.ietf.org/html/rfc4506#section-4.5
		/// </summary>
		public static void EncodeInt64(long v, IByteWriter w)
		{
			w.Write((byte)((v >> 56) & 0xff));
			w.Write((byte)((v >> 48) & 0xff));
			w.Write((byte)((v >> 40) & 0xff));
			w.Write((byte)((v >> 32) & 0xff));
			w.Write((byte)((v >> 24) & 0xff));
			w.Write((byte)((v >> 16) & 0xff));
			w.Write((byte)((v >>  8) & 0xff));
			w.Write((byte)(v & 0xff));
		}

		/// <summary>
		/// Decodes the UInt64.
		/// http://tools.ietf.org/html/rfc4506#section-4.5
		/// </summary>
		public static ulong DecodeUInt64(IByteReader r)
		{
			return
				((ulong)r.Read() << 56) |
				((ulong)r.Read() << 48) |
				((ulong)r.Read() << 40) |
				((ulong)r.Read() << 32) |
				((ulong)r.Read() << 24) |
				((ulong)r.Read() << 16) |
				((ulong)r.Read() << 8) |
				(ulong)r.Read();
		}

		/// <summary>
		/// Encodes the UInt64.
		/// http://tools.ietf.org/html/rfc4506#section-4.5
		/// </summary>
		public static void EncodeUInt64(ulong v, IByteWriter w)
		{
			w.Write((byte)((v >> 56) & 0xff));
			w.Write((byte)((v >> 48) & 0xff));
			w.Write((byte)((v >> 40) & 0xff));
			w.Write((byte)((v >> 32) & 0xff));
			w.Write((byte)((v >> 24) & 0xff));
			w.Write((byte)((v >> 16) & 0xff));
			w.Write((byte)((v >>  8) & 0xff));
			w.Write((byte)(v & 0xff));
		}

		/// <summary>
		/// Decodes the Single.
		/// http://tools.ietf.org/html/rfc4506#section-4.6
		/// </summary>
		public unsafe static Single DecodeSingle(IByteReader r)
		{
			int num = DecodeInt32(r);
			return *(float*)(&num);
		}

		/// <summary>
		/// Encodes the Single.
		/// http://tools.ietf.org/html/rfc4506#section-4.6
		/// </summary>
		public unsafe static void EncodeSingle(Single v, IByteWriter w)
		{
			EncodeInt32(*(int*)(&v), w);
		}

		/// <summary>
		/// Decodes the Double.
		/// http://tools.ietf.org/html/rfc4506#section-4.7
		/// </summary>
		public unsafe static Double DecodeDouble(IByteReader r)
		{
			long num = DecodeInt64(r);
			return *(double*)(&num);
		}

		/// <summary>
		/// Encodes the Double.
		/// http://tools.ietf.org/html/rfc4506#section-4.7
		/// </summary>
		public unsafe static void EncodeDouble(Double v, IByteWriter w)
		{
			EncodeInt64(*(long*)(&v), w);
		}

		private static uint CheckedReadLength(IByteReader r, uint max)
        {
            uint len;
            try
            {
                len = XdrEncoding.DecodeUInt32(r);
            }
            catch (SystemException ex)
            {
                throw new FormatException("cant't read 'length'", ex);
            }

            if (len > max)
                throw new FormatException("unexpected length: " + len.ToString());
            return len;
        }

        public static byte[] ReadFixOpaque(IByteReader r, uint len)
        {
            byte[] result = r.Read(len);
            uint tail = len % 4u;
            if (tail != 0)
                r.Read(4u - tail);
            return result;
        }

        public static byte[] ReadVarOpaque(IByteReader r, uint max)
        {
            return ReadFixOpaque(r, CheckedReadLength(r, max));
        }

        public static string ReadString(IByteReader r, uint max)
        {
            return Encoding.ASCII.GetString(ReadVarOpaque(r, max));
        }

		public static string ReadString(IByteReader r)
        {
            return Encoding.ASCII.GetString(ReadVarOpaque(r, uint.MaxValue));
        }

        public static void WriteFixOpaque(IByteWriter w, uint len, byte[] v)
        {
            if (v.LongLength != len)
                throw new FormatException("unexpected length: " + v.LongLength.ToString());

            NoCheckWriteFixOpaque(w, len, v);
        }

        private static byte[][] _tails = new byte[][]
		{
            null,
            new byte[] { 0x00},
            new byte[] { 0x00, 0x00},
            new byte[] { 0x00, 0x00, 0x00}
		};

        public static void WriteVarOpaque(IByteWriter w, uint max, byte[] v)
        {
            uint len = (uint)v.LongLength;
            if (len > max)
                throw new FormatException("unexpected length: " + len.ToString());

            try
            {
                EncodeUInt32(len, w);
            }
            catch (SystemException ex)
            {
                throw new FormatException("can't write length", ex);
            }
            NoCheckWriteFixOpaque(w, len, v);
        }

        private static void NoCheckWriteFixOpaque(IByteWriter w, uint len, byte[] v)
        {
            try
            {
                w.Write(v);
                uint tail = len % 4u;
                if (tail != 0)
                    w.Write(_tails[4u - tail]);
            }
            catch (SystemException ex)
            {
                throw new FormatException("can't write byte array", ex);
            }
        }

        public static void WriteString(IByteWriter w, uint max, string v)
        {
            WriteVarOpaque(w, max, Encoding.ASCII.GetBytes(v));
        }

		public static void WriteString(IByteWriter w, string v)
        {
            EncodeInt32(v.Length, w);
            WriteVarOpaque(w, (uint)v.Length, Encoding.ASCII.GetBytes(v));
        }

		public static void WriteBool(IByteWriter w, bool v)
        {
            EncodeInt32(v ? 1 : 0, w);
        }

		public static bool ReadBool(IByteReader r)
        {
            uint val = XdrEncoding.DecodeUInt32(r);
            if (val == 0)
                return false;
            if (val == 1)
                return true;

            throw new InvalidOperationException("unexpected value: " + val.ToString());
        }
	}
}
