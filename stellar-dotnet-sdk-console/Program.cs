using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;

namespace TestConsole
{
    public class Program
    {
        //For testing use the following account info, this only exists on test network and may be wiped at any time...
        //Public: GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI
        //Secret: SCD74D46TJYXOUXFC5YOA72UTPCCVHK2GRSLKSPRB66VK6UJHQX2Y3R3

        public static async Task Main(string[] args)
        {
            Network.UseTestNetwork();
            var server = new Server("https://horizon-testnet.stellar.org");

            var friendBot = await server.TestNetFriendBot
                .FundAccount(KeyPair.Random())
                .Execute();

            await GetLedgerTransactions(server);
            await ShowAccountTransactions(server);

            //Streams are Maybe fixed? in this API until a resolution is found for the HttpClient issue
            Console.WriteLine("-- Streaming All New Operations On The Network --");

            server.Operations
                .Cursor("now")
                .Order(OrderDirection.ASC)
                .Stream((sender, response) => { ShowOperationResponse(response); })
                .Connect();

            Console.ReadLine();
        }


        private static async Task ShowAccountTransactions(Server server)
        {
            Console.WriteLine("-- Show Account Transactions (ForAccount) --");

            var transactions = await server.Transactions
                .ForAccount(KeyPair.FromAccountId("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI"))
                .Execute();

            ShowTransactionRecords(transactions.Records);
            Console.WriteLine();
        }

        private static async Task GetLedgerTransactions(Server server)
        {
            Console.WriteLine("-- Show Ledger Transactions (ForLedger) --");
            // get a list of transactions that occurred in ledger 1400
            var transactions = await server.Transactions
                .ForLedger(2365)
                .Execute();

            ShowTransactionRecords(transactions.Records);
            Console.WriteLine();
        }

        private static void ShowTransactionRecords(List<TransactionResponse> transactions)
        {
            foreach (var tran in transactions)
                ShowTransactionRecord(tran);
        }

        private static void ShowTransactionRecord(TransactionResponse tran)
        {
            Console.WriteLine($"Ledger: {tran.Ledger}, Hash: {tran.Hash}, Fee Paid: {tran.FeePaid}");
        }

        private static void ShowOperationResponse(OperationResponse op)
        {
            Console.WriteLine($"Id: {op.Id}, Source: {op.SourceAccount.AccountId}, Type: {op.Type}");
        }
    }
}