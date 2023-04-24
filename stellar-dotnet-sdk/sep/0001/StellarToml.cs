using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nett;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Implement SEP-01: Stellar Toml.
    /// Parses the stellar toml data from a given anchor's domain.
    /// https://github.com/stellar/stellar-protocol/blob/master/ecosystem/sep-0001.md
    /// </summary>
    public class StellarToml
    {
        public GeneralInformation GeneralInformation { get; set; }
        public Documentation? Documentation { get; set; }
        public List<PointOfContact>? PointsOfContact { get; set; }
        public List<Currency>? Currencies { get; set; }
        public List<Validator>? Validators { get; set; }

        /// <summary>
        /// Initializes a new instance of the StellarToml class with the specified toml string.
        /// </summary>
        /// <param name="toml">The toml string to parse.</param>
        public StellarToml(string toml)
        {
            var document = Toml.ReadString(toml);


            GeneralInformation = new GeneralInformation
            {
                Version = document.TryGetValue("VERSION", out var version) ? document.Get<string?>("VERSION") : null,
                NetworkPassphrase = document.TryGetValue("NETWORK_PASSPHRASE", out var networkPass)
                    ? document.Get<string?>("NETWORK_PASSPHRASE")
                    : null,
                FederationServer = document.TryGetValue("FEDERATION_SERVER", out var federation)
                    ? document.Get<string?>("FEDERATION_SERVER")
                    : null,
                AuthServer = document.TryGetValue("AUTH_SERVER", out var auth)
                    ? document.Get<string?>("AUTH_SERVER")
                    : null,
                TransferServer = document.TryGetValue("TRANSFER_SERVER", out var transferServer)
                    ? document.Get<string?>("TRANSFER_SERVER")
                    : null,
                TransferServerSep24 = document.TryGetValue("TRANSFER_SERVER_SEP0024", out var transferServerSep24)
                    ? document.Get<string?>("TRANSFER_SERVER_SEP0024")
                    : null,
                KYCServer = document.TryGetValue("KYC_SERVER", out var kycServer)
                    ? document.Get<string?>("KYC_SERVER")
                    : null,
                WebAuthEndpoint = document.TryGetValue("WEB_AUTH_ENDPOINT", out var webAuth)
                    ? document.Get<string?>("WEB_AUTH_ENDPOINT")
                    : null,
                SigningKey = document.TryGetValue("SIGNING_KEY", out var signing)
                    ? document.Get<string?>("SIGNING_KEY")
                    : null,
                HorizonUrl = document.TryGetValue("HORIZON_URL", out var horizon)
                    ? document.Get<string?>("HORIZON_URL")
                    : null,
                UriRequestSigningKey = document.TryGetValue("URI_REQUEST_SIGNING_KEY", out var uriRequest)
                    ? document.Get<string?>("URI_REQUEST_SIGNING_KEY")
                    : null,
                DirectPaymentServer = document.TryGetValue("DIRECT_PAYMENT_SERVER", out var direct)
                    ? document.Get<string?>("DIRECT_PAYMENT_SERVER")
                    : null,
                AnchorQuoteServer = document.TryGetValue("ANCHOR_QUOTE_SERVER", out var anchorQuote)
                    ? document.Get<string?>("ANCHOR_QUOTE_SERVER")
                    : null,
            };

            var accountsArray = document.TryGetValue("ACCOUNTS", out var accounts)
                ? document.Get<TomlArray>("ACCOUNTS")
                : null;
            if (accountsArray != null)
            {
                foreach (var item in accountsArray.Items)
                {
                    GeneralInformation.Accounts.Add(item.Get<string>());
                }
            }

            var documentationTable = document.TryGetValue("DOCUMENTATION", out var docs)
                ? document.Get<TomlTable>("DOCUMENTATION")
                : null;
            if (documentationTable != null)
            {
                Documentation = new Documentation
                {
                    OrgName = documentationTable.TryGetValue("ORG_NAME", out var orgName)
                        ? documentationTable.Get<string?>("ORG_NAME")
                        : null,
                    OrgDBA = documentationTable.TryGetValue("ORG_DBA", out var orgDba)
                        ? documentationTable.Get<string?>("ORG_DBA")
                        : null,
                    OrgUrl = documentationTable.TryGetValue("ORG_URL", out var orgUrl)
                        ? documentationTable.Get<string?>("ORG_URL")
                        : null,
                    OrgLogo = documentationTable.TryGetValue("ORG_LOGO", out var orgLogo)
                        ? documentationTable.Get<string?>("ORG_LOGO")
                        : null,
                    OrgDescription = documentationTable.TryGetValue("ORG_DESCRIPTION", out var orgDesc)
                        ? documentationTable.Get<string?>("ORG_DESCRIPTION")
                        : null,
                    OrgPhysicalAddress = documentationTable.TryGetValue("ORG_PHYSICAL_ADDRESS", out var orgPhysicalAddr)
                        ? documentationTable.Get<string?>("ORG_PHYSICAL_ADDRESS")
                        : null,
                    OrgPhysicalAddressAttestation =
                        documentationTable.TryGetValue("ORG_PHYSICAL_ADDRESS_ATTESTATION", out var orgPhysicalAddrAtt)
                            ? documentationTable.Get<string?>("ORG_PHYSICAL_ADDRESS_ATTESTATION")
                            : null,
                    OrgPhoneNumber = documentationTable.TryGetValue("ORG_PHONE_NUMBER", out var orgPhone)
                        ? documentationTable.Get<string?>("ORG_PHONE_NUMBER")
                        : null,
                    OrgPhoneNumberAttestation =
                        documentationTable.TryGetValue("ORG_PHONE_NUMBER_ATTESTATION", out var orgPhoneAtt)
                            ? documentationTable.Get<string?>("ORG_PHONE_NUMBER_ATTESTATION")
                            : null,
                    OrgKeybase = documentationTable.TryGetValue("ORG_KEYBASE", out var orgKeyBase)
                        ? documentationTable.Get<string?>("ORG_KEYBASE")
                        : null,
                    OrgTwitter = documentationTable.TryGetValue("ORG_TWITTER", out var orgTwitter)
                        ? documentationTable.Get<string?>("ORG_TWITTER")
                        : null,
                    OrgGithub = documentationTable.TryGetValue("ORG_GITHUB", out var orgGit)
                        ? documentationTable.Get<string?>("ORG_GITHUB")
                        : null,
                    OrgOfficialEmail = documentationTable.TryGetValue("ORG_OFFICIAL_EMAIL", out var orgOfficialEmail)
                        ? documentationTable.Get<string?>("ORG_OFFICIAL_EMAIL")
                        : null,
                    OrgSupportEmail = documentationTable.TryGetValue("ORG_SUPPORT_EMAIL", out var orgSupEmail)
                        ? documentationTable.Get<string?>("ORG_SUPPORT_EMAIL")
                        : null,
                    OrgLicensingAuthority =
                        documentationTable.TryGetValue("ORG_LICENSING_AUTHORITY", out var orgLicenceAuth)
                            ? documentationTable.Get<string?>("ORG_LICENSING_AUTHORITY")
                            : null,
                    OrgLicenseType = documentationTable.TryGetValue("ORG_LICENSE_TYPE", out var orgLicenseType)
                        ? documentationTable.Get<string?>("ORG_LICENSE_TYPE")
                        : null,
                    OrgLicenseNumber = documentationTable.TryGetValue("ORG_LICENSE_NUMBER", out var orgLicenseNum)
                        ? documentationTable.Get<string?>("ORG_LICENSE_NUMBER")
                        : null,
                };
            }

            var principalsArray = document.TryGetValue("PRINCIPALS", out var principals)
                ? document.Get<TomlTableArray>("PRINCIPALS")
                : null;
            if (principalsArray != null)
            {
                PointsOfContact = new List<PointOfContact>();
                foreach (var item in principalsArray.Items)
                {
                    PointsOfContact.Add(new PointOfContact
                    {
                        Name = item.TryGetValue("name", out var name) ? item.Get<string?>("name") : null,
                        Email = item.TryGetValue("email", out var email) ? item.Get<string?>("email") : null,
                        Keybase = item.TryGetValue("keybase", out var keybase) ? item.Get<string?>("keybase") : null,
                        Twitter = item.TryGetValue("twitter", out var twitter) ? item.Get<string?>("twitter") : null,
                        Telegram =
                            item.TryGetValue("telegram", out var telegram) ? item.Get<string?>("telegram") : null,
                        Github = item.TryGetValue("github", out var github) ? item.Get<string?>("github") : null,
                        IdPhotoHash = item.TryGetValue("id_photo_hash", out var idPhotoHash)
                            ? item.Get<string?>("id_photo_hash")
                            : null,
                        VerificationPhotoHash =
                            item.TryGetValue("verification_photo_hash", out var verificationPhotoHash)
                                ? item.Get<string?>("verification_photo_hash")
                                : null,
                    });
                }
            }

            var currenciesArray = document.TryGetValue("CURRENCIES", out var currencies)
                ? document.Get<TomlTableArray>("CURRENCIES")
                : null;
            if (currenciesArray != null)
            {
                Currencies = new List<Currency>();
                foreach (var item in currenciesArray.Items)
                {
                    Currencies.Add(CurrencyFromItem(item));
                }
            }

            var validatorsArray = document.TryGetValue("VALIDATORS", out var validators)
                ? document.Get<TomlTableArray>("VALIDATORS")
                : null;
            if (validatorsArray != null)
            {
                Validators = new List<Validator>();
                foreach (var item in validatorsArray.Items)
                {
                    Validators.Add(new Validator
                    {
                        Alias = item.TryGetValue("ALIAS", out var alias) ? item.Get<string?>("ALIAS") : null,
                        DisplayName = item.TryGetValue("DISPLAY_NAME", out var displayName)
                            ? item.Get<string?>("DISPLAY_NAME")
                            : null,
                        PublicKey = item.TryGetValue("PUBLIC_KEY", out var publicKey)
                            ? item.Get<string?>("PUBLIC_KEY")
                            : null,
                        Host = item.TryGetValue("HOST", out var host) ? item.Get<string?>("HOST") : null,
                        History = item.TryGetValue("HISTORY", out var history) ? item.Get<string?>("HISTORY") : null,
                    });
                }
            }
        }

        /// <summary>
        /// Loads a Stellar TOML file from a given domain.
        /// </summary>
        /// <param name="domain">The domain where the TOML file is located.</param>
        /// <param name="httpClient">An optional HttpClient to use for the HTTP request.</param>
        /// <returns>A StellarToml instance.</returns>
        /// <exception cref="Exception">Thrown when the TOML file is not found.</exception>
        public static async Task<StellarToml> FromDomain(string domain, HttpClient? httpClient = null)
        {
            Uri uri = new Uri($"https://{domain}/.well-known/stellar.toml");
            httpClient ??= new HttpClient();
            using var response = await httpClient.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Stellar toml not found, response status code {response.StatusCode}");
            }

            string toml = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new StellarToml(toml);
        }

        /// <summary>
        /// Alternately to specifying a currency in its content, stellar.toml can link out to a separate TOML file
        /// for the currency by specifying toml="https://DOMAIN/.well-known/CURRENCY.toml" as the currency's only field.
        /// In this case you can use this method to load the currency data from the received link (Currency.toml).
        /// </summary>
        /// <param name="toml">The URL of the currency TOML file.</param>
        /// <returns>A Currency instance.</returns>
        /// <exception cref="Exception">Thrown when the TOML file is not found.</exception>
        public static async Task<Currency> CurrencyFromUrl(string toml, HttpClient? client = null)
        {
            Uri uri = new Uri(toml);

            client ??= new HttpClient();
            client.DefaultRequestHeaders.Add("X-Client-Name", "dotnet-stellar-sdk");
            client.DefaultRequestHeaders.Add("X-Client-Version", Server.SdkVersionNumber);
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Currency TOML not found, response status code {response.StatusCode}");
            }

            var tomlString = await response.Content.ReadAsStringAsync();
            var document = Toml.ReadString(tomlString);

            return CurrencyFromItem(document);
        }

        private static Currency CurrencyFromItem(TomlTable item)
        {
            Currency currency = new Currency
            {
                Toml = item.TryGetValue("toml", out var toml) ? item.Get<string?>("toml") : null,
                Code = item.TryGetValue("code", out var code) ? item.Get<string?>("code") : null,
                CodeTemplate = item.TryGetValue("code_template", out var codeTemplate)
                    ? item.Get<string?>("code_template")
                    : null,
                Issuer = item.TryGetValue("issuer", out var issuer) ? item.Get<string?>("issuer") : null,
                Status = item.TryGetValue("status", out var status) ? item.Get<string?>("status") : null,
                DisplayDecimals = item.TryGetValue("display_decimals", out var displayDecimals)
                    ? item.Get<int?>("display_decimals")
                    : null,
                Name = item.TryGetValue("name", out var name) ? item.Get<string?>("name") : null,
                Desc = item.TryGetValue("desc", out var desc) ? item.Get<string?>("desc") : null,
                Conditions =
                    item.TryGetValue("conditions", out var conditions) ? item.Get<string?>("conditions") : null,
                Image = item.TryGetValue("image", out var image) ? item.Get<string?>("image") : null,
                FixedNumber = item.TryGetValue("fixed_number", out var fixedNumber)
                    ? item.Get<long?>("fixed_number")
                    : null,
                MaxNumber = item.TryGetValue("max_number", out var maxNumber) ? item.Get<long?>("max_number") : null,
                IsUnlimited = item.TryGetValue("is_unlimited", out var isUnlimited)
                    ? item.Get<bool?>("is_unlimited")
                    : null,
                IsAssetAnchored = item.TryGetValue("is_asset_anchored", out var isAssetAnchored)
                    ? item.Get<bool?>("is_asset_anchored")
                    : null,
                AnchorAssetType = item.TryGetValue("anchor_asset_type", out var anchorAssetType)
                    ? item.Get<string?>("anchor_asset_type")
                    : null,
                AnchorAsset = item.TryGetValue("anchor_asset", out var anchorAsset)
                    ? item.Get<string?>("anchor_asset")
                    : null,
                RedemptionInstructions = item.TryGetValue("redemption_instructions", out var redemptionInstructions)
                    ? item.Get<string?>("redemption_instructions")
                    : null,
                Regulated = item.TryGetValue("regulated", out var regulated) ? item.Get<bool?>("regulated") : null,
                ApprovalServer = item.TryGetValue("approval_server", out var approvalServer)
                    ? item.Get<string?>("approval_server")
                    : null,
                ApprovalCriteria = item.TryGetValue("approval_criteria", out var approvalCriteria)
                    ? item.Get<string?>("approval_criteria")
                    : null,
            };

            var collateralAddresses = item.TryGetValue("collateral_addresses", out var collateralAdd)
                ? item.Get<TomlArray>("collateral_addresses")
                : null;
            if (collateralAddresses != null)
            {
                currency.CollateralAddresses = new List<string>();
                foreach (var address in collateralAddresses.Items)
                {
                    currency.CollateralAddresses.Add(address.Get<string>());
                }
            }

            var collateralAddressMessages =
                item.TryGetValue("collateral_address_messages", out var collateralAddrMessages)
                    ? item.Get<TomlArray>("collateral_address_messages")
                    : null;
            if (collateralAddressMessages != null)
            {
                currency.CollateralAddressMessages = new List<string>();
                foreach (var message in collateralAddressMessages.Items)
                {
                    currency.CollateralAddressMessages.Add(message.Get<string>());
                }
            }

            var collateralAddressSignatures =
                item.TryGetValue("collateral_address_signatures", out var collateralAddrSignatures)
                    ? item.Get<TomlArray>("collateral_address_signatures")
                    : null;
            if (collateralAddressSignatures != null)
            {
                currency.CollateralAddressSignatures = new List<string>();
                foreach (var signature in collateralAddressSignatures.Items)
                {
                    currency.CollateralAddressSignatures.Add(signature.Get<string>());
                }
            }

            return currency;
        }
    }

    /// <summary>
    /// General information from the stellar.toml file.
    /// </summary>
    public class GeneralInformation
    {
        /// <summary>
        /// The version of SEP-1 your stellar.toml adheres to. This helps parsers know which fields to expect.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// The passphrase for the specific Stellar network this infrastructure operates on.
        /// </summary>
        public string? NetworkPassphrase { get; set; }

        /// <summary>
        /// The endpoint for clients to resolve stellar addresses for users on your domain via SEP-2 Federation Protocol.
        /// </summary>
        public string? FederationServer { get; set; }

        /// <summary>
        /// The endpoint used for SEP-3 Compliance Protocol.
        /// </summary>
        public string? AuthServer { get; set; }

        /// <summary>
        /// The server used for SEP-6 Anchor/Client interoperability.
        /// </summary>
        public string? TransferServer { get; set; }

        /// <summary>
        /// The server used for SEP-24 Anchor/Client interoperability.
        /// </summary>
        public string? TransferServerSep24 { get; set; }

        /// <summary>
        /// The server used for SEP-12 Anchor/Client customer info transfer.
        /// </summary>
        public string? KYCServer { get; set; }

        /// <summary>
        /// The endpoint used for SEP-10 Web Authentication.
        /// </summary>
        public string? WebAuthEndpoint { get; set; }

        /// <summary>
        /// The signing key is used for SEP-3 Compliance Protocol (deprecated) and SEP-10 Authentication Protocol.
        /// </summary>
        public string? SigningKey { get; set; }

        /// <summary>
        /// Location of public-facing Horizon instance (if one is offered).
        /// </summary>
        public string? HorizonUrl { get; set; }

        /// <summary>
        /// A list of Stellar accounts that are controlled by this domain.
        /// </summary>
        public List<string> Accounts { get; set; }

        /// <summary>
        /// The signing key is used for SEP-7 delegated signing.
        /// </summary>
        public string? UriRequestSigningKey { get; set; }

        /// <summary>
        /// The server used for receiving SEP-31 direct fiat-to-fiat payments. Requires SEP-12 and hence a KYC_SERVER TOML attribute.
        /// </summary>
        public string? DirectPaymentServer { get; set; }

        /// <summary>
        /// The server used for receiving SEP-38 requests.
        /// </summary>
        public string? AnchorQuoteServer { get; set; }

        public GeneralInformation()
        {
            Accounts = new List<string>();
        }
    }

    /// <summary>
    /// Organization Documentation. From the stellar.toml DOCUMENTATION table.
    /// </summary>
    public class Documentation
    {
        /// <summary>
        /// Legal name of the organization
        /// </summary>
        public string? OrgName { get; set; }

        /// <summary>
        /// (may not apply) DBA of the organization.
        /// </summary>
        public string? OrgDBA { get; set; }

        /// <summary>
        /// The organization's official URL. The stellar.toml must be hosted on the same domain.
        /// </summary>
        public string? OrgUrl { get; set; }

        /// <summary>
        ///  An Url to a PNG image of the organization's logo on a transparent background.
        /// </summary>
        public string? OrgLogo { get; set; }

        /// <summary>
        /// Short description of the organization.
        /// </summary>
        public string? OrgDescription { get; set; }

        /// <summary>
        /// Physical address for the organization.
        /// </summary>
        public string? OrgPhysicalAddress { get; set; }

        /// <summary>
        /// URL on the same domain as the orgUrl that contains an image or pdf official document attesting to the physical address. It must list the orgName or orgDBA as the party at the address. Only documents from an official third party are acceptable. E.g. a utility bill, mail from a financial institution, or business license.
        /// </summary>
        public string? OrgPhysicalAddressAttestation { get; set; }

        /// <summary>
        /// The organization's phone number in E.164 format, e.g. +14155552671.
        /// </summary>
        public string? OrgPhoneNumber { get; set; }

        /// <summary>
        /// URL on the same domain as the orgUrl that contains an image or pdf of a phone bill showing both the phone number and the organization's name.
        /// </summary>
        public string? OrgPhoneNumberAttestation { get; set; }

        /// <summary>
        /// A Keybase account name for the organization. Should contain proof of ownership of any public online accounts you list here, including the organization's domain.
        /// </summary>
        public string? OrgKeybase { get; set; }

        /// <summary>
        /// The organization's Twitter account.
        /// </summary>
        public string? OrgTwitter { get; set; }

        /// <summary>
        /// The organization's Github account
        /// </summary>
        public string? OrgGithub { get; set; }

        /// <summary>
        /// An email where clients can contact the organization. Must be hosted at the orgUrl domain.
        /// </summary>
        public string? OrgOfficialEmail { get; set; }

        /// <summary>
        /// An email that users can use to request support regarding the organizations Stellar assets or applications.
        /// </summary>
        public string? OrgSupportEmail { get; set; }

        /// <summary>
        /// Name of the authority or agency that licensed the organization, if applicable.
        /// </summary>
        public string? OrgLicensingAuthority { get; set; }

        /// <summary>
        /// Type of financial or other license the organization holds, if applicable.
        /// </summary>
        public string? OrgLicenseType { get; set; }

        /// <summary>
        /// Official license number of the organization, if applicable.
        /// </summary>
        public string? OrgLicenseNumber { get; set; }
    }

    /// <summary>
    /// Point of Contact Documentation. From the stellar.toml PRINCIPALS list. It contains identifying information for the primary point of contact or principal of the organization.
    /// </summary>
    public class PointOfContact
    {
        /// <summary>
        /// Full legal name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Business email address for the principal.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Personal Keybase account. Should include proof of ownership for other online accounts, as well as the organization's domain.
        /// </summary>
        public string? Keybase { get; set; }

        /// <summary>
        /// Personal Telegram account.
        /// </summary>
        public string? Telegram { get; set; }

        /// <summary>
        /// Personal Twitter account.
        /// </summary>
        public string? Twitter { get; set; }

        /// <summary>
        /// Personal Github account.
        /// </summary>
        public string? Github { get; set; }

        /// <summary>
        /// SHA-256 hash of a photo of the principal's government-issued photo ID.
        /// </summary>
        public string? IdPhotoHash { get; set; }

        /// <summary>
        /// SHA-256 hash of a verification photo of principal. Should be well-lit and contain: principal holding ID card and signed, dated, hand-written message stating I, $name, am a principal of $orgName, a Stellar token issuer with address $issuerAddress.
        /// </summary>
        public string? VerificationPhotoHash { get; set; }
    }

    /// <summary>
    /// Currency Documentation. From the stellar.toml CURRENCIES list, one set of fields for each currency supported. Applicable fields should be completed and any that don't apply should be excluded.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Token code.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// A pattern with ? as a single character wildcard. Allows a CURRENCIES entry to apply to multiple assets that share the same info. An example is futures, where the only difference between issues is the date of the contract. E.g. CORN???????? to match codes such as CORN20180604.
        /// </summary>
        public string? CodeTemplate { get; set; }

        /// <summary>
        /// Token issuer Stellar public key.
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Status of token. One of live, dead, test, or private. Allows issuer to mark whether token is dead/for testing/for private use or is live and should be listed in live exchanges.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Preference for number of decimals to show when a client displays currency balance.
        /// </summary>
        public int? DisplayDecimals { get; set; }

        /// <summary>
        /// A short name for the token.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description of token and what it represents.
        /// </summary>
        public string? Desc { get; set; }

        /// <summary>
        /// Conditions on token.
        /// </summary>
        public string? Conditions { get; set; }

        /// <summary>
        /// URL to a PNG image on a transparent background representing token.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Fixed number of tokens, if the number of tokens issued will never change.
        /// </summary>
        public long? FixedNumber { get; set; }

        /// <summary>
        /// Max number of tokens, if there will never be more than maxNumber tokens.
        /// </summary>
        public long? MaxNumber { get; set; }

        /// <summary>
        /// The number of tokens is dilutable at the issuer's discretion.
        /// </summary>
        public bool? IsUnlimited { get; set; }

        /// <summary>
        /// true if token can be redeemed for underlying asset, otherwise false.
        /// </summary>
        public bool? IsAssetAnchored { get; set; }

        /// <summary>
        /// Type of asset anchored. Can be fiat, crypto, nft, stock, bond, commodity, realestate, or other.
        /// </summary>
        public string? AnchorAssetType { get; set; }

        /// <summary>
        /// If anchored token, code / symbol for asset that token is anchored to. E.g. USD, BTC, SBUX, Address of real-estate investment property.
        /// </summary>
        public string? AnchorAsset { get; set; }

        /// <summary>
        /// If anchored token, these are instructions to redeem the underlying asset from tokens.
        /// </summary>
        public string? RedemptionInstructions { get; set; }

        /// <summary>
        /// If this is an anchored crypto token, list of one or more public addresses that hold the assets for which you are issuing tokens.
        /// </summary>
        public List<string>? CollateralAddresses { get; set; }

        /// <summary>
        /// Messages stating that funds in the collateralAddresses list are reserved to back the issued asset.
        /// </summary>
        public List<string>? CollateralAddressMessages { get; set; }

        /// <summary>
        /// These prove you control the collateralAddresses. For each address you list, sign the entry in collateralAddressMessages with the address's private key and add the resulting string to this list as a base64-encoded raw signature.
        /// </summary>
        public List<string>? CollateralAddressSignatures { get; set; }

        /// <summary>
        /// Indicates whether or not this is a sep0008 regulated asset. If missing, false is assumed.
        /// </summary>
        public bool? Regulated { get; set; }

        /// <summary>
        /// URL of a sep0008 compliant approval service that signs validated transactions.
        /// </summary>
        public string? ApprovalServer { get; set; }

        /// <summary>
        /// A human readable string that explains the issuer's requirements for approving transactions.
        /// </summary>
        public string? ApprovalCriteria { get; set; }

        /// <summary>
        /// Alternately, stellar.toml can link out to a separate TOML file for each currency by specifying toml="https://DOMAIN/.well-known/CURRENCY.toml" as the currency's only field.
        /// In this case only this field is filled. To load the currency data, you can use StellarToml.currencyFromUrl(String toml).
        /// </summary>
        public string? Toml { get; set; }
    }

    /// <summary>
    /// Validator Information. From the the stellar.toml VALIDATORS list, one set of fields for each node your organization runs.
    /// Combined with the steps outlined in SEP-20, this section allows to declare the node(s), and to let others know the location of any public archives they maintain.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// A name for display in stellar-core configs that conforms to ^[a-z0-9-]{2,16}$.
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// A human-readable name for display in quorum explorers and other interfaces.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// The Stellar account associated with the node.
        /// </summary>
        public string? PublicKey { get; set; }

        /// <summary>
        /// The IP:port or domain:port peers can use to connect to the node.
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// The location of the history archive published by this validator.
        /// </summary>
        public string? History { get; set; }
    }
}