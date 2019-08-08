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
        /// <param name="serverKeypair">server signing keypair</param>
        /// <param name="clientAccountId">the client account id that needs authentication</param>
        /// <param name="anchorName">anchor name</param>
        /// <param name="nonce">48 bytes long cryptographic-quality random data</param>
        /// <param name="now">datetime from which the transaction is valid</param>
        /// <param name="timeout">transaction lifespan</param>
        /// <param name="network">network the transaction will be submitted to</param>
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
    }
}