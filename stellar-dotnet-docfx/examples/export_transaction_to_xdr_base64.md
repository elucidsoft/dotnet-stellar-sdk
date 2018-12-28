# Export Transaction to a XDR (Base64)

## Description
In this example you will learn how to export a Transaction  object to a **XDR base64**.

>[!TIP]
>There are 2 methods for do this, one for **signed** transactions and the other for **unsigned**, choose between those depending of what do you want to do.

## Code Example

```csharp
//Create transaction and add any operation
Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();

//Export to Signed XDR Base64 (Use this in case you have the Transaction signed)
string signedXDR = transaction.ToEnvelopeXdrBase64();

//Export to Unsigned XDR Base64 (Use this in case you want to sign it in a external signer)
string unsignedXDR = transaction.ToUnsignedEnvelopeXdrBase64();
```


## Documentation References
- [Transaction](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Transaction.html)

## Other References

- [XDR Concept](https://www.stellar.org/developers/guides/concepts/xdr.html)
- [Stellar Laboratory XDR Viewer](https://www.stellar.org/laboratory/#xdr-viewer?type=TransactionEnvelope&network=test)
- [Stellar Laboratory XDR Signer](https://www.stellar.org/laboratory/#txsigner?network=test)
