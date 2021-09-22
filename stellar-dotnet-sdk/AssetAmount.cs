using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Asset class represents an asset, either the native asset (XLM) or a asset code / issuer account ID pair.
    /// An asset code describes an asset code and issuer pair. In the case of the native asset XLM, the issuer will be null.
    /// </summary>
    public class AssetAmount
    {
        public Asset Asset { get; set; }
        public string Amount { get; set; }

        public AssetAmount() { }

        public AssetAmount(Asset asset, string amount)
        {
            Asset = asset;
            Amount = amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Hash(Asset.GetHashCode(), Amount);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AssetAmount))
            {
                return false;
            }

            AssetAmount other = (AssetAmount)obj;
            return Equals(Asset, other.Asset) && Equals(Amount, other.Amount);
        }
    }
}