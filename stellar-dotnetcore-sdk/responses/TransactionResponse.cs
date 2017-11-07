using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace stellar_dotnetcore_sdk.responses
{
    public class TransactionResponse
    {
        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; private set; }

        [JsonProperty(PropertyName = "ledger")]
        public long Ledger { get; private set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; private set; }

        [JsonProperty(PropertyName = "source_account")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair SourceAccount { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }

        [JsonProperty(PropertyName = "source_account_sequence")]
        public long SourceAccountSequence { get; private set; }

        [JsonProperty(PropertyName = "fee_paid")]
        public long FeePaid { get; private set; }

        [JsonProperty(PropertyName = "operation_count")]
        public int OperationCount { get; private set; }

        [JsonProperty(PropertyName = "envelope_xdr")]
        public string EnvelopeXdr { get; private set; }

        [JsonProperty(PropertyName = "result_xdr")]
        public string ResultXdr { get; private set; }

        [JsonProperty(PropertyName = "result_meta_xdr")]
        public string ResultMetaXdr { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public TransactionResponseLinks Links { get; private set; }

        public Memo Memo
        {
            get => _Memo;
            set
            {
                if (_Memo != null)
                {
                    throw new Exception("Memo has been already set.");
                }

                _Memo = value ?? throw new ArgumentNullException(nameof(value), "memo cannot be null");             
                _Memo = value;
            }
        }

        private Memo _Memo;

        public string MemoStr { get; }

        public TransactionResponse(string hash, long ledger, string createdAt, KeyPair sourceAccount, string pagingToken, long sourceAccountSequence, long feePaid, int operationCount, string envelopeXdr, string resultXdr, string resultMetaXdr, string memo, TransactionResponseLinks links)
        {
            Hash = hash;
            Ledger = ledger;
            CreatedAt = createdAt;
            SourceAccount = sourceAccount;
            PagingToken = pagingToken;
            SourceAccountSequence = sourceAccountSequence;
            FeePaid = feePaid;
            OperationCount = operationCount;
            EnvelopeXdr = envelopeXdr;
            ResultXdr = resultXdr;
            ResultMetaXdr = resultMetaXdr;
            MemoStr = memo;
            Links = links;
        }

       ///
       /// Links connected to transaction.
       ///
        public class TransactionResponseLinks
        {
            [JsonProperty(PropertyName = "account")]
            public Link Account { get; private set; }

            [JsonProperty(PropertyName = "effects")]
            public Link Effects { get; private set; }

            [JsonProperty(PropertyName = "ledger")]
            public Link Ledger { get; private set; }

            [JsonProperty(PropertyName = "operations")]
            public Link Operations { get; private set; }

            [JsonProperty(PropertyName = "precedes")]
            public Link Precedes { get; private set; }

            [JsonProperty(PropertyName = "self")]
            public Link Self { get; private set; }

            [JsonProperty(PropertyName = "succeeds")]
            public Link Succeeds { get; private set; }

            public TransactionResponseLinks(Link account, Link effects, Link ledger, Link operations, Link self, Link precedes, Link succeeds)
            {
                Account = account;
                Effects = effects;
                Ledger = ledger;
                Operations = operations;
                Self = self;
                Precedes = precedes;
                Succeeds = succeeds;
            }

        }
    }
}
