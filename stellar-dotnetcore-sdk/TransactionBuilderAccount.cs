using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public interface TransactionBuilderAccount
    {
        /**
         * Returns keypair associated with this Account
        */
        KeyPair GetKeypair();

        /**
         * Returns current sequence number ot this Account.
         */
        long GetSequenceNumber();

        /**
         * Returns sequence number incremented by one, but does not increment internal counter.
         */
        long GetIncrementedSequenceNumber();

        /**
         * Increments sequence number in this object by one.
         */
        void IncrementSequenceNumber();
    }
}
