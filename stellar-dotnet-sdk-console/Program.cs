using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace TestConsole
{
    public class Program
    {
        //For testing use the following account info, this only exists on test network and may be wiped at any time...
        //Public: GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI
        //Secret: SCD74D46TJYXOUXFC5YOA72UTPCCVHK2GRSLKSPRB66VK6UJHQX2Y3R3

        public static async Task Main(string[] args)
        {
            //Network.UseTestNetwork();
            //using (var server = new Server("https://horizon-testnet.stellar.org"))
            //{
            //    //var friendBot = await server.TestNetFriendBot
            //    //    .FundAccount(KeyPair.Random())
            //    //    .Execute();

            //    //await GetLedgerTransactions(server);
            //    //await ShowAccountTransactions(server);
            //    ShowTestKeyValue(server);
            //}

            using (var server = new Server("https://horizon.stellar.org"))
            {
                Console.WriteLine("-- Streaming All New Ledgers On The Network --");
                await server.Ledgers
                    .Cursor("now")
                    .Stream((sender, response) => { ShowOperationResponse(server, sender, response); })
                    .Connect();
            }

            Console.ReadLine();
        }

        private static async Task ShowAccountTransactions(Server server)
        {
            Console.WriteLine("-- Show Account Transactions (ForAccount) --");

            var transactions = await server.Transactions
                .ForAccount("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI")
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
            Console.WriteLine($"Ledger: {tran.Ledger}, Hash: {tran.Hash}, Fee Paid: {tran.FeePaid}, Pt:{tran.PagingToken}");
        }

        private static async void ShowOperationResponse(Server server, object sender, LedgerResponse lr)
        {
            var operationRequestBuilder = server.Operations.ForLedger(lr.Sequence);
            var operations = await operationRequestBuilder.Execute();

            var accts = 0;
            var payments = 0;
            var offers = 0;
            var options = 0;

            foreach (var op in operations.Records)
                switch (op.Type)
                {
                    case "create_account":
                        accts++;
                        break;
                    case "payment":
                        payments++;
                        break;
                    case "manage_offer":
                        offers++;
                        break;
                    case "set_options":
                        options++;
                        break;
                }

            Console.WriteLine($"id: {lr.Sequence}, tx/ops: {lr.SuccessfulTransactionCount + "/" + lr.OperationCount}, accts: {accts}, payments: {payments}, offers: {offers}, options: {options}");
            Console.WriteLine($"Uri: {((LedgersRequestBuilder) sender).Uri}");
        }

        private static void ShowTestKeyValue(Server server)
        {
            Console.WriteLine("-- Getting TestKey for Account --");

            var data = server.Accounts.AccountData("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI", "TestKey");

            var dataResult = data.Result;

            Console.WriteLine("Encoded Value: " + dataResult.Value);
            Console.WriteLine("Decoded Value: " + dataResult.ValueDecoded);

            Console.WriteLine();
        }

    }
}