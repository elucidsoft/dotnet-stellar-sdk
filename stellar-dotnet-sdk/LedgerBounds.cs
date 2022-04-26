using System;
using System.Collections.Generic;
using System.Text;
using xdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// LedgerBounds are Preconditions of a transaction per<a href="https://github.com/stellar/stellar-protocol/blob/master/core/cap-0021.md#specification">CAP-21<a/>
    /// </summary>
    public class LedgerBounds
    {
        private readonly uint _minLedger;
        private readonly uint _maxLedger;

        public uint MinLedger => _minLedger;
        public uint MaxLedger => _maxLedger;

        public LedgerBounds(uint minLedger, uint maxLedger)
        {
            _minLedger = minLedger;
            _maxLedger = maxLedger;
        }

        public static LedgerBounds FromXdr(xdr.LedgerBounds xdrLedgerBounds)
        {
            return new LedgerBounds(xdrLedgerBounds.MinLedger.InnerValue, xdrLedgerBounds.MaxLedger.InnerValue);
        }

        public xdr.LedgerBounds ToXdr()
        {
            return new xdr.LedgerBounds()
            {
                MinLedger = new xdr.Uint32() { InnerValue = _minLedger },
                MaxLedger = new xdr.Uint32() { InnerValue = _maxLedger }
            };
        }
    }
}