using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeNative : Asset
    {
        public AssetTypeNative() { }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string GetType()
        {
            return "native";
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override xdr.Asset ToXdr()
        {
            xdr.Asset thisXdr = new xdr.Asset();
            thisXdr.Discriminant = xdr.AssetType.Create(xdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE);
            return thisXdr;
        }
    }
}
