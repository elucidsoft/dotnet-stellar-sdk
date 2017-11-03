using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class MemoId
    {
        public MemoId(long id)
        {
            if (id < 0)
                throw new ArgumentException("id must be a positive number");
            Id = id;
        }

        public long Id { get; }

        public xdr.Memo ToXdr()
        {
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_ID);
            var idXdr = new Uint64();
            idXdr.InnerValue = Id;
            memo.Id = idXdr;
            return memo;
        }
    }
}