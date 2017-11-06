using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses.operations
{
    /// <summary>
    /// Represents ManageData operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="OperationRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class ManageDataOperationResponse : OperationResponse
    {
        public string Name { get;  }

        public string Value { get;  }

        public ManageDataOperationResponse(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
