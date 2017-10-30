using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public abstract class MemoHashAbstract : Memo
    {
        protected byte[] _MemoBytes;

        public byte[] MemoBytes { get { return _MemoBytes; } }

        public MemoHashAbstract(byte[] bytes)
        {
            if (bytes.Length < 32)
            {
                bytes = Util.PaddedByteArray(bytes, 32);
            }
            else if (bytes.Length > 32)
            {
                throw new MemoTooLongException("MEMO_HASH can contain 32 bytes at max.");
            }

            this._MemoBytes = bytes;
        }

        public MemoHashAbstract(string hexString): this(Util.HexToBytes(hexString))
        {
        }

        /**
         * <p>Returns hex representation of bytes contained in this memo.</p>
         *
         * <p>Example:</p>
         * <code>
         *   MemoHash memo = new MemoHash("4142434445");
         *   memo.getHexValue(); // 4142434445000000000000000000000000000000000000000000000000000000
         *   memo.getTrimmedHexValue(); // 4142434445
         * </code>
         */
        public string GetHexValue()
        {
            return Util.BytesToHex(this.MemoBytes);
        }

        /**
         * <p>Returns hex representation of bytes contained in this memo until null byte (0x00) is found.</p>
         *
         * <p>Example:</p>
         * <code>
         *   MemoHash memo = new MemoHash("4142434445");
         *   memo.getHexValue(); // 4142434445000000000000000000000000000000000000000000000000000000
         *   memo.getTrimmedHexValue(); // 4142434445
         * </code>
         */
        public string GetTrimmedHexValue()
        {
            return this.GetHexValue().Split("00")[0];
        }

        public override abstract xdr.Memo ToXdr();
    }
}