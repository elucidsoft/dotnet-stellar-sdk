using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    public class EffectsRequestBuilder : RequestBuilderStreamable<EffectsRequestBuilder, EffectResponse>
    {
        public EffectsRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "effects", httpClient)
        {
        }

        ///<Summary>
        /// Builds request to <code>GET /accounts/{account}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-account.html">Effects for Account</a>
        /// </Summary>
        /// <param name="account">Account for which to get effects</param> 
        public EffectsRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /ledgers/{ledgerSeq}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-ledger.html">Effects for Ledger</a>
        /// </Summary>
        /// <param name="ledgerSeq">Ledger for which to get effects</param> 
        public EffectsRequestBuilder ForLedger(long ledgerSeq)
        {
            SetSegments("ledgers", ledgerSeq.ToString(), "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /transactions/{transactionId}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-transaction.html">Effect for Transaction</a>
        /// </Summary>
        /// <param name="transactionId">Transaction ID for which to get effects</param>
        public EffectsRequestBuilder ForTransaction(string transactionId)
        {
            transactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId), "transactionId cannot be null");
            SetSegments("transactions", transactionId, "effects");
            return this;
        }

        ///<Summary>
        /// Builds request to <code>GET /operation/{operationId}/effects</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/effects-for-operation.html">Effect for Operation</a>
        /// </Summary>
        /// <param name="operationId">Operation ID for which to get effects</param>
        public EffectsRequestBuilder ForOperation(long operationId)
        {
            SetSegments("operations", operationId.ToString(), "effects");
            return this;
        }
    }
}
