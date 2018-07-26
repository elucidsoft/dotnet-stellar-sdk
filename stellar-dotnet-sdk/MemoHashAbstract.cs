using System;
using System.Linq;

namespace stellar_dotnet_sdk
{
    public abstract class MemoHashAbstract : Memo
    {
        protected byte[] _MemoBytes;

        public MemoHashAbstract(byte[] bytes)
        {
            if (bytes.Length < 32)
                bytes = Util.PaddedByteArray(bytes, 32);
            else if (bytes.Length > 32)
                throw new MemoTooLongException("MEMO_HASH can contain 32 bytes at max.");

            _MemoBytes = bytes;
        }

        public MemoHashAbstract(string hexString) : this(Util.HexToBytes(hexString))
        {
        }

        public byte[] MemoBytes => _MemoBytes;

        ///<summary>
        /// <p>Returns hex representation of bytes contained in this memo.</p>
        ///
        /// <p>Example:</p>
        /// <code>
        ///   MemoHash memo = new MemoHash("4142434445");
        ///   memo.getHexValue(); // 4142434445000000000000000000000000000000000000000000000000000000
        ///   memo.getTrimmedHexValue(); // 4142434445
        /// </code>
        ///</summary>
        public string GetHexValue()
        {
            return Util.BytesToHex(MemoBytes).ToLower();
        }

        ///<summary>
        /// <p>Returns hex representation of bytes contained in this memo until null byte (0x00) is found.</p>
        ///
        /// <p>Example:</p>
        /// <code>
        ///   MemoHash memo = new MemoHash("4142434445");
        ///   memo.getHexValue(); // 4142434445000000000000000000000000000000000000000000000000000000
        ///   memo.getTrimmedHexValue(); // 4142434445
        /// </code>
        ///</summary>
        public string GetTrimmedHexValue()
        {
            return GetHexValue().Split(new [] {"00"}, StringSplitOptions.None)[0].ToLower();
        }

        public abstract override xdr.Memo ToXdr();

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            MemoHashAbstract that = (MemoHashAbstract)o;
            return MemoBytes.SequenceEqual(that.MemoBytes);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Util.ComputeByteArrayHash(MemoBytes).GetHashCode();
                return hash;
            }
        }
    }
}