using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public abstract class LedgerKey
    {
        public abstract xdr.LedgerKey ToXdr();

        public static LedgerKey Account(KeyPair account) => new LedgerKeyAccount(account);
        public static LedgerKey ClaimableBalance(byte[] balanceId) => new LedgerKeyClaimableBalance(balanceId);
        public static LedgerKey Data(KeyPair account, string dataName) => new LedgerKeyData(account, dataName);
        public static LedgerKey Offer(KeyPair seller, long offerId) => new LedgerKeyOffer(seller, offerId);
        public static LedgerKey Trustline(KeyPair account, TrustlineAsset asset) => new LedgerKeyTrustline(account, asset);

        public static LedgerKey FromXdr(xdr.LedgerKey xdr)
        {
            switch (xdr.Discriminant.InnerValue)
            {
                case LedgerEntryType.LedgerEntryTypeEnum.ACCOUNT:
                    return LedgerKeyAccount.FromXdr(xdr.Account);
                case LedgerEntryType.LedgerEntryTypeEnum.DATA:
                    return LedgerKeyData.FromXdr(xdr.Data);
                case LedgerEntryType.LedgerEntryTypeEnum.OFFER:
                    return LedgerKeyOffer.FromXdr(xdr.Offer);
                case LedgerEntryType.LedgerEntryTypeEnum.TRUSTLINE:
                    return LedgerKeyTrustline.FromXdr(xdr.TrustLine);
                case LedgerEntryType.LedgerEntryTypeEnum.CLAIMABLE_BALANCE:
                    return LedgerKeyClaimableBalance.FromXdr(xdr.ClaimableBalance);
                default:
                    throw new Exception("Unknown ledger key " + xdr.Discriminant.InnerValue);
            }
        }
    }
}