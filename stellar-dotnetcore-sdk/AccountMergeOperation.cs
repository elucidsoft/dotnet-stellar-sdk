using System;

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