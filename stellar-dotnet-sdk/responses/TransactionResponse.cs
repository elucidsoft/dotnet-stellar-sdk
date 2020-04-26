using Newtonsoft.Json;
using System;
using System.ComponentModel;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.responses
{
    public class TransactionResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; private set; }

        [JsonProperty(PropertyName = "ledger")]
        public uint Ledger { get; private set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; private set; }

        [JsonProperty(PropertyName = "source_account")]
        public string SourceAccount { get; private set; }

        [DefaultValue(true)]
        [JsonProperty(PropertyName = "successful", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Successful { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }

        [JsonProperty(PropertyName = "source_account_sequence")]
        public long SourceAccountSequence { get; private set; }

        [JsonProperty(PropertyName = "fee_account")]
        public long FeeAccount { get; set; }

        [JsonProperty(PropertyName = "fee_charged")]
        public long FeeCharged { get; set; }

        [JsonProperty(PropertyName = "max_fee")]
        public long MaxFee { get; private set; }

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

        [JsonProperty(PropertyName = "memo_type")]
        public string MemoType { get; private set; }

        [JsonProperty(PropertyName = "memo")]
        public string MemoValue { get; private set; }

        [JsonProperty(PropertyName = "fee_bump_transaction")]
        public FeeBumpTransaction FeeBumpTransaction { get; set; }

        [JsonProperty(PropertyName = "inner_transaction")]
        public InnerTransaction InnerTransaction { get; set; }

        public Memo Memo
        {
            get
            {
                switch (MemoType)
                {
                    case "none":
                        return Memo.None();
                    case "id":
                        return Memo.Id(ulong.Parse(MemoValue));
                    case "hash":
                        return Memo.Hash(Convert.FromBase64String(MemoValue));
                    case "return":
                        return Memo.ReturnHash(Convert.FromBase64String(MemoValue));
                    default:
                        throw new ArgumentException(nameof(MemoType));
                }
            }
            private set
            {
                switch (value)
                {
                    case MemoNone _:
                        MemoType = "none";
                        MemoValue = null;
                        return;
                    case MemoId id:
                        MemoType = "id";
                        MemoValue = id.IdValue.ToString();
                        return;
                    case MemoHash h:
                        MemoType = "hash";
                        MemoValue = Convert.ToBase64String(h.MemoBytes);
                        return;
                    case MemoReturnHash r:
                        MemoType = "return";
                        MemoValue = Convert.ToBase64String(r.MemoBytes);
                        return;
                    default:
                        throw new ArgumentException(nameof(value));
                }
            }
        }

        public TransactionResult Result => TransactionResult.FromXdr(ResultXdr);

        public TransactionResponse()
        {
            // Used by deserializer
        }

        public TransactionResponse(string hash, uint ledger, string createdAt, string sourceAccount, bool successful,
            string pagingToken, long sourceAccountSequence, long feePaid, int operationCount, string envelopeXdr,
            string resultXdr, string resultMetaXdr, Memo memo, TransactionResponseLinks links)
        {
            Hash = hash;
            Ledger = ledger;
            CreatedAt = createdAt;
            SourceAccount = sourceAccount;
            Successful = successful;
            PagingToken = pagingToken;
            SourceAccountSequence = sourceAccountSequence;
            OperationCount = operationCount;
            EnvelopeXdr = envelopeXdr;
            ResultXdr = resultXdr;
            ResultMetaXdr = resultMetaXdr;
            Memo = memo;
            Links = links;
        }

        ///
        /// Links connected to transaction.
        ///
        public class TransactionResponseLinks
        {
            [JsonProperty(PropertyName = "account")]
            public Link<AccountResponse> Account { get; private set; }

            [JsonProperty(PropertyName = "effects")]
            public Link<Page<EffectResponse>> Effects { get; private set; }

            [JsonProperty(PropertyName = "ledger")]
            public Link<LedgerResponse> Ledger { get; private set; }

            [JsonProperty(PropertyName = "operations")]
            public Link<Page<OperationResponse>> Operations { get; private set; }

            [JsonProperty(PropertyName = "precedes")]
            public Link<TransactionResponse> Precedes { get; private set; }

            [JsonProperty(PropertyName = "self")]
            public Link<TransactionResponse> Self { get; private set; }

            [JsonProperty(PropertyName = "succeeds")]
            public Link<TransactionResponse> Succeeds { get; private set; }

            public TransactionResponseLinks(Link<AccountResponse> account, Link<Page<EffectResponse>> effects,
                Link<LedgerResponse> ledger, Link<Page<OperationResponse>> operations, Link<TransactionResponse> self,
                Link<TransactionResponse> precedes, Link<TransactionResponse> succeeds)
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