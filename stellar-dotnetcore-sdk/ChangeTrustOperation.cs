using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ChangeTrustOperation
    {
        internal class Builder
        {
            private ChangeTrustOp changeTrustOp;

            public Builder(ChangeTrustOp changeTrustOp)
            {
                this.changeTrustOp = changeTrustOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
