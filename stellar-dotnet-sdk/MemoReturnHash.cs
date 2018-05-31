using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
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
            var memo = new xdr.Memo();
            memo.Discriminant = MemoType.Create(MemoType.MemoTypeEnum.MEMO_RETURN);

            var hash = new Hash();
            hash.InnerValue = MemoBytes;

            memo.Hash = hash;

            return memo;
        }
    }
}