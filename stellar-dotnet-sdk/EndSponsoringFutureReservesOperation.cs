namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="EndSponsoringFutureReservesOp"/>.
    /// Use <see cref="Builder"/> to create a new EndSponsoringFutureReservesOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">End Sponsoring Futures Reserves</see>
    /// </summary>
    public class EndSponsoringFutureReservesOperation : Operation
    {
        private EndSponsoringFutureReservesOperation()
        {
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var body = new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.END_SPONSORING_FUTURE_RESERVES)
            };

            return body;
        }

        /// <summary>
        ///     Builds EndSponsoringFutureReserves operation.
        /// </summary>
        /// <see cref="EndSponsoringFutureReservesOperation" />
        public class Builder
        {
            private KeyPair _sourceAccount;

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="account">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair account)
            {
                _sourceAccount = account;
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public EndSponsoringFutureReservesOperation Build()
            {
                var operation = new EndSponsoringFutureReservesOperation();
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
         
    }
}