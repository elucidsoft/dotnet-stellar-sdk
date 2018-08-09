using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class LedgerResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "sequence")]
        public long Sequence { get; private set; }

        [JsonProperty(PropertyName = "hash")] public string Hash { get; private set; }

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

        [JsonProperty(PropertyName = "base_fee_in_stroops")]
        public String BaseFeeInStroops { get; private set; }

        [JsonProperty(PropertyName = "base_reserve_in_stroops")]
        public String BaseReserveInStroops { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public LedgerResponseLinks Links { get; private set; }


        public LedgerResponse(long sequence, String hash, String pagingToken, String prevHash, int transactionCount, int operationCount, String closedAt, String totalCoins, String feePool, long baseFee, String baseReserve, String baseFeeInStroops, String baseReserveInStroops, int maxTxSetSize, LedgerResponseLinks links)
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
            BaseFeeInStroops = baseFeeInStroops;
            BaseReserve = baseReserve;
            BaseReserveInStroops = baseReserveInStroops;
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

            [JsonProperty(PropertyName = "self")] public Link Self { get; private set; }

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