using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AllowTrustOperation
    {
        internal class Builder
        {
            private AllowTrustOp allowTrustOp;

            public Builder(AllowTrustOp allowTrustOp)
            {
                this.allowTrustOp = allowTrustOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}