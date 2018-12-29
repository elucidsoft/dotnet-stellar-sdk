using stellar_dotnet_sdk.xdr;
using System;
using static stellar_dotnet_sdk.xdr.Operation;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="BumpSequenceOp"/>.
    /// Use <see cref="Builder"/> to create a new BumpSequenceOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#bump-sequence">Bump Sequence</see>
    /// </summary>

    public class BumpSequenceOperation : Operation
    {
        public long BumpTo { get; }

        public override OperationThreshold Threshold
        {
            get => OperationThreshold.Low;
        }

        public BumpSequenceOperation(long bumpTo)
        {
            BumpTo = bumpTo;
        }

        public override OperationBody ToOperationBody()
        {
            var op = new BumpSequenceOp();
            var bumpTo = new xdr.Int64 { InnerValue = BumpTo };
            var sequenceNumber = new SequenceNumber { InnerValue = bumpTo };

            op.BumpTo = sequenceNumber;

            var body = new OperationBody
            {
                Discriminant = OperationType.Create(OperationType.OperationTypeEnum.BUMP_SEQUENCE),
                BumpSequenceOp = op
            };

            return body;
        }

        public class Builder
        {
            public long BumpTo { get; }
            private KeyPair _sourceAccount;

            public Builder(BumpSequenceOp op)
            {
                BumpTo = op.BumpTo.InnerValue.InnerValue;
            }

            public Builder(long bumpTo)
            {
                BumpTo = bumpTo;
            }

            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _sourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            public BumpSequenceOperation Build()
            {
                var operation = new BumpSequenceOperation(BumpTo);
                if (_sourceAccount != null)
                {
                    operation.SourceAccount = _sourceAccount;
                }

                return operation;
            }
        }
    }
}