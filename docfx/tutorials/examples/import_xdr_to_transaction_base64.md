# Import XDR to a Transaction (Base64)

## Description
In this example you will learn how to import a XDR Base64 to a Transaction.


## Code Example

```csharp
using System;
using stellar_dotnet_sdk;

public void ImportXDRBase64ToTransaction()
{
    //Create Transaction from the XDR
    Transaction transaction = Transaction.FromEnvelopeXdr("AAAAAKc+U3o1WjRxpnB6oOH2Og/zKt1Lc30vUzmujsNNCBr0AAAAZACZoa8AAAABAAAAAAAAAAAAAAABAAAAAQAAAACnPlN6NVo0caZweqDh9joP8yrdS3N9L1M5ro7DTQga9AAAAAEAAAAAF+GO8KrTBkIS3/UKI9eWmcC0Ohywzz4DhJjazdgP8TQAAAACU0tJTjAxAAAAAAAAAAAAAKc+U3o1WjRxpnB6oOH2Og/zKt1Lc30vUzmujsNNCBr0AAAA6NSlEAAAAAAAAAAAAU0IGvQAAABAm9m56U4mizdoj4iE1lOi05hu72yrZpFHhJHvCHX2YFlJPhmldjUwB4M2V7pgyQpqrqYKbZ7sHjMYHwj+7KbLCg==");

    //Get Source Account
    Console.WriteLine(transaction.SourceAccount.AccountId);

    //Get Memo
    Console.WriteLine(transaction.Memo.ToString());

    //Get Sequence Number
    Console.WriteLine(transaction.SequenceNumber);

    //Get first Operation AssetCode
    Console.WriteLine(transaction.Operations[0].ToOperationBody().PaymentOp.Asset.AlphaNum12.AssetCode);
}
```


## Documentation References
- [Transaction](https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnet_sdk.Transaction.html)

## Other References

- [XDR Concept](https://www.stellar.org/developers/guides/concepts/xdr.html)
- [Stellar Laboratory XDR Viewer](https://www.stellar.org/laboratory/#xdr-viewer?type=TransactionEnvelope&network=test)
