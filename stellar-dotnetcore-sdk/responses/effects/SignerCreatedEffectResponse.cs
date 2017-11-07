using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerCreatedEffectResponse : SignerEffectResponse
    {
        public SignerCreatedEffectResponse(int weight, string publicKey) 
            : base(weight, publicKey)
        {
        }
    }
}