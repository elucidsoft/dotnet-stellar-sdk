using System;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="SetOptionsOp"/>.
    /// Use <see cref="Builder"/> to create a new SetOptionsOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#set-options">Set Options</see>
    /// </summary>
    public class SetOptionsOperation : Operation
    {
        private SetOptionsOperation(KeyPair inflationDestination, uint? clearFlags, uint? setFlags,
            uint? masterKeyWeight, uint? lowThreshold, uint? mediumThreshold,
            uint? highThreshold, string homeDomain, sdkxdr.SignerKey signer, uint? signerWeight)
        {
            InflationDestination = inflationDestination;
            ClearFlags = clearFlags;
            SetFlags = setFlags;
            MasterKeyWeight = masterKeyWeight;
            LowThreshold = lowThreshold;
            MediumThreshold = mediumThreshold;
            HighThreshold = highThreshold;
            HomeDomain = homeDomain;
            Signer = signer;
            SignerWeight = signerWeight;
        }

        public KeyPair InflationDestination { get; }

        public uint? ClearFlags { get; }

        public uint? SetFlags { get; }

        public uint? MasterKeyWeight { get; }

        public uint? LowThreshold { get; }

        public uint? MediumThreshold { get; }

        public uint? HighThreshold { get; }

        public string HomeDomain { get; }

        public sdkxdr.SignerKey Signer { get; }

        public uint? SignerWeight { get; }

        public override OperationThreshold Threshold
        {
            get => OperationThreshold.High;
        }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.SetOptionsOp();
            if (InflationDestination != null)
            {
                var inflationDestination = new sdkxdr.AccountID();
                inflationDestination.InnerValue = InflationDestination.XdrPublicKey;
                op.InflationDest = inflationDestination;
            }

            if (ClearFlags != null)
            {
                var clearFlags = new sdkxdr.Uint32();
                clearFlags.InnerValue = ClearFlags.Value;
                op.ClearFlags = clearFlags;
            }

            if (SetFlags != null)
            {
                var setFlags = new sdkxdr.Uint32();
                setFlags.InnerValue = SetFlags.Value;
                op.SetFlags = setFlags;
            }

            if (MasterKeyWeight != null)
            {
                var uint32 = new sdkxdr.Uint32();
                uint32.InnerValue = MasterKeyWeight.Value;
                op.MasterWeight = uint32;
            }

            if (LowThreshold != null)
            {
                var uint32 = new sdkxdr.Uint32();
                uint32.InnerValue = LowThreshold.Value;
                op.LowThreshold = uint32;
            }

            if (MediumThreshold != null)
            {
                var uint32 = new sdkxdr.Uint32();
                uint32.InnerValue = MediumThreshold.Value;
                op.MedThreshold = uint32;
            }

            if (HighThreshold != null)
            {
                var uint32 = new sdkxdr.Uint32();
                uint32.InnerValue = HighThreshold.Value;
                op.HighThreshold = uint32;
            }

            if (HomeDomain != null)
            {
                var homeDomain = new sdkxdr.String32();
                homeDomain.InnerValue = HomeDomain;
                op.HomeDomain = homeDomain;
            }

            if (Signer != null)
            {
                var signer = new sdkxdr.Signer();
                var weight = new sdkxdr.Uint32();
                weight.InnerValue = SignerWeight.Value & 0xFF;
                signer.Key = Signer;
                signer.Weight = weight;
                op.Signer = signer;
            }

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.SET_OPTIONS);
            body.SetOptionsOp = op;
            return body;
        }

        /// <summary>
        ///     Builds SetOptions operation.
        /// </summary>
        /// <see cref="SetOptionsOperation" />
        public class Builder
        {
            private uint? _clearFlags;
            private uint? _highThreshold;
            private string _homeDomain;
            private KeyPair _inflationDestination;
            private uint? _lowThreshold;
            private uint? _masterKeyWeight;
            private uint? _mediumThreshold;
            private uint? _setFlags;
            private SignerKey _signer;
            private uint? _signerWeight;
            private KeyPair _sourceAccount;

            public Builder(sdkxdr.SetOptionsOp op)
            {
                if (op.InflationDest != null)
                    _inflationDestination = KeyPair.FromXdrPublicKey(op.InflationDest.InnerValue);
                if (op.ClearFlags != null)
                    _clearFlags = op.ClearFlags.InnerValue;
                if (op.SetFlags != null)
                    _setFlags = op.SetFlags.InnerValue;
                if (op.MasterWeight != null)
                    _masterKeyWeight = op.MasterWeight.InnerValue;
                if (op.LowThreshold != null)
                    _lowThreshold = op.LowThreshold.InnerValue;
                if (op.MedThreshold != null)
                    _mediumThreshold = op.MedThreshold.InnerValue;
                if (op.HighThreshold != null)
                    _highThreshold = op.HighThreshold.InnerValue;
                if (op.HomeDomain != null)
                    _homeDomain = op.HomeDomain.InnerValue;
                if (op.Signer != null)
                {
                    _signer = op.Signer.Key;
                    _signerWeight = op.Signer.Weight.InnerValue & 0xFF;
                }
            }

            /// <summary>
            ///     Creates a new SetOptionsOperation builder.
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Sets the inflation destination for the account.
            /// </summary>
            /// <param name="inflationDestination">The inflation destination account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetInflationDestination(KeyPair inflationDestination)
            {
                _inflationDestination = inflationDestination;
                return this;
            }

            /// <summary>
            ///     Clears the given flags from the account.
            /// </summary>
            /// <param name="clearFlags">
            ///     For details about the flags, please refer to the
            ///     <a href="https://www.stellar.org/developers/learn/concepts/accounts.html" target="_blank">accounts doc</a>.
            /// </param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetClearFlags(uint clearFlags)
            {
                _clearFlags = clearFlags;
                return this;
            }

            public Builder SetClearFlags(int clearFlags)
            {
                if (clearFlags < 0) throw new ArgumentException("clearFlags must be non negative");
                return SetClearFlags((uint)clearFlags);
            }

            /// <summary>
            ///     Sets the given flags on the account.
            /// </summary>
            /// <param name="setFlags">
            ///     For details about the flags, please refer to the
            ///     <a href="https://www.stellar.org/developers/learn/concepts/accounts.html" target="_blank">accounts doc</a>.
            /// </param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSetFlags(uint setFlags)
            {
                _setFlags = setFlags;
                return this;
            }

            public Builder SetSetFlags(int setFlags)
            {
                if (setFlags < 0) throw new ArgumentException("setFlags must be non negative");
                return SetSetFlags((uint)setFlags);
            }

            /// <summary>
            ///     Weight of the master key.
            /// </summary>
            /// <param name="masterKeyWeight">Number between 0 and 255</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetMasterKeyWeight(uint masterKeyWeight)
            {
                _masterKeyWeight = masterKeyWeight;
                return this;
            }

            public Builder SetMasterKeyWeight(int masterKeyWeight)
            {
                if (masterKeyWeight < 0) throw new ArgumentException("masterKeyWeight must be non negative");
                return SetMasterKeyWeight((uint)masterKeyWeight);
            }

            /// <summary>
            ///     A number from 0-255 representing the threshold this account sets on all operations it performs that have a low
            ///     threshold.
            /// </summary>
            /// <param name="lowThreshold">Number between 0 and 255</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetLowThreshold(uint lowThreshold)
            {
                _lowThreshold = lowThreshold;
                return this;
            }

            public Builder SetLowThreshold(int lowThreshold)
            {
                if (lowThreshold < 0) throw new ArgumentException("lowThreshold must be non negative");
                return SetLowThreshold((uint)lowThreshold);
            }

            /// <summary>
            ///     A number from 0-255 representing the threshold this account sets on all operations it performs that have a medium
            ///     threshold.
            /// </summary>
            /// <param name="mediumThreshold">Number between 0 and 255</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetMediumThreshold(uint mediumThreshold)
            {
                _mediumThreshold = mediumThreshold;
                return this;
            }

            public Builder SetMediumThreshold(int mediumThreshold)
            {
                if (mediumThreshold < 0) throw new ArgumentException("mediumThreshold must be non negative");
                return SetMediumThreshold((uint)mediumThreshold);
            }

            /// <summary>
            ///     A number from 0-255 representing the threshold this account sets on all operations it performs that have a high
            ///     threshold.
            /// </summary>
            /// <param name="highThreshold">Number between 0 and 255</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetHighThreshold(uint highThreshold)
            {
                _highThreshold = highThreshold;
                return this;
            }

            public Builder SetHighThreshold(int highThreshold)
            {
                if (highThreshold < 0) throw new ArgumentException("highThreshold must be non negative");
                return SetHighThreshold((uint)highThreshold);
            }

            /// <summary>
            ///     Sets the account's home domain address used in
            ///     <a href="https://www.stellar.org/developers/learn/concepts/federation.html" target="_blank">Federation</a>.
            /// </summary>
            /// <param name="homeDomain">A string of the address which can be up to 32 characters.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetHomeDomain(string homeDomain)
            {
                if (homeDomain.Length > 32)
                    throw new ArgumentException("Home domain must be <= 32 characters");
                _homeDomain = homeDomain;
                return this;
            }

            /// <summary>
            ///     Add, update, or remove a signer from the account. Signer is deleted if the weight = 0;
            /// </summary>
            /// <param name="signer">The signer key. Use <see cref="stellar_dotnet_sdk.Signer" /> helper to create this object.</param>
            /// <param name="weight">The weight to attach to the signer (0-255).</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSigner(SignerKey signer, uint weight)
            {
                _signer = signer ?? throw new ArgumentNullException(nameof(signer), "signer cannot be null");

                _signerWeight = weight & 0xFF;
                return this;
            }

            public Builder SetSigner(SignerKey signer, uint? weight)
            {
                if (weight == null)
                    throw new ArgumentNullException(nameof(weight), "weight cannot be null");
                return SetSigner(signer, weight.Value);
            }

            public Builder SetSigner(SignerKey signer, int weight)
            {
                if (weight < 0) throw new ArgumentException("weight must be non negative");
                return SetSigner(signer, (uint)weight);
            }

            public Builder SetSigner(SignerKey signer, int? weight)
            {
                if (weight == null)
                    throw new ArgumentNullException(nameof(weight), "weight cannot be null");
                return SetSigner(signer, weight.Value);
            }

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _sourceAccount = sourceAccount;
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public SetOptionsOperation Build()
            {
                var operation = new SetOptionsOperation(_inflationDestination, _clearFlags,
                    _setFlags, _masterKeyWeight, _lowThreshold, _mediumThreshold, _highThreshold,
                    _homeDomain, _signer, _signerWeight);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}