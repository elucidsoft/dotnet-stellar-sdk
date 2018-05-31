using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MemoId : Memo
    {
        public MemoId(long id)
        {
            if (id < 0)
                throw new ArgumentException("id must be a positive number");
            IdValue = id;
        }

        public long IdValue { get; }

        public override xdr.Memo ToXdr()
        {
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_ID);
            var idXdr = new Uint64();
            idXdr.InnerValue = IdValue;
            memo.Id = idXdr;
            return memo;
        }
    }
}