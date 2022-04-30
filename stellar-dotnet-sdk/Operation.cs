using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public abstract class Operation
    {
        private IAccountId _sourceAccount;

        public IAccountId SourceAccount
        {
            get => _sourceAccount;
            set => _sourceAccount = value ?? throw new ArgumentNullException(nameof(value), "keypair cannot be null");
        }

        /// <summary>
        /// Threshold level for the operation.
        /// </summary>
        public virtual OperationThreshold Threshold => OperationThreshold.Medium;

        public static long ToXdrAmount(string value)
        {
            return Amount.ToXdr(value);
        }

        public static string FromXdrAmount(long value)
        {
            return Amount.FromXdr(value);
        }

        ///<summary>
        /// Generates Operation XDR object.
        ///</summary>
        public xdr.Operation ToXdr()
        {
            var thisXdr = new xdr.Operation();
            if (SourceAccount != null)
            {
                thisXdr.SourceAccount = SourceAccount.MuxedAccount;
            }

            thisXdr.Body = ToOperationBody();
            return thisXdr;
        }

        ///<summary>
        /// Returns base64-encoded Operation XDR object.
        ///</summary>
        public string ToXdrBase64()
        {
            var operation = ToXdr();
            var writer = new XdrDataOutputStream();
            xdr.Operation.Encode(writer, operation);
            return Convert.ToBase64String(writer.ToArray());
        }

        ///<summary>
        ///</summary>
        /// <returns>new Operation object from Operation XDR object.</returns>
        /// <param name="thisXdr">XDR object</param>
        [Obsolete]
        public static Operation FromXdr(xdr.Operation thisXdr)
        {
            var body = thisXdr.Body;
            Operation operation;
            switch (body.Discriminant.InnerValue)
            {
                case OperationType.OperationTypeEnum.CREATE_ACCOUNT:
                    operation = new CreateAccountOperation.Builder(body.CreateAccountOp).Build();
                    break;
                case OperationType.OperationTypeEnum.PAYMENT:
                    operation = new PaymentOperation.Builder(body.PaymentOp).Build();
                    break;
                case OperationType.OperationTypeEnum.PATH_PAYMENT_STRICT_RECEIVE:
                    operation = new PathPaymentStrictReceiveOperation.Builder(body.PathPaymentStrictReceiveOp).Build();
                    break;
                case OperationType.OperationTypeEnum.MANAGE_SELL_OFFER:
                    operation = new ManageSellOfferOperation.Builder(body.ManageSellOfferOp).Build();
                    break;
                case OperationType.OperationTypeEnum.MANAGE_BUY_OFFER:
                    operation = new ManageBuyOfferOperation.Builder(body.ManageBuyOfferOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CREATE_PASSIVE_SELL_OFFER:
                    operation = new CreatePassiveSellOfferOperation.Builder(body.CreatePassiveSellOfferOp).Build();
                    break;
                case OperationType.OperationTypeEnum.SET_OPTIONS:
                    operation = new SetOptionsOperation.Builder(body.SetOptionsOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CHANGE_TRUST:
                    operation = new ChangeTrustOperation.Builder(body.ChangeTrustOp).Build();
                    break;
                case OperationType.OperationTypeEnum.ALLOW_TRUST:
                    operation = new AllowTrustOperation.Builder(body.AllowTrustOp).Build();
                    break;
                case OperationType.OperationTypeEnum.ACCOUNT_MERGE:
                    operation = new AccountMergeOperation.Builder(body).Build();
                    break;
                case OperationType.OperationTypeEnum.MANAGE_DATA:
                    operation = new ManageDataOperation.Builder(body.ManageDataOp).Build();
                    break;
                case OperationType.OperationTypeEnum.BUMP_SEQUENCE:
                    operation = new BumpSequenceOperation.Builder(body.BumpSequenceOp).Build();
                    break;
                case OperationType.OperationTypeEnum.INFLATION:
                    operation = new InflationOperation.Builder().Build();
                    break;
                case OperationType.OperationTypeEnum.PATH_PAYMENT_STRICT_SEND:
                    operation = new PathPaymentStrictSendOperation.Builder(body.PathPaymentStrictSendOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE:
                    operation = new CreateClaimableBalanceOperation.Builder(body.CreateClaimableBalanceOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE:
                    operation = new ClaimClaimableBalanceOperation.Builder(body.ClaimClaimableBalanceOp).Build();
                    break;
                case OperationType.OperationTypeEnum.BEGIN_SPONSORING_FUTURE_RESERVES:
                    operation = new BeginSponsoringFutureReservesOperation.Builder(body.BeginSponsoringFutureReservesOp).Build();
                    break;
                case OperationType.OperationTypeEnum.END_SPONSORING_FUTURE_RESERVES:
                    operation = new EndSponsoringFutureReservesOperation.Builder().Build();
                    break;
                case OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP:
                    operation = new RevokeSponsorshipOperation.Builder(body.RevokeSponsorshipOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CLAWBACK:
                    operation = new ClawbackOperation.Builder(body.ClawbackOp).Build();
                    break;
                case OperationType.OperationTypeEnum.CLAWBACK_CLAIMABLE_BALANCE:
                    operation = new ClawbackClaimableBalanceOperation.Builder(body.ClawbackClaimableBalanceOp).Build();
                    break;
                case OperationType.OperationTypeEnum.SET_TRUST_LINE_FLAGS:
                    operation = new SetTrustlineFlagsOperation.Builder(body.SetTrustLineFlagsOp).Build();
                    break;
                case OperationType.OperationTypeEnum.LIQUIDITY_POOL_DEPOSIT:
                    operation = new LiquidityPoolDepositOperation.Builder(body.LiquidityPoolDepositOp).Build();
                    break;
                case OperationType.OperationTypeEnum.LIQUIDITY_POOL_WITHDRAW:
                    operation = new LiquidityPoolWithdrawOperation.Builder(body.LiquidityPoolWithdrawOp).Build();
                    break;
                default:
                    throw new Exception("Unknown operation body " + body.Discriminant.InnerValue);
            }

            if (thisXdr.SourceAccount != null)
            {
                operation.SourceAccount = MuxedAccount.FromXdrMuxedAccount(thisXdr.SourceAccount);
            }

            return operation;
        }

        ///<summary>
        /// Generates OperationBody XDR object
        ///</summary>
        ///<returns>OperationBody XDR object</returns>
        public abstract xdr.Operation.OperationBody ToOperationBody();
    }
}