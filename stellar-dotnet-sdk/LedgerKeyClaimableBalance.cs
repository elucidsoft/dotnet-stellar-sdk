namespace stellar_dotnet_sdk
{
    public class LedgerKeyClaimableBalance : LedgerKey
    {
        public byte[] BalanceId { get; }

        public LedgerKeyClaimableBalance(byte[] balanceId)
        {
            BalanceId = balanceId;
        }

        public override xdr.LedgerKey ToXdr()
        {
            return new xdr.LedgerKey
            {
                Discriminant =
                    new xdr.LedgerEntryType {InnerValue = xdr.LedgerEntryType.LedgerEntryTypeEnum.CLAIMABLE_BALANCE},
                ClaimableBalance = new xdr.LedgerKey.LedgerKeyClaimableBalance
                {
                    BalanceID = new xdr.ClaimableBalanceID
                    {
                        Discriminant = new xdr.ClaimableBalanceIDType()
                        {
                            InnerValue = xdr.ClaimableBalanceIDType.ClaimableBalanceIDTypeEnum.CLAIMABLE_BALANCE_ID_TYPE_V0,
                        },
                        V0 = new xdr.Hash(BalanceId)
                    }
                }
            };
        }

        public static LedgerKeyClaimableBalance FromXdr(xdr.LedgerKey.LedgerKeyClaimableBalance xdr)
        {
            var balanceId = xdr.BalanceID.V0.InnerValue;
            return new LedgerKeyClaimableBalance(balanceId);
        }
    }
}