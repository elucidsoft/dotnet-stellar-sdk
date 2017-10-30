using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AccountMergeOperation
    {
        internal class Builder
        {
            private xdr.Operation.OperationBody body;

            public Builder(xdr.Operation.OperationBody body)
            {
                this.body = body;
            }

            internal Operation build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
