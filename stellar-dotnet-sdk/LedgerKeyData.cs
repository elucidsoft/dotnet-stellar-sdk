namespace stellar_dotnet_sdk
{
    public class LedgerKeyData : LedgerKey
    {
        public KeyPair Account { get; }
        public string DataName { get; }

        public LedgerKeyData(KeyPair account, string dataName)
        {
            Account = account;
            DataName = dataName;
        }

        public override xdr.LedgerKey ToXdr()
        {
            return new xdr.LedgerKey
            {
                Discriminant =
                    new xdr.LedgerEntryType { InnerValue = xdr.LedgerEntryType.LedgerEntryTypeEnum.DATA },
                Data = new xdr.LedgerKey.LedgerKeyData
                {
                    AccountID = new xdr.AccountID(Account.XdrPublicKey),
                    DataName = new xdr.String64(DataName),
                }
            };
        }

        public static LedgerKeyData FromXdr(xdr.LedgerKey.LedgerKeyData xdr)
        {
            var account = KeyPair.FromXdrPublicKey(xdr.AccountID.InnerValue);
            var dataName = xdr.DataName.InnerValue;
            return new LedgerKeyData(account, dataName);
        }
    }
}