using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class BumpSequenceOperationResponse: OperationResponse
    {
        [JsonProperty(PropertyName = "bump_to")]
        public long BumpTo { get; }

        public BumpSequenceOperationResponse(long bumpTo)
        {
            this.BumpTo = bumpTo;
        }
    }
}
