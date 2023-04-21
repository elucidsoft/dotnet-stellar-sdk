using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Implements SEP-0024 - interaction with anchors.
    /// https://github.com/stellar/stellar-protocol/blob/master/ecosystem/sep-0024.md
    /// </summary>
    public class TransferServerServiceSep24
    {
        private HttpClient HttpClient { get; set; }
        private string TransferServiceSep24Domain { get; set; }

        /// <summary>
        /// Initializes a new instance of the TransferServerServiceSep24 class.
        /// </summary>
        /// <param name="transferServiceSep24Domain">The domain of the anchor server.</param>
        private TransferServerServiceSep24(string transferServiceSep24Domain)
        {
            HttpClient = new HttpClient();
            TransferServiceSep24Domain = transferServiceSep24Domain;
        }

        public static async Task<TransferServerServiceSep24> FromDomain(string domain)
        {
            var stellarToml = await StellarToml.FromDomain(domain).ConfigureAwait(false);
            var transferServerSep24 = stellarToml.GeneralInformation.TransferServerSep24;
            if (transferServerSep24 == null)
            {
                throw new Exception($"{transferServerSep24} not found in stellar toml of {domain}");
            }

            return new TransferServerServiceSep24(transferServerSep24);
        }

        /// <summary>
        /// Sends a request to the anchor server's /info endpoint and retrieves information about its deposit, withdrawal, and fee options.
        /// </summary>
        /// <param name="language">The optional language code for the returned information.</param>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <returns>An InfoResponse object containing information about the anchor server's deposit, withdrawal, and fee options.</returns>
        public async Task<InfoResponse> Info(string? language, string? jwt)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/info");
            var requestBuilder = new InfoRequestBuilder(serverUri, HttpClient);

            var queryParams = new Dictionary<string, string>();
            if (language != null)
            {
                queryParams.Add("lang", language);
            }

            var response = await requestBuilder.Execute(jwt, queryParams).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Gets the fee details for a particular asset, operation, and amount using SEP24.
        /// </summary>
        /// <param name="feeRequest">The request containing asset, operation, amount, and optionally, type.</param>
        /// <returns>The response containing the calculated fee.</returns>
        public async Task<FeeResponse> Fee(FeeRequest feeRequest)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/fee");
            var requestBuilder = new FeeRequestBuilder(serverUri, HttpClient);

            var queryParams = new Dictionary<string, string>
            {
                { "asset_code", feeRequest.AssetCode },
                { "operation", feeRequest.Operation },
                { "amount", feeRequest.Amount.ToString(CultureInfo.InvariantCulture) },
            };

            if (feeRequest.Type != null)
            {
                queryParams.Add("type", feeRequest.Type);
            }

            var response = await requestBuilder.Execute(feeRequest.Jwt, queryParams).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Initiates an interactive deposit using Stellar SEP24.
        /// </summary>
        /// <param name="depositRequest">The deposit request containing the necessary information for the deposit.</param>
        /// <returns>The deposit response containing the interactive URL to continue the deposit process.</returns>
        public async Task<DepositResponse> DepositInteractive(DepositRequest depositRequest)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/transactions/deposit/interactive");
            var requestBuilder = new DepositRequestBuilder(serverUri, HttpClient);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(depositRequest.AssetCode), "asset_code" },
                { new StringContent(depositRequest.Account), "account" },
            };

            if (depositRequest.AssetIssuer != null)
            {
                formData.Add(new StringContent(depositRequest.AssetIssuer), "asset_issuer");
            }

            if (depositRequest.Amount != null)
            {
                formData.Add(new StringContent(depositRequest.Amount), "amount");
            }

            if (depositRequest.MemoType != null)
            {
                formData.Add(new StringContent(depositRequest.MemoType), "memo_type");
            }

            if (depositRequest.Memo != null)
            {
                formData.Add(new StringContent(depositRequest.Memo), "memo");
            }

            if (depositRequest.WalletName != null)
            {
                formData.Add(new StringContent(depositRequest.WalletName), "wallet_name");
            }

            if (depositRequest.WalletUrl != null)
            {
                formData.Add(new StringContent(depositRequest.WalletUrl), "wallet_url");
            }

            if (depositRequest.Lang != null)
            {
                formData.Add(new StringContent(depositRequest.Lang), "lang");
            }

            if (depositRequest.ClaimableBalanceSupported != null)
            {
                formData.Add(new StringContent(depositRequest.ClaimableBalanceSupported),
                    "claimable_balance_supported");
            }

            try
            {
                var response = await requestBuilder.ExecutePost(depositRequest.Jwt, formData).ConfigureAwait(false);
                return response;
            }
            catch (HttpResponseException e)
            {
                if (e.StatusCode == 403)
                {
                    HandleForbiddenResponse(e);
                }

                throw;
            }
        }

        /// <summary>
        /// Initiates an interactive withdrawal using Stellar SEP24.
        /// </summary>
        /// <param name="withdrawRequest">The withdraw request containing the necessary information for the withdrawal.</param>
        /// <returns>The withdraw response containing the interactive URL to continue the withdrawal process.</returns>
        public async Task<WithdrawResponse> WithdrawInteractive(WithdrawRequest withdrawRequest)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/transactions/withdraw/interactive");
            var requestBuilder = new WithdrawRequestBuilder(serverUri, HttpClient);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(withdrawRequest.AssetCode), "asset_code" },
                { new StringContent(withdrawRequest.Account), "account" },
            };

            if (withdrawRequest.AssetIssuer != null)
            {
                formData.Add(new StringContent(withdrawRequest.AssetIssuer), "asset_issuer");
            }

            if (withdrawRequest.Amount != null)
            {
                formData.Add(new StringContent(withdrawRequest.Amount), "amount");
            }

            if (withdrawRequest.MemoType != null)
            {
                formData.Add(new StringContent(withdrawRequest.MemoType), "memo_type");
            }

            if (withdrawRequest.Memo != null)
            {
                formData.Add(new StringContent(withdrawRequest.Memo), "memo");
            }

            if (withdrawRequest.WalletName != null)
            {
                formData.Add(new StringContent(withdrawRequest.WalletName), "wallet_name");
            }

            if (withdrawRequest.WalletUrl != null)
            {
                formData.Add(new StringContent(withdrawRequest.WalletUrl), "wallet_url");
            }

            if (withdrawRequest.Lang != null)
            {
                formData.Add(new StringContent(withdrawRequest.Lang), "lang");
            }

            if (withdrawRequest.RefundMemo != null)
            {
                formData.Add(new StringContent(withdrawRequest.RefundMemo), "refund_memo");
            }

            if (withdrawRequest.RefundMemoType != null)
            {
                formData.Add(new StringContent(withdrawRequest.RefundMemoType), "refund_memo_type");
            }

            try
            {
                var response = await requestBuilder.ExecutePost(withdrawRequest.Jwt, formData).ConfigureAwait(false);
                return response;
            }
            catch (HttpResponseException e)
            {
                if (e.StatusCode == 403)
                {
                    HandleForbiddenResponse(e);
                }

                throw;
            }
        }

        /// <summary>
        /// Retrieves the details of a specific transaction using Stellar SEP24.
        /// </summary>
        /// <param name="transactionRequest">The request containing the transaction identifiers.</param>
        /// <returns>The response containing the transaction details.</returns>
        public async Task<AnchorTransactionResponse> Transaction(AnchorTransactionRequest transactionRequest)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/transaction");
            var requestBuilder = new AnchorTransactionRequestBuilder(serverUri, HttpClient);

            var queryParams = new Dictionary<string, string>
            {
                { "id", transactionRequest.Id },
            };


            if (transactionRequest.StellarTransactionId != null)
            {
                queryParams.Add("stellar_transaction_id", transactionRequest.StellarTransactionId);
            }

            if (transactionRequest.ExternalTransactionId != null)
            {
                queryParams.Add("external_transaction_id", transactionRequest.ExternalTransactionId);
            }

            if (transactionRequest.Lang != null)
            {
                queryParams.Add("lang", transactionRequest.Lang);
            }

            var response = await requestBuilder.Execute(transactionRequest.Jwt, queryParams).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Retrieves the list of transactions for the given asset and filters using Stellar SEP24.
        /// </summary>
        /// <param name="transactionsRequest">The request containing the asset code and optional filter parameters.</param>
        /// <returns>The response containing the list of transactions.</returns>
        public async Task<AnchorTransactionsResponse> Transactions(AnchorTransactionsRequest transactionsRequest)
        {
            var serverUri = new Uri(TransferServiceSep24Domain + "/transactions");
            var requestBuilder = new AnchorTransactionsRequestBuilder(serverUri, HttpClient);

            var queryParams = new Dictionary<string, string>
            {
                { "asset_code", transactionsRequest.AssetCode },
            };


            if (transactionsRequest.NoOlderThan != null)
            {
                queryParams.Add("no_older_than", transactionsRequest.NoOlderThan);
            }

            if (transactionsRequest.Limit != null)
            {
                queryParams.Add("limit", transactionsRequest.Limit.ToString()!);
            }

            if (transactionsRequest.Kind != null)
            {
                queryParams.Add("kind", transactionsRequest.Kind);
            }

            if (transactionsRequest.PagingId != null)
            {
                queryParams.Add("paging_id", transactionsRequest.PagingId);
            }

            if (transactionsRequest.Lang != null)
            {
                queryParams.Add("lang", transactionsRequest.Lang);
            }

            var response = await requestBuilder.Execute(transactionsRequest.Jwt, queryParams).ConfigureAwait(false);
            return response;
        }

        private static void HandleForbiddenResponse(HttpResponseException exception)
        {
            var res = JsonConvert.DeserializeObject<Dictionary<string, object>>(exception.Body);
            if (res != null && res.ContainsKey("type"))
            {
                var type = res["type"].ToString()!;
                switch (type)
                {
                    case "non_interactive_customer_info_needed":
                        throw new CustomerInformationNeededException(CustomerInformationNeededResponse.FromJson(res));
                    case "customer_info_status":
                        throw new CustomerInformationStatusException(CustomerInformationStatusResponse.FromJson(res));
                    case "authentication_required":
                        throw new InvalidWebAuthenticationException("The endpoint requires authentication.");
                }
            }
        }
    }

    public class CustomerInformationNeededResponse : Response
    {
        public List<string> Fields { get; set; }

        public CustomerInformationNeededResponse(List<string> fields)
        {
            Fields = fields;
        }

        public static CustomerInformationNeededResponse FromJson(Dictionary<string, object> json)
        {
            var fields = json.ContainsKey("fields") ? ((JArray)json["fields"]).ToObject<List<string>>() : null;
            return new CustomerInformationNeededResponse(fields ?? new List<string>());
        }
    }

    public class CustomerInformationNeededException : Exception
    {
        private CustomerInformationNeededResponse _response;

        public CustomerInformationNeededException(CustomerInformationNeededResponse response) : base(
            "The anchor needs more information about the customer and all the information can be received interactively.")
        {
            _response = response;
        }

        public override string ToString()
        {
            var fields = _response.Fields;
            return
                $"The anchor needs more information about the customer and all the information can be received interactivelyx. Fields: {string.Join(", ", fields)}";
        }

        public CustomerInformationNeededResponse Response => _response;
    }

    public class CustomerInformationStatusResponse : Response
    {
        public string Status { get; set; }
        public string MoreInfoUrl { get; set; }
        public int? Eta { get; set; }

        public CustomerInformationStatusResponse(string status, string moreInfoUrl, int? eta)
        {
            Status = status;
            MoreInfoUrl = moreInfoUrl;
            Eta = eta;
        }

        public static CustomerInformationStatusResponse FromJson(Dictionary<string, object> json)
        {
            var status = json["status"].ToString();
            var moreInfoUrl = json.ContainsKey("more_info_url") ? json["more_info_url"].ToString() : null;
            var eta = json.ContainsKey("eta") ? Convert.ToInt32(json["eta"]) : (int?)null;
            return new CustomerInformationStatusResponse(status, moreInfoUrl, eta);
        }
    }

    public class CustomerInformationStatusException : Exception
    {
        private CustomerInformationStatusResponse _response;

        public CustomerInformationStatusException(CustomerInformationStatusResponse response) : base(
            "Customer information was submitted for the account, but the information is either still being processed or was not accepted.")
        {
            _response = response;
        }

        public override string ToString()
        {
            var status = _response.Status;
            var moreInfoUrl = _response.MoreInfoUrl;
            var eta = _response.Eta;
            return
                $"Customer information was submitted for the account, but the information is either still being processed or was not accepted. Status: {status} - More info url: {moreInfoUrl} - Eta: {eta}";
        }

        public CustomerInformationStatusResponse Response => _response;
    }


    /// <summary>
    /// Represents a transfer service sep24 deposit request.
    /// </summary>
    public class DepositRequest
    {
        /// <summary>
        /// The code of the asset the user is wanting to deposit with the anchor. Ex BTC,ETH,USD,INR,etc.
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// The stellar account ID of the user that wants to deposit. This is where the asset token will be sent.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// JWT previously received from the anchor via the SEP-10 authentication flow.
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// (optional) The issuer of the stellar asset the user wants to receive for their deposit with the anchor.
        /// If asset_issuer is not provided, the anchor should use the asset issued by themselves as described in their TOML file.
        /// </summary>
        public string? AssetIssuer { get; set; }

        /// <summary>
        /// (optional) type of memo that anchor should attach to the Stellar payment transaction, one of text, id or hash
        /// </summary>
        public string? MemoType { get; set; }

        /// <summary>
        /// (optional) value of memo to attach to transaction, for hash this should be base64-encoded
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// (optional) In communications / pages about the deposit, anchor should display the wallet name to the user to explain where funds are going.
        /// </summary>
        public string? WalletName { get; set; }

        /// <summary>
        /// (optional) Anchor should link to this when notifying the user that the transaction has completed.
        /// </summary>
        public string? WalletUrl { get; set; }

        /// <summary>
        /// (optional) Defaults to en. Language code specified using ISO 639-1. error fields in the response should be in this language.
        /// </summary>
        public string? Lang { get; set; }

        /// <summary>
        /// (optional) The amount of the asset the user would like to deposit with the anchor.
        /// If this is not provided it will be collected in the interactive flow.
        /// </summary>
        public string? Amount { get; set; }

        /// <summary>
        /// (optional) true if the client supports receiving deposit transactions as a claimable balance, false otherwise.
        /// </summary>
        public string? ClaimableBalanceSupported { get; set; }

        public DepositRequest(string assetCode, string account, string jwt, string assetIssuer, string? memoType,
            string? memo, string? walletName, string? walletUrl, string? lang, string? amount,
            string? claimableBalanceSupported)
        {
            AssetCode = assetCode;
            Account = account;
            Jwt = jwt;
            AssetIssuer = assetIssuer;
            MemoType = memoType;
            Memo = memo;
            WalletName = walletName;
            WalletUrl = walletUrl;
            Lang = lang;
            Amount = amount;
            ClaimableBalanceSupported = claimableBalanceSupported;
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 deposit response.
    /// </summary>
    public class DepositResponse : Response
    {
        /// <summary>
        /// URL hosted by the anchor. The wallet should show this URL to the user as a popup.
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }

        /// <summary>
        /// (optional) The anchor's internal ID for this deposit request. The wallet will use this ID to query the /transaction endpoint to check status of the request.
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Always set to interactive_customer_info_needed.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        public DepositResponse(string? url, string? id, string? type = "interactive_customer_info_needed")
        {
            Url = url;
            Id = id;
            Type = type;
        }

        public static DepositResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<DepositResponse>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /transactions/deposit/interactive endpoint of the anchor server.
    /// </summary>
    public class DepositRequestBuilder : RequestBuilder<DepositRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the DepositRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /transactions/deposit/interactive endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public DepositRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /transactions/deposit/interactive request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="multipartContent">A MultipartFormDataContent to include in the request.</param>
        /// <returns>An DepositResponse object containing information about the deposit.</returns>
        public async Task<DepositResponse> ExecutePost(string? jwt, MultipartFormDataContent multipartContent)
        {
            return await ExecutePost<DepositResponse>(BuildUri(), jwt: jwt, multipartContent: multipartContent);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 withdraw request.
    /// </summary>
    public class WithdrawRequest
    {
        /// <summary>
        /// The code of the asset the user is wanting to withdraw with the anchor. Ex BTC,ETH,USD,INR,etc.
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// The Stellar account the client will use as the source of the withdrawal payment to the anchor.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// JWT previously received from the anchor via the SEP-10 authentication flow.
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// (optional) The issuer of the stellar asset the user wants to withdraw with the anchor.
        /// If asset_issuer is not provided, the anchor should use the asset issued by themselves as described in their TOML file.
        /// </summary>
        public string? AssetIssuer { get; set; }

        /// <summary>
        /// (deprecated, optional) type of memo that anchor should attach to the Stellar payment transaction, one of text, id or hash.
        /// </summary>
        public string? MemoType { get; set; }

        /// <summary>
        /// (deprecated, optional) value of memo to attach to transaction, for hash this should be base64-encoded.
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// (optional)  In communications / pages about the withdrawal, anchor should display the wallet name to the user to explain where funds are coming from.
        /// </summary>
        public string? WalletName { get; set; }

        /// <summary>
        /// (optional) Anchor can show this to the user when referencing the wallet involved in the withdrawal (ex. in the anchor's transaction history).
        /// </summary>
        public string? WalletUrl { get; set; }

        /// <summary>
        /// (optional) Defaults to en. Language code specified using ISO 639-1. error fields in the response should be in this language.
        /// </summary>
        public string? Lang { get; set; }

        /// <summary>
        /// (optional) Amount of asset requested to withdraw. If this is not provided it will be collected in the interactive flow.
        /// </summary>
        public string? Amount { get; set; }

        /// <summary>
        /// (optional) The memo the anchor must use when sending refund payments back to the user.
        /// If not specified, the anchor should use the same memo used by the user to send the original payment.
        /// If specified, refund_memo_type must also be specified.
        /// </summary>
        public string? RefundMemo { get; set; }

        /// <summary>
        /// (optional) The type of the refund_memo. Can be id, text, or hash.
        /// See the memos documentation for more information. If specified, refund_memo must also be specified.
        /// </summary>
        public string? RefundMemoType { get; set; }

        public WithdrawRequest(string assetCode, string account, string jwt, string assetIssuer, string? memoType,
            string? memo, string? emailAddress, string? type, string? walletName, string? walletUrl, string? lang,
            string? amount, string? countryCode, string? refundMemo, string? refundMemoType)
        {
            AssetCode = assetCode;
            Account = account;
            Jwt = jwt;
            AssetIssuer = assetIssuer;
            MemoType = memoType;
            Memo = memo;
            WalletName = walletName;
            WalletUrl = walletUrl;
            Lang = lang;
            Amount = amount;
            RefundMemo = refundMemo;
            RefundMemoType = refundMemoType;
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 withdraw response.
    /// </summary>
    public class WithdrawResponse : Response
    {
        /// <summary>
        /// URL hosted by the anchor. The wallet should show this URL to the user as a popup.
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }

        /// <summary>
        /// (optional) The anchor's internal ID for this withdraw request. The wallet will use this ID to query the /transaction endpoint to check status of the request.
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Always set to interactive_customer_info_needed.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        public WithdrawResponse(string? url, string? id, string? type = "interactive_customer_info_needed")
        {
            Url = url;
            Id = id;
            Type = type;
        }

        public static WithdrawResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<WithdrawResponse>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /transactions/withdraw/interactive endpoint of the anchor server.
    /// </summary>
    public class WithdrawRequestBuilder : RequestBuilder<WithdrawRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the WithdrawRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /transactions/withdraw/interactive endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public WithdrawRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /transactions/withdraw/interactive request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="multipartContent">A MultipartFormDataContent to include in the request.</param>
        /// <returns>An WithdrawResponse object containing information about the withdraw.</returns>
        public async Task<WithdrawResponse> ExecutePost(string? jwt, MultipartFormDataContent multipartContent)
        {
            return await ExecutePost<WithdrawResponse>(BuildUri(), jwt: jwt, multipartContent: multipartContent);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 fee request.
    /// </summary>
    public class FeeRequest
    {
        /// <summary>
        /// The code of the asset. Ex BTC,ETH,USD,INR,etc.
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Kind of operation (deposit or withdraw).
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// JWT previously received from the anchor via the SEP-10 authentication flow.
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// (optional) Type of deposit or withdrawal (SEPA, bank_account, cash, etc...).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Amount of the asset that will be deposited/withdrawn.
        /// </summary>
        public double Amount { get; set; }

        public FeeRequest(string assetCode, string operation, string jwt, string? type, double amount)
        {
            AssetCode = assetCode;
            Operation = operation;
            Jwt = jwt;
            Type = type;
            Amount = amount;
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 fee response.
    /// </summary>
    public class FeeResponse : Response
    {
        /// <summary>
        /// The total fee (in units of the asset involved) that would be charged to deposit/withdraw the specified amount of asset_code.
        /// </summary>
        [JsonProperty("fee")]
        public double? Fee { get; set; }

        public FeeResponse(double? fee)
        {
            Fee = fee;
        }

        public static FeeResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<FeeResponse>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /fee endpoint of the anchor server.
    /// </summary>
    public class FeeRequestBuilder : RequestBuilder<FeeRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the FeeRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /fee endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public FeeRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /fee request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="queryParams">A dictionary of query parameters to include in the request.</param>
        /// <returns>An FeeResponse object containing information about the anchor server's fee.</returns>
        public async Task<FeeResponse> Execute(string? jwt, Dictionary<string, string> queryParams)
        {
            SetQueryParameters(queryParams);
            return await Execute<FeeResponse>(BuildUri(), jwt: jwt);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 anchor refund payment response object.
    /// </summary>
    public class AnchorRefundPayment : Response
    {
        /// <summary>
        /// The payment ID that can be used to identify the refund payment.
        /// This is either a Stellar transaction hash or an off-chain payment identifier,
        /// such as a reference number provided to the user when the refund was initiated.
        /// This id is not guaranteed to be unique.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Stellar or external.
        /// </summary>
        [JsonProperty("id_type")]
        public string IdType { get; set; }

        /// <summary>
        /// The amount sent back to the user for the payment identified by id, in units of amount_in_asset.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The amount charged as a fee for processing the refund, in units of amount_in_asset.
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        public AnchorRefundPayment(string id, string idType, string amount, string fee)
        {
            Id = id;
            IdType = idType;
            Amount = amount;
            Fee = fee;
        }

        public static AnchorRefundPayment? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorRefundPayment>(json);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 anchor refund response object.
    /// </summary>
    public class AnchorRefund : Response
    {
        /// <summary>
        /// The total amount refunded to the user, in units of amount_in_asset.
        /// If a full refund was issued, this amount should match amount_in.
        /// </summary>
        [JsonProperty("amount_refunded")]
        public string AmountRefunded { get; set; }

        /// <summary>
        /// The total amount charged in fees for processing all refund payments, in units of amount_in_asset.
        /// The sum of all fee values in the payments object list should equal this value.
        /// </summary>
        [JsonProperty("amount_fee")]
        public string AmountFee { get; set; }

        /// <summary>
        /// A list of objects containing information on the individual payments made back to the user as refunds.
        /// The schema for these objects is defined in the section below.
        /// </summary>
        [JsonProperty("payments")]
        public List<AnchorRefundPayment> Payments { get; set; }


        public AnchorRefund(string amountRefunded, string amountFee, List<AnchorRefundPayment> payments)
        {
            AmountRefunded = amountRefunded;
            AmountFee = amountFee;
            Payments = payments;
        }

        public static AnchorRefund? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorRefund>(json);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 anchor transaction response object.
    /// </summary>
    public class AnchorTransaction : Response
    {
        /// <summary>
        /// Unique, anchor-generated id for the deposit/withdrawal.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Deposit or withdrawal.
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Processing status of deposit/withdrawal.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// (optional) Estimated number of seconds until a status change is expected.
        /// </summary>
        [JsonProperty("status_eta")]
        public int? StatusEta { get; set; }

        /// <summary>
        /// (optional) True if the anchor has verified the user's KYC information for this transaction.
        /// </summary>
        [JsonProperty("kyc_verified")]
        public bool? KycVerified { get; set; }

        /// <summary>
        /// A URL that is opened by wallets after the interactive flow is complete.
        /// It can include banking information for users to start deposits, the status of the transaction,
        /// or any other information the user might need to know about the transaction.
        /// </summary>
        [JsonProperty("more_info_url")]
        public string MoreInfoUrl { get; set; }

        /// <summary>
        /// Amount received by anchor at start of transaction as a string with up to 7 decimals.
        /// Excludes any fees charged before the anchor received the funds.
        /// </summary>
        [JsonProperty("amount_in")]
        public string AmountIn { get; set; }

        /// <summary>
        /// (optional) The asset received or to be received by the Anchor.
        /// Must be present if the deposit/withdraw was made using non-equivalent assets.
        /// The value must be in SEP-38 Asset Identification Format.
        /// See the Asset Exchanges section for more information.
        /// </summary>
        [JsonProperty("amount_in_asset")]
        public string? AmountInAsset { get; set; }

        /// <summary>
        /// Amount sent by anchor to user at end of transaction as a string with up to 7 decimals.
        /// Excludes amount converted to XLM to fund account and any external fees.
        /// </summary>
        [JsonProperty("amount_out")]
        public string AmountOut { get; set; }

        /// <summary>
        /// (optional) The asset delivered or to be delivered to the user.
        /// Must be present if the deposit/withdraw was made using non-equivalent assets.
        /// The value must be in SEP-38 Asset Identification Format.
        /// See the Asset Exchanges section for more information.
        /// </summary>
        [JsonProperty("amount_out_asset")]
        public string? AmountOutAsset { get; set; }

        /// <summary>
        /// Amount of fee charged by anchor.
        /// </summary>
        [JsonProperty("amount_fee")]
        public string AmountFee { get; set; }

        /// <summary>
        /// (optional) The asset in which fees are calculated in.
        /// Must be present if the deposit/withdraw was made using non-equivalent assets.
        /// The value must be in SEP-38 Asset Identification Format.
        /// See the Asset Exchanges section for more information.
        /// </summary>
        [JsonProperty("amount_fee_asset")]
        public string? AmountFeeAsset { get; set; }

        /// <summary>
        /// Start date and time of transaction.
        /// </summary>
        [JsonProperty("started_at")]
        public string StartedAt { get; set; }

        /// <summary>
        /// (optional) The date and time of transaction reaching completed or refunded status.
        /// </summary>
        [JsonProperty("completed_at")]
        public string? CompletedAt { get; set; }

        /// <summary>
        /// (optional) The date and time of transaction reaching the current status.
        /// </summary>
        [JsonProperty("updated_at")]
        public string? UpdatedAt { get; set; }

        /// <summary>
        /// transaction_id on Stellar network of the transfer that either completed the deposit or started the withdrawal.
        /// </summary>
        [JsonProperty("stellar_transaction_id")]
        public string StellarTransactionId { get; set; }

        /// <summary>
        /// (optional) ID of transaction on external network that either started the deposit or completed the withdrawal.
        /// </summary>
        [JsonProperty("external_transaction_id")]
        public string? ExternalTransactionId { get; set; }

        /// <summary>
        /// (optional) Human readable explanation of transaction status, if needed.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// (deprecated, optional) This field is deprecated in favor of the refunds object and the refunded status.
        /// True if the transaction was refunded in full. False if the transaction was partially refunded or
        /// not refunded. For more details about any refunds, see the refunds object.
        /// </summary>
        [JsonProperty("refunded")]
        public bool? Refunded { get; set; }

        /// <summary>
        /// (optional) An object describing any on or off-chain refund associated with this transaction.
        /// </summary>
        [JsonProperty("refunds")]
        public AnchorRefund? AnchorRefund { get; set; }

        /// <summary>
        /// Sent from address, perhaps BTC, IBAN, or bank account in case of a deposit.
        /// Stellar address the assets were withdrawn from in case of a withdrawal.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Stellar address the deposited assets were sent to in case of a deposit.
        /// Sent to address, perhaps BTC, IBAN, or bank account in the case of a withdrawal.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// (optional) This is the memo (if any) used to transfer the asset to the to Stellar address.
        /// </summary>
        [JsonProperty("deposit_memo")]
        public string? DepositMemo { get; set; }

        /// <summary>
        /// (optional) Type for the deposit_memo.
        /// </summary>
        [JsonProperty("deposit_memo_type")]
        public string? DepositMemoType { get; set; }

        /// <summary>
        /// (optional) ID of the Claimable Balance used to send the asset initially requested.
        /// </summary>
        [JsonProperty("claimable_balance_id")]
        public string? ClaimableBalanceId { get; set; }

        /// <summary>
        /// (optional) If this is a withdrawal, this is the anchor's Stellar account that the user transferred
        /// (or will transfer) their issued asset to.
        /// </summary>
        [JsonProperty("withdraw_anchor_account")]
        public string? WithdrawAnchorAccount { get; set; }

        /// <summary>
        /// (optional) Memo used when the user transferred to withdraw_anchor_account.
        /// Assigned null if the withdraw is not ready to receive payment, for example if KYC is not completed.
        /// </summary>
        [JsonProperty("withdraw_memo")]
        public string? WithdrawMemo { get; set; }

        /// <summary>
        /// (optional) Memo type for withdraw_memo.
        /// </summary>
        [JsonProperty("withdraw_memo_type")]
        public string? WithdrawMemoType { get; set; }

        public AnchorTransaction(string id, string kind, string status, int? statusEta, bool? kycVerified,
            string moreInfoUrl, string amountIn, string? amountInAsset, string amountOut, string? amountOutAsset,
            string amountFee, string? amountFeeAsset, string startedAt, string? completedAt, string? updatedAt,
            string stellarTransactionId, string? externalTransactionId, string? message, bool? refunded,
            AnchorRefund? anchorRefund, string from, string to, string? depositMemo, string? depositMemoType,
            string? claimableBalanceId, string? withdrawAnchorAccount, string? withdrawMemo, string? withdrawMemoType)
        {
            Id = id;
            Kind = kind;
            Status = status;
            StatusEta = statusEta;
            KycVerified = kycVerified;
            MoreInfoUrl = moreInfoUrl;
            AmountIn = amountIn;
            AmountInAsset = amountInAsset;
            AmountOut = amountOut;
            AmountOutAsset = amountOutAsset;
            AmountFee = amountFee;
            AmountFeeAsset = amountFeeAsset;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            UpdatedAt = updatedAt;
            StellarTransactionId = stellarTransactionId;
            ExternalTransactionId = externalTransactionId;
            Message = message;
            Refunded = refunded;
            AnchorRefund = anchorRefund;
            From = from;
            To = to;
            DepositMemo = depositMemo;
            DepositMemoType = depositMemoType;
            ClaimableBalanceId = claimableBalanceId;
            WithdrawAnchorAccount = withdrawAnchorAccount;
            WithdrawMemo = withdrawMemo;
            WithdrawMemoType = withdrawMemoType;
        }

        public static AnchorTransaction? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorTransaction>(json);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 transaction request.
    /// </summary>
    public class AnchorTransactionRequest
    {
        /// <summary>
        /// JWT previously received from the anchor via the SEP-10 authentication flow.
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// (optional) The id of the transaction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// (optional) The stellar transaction id of the transaction.
        /// </summary>
        public string? StellarTransactionId { get; set; }

        /// <summary>
        /// (optional) The external transaction id of the transaction.
        /// </summary>
        public string? ExternalTransactionId { get; set; }

        /// <summary>
        /// (optional) Defaults to en if not specified or if the specified language is not supported.
        /// </summary>
        public string? Lang { get; set; }

        public AnchorTransactionRequest(string jwt, string id, string? stellarTransactionId = null,
            string? externalTransactionId = null, string? lang = "en")
        {
            Jwt = jwt;
            Id = id;
            StellarTransactionId = stellarTransactionId;
            ExternalTransactionId = externalTransactionId;
            Lang = lang;
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 single transaction response.
    /// </summary>
    public class AnchorTransactionResponse : Response
    {
        /// <summary>
        /// Represents a single transaction.
        /// </summary>
        [JsonProperty("transaction")]
        public AnchorTransaction? AnchorTransaction { get; set; }

        public AnchorTransactionResponse(AnchorTransaction? anchorTransaction)
        {
            AnchorTransaction = anchorTransaction;
        }

        public static AnchorTransactionResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorTransactionResponse>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /transaction endpoint of the anchor server.
    /// </summary>
    public class AnchorTransactionRequestBuilder : RequestBuilder<AnchorTransactionRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the AnchorTransactionRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /transaction endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public AnchorTransactionRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /transaction request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="queryParams">A dictionary of query parameters to include in the request.</param>
        /// <returns>An AnchorTransactionResponse object containing information about the single transaction.</returns>
        public async Task<AnchorTransactionResponse> Execute(string? jwt, Dictionary<string, string> queryParams)
        {
            SetQueryParameters(queryParams);
            return await Execute<AnchorTransactionResponse>(BuildUri(), jwt: jwt);
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 transactions request.
    /// </summary>
    public class AnchorTransactionsRequest
    {
        /// <summary>
        /// The code of the asset of interest. E.g. BTC, ETH, USD, INR, etc.
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// JWT previously received from the anchor via the SEP-10 authentication flow.
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// (optional) The response should contain transactions starting on or after this date & time.
        /// </summary>
        public string? NoOlderThan { get; set; }

        /// <summary>
        /// (optional) The response should contain at most limit transactions.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// (optional) The kind of transaction that is desired. Should be either deposit or withdrawal.
        /// </summary>
        public string? Kind { get; set; }

        /// <summary>
        /// (optional) The response should contain transactions starting prior to this ID (exclusive).
        /// </summary>
        public string? PagingId { get; set; }

        /// <summary>
        /// (optional) Defaults to en if not specified or if the specified language is not supported.
        /// </summary>
        public string? Lang { get; set; }

        public AnchorTransactionsRequest(string assetCode, string jwt, string? noOlderThan = null, int? limit = null,
            string? kind = null, string? pagingId = null, string? lang = "en")
        {
            AssetCode = assetCode;
            Jwt = jwt;
            NoOlderThan = noOlderThan;
            Limit = limit;
            Kind = kind;
            PagingId = pagingId;
            Lang = lang;
        }
    }

    /// <summary>
    /// Represents a transfer service sep24 transactions response.
    /// </summary>
    public class AnchorTransactionsResponse : Response
    {
        /// <summary>
        /// Represents transactions.
        /// </summary>
        [JsonProperty("transactions")]
        public List<AnchorTransaction?>? AnchorTransactions { get; set; }

        public AnchorTransactionsResponse(List<AnchorTransaction?>? anchorTransactions)
        {
            AnchorTransactions = anchorTransactions;
        }

        public static AnchorTransactionsResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorTransactionsResponse>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /transactions endpoint of the anchor server.
    /// </summary>
    public class AnchorTransactionsRequestBuilder : RequestBuilder<AnchorTransactionsRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the AnchorTransactionsRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /transactions endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public AnchorTransactionsRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /transactions request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="queryParams">A dictionary of query parameters to include in the request.</param>
        /// <returns>An AnchorTransactionsResponse object containing information about the transactions history.</returns>
        public async Task<AnchorTransactionsResponse> Execute(string? jwt, Dictionary<string, string> queryParams)
        {
            SetQueryParameters(queryParams);
             return await Execute<AnchorTransactionsResponse>(BuildUri(), jwt: jwt);
        }
    }

    /// <summary>
    /// Represents the response from the anchor server's /info endpoint.
    /// </summary>
    public class InfoResponse : Response
    {
        [JsonProperty("deposit")] public Dictionary<string, DepositAsset> DepositAssets { get; set; }

        [JsonProperty("withdraw")] public Dictionary<string, WithdrawAsset> WithdrawAssets { get; set; }

        [JsonProperty("fee")] public AnchorFeeInfo FeeInfo { get; set; }

        [JsonProperty("features")] public AnchorFeatureInfo AnchorFeatureInfo { get; set; }

        public InfoResponse(Dictionary<string, DepositAsset> depositAssets,
            Dictionary<string, WithdrawAsset> withdrawAssets,
            AnchorFeeInfo feeInfo,
            AnchorFeatureInfo anchorFeatureInfo)
        {
            DepositAssets = depositAssets;
            WithdrawAssets = withdrawAssets;
            FeeInfo = feeInfo;
            AnchorFeatureInfo = anchorFeatureInfo;
        }

        public static InfoResponse? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InfoResponse>(json);
        }
    }

    /// <summary>
    /// Represents a deposit asset and its associated information.
    /// </summary>
    public class DepositAsset : Response
    {
        /// <summary>
        /// Indicates whether deposit for this asset is supported or not.
        /// </summary>
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Fixed (base) fee for deposit. In units of the deposited asset.
        /// </summary>
        [JsonProperty("fee_fixed")]
        public double? FeeFixed { get; set; }

        /// <summary>
        /// Percentage fee for deposit. In percentage points.
        /// </summary>
        [JsonProperty("fee_percent")]
        public double? FeePercent { get; set; }

        /// <summary>
        /// Optional minimum amount. No limit if not specified.
        /// </summary>

        [JsonProperty("min_amount")]
        public double? MinAmount { get; set; }

        /// <summary>
        /// Optional maximum amount. No limit if not specified.
        /// </summary>

        [JsonProperty("max_amount")]
        public double? MaxAmount { get; set; }

        public DepositAsset(bool? enabled, double? feeFixed, double? feePercent, double? minAmount, double? maxAmount)
        {
            Enabled = enabled;
            FeeFixed = feeFixed;
            FeePercent = feePercent;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }

        public static DepositAsset? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<DepositAsset>(json);
        }
    }

    /// <summary>
    /// Represents a withdraw asset and its associated information.
    /// </summary>
    public class WithdrawAsset : Response
    {
        /// <summary>
        /// Indicates whether withdraw for this asset is supported or not.
        /// </summary>
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Fixed (base) fee for withdraw. In units of the deposited asset.
        /// </summary>
        [JsonProperty("fee_fixed")]
        public double? FeeFixed { get; set; }

        /// <summary>
        /// Percentage fee for withdraw. In percentage points.
        /// </summary>
        [JsonProperty("fee_percent")]
        public double? FeePercent { get; set; }

        /// <summary>
        /// Optional minimum amount. No limit if not specified.
        /// </summary>

        [JsonProperty("min_amount")]
        public double? MinAmount { get; set; }

        /// <summary>
        /// Optional maximum amount. No limit if not specified.
        /// </summary>

        [JsonProperty("max_amount")]
        public double? MaxAmount { get; set; }


        public WithdrawAsset(bool? enabled, bool? authenticationRequired, double? feeFixed, double? feePercent,
            double? minAmount, double? maxAmount)
        {
            Enabled = enabled;
            FeeFixed = feeFixed;
            FeePercent = feePercent;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }

        public static WithdrawAsset? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<WithdrawAsset>(json);
        }
    }

    /// <summary>
    /// Represents information about anchor's fee endpoint.
    /// </summary>
    public class AnchorFeeInfo : Response
    {
        /// <summary>
        /// Indicates whether the endpoint is available or not.
        /// </summary>
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Indicates whether client must be authenticated before accessing the fee endpoint or not.
        /// </summary>

        [JsonProperty("authentication_required")]
        public bool? AuthenticationRequired { get; set; }

        public AnchorFeeInfo(bool? enabled, bool? authenticationRequired)
        {
            Enabled = enabled;
            AuthenticationRequired = authenticationRequired;
        }

        public static AnchorFeeInfo? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorFeeInfo>(json);
        }
    }

    /// <summary>
    /// The AnchorFeatureInfo contains boolean values indicating whether or not specific features are supported by the anchor.
    /// If the object or specific feature is not present in the response, the default value described below may be assumed.
    /// This information enables wallets to adjust their behavior based on the feature set supported by the anchor.
    /// </summary>
    public class AnchorFeatureInfo : Response
    {
        /// <summary>
        /// Whether or not the anchor supports creating accounts for users requesting deposits.
        /// </summary>
        [JsonProperty("account_creation")]
        public bool? AccountCreation { get; set; }

        /// <summary>
        /// Whether or not the anchor supports sending deposit funds as claimable balances.
        /// This is relevant for users of Stellar accounts without a trustline to the requested asset.
        /// </summary>
        [JsonProperty("claimable_balances")]
        public bool? ClaimableBalances { get; set; }

        public AnchorFeatureInfo(bool? accountCreation = true, bool? claimableBalances = false)
        {
            AccountCreation = accountCreation;
            ClaimableBalances = claimableBalances;
        }

        public static AnchorFeatureInfo? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AnchorFeatureInfo>(json);
        }
    }

    /// <summary>
    /// Represents a request builder for the /info endpoint of the anchor server.
    /// </summary>
    public class InfoRequestBuilder : RequestBuilder<InfoRequestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the InfoRequestBuilder class.
        /// </summary>
        /// <param name="uri">The URI of the anchor server's /info endpoint.</param>
        /// <param name="httpClient">An instance of HttpClient to send requests to the server.</param>
        public InfoRequestBuilder(Uri uri, HttpClient httpClient)
            : base(uri, null, httpClient)
        {
        }

        /// <summary>
        /// Builds and executes the /info request.
        /// </summary>
        /// <param name="jwt">The JSON Web Token (JWT) used for authentication with the anchor server.</param>
        /// <param name="queryParams">A dictionary of query parameters to include in the request.</param>
        /// <returns>An InfoResponse object containing information about the anchor server's deposit, withdrawal, and fee options.</returns>
        public async Task<InfoResponse> Execute(string? jwt, Dictionary<string, string> queryParams)
        {
            SetQueryParameters(queryParams);
            return await Execute<InfoResponse>(BuildUri(), jwt: jwt);
        }
    }
}