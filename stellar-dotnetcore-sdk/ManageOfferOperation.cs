using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ManageOfferOperation
    {
        internal class Builder
        {
            private ManageOfferOp manageOfferOp;

            public Builder(ManageOfferOp manageOfferOp)
            {
                this.manageOfferOp = manageOfferOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
