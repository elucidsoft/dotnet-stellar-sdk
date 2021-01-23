namespace stellar_dotnet_sdk
{
    public class LedgerKeyTrustline : LedgerKey
    {
        public KeyPair Account { get; }
        public Asset Asset { get; }

        public LedgerKeyTrustline(KeyPair account, Asset asset)
        {
            Account = account;
            Asset = asset;
        }

        public override xdr.LedgerKey ToXdr()
        {
            return new xdr.LedgerKey
            {
                Discriminant =
                    new xdr.LedgerEntryType { InnerValue = xdr.LedgerEntryType.LedgerEntryTypeEnum.TRUSTLINE },
                TrustLine = new xdr.LedgerKey.LedgerKeyTrustLine
                {
                    AccountID = new xdr.AccountID(Account.XdrPublicKey),
                    Asset = Asset.ToXdr(),
                }
            };
        }

        public static LedgerKeyTrustline FromXdr(xdr.LedgerKey.LedgerKeyTrustLine xdr)
        {
            var account = KeyPair.FromXdrPublicKey(xdr.AccountID.InnerValue);
            var asset = Asset.FromXdr(xdr.Asset);
            return new LedgerKeyTrustline(account, asset);
        }
    }
}