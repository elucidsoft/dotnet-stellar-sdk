using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Read a SEP 10 challenge transaction and return the client account id.
        ///
        /// Performs the following checks:
        ///
        ///   1. Transaction sequence number is 0
        ///   2. Transaction source account is <paramref name="serverAccountId"/>
        ///   3. Transaction has one operation only, of type ManageDataOperation
        ///   4. The ManageDataOperation name and value are correct
        ///   5. Transaction time bounds are still valid
        ///   6. Transaction is signed by server
        /// </summary>
        /// <param name="transaction">The challenge transaction</param>
        /// <param name="serverAccountId">The server account id</param>
        /// <param name="network">The network the transaction was submitted to, defaults to Network.Current</param>
        /// <param name="now">Current time, defaults to DateTimeOffset.Now</param>
        /// <returns>The client account id</returns>
        /// <exception cref="InvalidWebAuthenticationException"></exception>
        public static string ReadChallengeTransaction(Transaction transaction, string serverAccountId,
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
                throw new InvalidWebAuthenticationException(
                    "Challenge transaction operation must be of type ManageDataOperation");

            if (operation.SourceAccount is null)
                throw new InvalidWebAuthenticationException("Challenge transaction operation must have source account");

            var clientAccountId = operation.SourceAccount.Address;

            var stringValue = Encoding.UTF8.GetString(operation.Value);
            if (stringValue.Length != 64)
                throw new InvalidWebAuthenticationException(
                    "Challenge transaction operation data must be 64 bytes long");

            try
            {
                // There is no need to check for decoded value length since we know it's valid base64 and 64 bytes long.
                var _ = Convert.FromBase64String(stringValue);
            }
            catch (System.FormatException)
            {
                throw new InvalidWebAuthenticationException(
                    "Challenge transaction operation data must be base64 encoded");
            }

            if (!ValidateSignedBy(transaction, serverAccountId, network))
                throw new InvalidWebAuthenticationException("Challenge transaction not signed by server");

            if (!ValidateTimeBounds(transaction.TimeBounds, now ?? DateTimeOffset.Now))
                throw new InvalidWebAuthenticationException("Challenge transaction expired");

            return clientAccountId;
        }

        public static ICollection<string> VerifyChallengeTransactionThreshold(Transaction transaction,
            string serverAccountId,
            int threshold, Dictionary<string, int> signerSummary, Network network = null, DateTimeOffset? now = null)
        {
            var signersFound =
                VerifyChallengeTransactionSigners(transaction, serverAccountId, signerSummary.Keys.ToArray(), network,
                    now);
            var weight = signersFound.Sum(signer => signerSummary[signer]);
            if (weight < threshold)
                throw new InvalidWebAuthenticationException(
                    $"Signers with weight {weight} do not meet threshold {threshold}");
            return signersFound;
        }

        /// <summary>
        /// Verify that all signers of a SEP 10 transaction are accounted for.
        ///
        /// A transaction is verified if it signed by the server account, and all other signatures match the provided
        /// signers. Additional signers can be provided that do not have a signature, but all signatures must be
        /// matched to a signer for verification to succeed. If verification succeeds, the list of signers that were
        /// found is returned, excluding the server account id.
        /// </summary>
        /// <param name="transaction">The challenge transaction</param>
        /// <param name="serverAccountId">The server account id</param>
        /// <param name="signers"></param>
        /// <param name="network">The network the transaction was submitted to, defaults to Network.Current</param>
        /// <param name="now">Current time, defaults to DateTimeOffset.Now</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] VerifyChallengeTransactionSigners(Transaction transaction, string serverAccountId,
            ICollection<string> signers, Network network = null, DateTimeOffset? now = null)
        {
            if (!signers.Any())
                throw new ArgumentException($"{nameof(signers)} must be non-empty");

            network = network ?? Network.Current;

            ReadChallengeTransaction(transaction, serverAccountId, network, now);

            // Remove server signer if present
            var serverKeypair = KeyPair.FromAccountId(serverAccountId);
            var clientSigners = signers.Where(signer => signer != serverKeypair.Address).ToList();

            var allSigners = clientSigners.Select(signer => signer.Clone()).ToList();
            allSigners.Add(serverKeypair.Address);
            var allSignersFound = VerifyTransactionSignatures(transaction, signers, network);

            var serverSigner = allSignersFound.FirstOrDefault(signer => signer == serverKeypair.Address);
            if (serverSigner is null)
                throw new InvalidWebAuthenticationException("Challenge transaction not signed by server");

            if (allSignersFound.Count != transaction.Signatures.Count)
                throw new InvalidWebAuthenticationException("Challenge transaction has unrecognized signatures");

            return allSignersFound.Where(signer => signer != serverSigner).ToArray();
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
        [Obsolete("Use VerifyChallengeTransactionThreshold and VerifyChallengeTransactionSigners")]
        public static bool VerifyChallengeTransaction(Transaction transaction, string serverAccountId,
            Network network = null, DateTimeOffset? now = null)
        {
            network = network ?? Network.Current;

            var clientAccountId = ReadChallengeTransaction(transaction, serverAccountId, network, now);

            if (!ValidateSignedBy(transaction, clientAccountId, network))
                throw new InvalidWebAuthenticationException("Challenge transaction not signed by client");

            return true;
        }

        private static bool ValidateSignedBy(Transaction transaction, string accountId, Network network)
        {
            var signaturesUsed = VerifyTransactionSignatures(transaction, new[] {accountId}, network);
            return signaturesUsed.Count == 1;
        }

        private static bool ValidateTimeBounds(TimeBounds timeBounds, DateTimeOffset now)
        {
            if (timeBounds is null) return false;
            if (timeBounds.MinTime == 0 || timeBounds.MaxTime == 0) return false;
            var unixNow = now.ToUnixTimeSeconds();
            return timeBounds.MinTime <= unixNow && unixNow <= timeBounds.MaxTime;
        }

        private static ICollection<string> VerifyTransactionSignatures(Transaction transaction,
            IEnumerable<string> signers, Network network)
        {
            var txHash = transaction.Hash(network);
            var signaturesUsed = new Dictionary<xdr.DecoratedSignature, string>();

            foreach (var signer in signers)
            {
                var keypair = KeyPair.FromAccountId(signer);
                foreach (var signature in transaction.Signatures)
                {
                    if (signaturesUsed.ContainsKey(signature))
                        continue;

                    if (!signature.Hint.InnerValue.SequenceEqual(keypair.SignatureHint.InnerValue))
                        continue;

                    if (keypair.Verify(txHash, signature.Signature))
                    {
                        signaturesUsed[signature] = keypair.Address;
                    }
                }
            }

            return signaturesUsed.Values.ToArray();
        }
    }
}