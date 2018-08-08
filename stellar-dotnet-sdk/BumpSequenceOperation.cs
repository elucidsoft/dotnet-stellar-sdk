using stellar_dotnet_sdk.xdr;
using System;
using static stellar_dotnet_sdk.xdr.Operation;

namespace stellar_dotnet_sdk
{
    public class BumpSequenceOperation : Operation
    {
        public long BumpTo { get; }

        public BumpSequenceOperation(long bumpTo)
        {
            this.BumpTo = bumpTo;
        }

        public override OperationBody ToOperationBody()
        {
            BumpSequenceOp op = new BumpSequenceOp();
            xdr.Int64 bumpTo = new xdr.Int64();
            bumpTo.InnerValue = BumpTo;
            SequenceNumber sequenceNumber = new SequenceNumber();
            sequenceNumber.InnerValue = bumpTo;

            op.BumpTo = sequenceNumber;
            OperationBody body = new OperationBody();
            body.Discriminant = OperationType.Create(OperationType.OperationTypeEnum.BUMP_SEQUENCE);
            body.BumpSequenceOp = op;
            return body;
        }

        public class Builder
        {
            public long BumpTo { get; }
            private KeyPair mSourceAccount;


            public Builder(BumpSequenceOp op)
            {
                BumpTo = op.BumpTo.InnerValue.InnerValue;
            }

            public Builder(long bumpTo)
            {
                BumpTo = bumpTo;
            }

            public BumpSequenceOperation.Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            public BumpSequenceOperation Build()
            {
                BumpSequenceOperation operation = new BumpSequenceOperation(BumpTo);
                if(mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }

                return operation;
            }
        }
    }
}
