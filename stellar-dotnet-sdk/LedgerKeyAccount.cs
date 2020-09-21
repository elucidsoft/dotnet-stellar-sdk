namespace stellar_dotnet_sdk
{
    public class LedgerKeyAccount : LedgerKey
    {
        public KeyPair Account { get; }
        
        public LedgerKeyAccount(KeyPair account)
        {
            Account = account;
        }

        public override xdr.LedgerKey ToXdr()
        {
            return new xdr.LedgerKey
            {
                Discriminant =
                    new xdr.LedgerEntryType {InnerValue = xdr.LedgerEntryType.LedgerEntryTypeEnum.ACCOUNT},
                Account = new xdr.LedgerKey.LedgerKeyAccount {AccountID = new xdr.AccountID(Account.XdrPublicKey)}
            };
        }

        public static LedgerKeyAccount FromXdr(xdr.LedgerKey.LedgerKeyAccount xdr)
        {
            var account = KeyPair.FromXdrPublicKey(xdr.AccountID.InnerValue);
            return new LedgerKeyAccount(account);
        }
    }
}