# Get Account Balance

## Description

In this example you will learn how to get the balance of an account.

>[!TIP]
>If the **AssetCode** is empty, that means this asset is native (XLM in case of Stellar Network).

## Code Example

```csharp
using System;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

//Set network and server
public async Task GetAccountBalance()
{
   //Set network and server
   Network network = new Network("Test SDF Network ; September 2015");
   Server server = new Server("https://horizon-testnet.stellar.org");

   //Generate a keypair from the account id.
   KeyPair keypair = KeyPair.FromAccountId("ACCOUNT_ID");

   //Load the account
   AccountResponse accountResponse = await server.Accounts.Account(keypair.AccountId);

   //Get the balance
   Balance[] balances = accountResponse.Balances;

   //Show the balance
   for (int i = 0; i < balances.Length; i++)
   {
       Balance asset = balances[i];
       Console.WriteLine("Asset Code: " + asset.AssetCode);
       Console.WriteLine("Asset Amount: " + asset.BalanceString);
   }
}
```

## Documentation References
- [Balance](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.responses.Balance.html)

## Other References

- [Assets](https://www.stellar.org/developers/guides/concepts/assets.html)
