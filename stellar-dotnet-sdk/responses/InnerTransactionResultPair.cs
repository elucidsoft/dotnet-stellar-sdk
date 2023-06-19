using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk.responses;

public class InnerTransactionResultPair
{
    public string TransactionHash { get; set; }
        
    public TransactionResult Result { get; set; }
        
    public static InnerTransactionResultPair FromXdr(string encoded)
    {
        byte[] bytes = Convert.FromBase64String(encoded);
        var result = xdr.InnerTransactionResultPair.Decode(new xdr.XdrDataInputStream(bytes));
        return FromXdr(result);
    }

    public static InnerTransactionResultPair FromXdr(xdr.InnerTransactionResultPair result)
    {
        var writer = new XdrDataOutputStream();
        xdr.InnerTransactionResult.Encode(writer, result.Result);
        var xdrTransaction = Convert.ToBase64String(writer.ToArray());

        return new InnerTransactionResultPair
        {
            TransactionHash = Convert.ToBase64String(result.TransactionHash.InnerValue),
            Result = TransactionResult.FromXdr(xdrTransaction),
        };
    }
}