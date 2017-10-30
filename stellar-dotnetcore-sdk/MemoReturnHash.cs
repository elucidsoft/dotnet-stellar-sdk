namespace stellar_dotnetcore_sdk
{
    public class MemoReturnHash : MemoHashAbstract
    {
        public MemoReturnHash(byte[] bytes) : base(bytes)
        {
        }

        public MemoReturnHash(string hexString) : base(hexString)
        {

        }

        public override xdr.Memo ToXdr()
        {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_RETURN);

            xdr.Hash hash = new xdr.Hash();
            hash.InnerValue = MemoBytes;

            memo.Hash = hash;

            return memo;
        }
    }
}