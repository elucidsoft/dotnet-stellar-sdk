using Newtonsoft.Json;
using stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;

namespace stellar_dotnet_sdk.responses
{
    public class SubmitTransactionResponse : Response
    {
        private readonly string _envelopeXdr;
        private readonly string _resultXdr;

        [JsonProperty(PropertyName = "hash")] public string Hash { get; private set; }

        [JsonProperty(PropertyName = "ledger")]
        public int? Ledger { get; private set; }

        [JsonProperty(PropertyName = "envelope_xdr")]
        public string EnvelopeXdr
        {
            get
            {
                if (IsSuccess())
                {
                    return _envelopeXdr;
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
                    return _resultXdr;
                }
                else
                {
                    return SubmitTransactionResponseExtras.ResultXdr;
                }
            }
        }

        public TransactionResult Result
        {
            get
            {
                if (IsSuccess())
                {
                    return TransactionResult.FromXdr(_resultXdr);
                }
                return TransactionResult.FromXdr(SubmitTransactionResponseExtras.ResultXdr);
            }
        }

        [JsonProperty(PropertyName = "extras")]
        public Extras SubmitTransactionResponseExtras { get; private set; }

        public SubmitTransactionResponse(Extras extras, int? ledger, string hash, string envelopeXdr, string resultXdr)
        {
            SubmitTransactionResponseExtras = extras;
            Ledger = ledger;
            Hash = hash;
            _envelopeXdr = envelopeXdr;
            _resultXdr = resultXdr;
        }

        public bool IsSuccess()
        {
            return Ledger != null;
        }

        ///<summary>
        /// Helper method that returns Offer ID for ManageOffer from TransactionResult Xdr.
        /// This is helpful when you need ID of an offer to update it later.
        /// </summary>
        /// <param name="position">Position of ManageOffer operation. If ManageOffer is second operation in this transaction this should be equal <code>1</code>.</param>
        /// <returns>Offer ID or <code>null</code> when operation at <code>position</code> is not a ManageOffer operation or error has occurred.</returns>
        public long? GetOfferIdFromResult(int position)
        {
            if (!IsSuccess())
            {
                return null;
            }

            byte[] bytes = Convert.FromBase64String(ResultXdr);
            XdrDataInputStream xdrInputStream = new XdrDataInputStream(bytes);
            xdr.TransactionResult result;

            try
            {
                result = xdr.TransactionResult.Decode(xdrInputStream);
            }
            catch (Exception)
            {
                return null;
            }

            if (result.Result.Results[position] == null)
            {
                return null;
            }

            if (result.Result.Results[position].Tr.Discriminant.InnerValue != OperationType.OperationTypeEnum.MANAGE_SELL_OFFER)
            {
                return null;
            }

            if (result.Result.Results[0].Tr.ManageSellOfferResult.Success.Offer.Offer == null)
            {
                return null;
            }

            return result.Result.Results[0].Tr.ManageSellOfferResult.Success.Offer.Offer.OfferID.InnerValue;
        }

        ///<summary>
        /// Additional information returned by a server.
        ///</summary>
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

            ///<summary>
            /// Contains result codes for this transaction.
            /// see <a href="https://github.com/stellar/horizon/blob/master/src/github.com/stellar/horizon/codes/main.go" target="_blank">Possible values</a>
            /// </summary>
            public class ResultCodes
            {
                [JsonProperty(PropertyName = "transaction")]
                public string TransactionResultCode { get; private set; }

                [JsonProperty(PropertyName = "operations")]
                public List<string> OperationsResultCodes { get; private set; }

                public ResultCodes(string transactionResultCode, List<string> operationsResultCodes)
                {
                    TransactionResultCode = transactionResultCode;
                    OperationsResultCodes = operationsResultCodes;
                }
            }
        }
    }
}