using System.Collections.Generic;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class PathResponse : Response
    {
        [JsonProperty(PropertyName = "destination_amount")]
        public string DestinationAmount { get; private set; }

        [JsonProperty(PropertyName = "destination_asset_type")]
        public string DestinationAssetType { get; private set; }

        [JsonProperty(PropertyName = "destination_asset_code")]
        public string DestinationAssetCode { get; private set; }

        [JsonProperty(PropertyName = "destination_asset_issuer")]
        public string DestinationAssetIssuer { get; private set; }

        [JsonProperty(PropertyName = "source_amount")]
        public string SourceAmount { get; private set; }

        [JsonProperty(PropertyName = "source_asset_type")]
        public string SourceAssetType { get; private set; }

        [JsonProperty(PropertyName = "source_asset_code")]
        public string SourceAssetCode { get; private set; }

        [JsonProperty(PropertyName = "source_asset_issuer")]
        public string SourceAssetIssuer { get; private set; }

        [JsonProperty(PropertyName = "path", ItemConverterType = typeof(AssetDeserializer))]
        public List<Asset> Path { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public PathResponseLinks Links { get; private set; }

        public AssetTypeCreditAlphaNum DestinationAsset => Asset.CreateNonNativeAsset(DestinationAssetCode, DestinationAssetIssuer);
        public AssetTypeCreditAlphaNum SourceAsset => Asset.CreateNonNativeAsset(SourceAssetCode, SourceAssetIssuer);

        public PathResponse()
        {
            // Used by deserializer
        }

        public PathResponse(string destinationAmount, string destinationAssetType, string destinationAssetCode, string destinationAssetIssuer, string sourceAmount,
            string sourceAssetType, string sourceAssetCode, string sourceAssetIssuer, List<Asset> path, PathResponseLinks links)
        {
            DestinationAmount = destinationAmount;
            DestinationAssetType = destinationAssetType;
            DestinationAssetCode = destinationAssetCode;
            DestinationAssetIssuer = destinationAssetIssuer;
            SourceAmount = sourceAmount;
            SourceAssetType = sourceAssetType;
            SourceAssetCode = sourceAssetCode;
            SourceAssetIssuer = sourceAssetIssuer;
            Path = path;
            Links = links;
        }
    }
}