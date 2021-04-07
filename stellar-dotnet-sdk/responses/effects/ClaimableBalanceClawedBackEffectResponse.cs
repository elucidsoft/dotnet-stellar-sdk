using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents claimable_balance_clawed_back effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class ClaimableBalanceClawedBackEffectResponse : EffectResponse
    {
        public override int TypeId => 80;

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        public ClaimableBalanceClawedBackEffectResponse()
        {

        }

        public ClaimableBalanceClawedBackEffectResponse(string balanceID)
        {
            BalanceID = balanceID;
        }
    }
}