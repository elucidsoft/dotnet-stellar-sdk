using System;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk;

namespace TestConsole
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            Network.UseTestNetwork();
            var server = new Server("https://horizon-testnet.stellar.org");

            // get a list of transactions that occurred in ledger 1400
            var transactions = await server.Transactions
                .ForLedger(2365)
                .Execute();

            foreach (var tran in transactions.Records)
            {
               
                Console.WriteLine($"Ledger: {tran.Ledger}, Hash: {tran.Hash}, Fee Paid: {tran.FeePaid}");
            }

            Console.Read();
        }
    }
}
