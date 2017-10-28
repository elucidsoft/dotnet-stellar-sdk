// Copyright ExM
// Licensed as LGPL
// Source at git://github.com/ExM/OncRpc.git

using System;

namespace stellar_dotnetcore_sdk.xdr
{
	public interface IByteWriter
	{
		void Write(byte[] buffer);
		void Write(byte b);
	}
}
