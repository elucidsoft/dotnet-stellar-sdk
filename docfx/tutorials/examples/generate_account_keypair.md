# Generate Account Keypair

## Description
In this example you will learn how to generate an account keypair.

> [!NOTE]
> Generating this keypair **won't** create the account.

## Code Example

```csharp
//Generate a random KeyPair
KeyPair keypair = KeyPair.Random();

//Show the KeyPair public and secret
Console.WriteLine("Account ID: " + keypair.AccountId);
Console.WriteLine("Secret: " + keypair.SecretSeed);
```

## Documentation References
- [Account](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Account.html)
- [KeyPair](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.KeyPair.html)

## Other References

- [Account Concept](https://www.stellar.org/developers/guides/concepts/accounts.html)
