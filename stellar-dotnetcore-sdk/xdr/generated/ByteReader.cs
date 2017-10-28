// Copyright ExM
// Licensed as LGPL
// Source at git://github.com/ExM/OncRpc.git

using System;
using System.IO;

namespace stellar_dotnetcore_sdk.xdr
{
	public class ByteReader: IByteReader
	{
		private int _pos = 0;
		private byte[] _bytes;

		public ByteReader(params byte[] bytes)
		{
			_bytes = bytes;
		}

		public int Position
		{
			get
			{
				return _pos;
			}
		}

		public byte[] Read(uint count)
		{
			byte[] result = new byte[count];
			Array.Copy(_bytes, _pos, result, 0, count);
			_pos += (int)count;
			return result;
		}

		public byte Read()
		{
			byte result = _bytes[_pos];
			_pos++;
			return result;
		}
	}
}
