using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MemoNone : Memo
    {
        public override xdr.Memo ToXdr()
        {
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_NONE);
            return memo;
        }
    }
}