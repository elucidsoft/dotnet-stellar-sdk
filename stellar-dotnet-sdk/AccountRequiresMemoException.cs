using System;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// AccountRequiresMemoException is thrown when a transaction is trying to submit a payment operation to an account
    /// that requires a memo.
    ///
    /// See SEP0029: https://github.com/stellar/stellar-protocol/blob/master/ecosystem/sep-0029.md
    /// </summary>
    public class AccountRequiresMemoException : Exception
    {
        private string _message;

        public AccountRequiresMemoException(string message, string accountId, Operation operation) : base(message)
        {
            _message = message;
            AccountId = accountId;
            Operation = operation;
        }

        /// <summary>
        /// AccountId which requires the memo.
        /// </summary>
        public string AccountId { get; }

        /// <summary>
        /// Operation where AccountId is the destination.
        /// </summary>
        public Operation Operation { get; }
    }
}