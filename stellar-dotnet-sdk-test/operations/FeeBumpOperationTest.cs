using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using xdrSDK = stellar_dotnet_sdk.xdr;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.operations;

[TestClass]
public class FeeBumpOperationTest
{
    [TestMethod]
    public async Task TransactionResultSuccess()
    {
        Network.UseTestNetwork();
        using var server = new Server("https://horizon-testnet.stellar.org");

        var sourceKeyPair = KeyPair.Random();
        var sponsorKeyPair = KeyPair.Random();

        await Task.WhenAll(
            server.TestNetFriendBot
                .FundAccount(sourceKeyPair.AccountId)
                .Execute(),
            server.TestNetFriendBot
                .FundAccount(sponsorKeyPair.AccountId)
                .Execute()
        );

        var sourceAccount = await server.Accounts.Account(sourceKeyPair.AccountId);

        const uint maxFee = 2000000u;

        var transactionBuilder = new TransactionBuilder(sourceAccount);
        transactionBuilder.SetFee(maxFee);
            
        var paymentOperationBuilder = new PaymentOperation.Builder(
            sponsorKeyPair,
            new AssetTypeNative(),
            "10"
        );
        transactionBuilder.AddOperation(
            paymentOperationBuilder.Build()
        );
        var transaction = transactionBuilder.Build();
        transaction.Sign(sourceKeyPair);

        var feeBumpTransaction = TransactionBuilder.BuildFeeBumpTransaction(
            sponsorKeyPair,
            transaction,
            maxFee
        );
        feeBumpTransaction.Sign(sponsorKeyPair);

        var response = await server.SubmitTransaction(feeBumpTransaction);

        Assert.IsTrue(response.IsSuccess());
        Assert.IsFalse(string.IsNullOrEmpty(response.Hash));
        Assert.IsInstanceOfType(response.Result, typeof(FeeBumpTransactionResultSuccess));
    }
        
    [TestMethod]
    public async Task TransactionResultFailed()
    {
        Network.UseTestNetwork();
        using var server = new Server("https://horizon-testnet.stellar.org");

        var sourceKeyPair = KeyPair.Random();
        var sponsorKeyPair = KeyPair.Random();

        await Task.WhenAll(
            server.TestNetFriendBot
                .FundAccount(sourceKeyPair.AccountId)
                .Execute(),
            server.TestNetFriendBot
                .FundAccount(sponsorKeyPair.AccountId)
                .Execute()
        );

        var sourceAccount = await server.Accounts.Account(sourceKeyPair.AccountId);

        const uint maxFee = 2000000u;

        var transactionBuilder = new TransactionBuilder(sourceAccount);
        transactionBuilder.SetFee(maxFee);
            
        var paymentOperationBuilder = new PaymentOperation.Builder(
            sponsorKeyPair,
            new AssetTypeNative(),
            "100000"
        );
        transactionBuilder.AddOperation(
            paymentOperationBuilder.Build()
        );
        var transaction = transactionBuilder.Build();
        transaction.Sign(sourceKeyPair);

        var feeBumpTransaction = TransactionBuilder.BuildFeeBumpTransaction(
            sponsorKeyPair,
            transaction,
            maxFee
        );
        feeBumpTransaction.Sign(sponsorKeyPair);

        var response = await server.SubmitTransaction(feeBumpTransaction);
            
        Assert.IsFalse(response.IsSuccess());
            
        var result = response.Result as FeeBumpTransactionResultFailed;
        Assert.IsNotNull(result);
            
        var innerResult = result.InnerResultPair.Result as TransactionResultFailed;
        Assert.IsNotNull(innerResult);
        Assert.IsInstanceOfType(innerResult.Results[0], typeof(PaymentUnderfunded));
    }
}