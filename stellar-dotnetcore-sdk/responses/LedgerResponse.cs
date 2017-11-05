using System;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    public class LedgerResponse : Response
    {
        [JsonProperty(PropertyName = "sequence")]
        public long Sequence { get; private set; }
        [JsonProperty(PropertyName = "hash")]
        public String Hash { get; private set; }
        [JsonProperty(PropertyName = "paging_token")]
        public String PagingToken { get; private set; }
        [JsonProperty(PropertyName = "prev_hash")]
        public String PrevHash { get; private set; }
        [JsonProperty(PropertyName = "transaction_count")]
        public int TransactionCount { get; private set; }
        [JsonProperty(PropertyName = "operation_count")]
        public int OperationCount { get; private set; }
        [JsonProperty(PropertyName = "closed_at")]
        public String ClosedAt { get; private set; }
        [JsonProperty(PropertyName = "total_coins")]
        public String TotalCoins { get; private set; }
        [JsonProperty(PropertyName = "fee_pool")]
        public String FeePool { get; private set; }
        [JsonProperty(PropertyName = "base_fee")]
        public long BaseFee { get; private set; }
        [JsonProperty(PropertyName = "base_reserve")]
        public String BaseReserve { get; private set; }
        [JsonProperty(PropertyName = "max_tx_set_size")]
        public int MaxTxSetSize { get; private set; }
        [JsonProperty(PropertyName = "_links")]
        public Links LedgerResponseLinks { get; private set; }


        public LedgerResponse(long sequence, String hash, String pagingToken, String prevHash, int transactionCount, int operationCount, String closedAt, String totalCoins, String feePool, long baseFee, String baseReserve, int maxTxSetSize, Links links)
        {
            this.Sequence = sequence;
            this.Hash = hash;
            this.PagingToken = pagingToken;
            this.PrevHash = prevHash;
            this.TransactionCount = transactionCount;
            this.OperationCount = operationCount;
            this.ClosedAt = closedAt;
            this.TotalCoins = totalCoins;
            this.FeePool = feePool;
            this.BaseFee = baseFee;
            this.BaseReserve = baseReserve;
            this.MaxTxSetSize = maxTxSetSize;
            this.LedgerResponseLinks = links;
        }

      

        ///
        /// Links connected to ledger.
        ///
        public class Links
        {
            [JsonProperty(PropertyName = "effects")]
            public Link Effects { get; private set; }
            [JsonProperty(PropertyName = "operations")]
            public Link Operations { get; private set; }
            [JsonProperty(PropertyName = "self")]
            public Link Self { get; private set; }
            [JsonProperty(PropertyName = "transactions")]
            public Link Transactions { get; private set; }

            Links(Link effects, Link operations, Link self, Link transactions)
            {
                Effects = effects;
                Operations = operations;
                Self = self;
                Transactions = transactions;
            }

        }
    }
}
