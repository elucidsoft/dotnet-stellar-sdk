using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class TransferServerServiceSep24Test
    {
        private TransferServerServiceSep24 _transferServerServiceSep24;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            // Read the valid_stellar.toml file and create a StellarToml instance
            string testdataPath = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "..", "..", "..",
                "testdata", "sep"));
            string validTomlPath = Path.Combine(testdataPath, "valid_stellar.toml");
            string validTomlContent = File.ReadAllText(validTomlPath);
            var stellarToml = new StellarToml(validTomlContent);

            // Create the TransferServerServiceSep24 instance with the StellarToml instance
            _transferServerServiceSep24 =
                await TransferServerServiceSep24.FromDomain("https://test.anchor.example.com/sep24", stellarToml,
                    httpClient);
        }

        [TestMethod]
        public async Task TestInfo()
        {
            // Arrange
            var infoResponseJson = @"{
                ""deposit"": {
                    ""USD"": {
                        ""enabled"": true,
                        ""fee_fixed"": 5,
                        ""fee_percent"": 1,
                        ""min_amount"": 0.1,
                        ""max_amount"": 1000
                    }
                },
                ""withdraw"": {
                    ""USD"": {
                        ""enabled"": true,
                        ""fee_fixed"": 5,
                        ""fee_percent"": 0.5,
                        ""min_amount"": 1,
                        ""max_amount"": 2000
                    }
                },
                ""fee"": {
                    ""enabled"": true,
                    ""authentication_required"": false
                },
                ""features"": {
                    ""account_creation"": true,
                    ""claimable_balances"": false
                }
            }";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(infoResponseJson),
                });

            // Act
            var infoResponse = await _transferServerServiceSep24.Info("en", null);

            // Assert
            Assert.IsNotNull(infoResponse);
            Assert.IsTrue(infoResponse.DepositAssets["USD"].Enabled);
            Assert.IsTrue(infoResponse.WithdrawAssets["USD"].Enabled);
            Assert.IsTrue(infoResponse.FeeInfo.Enabled);
            Assert.IsFalse(infoResponse.AnchorFeatureInfo.ClaimableBalances);
        }

        [TestMethod]
        public async Task TestFee()
        {
            // Arrange
            var feeRequest = new FeeRequest("USD", "deposit", "test_jwt", null, 100.0);
            var feeResponseJson = @"{ ""fee"": 5.5 }";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(feeResponseJson),
                });

            // Act
            var feeResponse = await _transferServerServiceSep24.Fee(feeRequest);

            // Assert
            Assert.IsNotNull(feeResponse);
            Assert.AreEqual(5.5, feeResponse.Fee);
        }

        [TestMethod]
        public async Task TestDepositInteractive()
        {
            // Arrange
            var depositRequest = new DepositRequest("USD", "GALAXY42EXAMPLE", "test_jwt", "GAISSUER42EXAMPLE", null,
                null, null, null, null, null, null);
            var depositResponseJson = @"{
                ""url"": ""https://example.com/deposit"",
                ""id"": ""12345"",
                ""type"": ""interactive_customer_info_needed""
            }";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(depositResponseJson),
                });

            // Act
            var depositResponse = await _transferServerServiceSep24.DepositInteractive(depositRequest);

            // Assert
            Assert.IsNotNull(depositResponse);
            Assert.AreEqual("https://example.com/deposit", depositResponse.Url);
            Assert.AreEqual("12345", depositResponse.Id);
            Assert.AreEqual("interactive_customer_info_needed", depositResponse.Type);
        }

        [TestMethod]
        public async Task TestWithdrawInteractive()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest("USD", "GALAXY42EXAMPLE", "test_jwt", "GAISSUER42EXAMPLE", null,
                null, null, null, null, null, null, null, null, null, null);
            var withdrawResponseJson = @"{
                ""url"": ""https://example.com/withdraw"",
                ""id"": ""12345"",
                ""type"": ""interactive_customer_info_needed""
            }";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(withdrawResponseJson),
                });

            // Act
            var withdrawResponse = await _transferServerServiceSep24.WithdrawInteractive(withdrawRequest);

            // Assert
            Assert.IsNotNull(withdrawResponse);
            Assert.AreEqual("https://example.com/withdraw", withdrawResponse.Url);
            Assert.AreEqual("12345", withdrawResponse.Id);
            Assert.AreEqual("interactive_customer_info_needed", withdrawResponse.Type);
        }

        [TestMethod]
        public async Task TestTransaction()
        {
            // Arrange
            var transactionRequest = new AnchorTransactionRequest("test_jwt", "test_id", "test_stellar_tx_id",
                "test_external_tx_id", "en");

            var transactionResponseJson = @"{
                ""transaction"": {
                    ""id"": ""test_id"",
                    ""kind"": ""deposit"",
                    ""status"": ""completed"",
                    ""status_eta"": 3600,
                    ""more_info_url"": ""https://test.moreinfo.url"",
                    ""amount_in"": ""100.0000000"",
                    ""amount_out"": ""95.0000000"",
                    ""amount_fee"": ""5.0000000"",
                    ""started_at"": ""2023-01-01T00:00:00Z"",
                    ""completed_at"": ""2023-01-01T01:00:00Z"",
                    ""stellar_transaction_id"": ""test_stellar_tx_id"",
                    ""external_transaction_id"": ""test_external_tx_id""
                }
             }";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(transactionResponseJson),
                });

            // Act
            var transactionResponse = await _transferServerServiceSep24.Transaction(transactionRequest);

            // Assert
            Assert.IsNotNull(transactionResponse);
            Assert.AreEqual("test_id", transactionResponse.AnchorTransaction?.Id);
            Assert.AreEqual("deposit", transactionResponse.AnchorTransaction?.Kind);
            Assert.AreEqual("completed", transactionResponse.AnchorTransaction?.Status);
            Assert.AreEqual(3600, transactionResponse.AnchorTransaction?.StatusEta);
            Assert.AreEqual("https://test.moreinfo.url", transactionResponse.AnchorTransaction?.MoreInfoUrl);
            Assert.AreEqual("100.0000000", transactionResponse.AnchorTransaction?.AmountIn);
            Assert.AreEqual("95.0000000", transactionResponse.AnchorTransaction?.AmountOut);
            Assert.AreEqual("5.0000000", transactionResponse.AnchorTransaction?.AmountFee);
            Assert.AreEqual("2023-01-01T00:00:00Z", transactionResponse.AnchorTransaction?.StartedAt);
            Assert.AreEqual("2023-01-01T01:00:00Z", transactionResponse.AnchorTransaction?.CompletedAt);
            Assert.AreEqual("test_stellar_tx_id", transactionResponse.AnchorTransaction?.StellarTransactionId);
            Assert.AreEqual("test_external_tx_id", transactionResponse.AnchorTransaction?.ExternalTransactionId);
        }

        [TestMethod]
        public async Task TestTransactions()
        {
            // Arrange
            var transactionsRequest = new AnchorTransactionsRequest("USD", "test_jwt", "2023-01-01T00:00:00Z", 2,
                "deposit", "test_paging_id", "en");

            var transactionsResponseJson = @"{
            ""transactions"": [
                {
                    ""id"": ""test_id1"",
                    ""kind"": ""deposit"",
                    ""status"": ""completed"",
                    ""status_eta"": 3600,
                    ""more_info_url"": ""https://test.moreinfo.url1"",
                    ""amount_in"": ""100.0000000"",
                    ""amount_out"": ""95.0000000"",
                    ""amount_fee"": ""5.0000000"",
                    ""started_at"": ""2023-01-01T00:00:00Z"",
                    ""completed_at"": ""2023-01-01T01:00:00Z"",
                    ""stellar_transaction_id"": ""test_stellar_tx_id1"",
                    ""external_transaction_id"": ""test_external_tx_id1""
                },
                {
                    ""id"": ""test_id2"",
                    ""kind"": ""deposit"",
                    ""status"": ""completed"",
                    ""status_eta"": 3600,
                    ""more_info_url"": ""https://test.moreinfo.url2"",
                    ""amount_in"": ""200.0000000"",
                    ""amount_out"": ""190.0000000"",
                    ""amount_fee"": ""10.0000000"",
                    ""started_at"": ""2023-01-02T00:00:00Z"",
                    ""completed_at"": ""2023-01-02T01:00:00Z"",
                    ""stellar_transaction_id"": ""test_stellar_tx_id2"",
                    ""external_transaction_id"": ""test_external_tx_id2""
                }
            ]}";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(transactionsResponseJson),
                });

            // Act
            var transactionsResponse = await _transferServerServiceSep24.Transactions(transactionsRequest);

            // Assert
            Assert.IsNotNull(transactionsResponse);
            if (transactionsResponse.AnchorTransactions != null)
            {
                Assert.AreEqual(2, transactionsResponse.AnchorTransactions.Count);

                var transaction1 = transactionsResponse.AnchorTransactions[0];
                Assert.AreEqual("test_id1", transaction1?.Id);
                Assert.AreEqual("deposit", transaction1?.Kind);
                Assert.AreEqual("completed", transaction1?.Status);
                // ... (Assert the rest of the fields for transaction1)

                var transaction2 = transactionsResponse.AnchorTransactions[1];
                Assert.AreEqual("test_id2", transaction2?.Id);
                Assert.AreEqual("deposit", transaction2?.Kind);
                Assert.AreEqual("completed", transaction2?.Status);
            }
            // ... (Assert the rest of the fields for transaction2)
        }
    }
}