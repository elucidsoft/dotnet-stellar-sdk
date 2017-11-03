namespace stellar_dotnetcore_sdk
{
    public interface ITransactionBuilderAccount
    {
        KeyPair KeyPair { get; }
        long SequenceNumber { get; }

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