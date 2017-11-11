namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerUpdatedEffectResponse : SignerEffectResponse
    {
        /// <inheritdoc />
        public SignerUpdatedEffectResponse(int weight, string publicKey) 
            : base(weight, publicKey)
        {
        }
    }
}