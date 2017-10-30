namespace stellar_dotnetcore_sdk
{
    public class MemoHash : MemoHashAbstract
    {
        public MemoHash(byte[] bytes) : base(bytes)
        {
        }

        public MemoHash(string hexString) : base(hexString)
        {
        }
        
        public override xdr.Memo ToXdr() {
            xdr.Memo memo = new xdr.Memo();
            memo.Discriminant = xdr.MemoType.Create(xdr.MemoType.MemoTypeEnum.MEMO_HASH);

            xdr.Hash hash = new xdr.Hash();
            hash.InnerValue = MemoBytes;

            memo.Hash = hash;

            return memo;
        }
    }
}