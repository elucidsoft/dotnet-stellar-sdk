using System;
using System.Text;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Implement SEP-10: Stellar Web Authentication.
    /// https://github.com/stellar/stellar-protocol/blob/master/ecosystem/sep-0010.md
    /// </summary>
    public static class WebAuthentication
    {
        /// <summary>
        /// Build a challenge transaction you can use for Stellar Web Authentication.
        /// </summary>
        /// <param name="serverKeypair">Server signing keypair</param>
        /// <param name="clientAccountId">The client account id that needs authentication</param>
        /// <param name="anchorName">The anchor name</param>
        /// <param name="nonce">48 bytes long cryptographic-quality random data</param>
        /// <param name="now">The datetime from which the transaction is valid</param>
        /// <param name="timeout">The transaction lifespan</param>
        /// <param name="network">The network the transaction will be submitted to</param>
        /// <returns>The challenge transaction</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Transaction BuildChallengeTransaction(KeyPair serverKeypair, string clientAccountId,
            string anchorName, byte[] nonce = null, DateTimeOffset? now = null, TimeSpan? timeout = null,
            Network network = null)
        {
            if (serverKeypair is null) throw new ArgumentNullException(nameof(serverKeypair));
            if (string.IsNullOrEmpty(clientAccountId)) throw new ArgumentNullException(nameof(clientAccountId));
            if (string.IsNullOrEmpty(anchorName)) throw new ArgumentNullException(nameof(anchorName));

            if (nonce is null)
            {
                var rng = NSec.Cryptography.RandomGenerator.Default;
                // A base64 digit represents 6 bites, to generate 64 bytes long base64 encoded strings
                // we need 64 * 6 / 8 bytes.
                nonce = rng.GenerateBytes(48);
            }
            else if (nonce.Length != 48)
            {
                throw new ArgumentException("nonce must be 48 bytes long");
            }

            network = network ?? Network.Current;
            var validFrom = now ?? DateTimeOffset.Now;
            var validTo = validFrom.Add(timeout ?? TimeSpan.FromMinutes(5.0));

            var sourceAccountKeypair = KeyPair.FromAccountId(clientAccountId);

            // Sequence number is incremented by 1 before building the transaction, set it to -1 to have 0
            var serverAccount = new Account(serverKeypair.AccountId, -1);

            var manageDataKey = $"{anchorName} auth";
            var manageDataValue = Encoding.UTF8.GetBytes(Convert.ToBase64String(nonce));

            var timeBounds = new TimeBounds(validFrom, validTo);

            var operation = new ManageDataOperation.Builder(manageDataKey, manageDataValue)
                .SetSourceAccount(sourceAccountKeypair)
                .Build();

            var tx = new Transaction.Builder(serverAccount)
                .AddTimeBounds(timeBounds)
                .AddOperation(operation)
                .Build();

            tx.Sign(serverKeypair, network);

            return tx;
        }

        /// <summary>
        /// Verify that a transaction is a valid Stellar Web Authentication transaction.
        ///
        /// Performs the following checks:
        ///
        ///   1. Transaction sequence number is 0
        ///   2. Transaction source account is <paramref name="serverAccountId"/>
        ///   3. Transaction has one operation only, of type ManageDataOperation
        ///   4. The ManageDataOperation name and value are correct
        ///   5. Transaction time bounds are still valid
        ///   6. Transaction is signed by server and client
        /// </summary>
        /// <param name="transaction">The challenge transaction</param>
        /// <param name="serverAccountId">The server account id</param>
        /// <param name="network">The network the transaction was submitted to, defaults to Network.Current</param>
        /// <param name="now">Current time, defaults to DateTimeOffset.Now</param>
        /// <returns>True if the transaction is valid</returns>
        /// <exception cref="InvalidWebAuthenticationException"></exception>
        public static bool VerifyChallengeTransaction(Transaction transaction, string serverAccountId,
            Network network = null, DateTimeOffset? now = null)
        {
            network = network ?? Network.Current;

            if (transaction.SequenceNumber != 0)
                throw new InvalidWebAuthenticationException("Challenge transaction sequence number must be 0");

            if (transaction.SourceAccount.AccountId != serverAccountId)
                throw new InvalidWebAuthenticationException("Challenge transaction source must be serverAccountId");

            if (transaction.Operations.Length != 1)
                throw new InvalidWebAuthenticationException("Challenge transaction must contain one operation");

            var operation = transaction.Operations[0] as ManageDataOperation;

            if (operation is null)
                throw new InvalidWebAuthenticationException("Challenge transaction operation must be of type ManageDataOperation");

            var stringValue = Encoding.UTF8.GetString(operation.Value);
            if (stringValue.Length != 64)
                throw new InvalidWebAuthenticationException("Challenge transaction operation data must be 64 bytes long");

            try
            {
                // There is no need to check for decoded value length since we know it's valid base64 and 64 bytes long.
                var _ = Convert.FromBase64String(stringValue);
            }
            catch (System.FormatException)
            {
                throw new InvalidWebAuthenticationException("Challenge transaction operation data must be base64 encoded");
            }

            if (!ValidateSignedBy(transaction, serverAccountId, network))
                throw new InvalidWebAuthenticationException("Challenge transaction not signed by server");

            if (!ValidateSignedBy(transaction, operation.SourceAccount.AccountId, network))
                throw new InvalidWebAuthenticationException("Challenge transaction not signed by client");

            if (!ValidateTimeBounds(transaction.TimeBounds, now ?? DateTimeOffset.Now))
                throw new InvalidWebAuthenticationException("Challenge transaction expired");

            return true;
        }

        private static bool ValidateSignedBy(Transaction transaction, string accountId, Network network)
        {
            var transactionHash = transaction.Hash(network);
            var keypair = KeyPair.FromAccountId(accountId);

            var signature = transaction.Signatures.Find(sig => keypair.Verify(transactionHash, sig.Signature));
            return signature != null;
        }

        private static bool ValidateTimeBounds(TimeBounds timeBounds, DateTimeOffset now)
        {
            if (timeBounds is null) return false;
            if (timeBounds.MinTime == 0 || timeBounds.MaxTime == 0) return false;
            var unixNow = now.ToUnixTimeSeconds();
            return timeBounds.MinTime <= unixNow && unixNow <= timeBounds.MaxTime;
        }
    }
}