using System;

namespace stellar_dotnetcore_sdk
{
    public class Account : ITransactionBuilderAccount
    {
        /**
         * Class constructor.
         * @param keypair KeyPair associated with this Account
         * @param sequenceNumber Current sequence number of the account (can be obtained using java-stellar-sdk or horizon server)
         */
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

        /**
         * Increments sequence number in this object by one.
         */
        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }
    }
}