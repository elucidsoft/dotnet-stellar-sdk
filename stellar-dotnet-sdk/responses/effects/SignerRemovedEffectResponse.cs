namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerRemovedEffectResponse : SignerEffectResponse
    {
        public override int TypeId => 11;

        public SignerRemovedEffectResponse()
        {

        }

        /// <inheritdoc />
        public SignerRemovedEffectResponse(int weight, string publicKey)
            : base(weight, publicKey)
        {
        }
    }
}