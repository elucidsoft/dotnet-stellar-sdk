using System;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="RevokeSponsorshipOperation"/>.
    /// Use <see cref="Builder"/> to create a new RevokeLedgerEntrySponsorshipOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">Revoke Sponsorship</see>
    /// </summary>
    public class RevokeLedgerEntrySponsorshipOperation : RevokeSponsorshipOperation
    {
        public LedgerKey LedgerKey { get; }

        public RevokeLedgerEntrySponsorshipOperation(LedgerKey ledgerKey)
        {
            LedgerKey = ledgerKey;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP),
                RevokeSponsorshipOp = new xdr.RevokeSponsorshipOp
                {
                    Discriminant = xdr.RevokeSponsorshipType.Create(xdr.RevokeSponsorshipType.RevokeSponsorshipTypeEnum
                        .REVOKE_SPONSORSHIP_LEDGER_ENTRY),
                    LedgerKey = LedgerKey.ToXdr(),
                }
            };
        }

        /// <summary>
        ///     Builds RevokeLedgerEntrySponsorshipOperation operation.
        /// </summary>
        /// <see cref="RevokeLedgerEntrySponsorshipOperation" />
        public class Builder
        {
            private LedgerKey _ledgerKey;
            private KeyPair _sourceAccount;

            /// <summary>
            ///     Construct a new RevokeLedgerEntrySponsorshipOperation builder from a LedgerKey XDR.
            /// </summary>
            /// <param name="ledgerKey"></param>
            public Builder(xdr.LedgerKey ledgerKey)
            {
                _ledgerKey = LedgerKey.FromXdr(ledgerKey);
            }

            /// <summary>
            ///     Create a new RevokeLedgerEntrySponsorshipOperation builder with the given sponsoredId.
            /// </summary>
            /// <param name="ledgerKey"></param>
            public Builder(LedgerKey ledgerKey)
            {
                _ledgerKey = ledgerKey ?? throw new ArgumentNullException(nameof(ledgerKey));
            }

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
            public RevokeLedgerEntrySponsorshipOperation Build()
            {
                var operation = new RevokeLedgerEntrySponsorshipOperation(_ledgerKey);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}