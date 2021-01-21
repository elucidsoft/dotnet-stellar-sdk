namespace stellar_dotnet_sdk
{
    public static class AssetExtensions
    {
        /// <summary>
        /// Encode the asset to a string used in query parameters. The native assets is encoded as "native", while
        /// credit assets are encoded as "CODE:ISSUER".
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <returns></returns>
        public static string ToQueryParameterEncodedString(this Asset asset)
        {
            if (asset is AssetTypeCreditAlphaNum)
            {
                var creditAsset = (AssetTypeCreditAlphaNum)asset;
                return $"{creditAsset.Code}:{creditAsset.Issuer}";
            }
            return asset.GetType();
        }
    }
}