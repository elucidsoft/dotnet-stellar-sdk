using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk.requests
{
    /// <summary>
    ///     Builds requests connected to accounts.
    /// </summary>
    public class AccountsRequestBuilder : RequestBuilderExecutePageable<AccountsRequestBuilder, AccountResponse>
    {
        /// <summary>
        ///     Builds requests connected to accounts.
        /// </summary>
        /// <param name="serverUri"></param>
        public AccountsRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "accounts", httpClient)
        {
        }

        /// <summary>
        ///     Requests specific uri and returns AccountResponse
        ///     This method is helpful for getting the links.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<AccountResponse> Account(Uri uri)
        {
            var responseHandler = new ResponseHandler<AccountResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        public async Task<AccountDataResponse> AccountData(Uri uri)
        {
            var responseHandler = new ResponseHandler<AccountDataResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        /// <summary>
        ///     Requests GET /accounts/{account}
        ///     https://www.stellar.org/developers/horizon/reference/accounts-single.html
        /// </summary>
        /// <param name="account">Account to fetch</param>
        /// <returns></returns>
        public async Task<AccountResponse> Account(string account)
        {
            SetSegments("accounts", account);
            return await Account(BuildUri());
        }

        /// <summary>
        ///     Requests GET /accounts/{account}/data/{key}
        ///     https://www.stellar.org/developers/horizon/reference/endpoints/data-for-account.html
        /// </summary>
        /// <param name="account">Account to fetch</param>
        /// <param name="key">Key to the data needing retrieval.</param>
        /// <returns></returns>
        public async Task<AccountDataResponse> AccountData(string accountId, string key)
        {
            SetSegments("accounts", accountId, "data", key);
            return await AccountData(BuildUri());
        }

        /// <summary>
        /// Filter accounts that have the given signer or have a trustline to the given asset.
        /// https://www.stellar.org/developers/horizon/reference/endpoints/accounts.html
        /// </summary>
        /// <param name="options">The filtering options</param>
        public AccountsRequestBuilder Accounts(AccountsRequestOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.Signer != null)
            {
                UriBuilder.SetQueryParam("signer", options.Signer);
            }

            if (options.Asset != null)
            {
                UriBuilder.SetQueryParam("asset", AssetToQueryParam(options.Asset));
            }

            return this;
        }

        /// <summary>
        /// Filter accounts that have the given signer or have a trustline to the given asset.
        /// https://www.stellar.org/developers/horizon/reference/endpoints/accounts.html
        /// </summary>
        /// <param name="optionsAction"></param>
        public AccountsRequestBuilder Accounts(Action<AccountsRequestOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));
            var options = new AccountsRequestOptions();
            optionsAction.Invoke(options);
            return Accounts(options);
        }

        /// <summary>
        /// Filter accounts that have the given signer.
        /// </summary>
        /// <param name="signer">The signer.</param>
        /// <returns></returns>
        public AccountsRequestBuilder WithSigner(string signer)
        {
            return Accounts(options => options.Signer = signer);
        }

        /// <summary>
        /// Filter accounts that have a trustline to the given asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public AccountsRequestBuilder WithTrustline(Asset asset)
        {
            return Accounts(options => options.Asset = asset);
        }

        private string AssetToQueryParam(Asset asset)
        {
            switch (asset)
            {
                case AssetTypeNative _:
                    return "native";
                case AssetTypeCreditAlphaNum credit:
                    return $"{credit.Code}:{credit.Issuer}";
                default:
                    throw new ArgumentException($"Unknown Asset type {asset.Type}");
            }
        }

        public class AccountsRequestOptions
        {
            /// <summary>
            /// Filter accounts that have the given Signer.
            /// </summary>
            public string Signer { get; set; }

            /// <summary>
            /// Filter accounts that trust the given Asset.
            /// </summary>
            public Asset Asset { get; set; }
        }
    }
}