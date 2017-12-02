using System;

namespace stellar_dotnetcore_sdk
{
    /// <summary>
    /// Returns information and links relating to a single account.
    /// </summary>
    public class Account : ITransactionBuilderAccount
    {
        ///<summary>
        /// Class constructor.
        /// </summary>
        /// <param name="keypair">KeyPair associated with this Account</param> 
        /// <param name="sequenceNumber">Current sequence number of the account (can be obtained using java-stellar-sdk or horizon server)</param> 
        public Account(KeyPair keypair, long? sequenceNumber)
        {
            KeyPair = keypair ?? throw new ArgumentNullException(nameof(keypair), "keypair cannot be null");
            SequenceNumber = sequenceNumber ?? throw new ArgumentNullException(nameof(sequenceNumber), "sequenceNumber cannot be null");
        }


        /// <summary>
        /// 
        /// </summary>
        public KeyPair KeyPair { get; }

        /// <summary>
        /// The sequence number
        /// </summary>
        public long SequenceNumber { get; private set; }


        /// <summary>
        /// Returns the Sequence number incremented by one.
        /// </summary>
        /// <returns>SequenceNumber + 1</returns>
        public long GetIncrementedSequenceNumber()
        {
            return SequenceNumber + 1;
        }

        ///<summary>
        /// Increments sequence number in this object by one.
        ///</summary>
        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }
    }
}