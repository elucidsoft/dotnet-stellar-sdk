namespace stellar_dotnetcore_sdk
{
    public class MemoNone : Memo
    {
        public MemoNone()
        {
        }

        public override xdr.Memo ToXdr()
        {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_NONE);
            return memo;
        }
    }
}