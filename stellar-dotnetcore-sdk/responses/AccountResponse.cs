using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses
{
    public class AccountResponse
    {
        public AccountResponse(KeyPair keyPair) => KeyPair = keyPair;

        public AccountResponse(KeyPair keyPair, long sequenceNumber)
            : this(keyPair)
        {
            SequenceNumber = sequenceNumber;
        }

        [JsonProperty(PropertyName = "account_id")]
        public KeyPair KeyPair { get; set; }

        [JsonProperty(PropertyName = "sequence")]
        public long SequenceNumber { get; set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; set; }

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
        public Links Links { get; set; }

        public long IncrementedSequenceNumber { get => SequenceNumber + 1; }

        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }
    }

    /// <summary>
    /// Represents account thresholds.
    /// </summary>
    public class Thresholds
    {
        public Thresholds(int lowThreshold, int medThreshold, int highThreshold)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
        }

        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; set; }

        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; set; }

        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; set; }
    }

    /// <summary>
    /// Represents account flags.
    /// </summary>
    public class Flags
    {
        [JsonProperty(PropertyName = "auth_required")]
        public bool AuthRequired { get; set; }

        [JsonProperty(PropertyName = "auth_revocable")]
        public bool AuthRevocable { get; set; }
    }


    /// <summary>
    /// Represents account balance.
    /// </summary>
    public class Balance
    {
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public string BalanceString { get; set; }
    }

    /// <summary>
    /// Represents account signers.
    /// </summary>
    public class Signer
    {
        public Signer(string accountId, int? weight)
        {
            AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId), "accountId cannot be null");
            Weight = weight ?? throw new ArgumentNullException(nameof(weight), "weight cannot be null");
        }

        [JsonProperty(PropertyName = "public_key")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; set; }
    }

    public class Links
    {
        public Links(Link effects, Link offers, Link operations, Link self, Link transactions)
        {
            Effects = effects;
            Offers = offers;
            Operations = operations;
            Self = self;
            Transactions = transactions;
        }

        [JsonProperty(PropertyName = "effects")]
        public Link Effects { get; set; }

        [JsonProperty(PropertyName = "offers")]
        public Link Offers { get; set; }

        [JsonProperty(PropertyName = "operations")]
        public Link Operations { get; set; }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; set; }

        [JsonProperty(PropertyName = "transactions")]
        public Link Transactions { get; set; }
    }
}
