using System;

namespace stellar_dotnetcore_sdk
{
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


        public KeyPair KeyPair { get; }

        public long SequenceNumber { get; private set; }


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