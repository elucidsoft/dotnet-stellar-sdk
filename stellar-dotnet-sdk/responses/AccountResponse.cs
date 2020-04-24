using System.Collections.Generic;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class AccountResponse : Response, ITransactionBuilderAccount
    {
        private AccountResponse()
        {
        }

        public AccountResponse(string accountId)
        {
            AccountId = accountId;
        }

        public AccountResponse(string accountId, long sequenceNumber)
            : this(accountId)
        {
            SequenceNumber = sequenceNumber;
        }

        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "sequence")]
        public long SequenceNumber { get; set; }

        [JsonProperty(PropertyName = "subentry_count")]
        public int SubentryCount { get; set; }

        [JsonProperty(PropertyName = "inflation_destination")]
        public string InflationDestination { get; set; }

        [JsonProperty(PropertyName = "home_domain")]
        public string HomeDomain { get; set; }

        [JsonProperty(PropertyName = "thresholds")]
        public Thresholds Thresholds { get; set; }

        [JsonProperty(PropertyName = "flags")] 
        public Flags Flags { get; set; }

        [JsonProperty(PropertyName = "balances")]
        public Balance[] Balances { get; set; }

        [JsonProperty(PropertyName = "signers")]
        public Signer[] Signers { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public AccountResponseLinks Links { get; set; }

        [JsonProperty("Data")] public Dictionary<string, string> Data { get; set; }

        public long IncrementedSequenceNumber => SequenceNumber + 1;

        public KeyPair KeyPair => KeyPair.FromAccountId(AccountId);

        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }

        public KeyPair GetKeyPair(string accountId)
        {
            return KeyPair.FromAccountId(accountId);
        }
    }
}
