namespace stellar_dotnetcore_sdk
{
    public interface ITransactionBuilderAccount
    {
        KeyPair KeyPair { get; }
        long SequenceNumber { get; }

        ///<summary>
        /// Returns sequence number incremented by one, but does not increment internal counter.
        ///</summary>
        long GetIncrementedSequenceNumber();

        ///<summary>
        /// Increments sequence number in this object by one.
        ///</summary>
        void IncrementSequenceNumber();
    }
}