using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class CreatePassiveOfferOperation
    {
        internal class Builder
        {
            private CreatePassiveOfferOp createPassiveOfferOp;

            public Builder(CreatePassiveOfferOp createPassiveOfferOp)
            {
                this.createPassiveOfferOp = createPassiveOfferOp;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
