using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class PathPaymentOperation
    {
        internal class Builder
        {
            private PathPaymentOp pathPaymentOp;

            public Builder(PathPaymentOp pathPaymentOp)
            {
                this.pathPaymentOp = pathPaymentOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
