# Generate Account Keypair

## Description
In this example you will learn how to generate an account keypair.

Generating this keypair **won't** create the account.

## Code Example

    KeyPair keypair = KeyPair.Random();
    Console.WriteLine("Secret: " + keypair.SecretSeed);
    Console.WriteLine("Account ID: " + keypair.AccountId);

## Documentation References
- [Account](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Account.html)
- [KeyPair](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.KeyPair.html)

## Other References

- [Account Concept](https://www.stellar.org/developers/guides/concepts/accounts.html)
