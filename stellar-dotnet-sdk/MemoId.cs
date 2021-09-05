using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MemoId : Memo
    {
        public MemoId(ulong id)
        {
            if (id < 0)
                throw new ArgumentException("id must be a positive number");
            IdValue = id;
        }

        public ulong IdValue { get; }

        public override xdr.Memo ToXdr()
        {
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_ID);
            var idXdr = new Uint64();
            idXdr.InnerValue = (long)IdValue;
            memo.Id = idXdr;
            return memo;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            MemoId memoId = (MemoId)o;
            return IdValue == memoId.IdValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Start
                .Hash(IdValue);
        }
    }
}