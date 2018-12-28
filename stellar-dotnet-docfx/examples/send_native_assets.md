# Send Native Assets

## Description

In this example you will learn how to send **native assets** from one account to another.

>[!IMPORTANT]
>This example uses secret seed and sign an operation, this is usually handled by wallets or in a secure way, be careful where you input your secret seed!

## Code Example
```csharp
//Set network and server
Network network = new Network("Test SDF Network ; September 2015");
Server server = new Server("https://horizon-testnet.stellar.org");

//Source keypair from the secret seed
KeyPair sourceKeypair = KeyPair.FromSecretSeed("SOURCE_SECRET_SEED");

//Destination keypair from the account id
destinationKeyPair = KeyPair.FromAccountId("DESTINATION_ACCOUNT_ID");

//Load source account data
AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeyPair);

//Create source account object
Account sourceAccount = new Account(sourceAccountResponse.KeyPair, sourceAccountResponse.SequenceNumber);

//Create asset object with specific amount
//You can use native or non native ones.
Asset asset = new AssetTypeNative();
string amount = "1";

//Create payment operation
PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();

//Create transaction and add the payment operation we created
Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

//Sign Transaction
transaction.Sign(sourceKeyPair);

//Try to send the transaction
try
{
	Console.WriteLine("Sending Transaction");
	await server.SubmitTransaction(transaction);
	Console.WriteLine("Success!");
}
catch (Exception exception)
{
	Console.WriteLine("Send Transaction Failed");
	Console.WriteLine("Exception: " + exception.Message);
}
```

## Documentation References
- [Asset](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Asset.html)
- [Native Asset](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.AssetTypeNative.html)
- [Payment Operation](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.PaymentOperation.html)
- [Transaction](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Transaction.html)

## Other References

- [Assets](https://www.stellar.org/developers/guides/concepts/assets.html)
- [Quick Start - Send/Receive Money](https://www.stellar.org/developers/guides/get-started/transactions.html)
