namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Operation threshold level.
    /// </summary>
    public enum OperationThreshold
    {
        /// <summary>
        /// Low security level.
        /// <seealso cref="AllowTrustOperation"/>
        /// <seealso cref="BumpSequenceOperation"/>
        /// </summary>
        Low = 1,

        /// <summary>
        /// Medium security level.
        /// <seealso cref="ChangeTrustOperation"/>
        /// <seealso cref="CreateAccountOperation"/>
        /// <seealso cref="CreatePassiveSellOfferOperation"/>
        /// <seealso cref="ManageDataOperation"/>
        /// <seealso cref="ManageSellOfferOperation"/>
        /// <seealso cref="PathPaymentOperation"/>
        /// <seealso cref="PaymentOperation"/>
        /// </summary>
        Medium = 2,

        /// <summary>
        /// High security level.
        /// <seealso cref="SetOptionsOperation"/>
        /// <seealso cref="AccountMergeOperation"/>
        /// </summary>
        High = 3,
    }
}