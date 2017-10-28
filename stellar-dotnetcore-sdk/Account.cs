using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class Account : ITransactionBuilderAccount
    {
        private KeyPair _keyPair;
        private long _sequenceNumber;


        public KeyPair KeyPair { get { return _keyPair; } }
        public long SequenceNumber { get { return _sequenceNumber; } }

        /**
         * Class constructor.
         * @param keypair KeyPair associated with this Account
         * @param sequenceNumber Current sequence number of the account (can be obtained using java-stellar-sdk or horizon server)
         */
        public Account(KeyPair keypair, long? sequenceNumber)
        {
            _keyPair = keypair ?? throw new ArgumentNullException(nameof(keypair), "keypair cannot be null");
            _sequenceNumber = sequenceNumber ?? throw new ArgumentNullException(nameof(sequenceNumber), "sequenceNumber cannot be null");
        }


        public long GetIncrementedSequenceNumber()
        {
            return  SequenceNumber + 1;
        }

        /**
         * Increments sequence number in this object by one.
         */
        public void IncrementSequenceNumber()
        {
            _sequenceNumber++;
        }
    }
}
