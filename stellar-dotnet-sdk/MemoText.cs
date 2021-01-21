using System;
using System.Text;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MemoText : Memo
    {
        public MemoText(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text), "text cannot be null");

            var bytes = Encoding.UTF8.GetBytes(text);
            if (bytes.Length > 28)
                throw new MemoTooLongException("text must be <= 28 bytes. length=" + bytes.Length);

            MemoBytesValue = bytes;
        }

        public MemoText(byte[] text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text), "text cannot be null");
            if (text.Length > 28)
                throw new MemoTooLongException("text must be <= 28 bytes. length=" + text.Length);

            MemoBytesValue = text;
        }

        public string MemoTextValue => Encoding.UTF8.GetString(MemoBytesValue);
        public byte[] MemoBytesValue { get; }

        public override xdr.Memo ToXdr()
        {
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_TEXT);
            memo.Text = MemoTextValue ?? "none";
            return memo;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            MemoText memoText = (MemoText)o;
            return Equals(MemoTextValue, memoText.MemoTextValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Start
                .Hash(MemoTextValue);
        }
    }
}