using System;
using System.IO;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public abstract class Operation
    {
        private static readonly decimal ONE = new decimal(10000000);

        private KeyPair _sourceAccount;

        public KeyPair SourceAccount
        {
            get => _sourceAccount;
            set => _sourceAccount = value ?? throw new ArgumentNullException(nameof(value), "keypair cannot be null");
        }

        protected static long ToXdrAmount(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "value cannot be null");


            //This bascially takes a decimal value and turns it into a large integer.
            var amount = (long) (decimal.Parse(value) * ONE);
            return amount;
        }

        protected static string FromXdrAmount(long value)
        {
            var amount = new decimal(value) * ONE;
            return amount.ToString();
        }

        /**
         * Generates Operation XDR object.
         */
        public xdr.Operation ToXdr()
        {
            var thisXdr = new xdr.Operation();
            if (SourceAccount != null)
            {
                var sourceAccount = new AccountID();
                sourceAccount.InnerValue = SourceAccount.XdrPublicKey;
                thisXdr.SourceAccount = sourceAccount;
            }
            thisXdr.Body = ToOperationBody();
            return thisXdr;
        }

        /**
         * Returns base64-encoded Operation XDR object.
         */
        public string ToXdrBase64()
        {
            var operation = ToXdr();
            var memoryStream = new MemoryStream();
            var writer = new XdrDataOutputStream();
            xdr.Operation.Encode(writer, operation);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        /**
         * Returns new Operation object from Operation XDR object.
         * @param xdr XDR object
         */
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
                case OperationType.OperationTypeEnum.PATH_PAYMENT:
                    operation = new PathPaymentOperation.Builder(body.PathPaymentOp).build();
                    break;
                case OperationType.OperationTypeEnum.MANAGE_OFFER:
                    operation = new ManageOfferOperation.Builder(body.ManageOfferOp).build();
                    break;
                case OperationType.OperationTypeEnum.CREATE_PASSIVE_OFFER:
                    operation = new CreatePassiveOfferOperation.Builder(body.CreatePassiveOfferOp).build();
                    break;
                case OperationType.OperationTypeEnum.SET_OPTIONS:
                    operation = new SetOptionsOperation.Builder(body.SetOptionsOp).build();
                    break;
                case OperationType.OperationTypeEnum.CHANGE_TRUST:
                    operation = new ChangeTrustOperation.Builder(body.ChangeTrustOp).build();
                    break;
                case OperationType.OperationTypeEnum.ALLOW_TRUST:
                    operation = new AllowTrustOperation.Builder(body.AllowTrustOp).build();
                    break;
                case OperationType.OperationTypeEnum.ACCOUNT_MERGE:
                    operation = new AccountMergeOperation.Builder(body).build();
                    break;
                case OperationType.OperationTypeEnum.MANAGE_DATA:
                    operation = new ManageDataOperation.Builder(body.ManageDataOp).build();
                    break;
                default:
                    throw new Exception("Unknown operation body " + body.Discriminant.InnerValue);
            }
            if (thisXdr.SourceAccount != null)
                operation.SourceAccount = KeyPair.FromXdrPublicKey(thisXdr.SourceAccount.InnerValue);
            return operation;
        }


        /**
         * Generates OperationBody XDR object
         * @return OperationBody XDR object
         */
        public abstract xdr.Operation.OperationBody ToOperationBody();
    }
}