using System;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class MemoText : Memo
    {
        private string _MemoTextValue;
        
        public string MemoTextValue { get { return _MemoTextValue; } }

        public MemoText(string text)
        {
            _MemoTextValue = text ?? throw new ArgumentNullException(nameof(text), "text cannot be null");

            int length = Encoding.UTF8.GetBytes(text).Length;
            if (length > 28)
            {
                throw new MemoTooLongException("text must be <= 28 bytes. length=" + length);
            }
        }

       
        public override xdr.Memo ToXdr() {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_TEXT);
            memo.Text = MemoTextValue;
            return memo;
        }
    }
}