using System;

namespace stellar_dotnet_sdk
{
    public abstract class RevokeSponsorshipOperation : Operation
    {
        /// <summary>
        ///     Builds RevokeSponsorshipOperation operation.
        /// </summary>
        /// <see cref="RevokeSponsorshipOperation" />
        public class Builder
        {
            private readonly RevokeLedgerEntrySponsorshipOperation.Builder _ledgerEntryBuilder;
            private readonly RevokeSignerSponsorshipOperation.Builder _signerBuilder;

            public Builder(xdr.RevokeSponsorshipOp op)
            {
                switch (op.Discriminant.InnerValue)
                {
                    case xdr.RevokeSponsorshipType.RevokeSponsorshipTypeEnum.REVOKE_SPONSORSHIP_LEDGER_ENTRY:
                        _ledgerEntryBuilder = new RevokeLedgerEntrySponsorshipOperation.Builder(op.LedgerKey);
                        _signerBuilder = null;
                        break;
                    case xdr.RevokeSponsorshipType.RevokeSponsorshipTypeEnum.REVOKE_SPONSORSHIP_SIGNER:
                        _signerBuilder = new RevokeSignerSponsorshipOperation.Builder(op.Signer);
                        _ledgerEntryBuilder = null;
                        break;
                    default:
                        throw new Exception("Unknown RevokeSponsorshipOp " + op.Discriminant.InnerValue);
                }
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public RevokeSponsorshipOperation Build()
            {
                if (_ledgerEntryBuilder != null)
                    return _ledgerEntryBuilder.Build();
                if (_signerBuilder != null)
                    return _signerBuilder.Build();

                // unreachable
                throw new Exception("Builder not constructed");
            }

        }
    }
}