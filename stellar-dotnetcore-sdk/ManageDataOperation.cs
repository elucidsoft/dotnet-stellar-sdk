using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ManageDataOperation : Operation
    {
        private readonly String name;
        private readonly byte[] value;

        public string Name => name;
        public byte[] Value => value;

        private ManageDataOperation(String name, byte[] value)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name), "name cannot be null");
            this.value = value;
        }


        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            sdkxdr.ManageDataOp op = new sdkxdr.ManageDataOp();
            sdkxdr.String64 name = new sdkxdr.String64();
            name.InnerValue = this.Name;
            op.DataName = name;

            if (Value != null)
            {
                sdkxdr.DataValue dataValue = new sdkxdr.DataValue();
                dataValue.InnerValue = this.Value;
                op.DataValue = dataValue;
            }

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.MANAGE_DATA);
            body.ManageDataOp = op;

            return body;
        }

        public class Builder
        {
            private readonly String name;
            private readonly byte[] value;

            private KeyPair mSourceAccount;

            ///<summary>
            /// Construct a new ManageOffer builder from a ManageDataOp XDR.
            /// </summary>
            /// <param name="op"><see cref="sdkxdr.ManageDataOp"/></param> 
            public Builder(sdkxdr.ManageDataOp op)
            {
                name = op.DataName.InnerValue;
                if (op.DataValue != null)
                {
                    value = op.DataValue.InnerValue;
                }
                else
                {
                    value = null;
                }
            }

            ///<summary>
            /// Creates a new ManageData builder. If you want to delete data entry pass null as a <code>value</code> param.
            /// </summary>
            /// <param name="name">The name of data entry</param> 
            /// <param name="value">The value of data entry. <code>null</code>null will delete data entry.</param> 
            public Builder(String name, byte[] value)
            {
                this.name = name ?? throw new ArgumentNullException(nameof(name), "name cannot be null");
                this.value = value;
            }

            ///<summary>
            /// Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param> 
            /// <returns>Builder object so you can chain methods.</returns> 
            ///
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            ///<summary>
            /// Builds an operation
            /// </summary>
            public ManageDataOperation Build()
            {
                ManageDataOperation operation = new ManageDataOperation(name, value);
                if (mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }
                return operation;
            }
        }
    }
}