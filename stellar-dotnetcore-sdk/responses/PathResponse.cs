using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    public class PathResponse : Response
    {
        [JsonProperty(PropertyName = "destination_amount")]
        public string DestinationAmount { get; }

        [JsonProperty(PropertyName = "destination_asset_type")]
        public string DestinationAssetType { get; }

        [JsonProperty(PropertyName = "destination_asset_code")]
        public string DestinationAssetCode { get; }

        [JsonProperty(PropertyName = "destination_asset_issuer")]
        public string DestinationAssetIssuer { get; }

        [JsonProperty(PropertyName = "source_amount")]
        public string SourceAmount { get; }

        [JsonProperty(PropertyName = "source_asset_type")]
        public string SourceAssetType { get; }

        [JsonProperty(PropertyName = "source_asset_code")]
        public string SourceAssetCode { get; }

        [JsonProperty(PropertyName = "source_asset_issuer")]
        public string SourceAssetIssuer { get; }

        [JsonProperty(PropertyName = "path")]
        public List<Asset> Path { get; }

        [JsonProperty(PropertyName = "_links")]
        public PathResponseLinks Links { get; }

        public Asset DestinationAsset => Asset.CreateNonNativeAsset(DestinationAssetType, DestinationAssetIssuer, DestinationAssetCode);
        public Asset SourceAsset => Asset.CreateNonNativeAsset(SourceAssetType, SourceAssetIssuer, SourceAssetCode);
        
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

    public class PathResponseLinks
    {
        [JsonProperty(PropertyName = "self")]
        public Link Self { get; }

        public PathResponseLinks(Link self)
        {
            Self = self;
        }
    }
}
