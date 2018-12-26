namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerCreatedEffectResponse : SignerEffectResponse
    {
        public override int TypeId => 10;

        public SignerCreatedEffectResponse()
        {

        }

        /// <inheritdoc />
        public SignerCreatedEffectResponse(int weight, string publicKey)
            : base(weight, publicKey)
        {
        }
    }
}