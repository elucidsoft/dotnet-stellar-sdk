using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public abstract class Operation
    {
        public Operation() { }

        private KeyPair _sourceAccount;

        public KeyPair SourceAccount
        {
            get { return _sourceAccount; }
            set { _sourceAccount = value ?? throw new ArgumentNullException(nameof(value), "keypair cannot be null"); }
        }

        private static readonly Decimal ONE = new Decimal(10000000);

        protected static long ToXdrAmount(String value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "value cannot be null");


            //This bascially takes a decimal value and turns it into a large integer.
            long amount = (long)(Decimal.Parse(value) * Operation.ONE);
            return amount;
        }

        protected static String FromXdrAmount(long value)
        {
            Decimal amount = new Decimal(value) * Operation.ONE;
            return amount.ToString();
        }

        /**
         * Generates Operation XDR object.
         */
        public xdr.Operation ToXdr()
        {
            xdr.Operation thisXdr = new xdr.Operation();
            if (SourceAccount != null)
            {
                xdr.AccountID sourceAccount = new xdr.AccountID();
                sourceAccount.InnerValue = SourceAccount.XdrPublicKey;
                thisXdr.SourceAccount = sourceAccount;
            }
            thisXdr.Body = ToOperationBody();
            return thisXdr;
        }

        /**
         * Returns base64-encoded Operation XDR object.
         */
        public String ToXdrBase64()
        {
            xdr.Operation operation = this.ToXdr();
            var memoryStream = new MemoryStream();
            var writer = new xdr.XdrDataOutputStream(memoryStream);
            xdr.Operation.Encode(writer, operation);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        /**
         * Returns new Operation object from Operation XDR object.
         * @param xdr XDR object
         */
        public static Operation FromXdr(xdr.Operation thisXdr)
        {
            xdr.Operation.OperationBody body = thisXdr.Body;
            Operation operation;
            switch (body.Discriminant.InnerValue)
            {
                case xdr.OperationType.OperationTypeEnum.CREATE_ACCOUNT:
                    operation = new CreateAccountOperation.Builder(body.CreateAccountOp).Build();
                    break;
                case xdr.OperationType.OperationTypeEnum.PAYMENT:
                    operation = new PaymentOperation.Builder(body.PaymentOp).Build();
                    break;
                case xdr.OperationType.OperationTypeEnum.PATH_PAYMENT:
                    operation = new PathPaymentOperation.Builder(body.PathPaymentOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.MANAGE_OFFER:
                    operation = new ManageOfferOperation.Builder(body.ManageOfferOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.CREATE_PASSIVE_OFFER:
                    operation = new CreatePassiveOfferOperation.Builder(body.CreatePassiveOfferOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.SET_OPTIONS:
                    operation = new SetOptionsOperation.Builder(body.SetOptionsOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.CHANGE_TRUST:
                    operation = new ChangeTrustOperation.Builder(body.ChangeTrustOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.ALLOW_TRUST:
                    operation = new AllowTrustOperation.Builder(body.AllowTrustOp).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.ACCOUNT_MERGE:
                    operation = new AccountMergeOperation.Builder(body).build();
                    break;
                case xdr.OperationType.OperationTypeEnum.MANAGE_DATA:
                    operation = new ManageDataOperation.Builder(body.ManageDataOp).build();
                    break;
                default:
                    throw new Exception("Unknown operation body " + body.Discriminant.InnerValue);
            }
            if (thisXdr.SourceAccount != null)
            {
                operation.SourceAccount = KeyPair.FromXdrPublicKey(thisXdr.SourceAccount.InnerValue);
            }
            return operation;
        }


        /**
         * Generates OperationBody XDR object
         * @return OperationBody XDR object
         */
        public abstract xdr.Operation.OperationBody ToOperationBody();
    }
}
