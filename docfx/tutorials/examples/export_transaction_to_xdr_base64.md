# Export Transaction to a XDR (Base64)

## Description
In this example you will learn how to export a Transaction  object to a **XDR base64**.

>[!TIP]
>There are 2 methods for do this, one for **signed** transactions and the other for **unsigned**, choose between those depending of what do you want to do.

## Code Example

```csharp
using System;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

public async Task ImportTransactionToXDRBase64()
{
    //Set network and server
    Network network = new Network("Test SDF Network ; September 2015");
    Server server = new Server("https://horizon-testnet.stellar.org");

    //Source keypair from the secret seed
    KeyPair sourceKeypair = KeyPair.FromSecretSeed("SOURCE_SECRET_SEED");

    //Destination keypair from the account id
    KeyPair destinationKeyPair = KeyPair.FromAccountId("DESTINATION_ACCOUNT_ID");

    //Load source account data
    AccountResponse sourceAccountResponse = await server.Accounts.Account(destinationKeyPair.AccountId);

    //Create source account object
    Account sourceAccount = new Account(sourceKeypair.AccountId, sourceAccountResponse.SequenceNumber);

    //Create asset object with specific amount
    //You can use native or non native ones.
    Asset asset = new AssetTypeNative();
    string amount = "1";

    //Create payment operation
    PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount).SetSourceAccount(sourceAccount.KeyPair).Build();

    //Create transaction and add operation
    Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

    //Export to Signed XDR Base64 (Use this in case you have the Transaction signed)
    string signedXDR = transaction.ToEnvelopeXdrBase64();

    //Export to Unsigned XDR Base64 (Use this in case you want to sign it in a external signer)
    string unsignedXDR = transaction.ToUnsignedEnvelopeXdrBase64();

    //Show XDRs
    Console.WriteLine("Signed XDR");
    Console.WriteLine(signedXDR);
    Console.WriteLine();

    Console.WriteLine("Unsigned XDR");
    Console.WriteLine(signedXDR);
}
```


## Documentation References
- [Transaction](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Transaction.html)

## Other References

- [XDR Concept](https://www.stellar.org/developers/guides/concepts/xdr.html)
- [Stellar Laboratory XDR Viewer](https://www.stellar.org/laboratory/#xdr-viewer?type=TransactionEnvelope&network=test)
- [Stellar Laboratory XDR Signer](https://www.stellar.org/laboratory/#txsigner?network=test)
