using System;

namespace stellar_dotnetcore_sdk
{
    public class MemoId
    {
        private long _Id;

        public long Id { get { return _Id; } }
        public MemoId(long id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id must be a positive number");
            }
            this._Id = id;
        }

        public xdr.Memo ToXdr()
        {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_ID);
            xdr.Uint64 idXdr = new xdr.Uint64();
            idXdr.InnerValue = _Id;
            memo.Id = idXdr;
            return memo;
        }
    }
}