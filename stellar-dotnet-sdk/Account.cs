using System;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Returns information and links relating to a single account.
    /// </summary>
    public class Account : ITransactionBuilderAccount
    {
        ///<summary>
        /// Class constructor.
        /// </summary>
        /// <param name="accountId">KeyPair associated with this Account</param>
        /// <param name="sequenceNumber">Current sequence number of the account (can be obtained using dotnet-stellar-sdk or horizon server)</param>
        public Account(string accountId, long? sequenceNumber)
        {
            if (accountId is null) throw new ArgumentNullException(nameof(accountId), "accountId cannot be null");

            MuxedAccount = KeyPair.FromAccountId(accountId);
            SequenceNumber = sequenceNumber ?? throw new ArgumentNullException(nameof(sequenceNumber), "sequenceNumber cannot be null");
        }

        ///<summary>
        /// Class constructor.
        /// </summary>
        /// <param name="muxedAccount">KeyPair associated with this Account</param>
        /// <param name="sequenceNumber">Current sequence number of the account (can be obtained using dotnet-stellar-sdk or horizon server)</param>
        public Account(IAccountId muxedAccount, long? sequenceNumber)
        {
            MuxedAccount = muxedAccount ?? throw new ArgumentNullException(nameof(muxedAccount), "muxedAccount cannot be null");
            SequenceNumber = sequenceNumber ?? throw new ArgumentNullException(nameof(sequenceNumber), "sequenceNumber cannot be null");
        }

        /// <summary>
        /// Returns the AccountID of the account.
        /// </summary>
        public string AccountId => MuxedAccount.AccountId;

        /// <summary>
        /// Returns the KeyPair of the account.
        /// </summary>
        public KeyPair KeyPair
        {
            get
            {
                switch (MuxedAccount)
                {
                    case KeyPair kp:
                        return kp;
                    case MuxedAccountMed25519 ma:
                        return ma.Key;
                    default:
                        throw new Exception("Invalid Account MuxedAccount type");
                }
            }
        }

        public IAccountId MuxedAccount { get; }

        /// <summary>
        /// The sequence number
        /// </summary>
        public long SequenceNumber { get; private set; }

        /// <summary>
        /// Returns the Sequence number incremented by one.
        /// </summary>
        /// <returns>SequenceNumber + 1</returns>
        public long IncrementedSequenceNumber => SequenceNumber + 1;

        ///<summary>
        /// Increments sequence number in this object by one.
        ///</summary>
        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }
    }
}