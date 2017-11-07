using Newtonsoft.Json;
using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses
{
    public class SubmitTransactionResponse : Response
    {
        private readonly string _EnvelopeXdr;
        private readonly string _ResultXdr;

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; private set; }

        [JsonProperty(PropertyName = "ledger")]
        public long? Ledger { get; private set; }

        [JsonProperty(PropertyName = "envelope_xdr")]
        public string EnvelopeXdr
        {
            get
            {
                if (IsSuccess())
                {
                    return _EnvelopeXdr;
                }
                else
                {
                    return SubmitTransactionResponseExtras.EnvelopeXdr;
                }
            }
        }

        [JsonProperty(PropertyName = "result_xdr")]
        public string ResultXdr
        {
            get
            {
                if (IsSuccess())
                {
                    return _ResultXdr;
                }
                else
                {
                    return SubmitTransactionResponseExtras.ResultXdr;
                }
            }
        }

        [JsonProperty(PropertyName = "extras")]
        public Extras SubmitTransactionResponseExtras { get; private set; }

        public SubmitTransactionResponse(Extras extras, long? ledger, string hash, string envelopeXdr, string resultXdr)
        {
            SubmitTransactionResponseExtras = extras;
            Ledger = ledger;
            Hash = hash;
            _EnvelopeXdr = envelopeXdr;
            _ResultXdr = resultXdr;
        }

        public bool IsSuccess()
        {
            return Ledger != null;
        }

        /**
         * Helper method that returns Offer ID for ManageOffer from TransactionResult Xdr.
         * This is helpful when you need ID of an offer to update it later.
         * @param position Position of ManageOffer operation. If ManageOffer is second operation in this transaction this should be equal <code>1</code>.
         * @return Offer ID or <code>null</code> when operation at <code>position</code> is not a ManageOffer operation or error has occurred.
         */
        public long? GetOfferIdFromResult(int position)
        {
            if (!IsSuccess())
            {
                return null;
            }

            byte[] bytes = Convert.FromBase64String(ResultXdr);
            XdrDataInputStream xdrInputStream = new XdrDataInputStream(bytes);
            TransactionResult result;

            try
            {
                result = TransactionResult.Decode(xdrInputStream);
            }
            catch (Exception)
            {
                return null;
            }

            if (result.Result.Results[position] == null)
            {
                return null;
            }

            if (result.Result.Results[position].Tr.Discriminant.InnerValue != OperationType.OperationTypeEnum.MANAGE_OFFER)
            {
                return null;
            }

            if (result.Result.Results[0].Tr.ManageOfferResult.Success.Offer.Offer == null)
            {
                return null;
            }

            return result.Result.Results[0].Tr.ManageOfferResult.Success.Offer.Offer.OfferID.InnerValue;
        }

        /**
         * Additional information returned by a server.
         */
        public class Extras
        {
            [JsonProperty(PropertyName = "envelope_xdr")]
            public string EnvelopeXdr { get; private set; }

            [JsonProperty(PropertyName = "result_xdr")]
            public string ResultXdr { get; private set; }

            [JsonProperty(PropertyName = "result_codes")]
            public ResultCodes ExtrasResultCodes { get; private set; }

            public Extras(string envelopeXdr, string resultXdr, ResultCodes resultCodes)
            {
                EnvelopeXdr = envelopeXdr;
                ResultXdr = resultXdr;
                ExtrasResultCodes = resultCodes;
            }

            /**
             * Contains result codes for this transaction.
             * @see <a href="https://github.com/stellar/horizon/blob/master/src/github.com/stellar/horizon/codes/main.go" target="_blank">Possible values</a>
             */
            public class ResultCodes
            {
                [JsonProperty(PropertyName = "transaction")]
                public string TransactionResultCode { get; private set; }

                [JsonProperty(PropertyName = "operations")]
                public List<string> OperationsResultCodes { get; private set; }

                public ResultCodes(string transactionResultCode, List<string> operationsResultCodes)
                {
                    this.TransactionResultCode = transactionResultCode;
                    this.OperationsResultCodes = operationsResultCodes;
                }
            }
        }
    }
}
