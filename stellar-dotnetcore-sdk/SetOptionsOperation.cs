using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class SetOptionsOperation
    {
        internal class Builder
        {
            private SetOptionsOp setOptionsOp;

            public Builder(SetOptionsOp setOptionsOp)
            {
                this.setOptionsOp = setOptionsOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
