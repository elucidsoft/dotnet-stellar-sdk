using System;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="ManageDataOp"/>.
    /// Use <see cref="Builder"/> to create a new ManageDataOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#manage-data">Manage Data</see>
    /// </summary>

    public class ManageDataOperation : Operation
    {
        private ManageDataOperation(string name, byte[] value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "name cannot be null");
            Value = value;
        }

        public string Name { get; }

        public byte[] Value { get; }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.ManageDataOp();
            var name = new sdkxdr.String64();
            name.InnerValue = Name;
            op.DataName = name;

            if (Value != null)
            {
                var dataValue = new sdkxdr.DataValue();
                dataValue.InnerValue = Value;
                op.DataValue = dataValue;
            }

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.MANAGE_DATA);
            body.ManageDataOp = op;

            return body;
        }

        public class Builder
        {
            private readonly string name;
            private readonly byte[] value;

            private KeyPair mSourceAccount;

            /// <summary>
            ///     Construct a new ManageOffer builder from a ManageDataOp XDR.
            /// </summary>
            /// <param name="op">
            ///     <see cref="sdkxdr.ManageDataOp" />
            /// </param>
            public Builder(sdkxdr.ManageDataOp op)
            {
                name = op.DataName.InnerValue;
                if (op.DataValue != null)
                    value = op.DataValue.InnerValue;
                else
                    value = null;
            }

            /// <summary>
            ///     Creates a new ManageData builder. If you want to delete data entry pass null as a <code>value</code> param.
            /// </summary>
            /// <param name="name">The name of data entry</param>
            /// <param name="value">The value of data entry. <code>null</code>null will delete data entry.</param>
            public Builder(string name, byte[] value)
            {
                this.name = name ?? throw new ArgumentNullException(nameof(name), "name cannot be null");
                this.value = value;
            }

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public ManageDataOperation Build()
            {
                var operation = new ManageDataOperation(name, value);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}