using Newtonsoft.Json;
using System;

namespace stellar_dotnetcore_sdk.responses
{
    public class TransactionResponse
    {
        [JsonProperty(PropertyName = "hash")]
        public string hash { get; private set; }
        [JsonProperty(PropertyName = "ledger")]
        public long ledger { get; private set; }
        [JsonProperty(PropertyName = "created_at")]
        public string createdAt { get; private set; }
        [JsonProperty(PropertyName = "source_account")]
        public KeyPair sourceAccount { get; private set; }
        [JsonProperty(PropertyName = "paging_token")]
        public string pagingToken { get; private set; }
        [JsonProperty(PropertyName = "source_account_sequence")]
        public long sourceAccountSequence { get; private set; }
        [JsonProperty(PropertyName = "fee_paid")]
        public long feePaid { get; private set; }
        [JsonProperty(PropertyName = "operation_count")]
        public int operationCount { get; private set; }
        [JsonProperty(PropertyName = "envelope_xdr")]
        public string envelopeXdr { get; private set; }
        [JsonProperty(PropertyName = "result_xdr")]
        public string resultXdr { get; private set; }
        [JsonProperty(PropertyName = "result_meta_xdr")]
        public string resultMetaXdr { get; private set; }
        [JsonProperty(PropertyName = "_links")]
        public TransactionResponseLinks Links { get; private set; }

        // GSON won't serialize `transient` variables automatically. We need this behaviour
        // because Memo is an abstract class and GSON tries to instantiate it. (JAVA COMMENT)
        //Replaced transient with a simple JsonIgnore. MJM
        [JsonIgnore]
        public Memo Memo
        {
            get => _Memo;
            set {
                _Memo = value ?? throw new ArgumentNullException(nameof(value), "memo cannot be null");

                if (_Memo != null)
                {
                    throw new Exception("Memo has been already set.");
                }
                _Memo = value;
            }
        }

        [JsonIgnore]
        private Memo _Memo;

        public TransactionResponse(string hash, long ledger, string createdAt, KeyPair sourceAccount, string pagingToken, long sourceAccountSequence, long feePaid, int operationCount, string envelopeXdr, string resultXdr, string resultMetaXdr, Memo memo, TransactionResponseLinks links)
        {
            this.hash = hash;
            this.ledger = ledger;
            this.createdAt = createdAt;
            this.sourceAccount = sourceAccount;
            this.pagingToken = pagingToken;
            this.sourceAccountSequence = sourceAccountSequence;
            this.feePaid = feePaid;
            this.operationCount = operationCount;
            this.envelopeXdr = envelopeXdr;
            this.resultXdr = resultXdr;
            this.resultMetaXdr = resultMetaXdr;
            this.Memo = memo;
            this.Links = links;
        }

       ///
       /// Links connected to transaction.
       ///
        public class TransactionResponseLinks
        {
            [JsonProperty(PropertyName = "account")]
            public Link account { get; private set; }
            [JsonProperty(PropertyName = "effects")]
            public Link effects { get; private set; }
            [JsonProperty(PropertyName = "ledger")]
            public Link ledger { get; private set; }
            [JsonProperty(PropertyName = "operations")]
            public Link operations { get; private set; }
            [JsonProperty(PropertyName = "precedes")]
            public Link precedes { get; private set; }
            [JsonProperty(PropertyName = "self")]
            public Link self { get; private set; }
            [JsonProperty(PropertyName = "succeeds")]
            public Link succeeds { get; private set; }

            public TransactionResponseLinks(Link account, Link effects, Link ledger, Link operations, Link self, Link precedes, Link succeeds)
            {
                this.account = account;
                this.effects = effects;
                this.ledger = ledger;
                this.operations = operations;
                this.self = self;
                this.precedes = precedes;
                this.succeeds = succeeds;
            }

        }
    }
}
