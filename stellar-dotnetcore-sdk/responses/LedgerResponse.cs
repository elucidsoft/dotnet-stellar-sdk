using System;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    public class LedgerResponse : Response
    {
        [JsonProperty(PropertyName = "sequence")]
        public long Sequence { get; private set; }

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }

        [JsonProperty(PropertyName = "prev_hash")]
        public string PrevHash { get; private set; }

        [JsonProperty(PropertyName = "transaction_count")]
        public int TransactionCount { get; private set; }

        [JsonProperty(PropertyName = "operation_count")]
        public int OperationCount { get; private set; }

        [JsonProperty(PropertyName = "closed_at")]
        public string ClosedAt { get; private set; }

        [JsonProperty(PropertyName = "total_coins")]
        public string TotalCoins { get; private set; }

        [JsonProperty(PropertyName = "fee_pool")]
        public string FeePool { get; private set; }

        [JsonProperty(PropertyName = "base_fee")]
        public long BaseFee { get; private set; }

        [JsonProperty(PropertyName = "base_reserve")]
        public string BaseReserve { get; private set; }

        [JsonProperty(PropertyName = "max_tx_set_size")]
        public int MaxTxSetSize { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public LedgerResponseLinks Links { get; private set; }


        public LedgerResponse(long sequence, string hash, string pagingToken, string prevHash, int transactionCount, int operationCount, string closedAt, string totalCoins, string feePool, long baseFee, String baseReserve, int maxTxSetSize, LedgerResponseLinks links)
        {
            Sequence = sequence;
            Hash = hash;
            PagingToken = pagingToken;
            PrevHash = prevHash;
            TransactionCount = transactionCount;
            OperationCount = operationCount;
            ClosedAt = closedAt;
            TotalCoins = totalCoins;
            FeePool = feePool;
            BaseFee = baseFee;
            BaseReserve = baseReserve;
            MaxTxSetSize = maxTxSetSize;
            Links = links;
        }     

        ///
        /// Links connected to ledger.
        ///
        public class LedgerResponseLinks
        {
            [JsonProperty(PropertyName = "effects")]
            public Link Effects { get; private set; }

            [JsonProperty(PropertyName = "operations")]
            public Link Operations { get; private set; }

            [JsonProperty(PropertyName = "self")]
            public Link Self { get; private set; }

            [JsonProperty(PropertyName = "transactions")]
            public Link Transactions { get; private set; }

            public LedgerResponseLinks(Link effects, Link operations, Link self, Link transactions)
            {
                Effects = effects;
                Operations = operations;
                Self = self;
                Transactions = transactions;
            }

        }
    }
}
