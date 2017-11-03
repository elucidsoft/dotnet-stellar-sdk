using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ManageDataOperation
    {
        internal class Builder
        {
            private ManageDataOp manageDataOp;

            public Builder(ManageDataOp manageDataOp)
            {
                this.manageDataOp = manageDataOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}