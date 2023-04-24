using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Threading;
using stellar_dotnet_sdk;
using System.Reflection;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class StellarTomlTest
    {
        [TestMethod]
        public async Task FromDomainValidDomainReturnsStellarToml()
        {
            // Arrange
            string domain = "example.com";
            string testdataPath = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "..", "..", "..",
                "testdata", "sep"));
            string validTomlPath = Path.Combine(testdataPath, "valid_stellar.toml");
            string validTomlContent = File.ReadAllText(validTomlPath);
            var httpClient = GetMockedHttpClient(HttpStatusCode.OK, validTomlContent);

            // Act
            StellarToml stellarToml = await StellarToml.FromDomain(domain, httpClient);

            // Assert
            Assert.IsNotNull(stellarToml);
            Assert.AreEqual("Test Stellar Network ; April 2023", stellarToml.GeneralInformation.NetworkPassphrase);
            Assert.AreEqual("https://test.anchor.example.com/sep24",
                stellarToml.GeneralInformation.TransferServerSep24);
            // Add more assertions based on the expected properties of the StellarToml instance.
        }

        [TestMethod]
        public async Task FromDomainInvalidDomainThrowsException()
        {
            // Arrange
            string domain = "invalid.example.com";
            var httpClient = GetMockedHttpClient(HttpStatusCode.NotFound, string.Empty);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => StellarToml.FromDomain(domain, httpClient));
        }

        [TestMethod]
        public async Task CurrencyFromUrlValidUrlReturnsCurrency()
        {
            // Arrange
            string currencyUrl = "https://example.com/.well-known/CURRENCY.toml";
            string testdataPath = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "..", "..", "..",
                "testdata", "sep"));
            string validCurrencyPath = Path.Combine(testdataPath, "valid_currency.toml");
            string validCurrencyContent = await File.ReadAllTextAsync(validCurrencyPath);

            var httpClient = GetMockedHttpClient(HttpStatusCode.OK, validCurrencyContent);

            // Act
            Currency currency = await StellarToml.CurrencyFromUrl(currencyUrl, httpClient);

            // Assert
            Assert.IsNotNull(currency);
            Assert.AreEqual("USD", currency.Code);
            Assert.AreEqual("US Dollar", currency.Name);
            Assert.AreEqual("GDUKMGUGDZQK6YHYA5Z6AY2G4XDSZPSZ3SW5UN3ARVMO6QSRDWP5YLEX", currency.Issuer);
            // Add more assertions based on the expected properties of the Currency instance.
        }

        [TestMethod]
        public async Task CurrencyFromUrlInvalidUrlThrowsException()
        {
            // Arrange
            string invalidUrl = "https://invalid.example.com/.well-known/CURRENCY.toml";
            var httpClient = GetMockedHttpClient(HttpStatusCode.NotFound, string.Empty);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                StellarToml.CurrencyFromUrl(invalidUrl, httpClient));
        }

        private static HttpClient GetMockedHttpClient(HttpStatusCode statusCode, string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content),
                })
                .Verifiable();

            return new HttpClient(handlerMock.Object);
        }
    }
}