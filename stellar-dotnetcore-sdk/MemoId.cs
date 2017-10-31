using System;

namespace stellar_dotnetcore_sdk
{
    public class MemoId
    {
        private long id;

        public MemoId(long id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id must be a positive number");
            }
            this.id = id;
        }

        public long getId()
        {
            return id;
        }

        public xdr.Memo toXdr()
        {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_ID);
            xdr.Uint64 idXdr = new xdr.Uint64();
            idXdr.InnerValue = id;
            memo.Id = idXdr;
            return memo;
        }
    }
}