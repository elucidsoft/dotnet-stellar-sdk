using System;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk;

public abstract class SCVal
{
    public xdr.SCVal ToXdr()
    {
        return this switch
        {
            SCBool scBool => scBool.ToSCValXdr(),
            SCVoid scVoid => scVoid.ToSCValXdr(),
            SCError scError => scError.ToSCValXdr(),
            SCUint32 scUint32 => scUint32.ToSCValXdr(),
            SCInt32 scInt32 => scInt32.ToSCValXdr(),
            SCUint64 scUint64 => scUint64.ToSCValXdr(),
            SCInt64 scInt64 => scInt64.ToSCValXdr(),
            SCTimePoint scTimePoint => scTimePoint.ToSCValXdr(),
            SCDuration scDuration => scDuration.ToSCValXdr(),
            SCUint128 scUint128 => scUint128.ToSCValXdr(),
            SCInt128 scInt128 => scInt128.ToSCValXdr(),
            SCUint256 scUint256 => scUint256.ToSCValXdr(),
            SCInt256 scInt256 => scInt256.ToSCValXdr(),
            SCBytes scBytes => scBytes.ToSCValXdr(),
            SCString scString => scString.ToSCValXdr(),
            SCSymbol scSymbol => scSymbol.ToSCValXdr(),
            SCVec scVec => scVec.ToSCValXdr(),
            SCMap scMap => scMap.ToSCValXdr(),
            SCAddress scAddress => scAddress.ToSCValXdr(),
            SCContractInstance scContractInstance => scContractInstance.ToSCValXdr(),
            SCLedgerKeyContractInstance scLedgerKeyContractInstance => scLedgerKeyContractInstance.ToSCValXdr(),
            SCNonceKey scNonceKey => scNonceKey.ToSCValXdr(),
            _ => throw new InvalidOperationException("Unknown SCVal type")
        };
    }

    public static SCVal FromXdr(xdr.SCVal xdrVal)
    {
        return xdrVal.Discriminant.InnerValue switch
        {
            SCValType.SCValTypeEnum.SCV_BOOL => SCBool.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_VOID => SCVoid.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_ERROR => SCError.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_U32 => SCUint32.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_I32 => SCInt32.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_U64 => SCUint64.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_I64 => SCInt64.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_TIMEPOINT => SCTimePoint.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_DURATION => SCDuration.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_U128 => SCUint128.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_I128 => SCInt128.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_U256 => SCUint256.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_I256 => SCInt256.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_BYTES => SCBytes.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_STRING => SCString.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_SYMBOL => SCSymbol.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_VEC => SCVec.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_MAP => SCMap.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_ADDRESS => SCAddress.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_CONTRACT_INSTANCE => SCContractInstance.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_LEDGER_KEY_CONTRACT_INSTANCE => SCLedgerKeyContractInstance.FromSCValXdr(xdrVal),
            SCValType.SCValTypeEnum.SCV_LEDGER_KEY_NONCE => SCNonceKey.FromSCValXdr(xdrVal),
            _ => throw new InvalidOperationException("Unknown SCVal type")
        };
    }
    
    /// <summary>
    /// Creates a new SCVal object from the given SCVal XDR base64 string.
    /// </summary>
    /// <param name="xdrBase64"></param>
    /// <returns>SCVal object</returns>
    public static SCVal FromXdrBase64(string xdrBase64)
    {
        var bytes = Convert.FromBase64String(xdrBase64);
        var reader = new XdrDataInputStream(bytes);
        var thisXdr = xdr.SCVal.Decode(reader);
        return FromXdr(thisXdr);
    }
    
    ///<summary>
    /// Returns base64-encoded SCVal XDR object.
    ///</summary>
    public string ToXdrBase64()
    {
        var xdrValue = ToXdr();
        var writer = new XdrDataOutputStream();
        xdr.SCVal.Encode(writer, xdrValue);
        return Convert.ToBase64String(writer.ToArray());
    }
}

public class SCBool : SCVal
{
    public SCBool(bool value)
    {
        InnerValue = value;
    }
    public bool InnerValue { get; set; }

    public new bool ToXdr()
    {
        return InnerValue;
    }

    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_BOOL,
            },
            B = ToXdr(),
        };
    }
    
    public static SCBool FromXdr(bool xdrBool)
    {
        return new SCBool(xdrBool);
    }
    
    public static SCBool FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_BOOL)
            throw new ArgumentException("Not an SCBool", nameof(xdrVal));

        return FromXdr(xdrVal.B);
    }
}

public class SCVoid : SCVal
{
    public new void ToXdr() { }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_VOID,
            },
        };
    }

    public static SCVoid FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_VOID)
            throw new ArgumentException("Not an SCVoid", nameof(xdrVal));

        return FromXdr();  
    } 
    
    public static SCVoid FromXdr()
    {
        return new SCVoid();
    }
}

public abstract class SCError : SCVal
{
    public SCErrorCode.SCErrorCodeEnum Code { get; set; }
    public xdr.SCError ToXdr()
    {
        return this switch
        {
            SCContractError scContractError => scContractError.ToSCErrorXdr(),
            SCWasmVmError scWasmVmError => scWasmVmError.ToSCErrorXdr(),
            SCContextError scContextError => scContextError.ToSCErrorXdr(),
            SCStorageError scStorageError => scStorageError.ToSCErrorXdr(),
            SCObjectError scObjectError => scObjectError.ToSCErrorXdr(),
            SCCryptoError scCryptoError => scCryptoError.ToSCErrorXdr(),
            SCEventsError scEventsError => scEventsError.ToSCErrorXdr(),
            SCBudgetError scBudgetError => scBudgetError.ToSCErrorXdr(),
            SCValueError scValueError => scValueError.ToSCErrorXdr(),
            SCAuthError scAuthError => scAuthError.ToSCErrorXdr(),
            _ => throw new InvalidOperationException("Unknown SCVal type")
        };
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_ERROR,
            },
            Error = ToXdr(),
        };
    }
    
    public static SCError FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_ERROR)
            throw new ArgumentException("Not an SCError", nameof(xdrVal));

        return FromXdr(xdrVal.Error);
    }

    public static SCError FromXdr(xdr.SCError xdrSCError)
    {
        return xdrSCError.Discriminant.InnerValue switch
        {
            SCErrorType.SCErrorTypeEnum.SCE_CONTRACT => SCContractError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_WASM_VM => SCWasmVmError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_CONTEXT => SCContextError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_STORAGE => SCStorageError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_OBJECT => SCObjectError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_CRYPTO => SCCryptoError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_EVENTS => SCEventsError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_BUDGET => SCBudgetError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_VALUE => SCValueError.FromSCErrorXdr(xdrSCError),
            SCErrorType.SCErrorTypeEnum.SCE_AUTH => SCAuthError.FromSCErrorXdr(xdrSCError),
            _ => throw new InvalidOperationException("Unknown SCError type")
        };
    }
}

public class SCContractError : SCError
{
    public SCContractError(uint value)
    {
        ContractCode = value;
    }
    public uint ContractCode { get; set; }

    public static SCContractError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCContractError(xdrSCError.ContractCode.InnerValue);
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_CONTRACT,
            },
            ContractCode = new xdr.Uint32(ContractCode),
        };
    }
}

public class SCWasmVmError : SCError
{
    public static SCWasmVmError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCWasmVmError()
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_WASM_VM,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCContextError : SCError
{
    public static SCContextError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCContextError()
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_CONTEXT,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCStorageError : SCError
{
    public static SCStorageError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCStorageError()
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_STORAGE,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCObjectError : SCError
{
    public static SCObjectError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCObjectError
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_OBJECT,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCCryptoError : SCError
{
    public static SCCryptoError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCCryptoError
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_CRYPTO,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCEventsError : SCError
{
    public static SCEventsError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCEventsError
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_EVENTS,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCBudgetError : SCError
{
    public static SCBudgetError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCBudgetError
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_BUDGET,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCValueError : SCError
{
    public static SCValueError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCValueError()
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_VALUE,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCAuthError : SCError
{
    public static SCAuthError FromSCErrorXdr(xdr.SCError xdrSCError)
    {
        return new SCAuthError
        {
            Code = xdrSCError.Code.InnerValue
        };
    }
    
    public xdr.SCError ToSCErrorXdr()
    {
        return new xdr.SCError
        {
            Discriminant = new xdr.SCErrorType
            {
                InnerValue = xdr.SCErrorType.SCErrorTypeEnum.SCE_AUTH,
            },
            Code = SCErrorCode.Create(Code)
        };
    }
}

public class SCUint32 : SCVal
{
    public SCUint32(uint value)
    {
        InnerValue = value;
    }
    
    public uint InnerValue { get; set; }
    
    public xdr.Uint32 ToXdr()
    {
        return new Uint32(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_U32,
            },
            U32 = ToXdr(),
        };
    }
    
    public static SCUint32 FromXdr(xdr.Uint32 xdrUint32)
    {
        return new SCUint32(xdrUint32.InnerValue);
    }
    
    public static SCUint32 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_U32)
            throw new ArgumentException("Not an SCUint32", nameof(xdrVal));

        return FromXdr(xdrVal.U32);
    }
}

public class SCInt32 : SCVal
{
    public SCInt32(int value)
    {
        InnerValue = value;
    }

    public int InnerValue { get; set; }

    public xdr.Int32 ToXdr()
    {
        return new xdr.Int32(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_I32,
            },
            I32 = ToXdr(),
        };
    }
    
    public static SCInt32 FromXdr(xdr.Int32 xdrInt32)
    {
        return new SCInt32(xdrInt32.InnerValue);
    }
    
    public static SCInt32 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_I32)
            throw new ArgumentException("Not an SCInt32", nameof(xdrVal));

        return FromXdr(xdrVal.I32);
    }
}

public class SCUint64 : SCVal
{
    public ulong InnerValue { get; set; }
    
    public xdr.Uint64 ToXdr()
    {
        return new xdr.Uint64(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_U64,
            },
            U64 = ToXdr(),
        };
    }
    
    public static SCUint64 FromXdr(xdr.Uint64 xdrUint64)
    {
        return new SCUint64
        {
            InnerValue = xdrUint64.InnerValue,
        };
    }
    
    public static SCUint64 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_U64)
            throw new ArgumentException("Not an SCUint64", nameof(xdrVal));

        return FromXdr(xdrVal.U64);
    }
}

public class SCInt64 : SCVal
{
    public SCInt64(long value)
    {
        InnerValue = value;
    }
    
    public long InnerValue { get; set; }
    
    public xdr.Int64 ToXdr()
    {
        return new xdr.Int64(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_I64,
            },
            I64 = ToXdr(),
        };
    }
    
    public static SCInt64 FromXdr(xdr.Int64 xdrInt64)
    {
        return new SCInt64(xdrInt64.InnerValue);
    }
    
    public static SCInt64 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_I64)
            throw new ArgumentException("Not an SCInt64", nameof(xdrVal));

        return FromXdr(xdrVal.I64);
    }
}

public class SCTimePoint : SCVal
{
    public SCTimePoint(ulong value)
    {
        InnerValue = value;
    }
    
    public ulong InnerValue { get; set; }
    
    public xdr.TimePoint ToXdr()
    {
        return new xdr.TimePoint(new xdr.Uint64(InnerValue));
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_TIMEPOINT,
            },
            Timepoint = ToXdr(),
        };
    }
    
    public static SCTimePoint FromXdr(xdr.TimePoint xdrTimePoint)
    {
        return new SCTimePoint(xdrTimePoint.InnerValue.InnerValue);
    }
    
    public static SCTimePoint FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_TIMEPOINT)
            throw new ArgumentException("Not an SCTimePoint", nameof(xdrVal));

        return FromXdr(xdrVal.Timepoint);
    }
}

public class SCDuration : SCVal
{
    public SCDuration(ulong value)
    {
        InnerValue = value;
    }
    
    public ulong InnerValue { get; set; }
    
    public xdr.Duration ToXdr()
    {
        return new xdr.Duration(new xdr.Uint64(InnerValue));
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_DURATION,
            },
            Duration = ToXdr(),
        };
    }
    
    public static SCDuration FromXdr(xdr.Duration xdrDuration)
    {
        return new SCDuration(xdrDuration.InnerValue.InnerValue);
    }
    
    public static SCDuration FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_DURATION)
            throw new ArgumentException("Not an SCDuration", nameof(xdrVal));

        return FromXdr(xdrVal.Duration);
    }
}

public class SCUint128 : SCVal
{
    public ulong Lo { get; set; }
    public ulong Hi { get; set; }
    
    public xdr.UInt128Parts ToXdr()
    {
        return new xdr.UInt128Parts
        {
            Lo = new xdr.Uint64(Lo),
            Hi = new xdr.Uint64(Hi),
        };
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_U128,
            },
            U128 = ToXdr(),
        };
    }
    
    public static SCUint128 FromXdr(xdr.UInt128Parts xdrUInt128Parts)
    {
        return new SCUint128
        {
            Lo = xdrUInt128Parts.Lo.InnerValue,
            Hi = xdrUInt128Parts.Hi.InnerValue,
        };
    }
    
    public static SCUint128 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_U128)
            throw new ArgumentException("Not an SCUint128", nameof(xdrVal));

        return FromXdr(xdrVal.U128);
    }
}

public class SCInt128 : SCVal
{
    public ulong Lo { get; set; }
    public long Hi { get; set; }
    
    public xdr.Int128Parts ToXdr()
    {
        return new xdr.Int128Parts
        {
            Lo = new xdr.Uint64(Lo),
            Hi = new xdr.Int64(Hi),
        };
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_I128,
            },
            I128 = new xdr.Int128Parts
            {
                Hi = new xdr.Int64(Hi),
                Lo = new xdr.Uint64(Lo),
            },
        };
    }
    
    public static SCInt128 FromXdr(xdr.Int128Parts xdrInt128Parts)
    {
        return new SCInt128
        {
            Lo = xdrInt128Parts.Lo.InnerValue,
            Hi = xdrInt128Parts.Hi.InnerValue,
        };
    }
    
    public static SCInt128 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_I128)
            throw new ArgumentException("Not an SCInt128", nameof(xdrVal));

        return new SCInt128
        {
            Hi = xdrVal.I128.Hi.InnerValue,
            Lo = xdrVal.I128.Lo.InnerValue,
        };
    }
}

public class SCUint256 : SCVal
{
    public ulong HiHi { get; set; }
    public ulong HiLo { get; set; }
    public ulong LoHi { get; set; }
    public ulong LoLo { get; set; }
    
    public xdr.UInt256Parts ToXdr()
    {
        return new xdr.UInt256Parts
        {
            HiHi = new xdr.Uint64(HiHi),
            HiLo = new xdr.Uint64(HiLo),
            LoHi = new xdr.Uint64(LoHi),
            LoLo = new xdr.Uint64(LoLo),
        };
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_U256,
            },
            U256 = ToXdr(),
        };
    }
    
    public static SCUint256 FromXdr(xdr.UInt256Parts xdrUInt256Parts)
    {
        return new SCUint256
        {
            HiHi = xdrUInt256Parts.HiHi.InnerValue,
            HiLo = xdrUInt256Parts.HiLo.InnerValue,
            LoHi = xdrUInt256Parts.LoHi.InnerValue,
            LoLo = xdrUInt256Parts.LoLo.InnerValue,
        };
    }
    
    public static SCUint256 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_U256)
            throw new ArgumentException("Not an SCUint256", nameof(xdrVal));

        return FromXdr(xdrVal.U256);
    }
}

public class SCInt256 : SCVal
{
    public long HiHi { get; set; }
    public ulong HiLo { get; set; }
    public ulong LoHi { get; set; }
    public ulong LoLo { get; set; }
    
    public xdr.Int256Parts ToXdr()
    {
        return new xdr.Int256Parts
        {
            HiHi = new xdr.Int64(HiHi),
            HiLo = new xdr.Uint64(HiLo),
            LoHi = new xdr.Uint64(LoHi),
            LoLo = new xdr.Uint64(LoLo),
        };
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_I256,
            },
            I256 = ToXdr(),
        };
    }
    
    public static SCInt256 FromXdr(xdr.Int256Parts xdrInt256Parts)
    {
        return new SCInt256
        {
            HiHi = xdrInt256Parts.HiHi.InnerValue,
            HiLo = xdrInt256Parts.HiLo.InnerValue,
            LoHi = xdrInt256Parts.LoHi.InnerValue,
            LoLo = xdrInt256Parts.LoLo.InnerValue,
        };
    }
    
    public static SCInt256 FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_I256)
            throw new ArgumentException("Not an SCInt256", nameof(xdrVal));

        return FromXdr(xdrVal.I256);
    }
}

public class SCBytes : SCVal
{
    public SCBytes(byte[] value)
    {
        InnerValue = value;
    }
    public byte[] InnerValue { get; set; }
    
    public xdr.SCBytes ToXdr()
    {
        return new xdr.SCBytes(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_BYTES,
            },
            Bytes = ToXdr(),
        };
    }
    
    public static SCBytes FromXdr(xdr.SCBytes xdrSCBytes)
    {
        return new SCBytes(xdrSCBytes.InnerValue);
    }
    
    public static SCBytes FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_BYTES)
            throw new ArgumentException("Not an SCBytes", nameof(xdrVal));

        return FromXdr(xdrVal.Bytes);
    }
}

public class SCString : SCVal
{
    public SCString(string value)
    {
        InnerValue = value;
    }

    public string InnerValue { get; set; }
    
    public xdr.SCString ToXdr()
    {
        return new xdr.SCString(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_STRING,
            },
            Str = ToXdr(),
        };
    }
    
    public static SCString FromXdr(xdr.SCString xdrSCString)
    {
        return new SCString(xdrSCString.InnerValue);
    }
    
    public static SCString FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_STRING)
            throw new ArgumentException("Not an SCString", nameof(xdrVal));

        return FromXdr(xdrVal.Str);
    }
}

public class SCSymbol : SCVal
{
    public SCSymbol(string innerValue)
    {
        InnerValue = innerValue;
    }

    public string InnerValue { get; set; }
    
    public xdr.SCSymbol ToXdr()
    {
        return new xdr.SCSymbol(InnerValue);
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_SYMBOL,
            },
            Sym = ToXdr(),
        };
    }
    
    public static SCSymbol FromXdr(xdr.SCSymbol xdrSCSymbol)
    {
        return new SCSymbol(xdrSCSymbol.InnerValue);
    }
    
    public static SCSymbol FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_SYMBOL)
            throw new ArgumentException("Not an SCSymbol", nameof(xdrVal));

        return FromXdr(xdrVal.Sym);
    }
}

public class SCVec : SCVal
{
    public SCVec(SCVal[] value)
    {
        InnerValue = value;
    }
    
    public SCVal[] InnerValue { get; set; }
    
    public xdr.SCVec ToXdr()
    {
        return new xdr.SCVec(InnerValue.Select(a => a.ToXdr()).ToArray());
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_VEC,
            },
            Vec = ToXdr(),
        };
    }
    
    public static SCVec FromXdr(xdr.SCVec xdrSCVec)
    {
        return new SCVec(xdrSCVec.InnerValue.Select(SCVal.FromXdr).ToArray());
    }
    
    public static SCVec FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_VEC)
            throw new ArgumentException("Not an SCVec", nameof(xdrVal));

        return FromXdr(xdrVal.Vec);
    }
}

public class SCMap : SCVal
{
    public SCMapEntry[] Entries { get; set; } = Array.Empty<SCMapEntry>();
    
    public xdr.SCMap ToXdr()
    {
        return Entries.Length == 0 ? new xdr.SCMap { InnerValue = Array.Empty<xdr.SCMapEntry>() } : new xdr.SCMap(Entries.Select(a => a.ToXdr()).ToArray());
    }
    
    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_MAP,
            },
            Map = ToXdr(),
        };
    }
    
    public static SCMap FromXdr(xdr.SCMap xdrSCMap)
    {
        if (xdrSCMap == null) return new SCMap();
        return new SCMap
        {
            Entries = xdrSCMap.InnerValue.Select(SCMapEntry.FromXdr).ToArray(),
        };
    }
    
    public static SCMap FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_MAP)
            throw new ArgumentException("Not an SCMap", nameof(xdrVal));

        return FromXdr(xdrVal.Map);
    }
}

public class SCMapEntry
{
    public SCVal Key { get; set; }
    public SCVal Value { get; set; }
    
    public static SCMapEntry FromXdr(xdr.SCMapEntry xdr)
    {
        return new SCMapEntry
        {
            Key = SCVal.FromXdr(xdr.Key),
            Value = SCVal.FromXdr(xdr.Val),
        };
    }
    
    public xdr.SCMapEntry ToXdr()
    {
        return new xdr.SCMapEntry
        {
            Key = Key.ToXdr(),
            Val = Value.ToXdr(),
        };
    }
}

public abstract class SCAddress : SCVal
{
    public static SCAddress FromXdr(xdr.SCAddress xdrSCAddress)
    {
        return xdrSCAddress.Discriminant.InnerValue switch
        {
            SCAddressType.SCAddressTypeEnum.SC_ADDRESS_TYPE_ACCOUNT => SCAccountId.FromXdr(xdrSCAddress),
            SCAddressType.SCAddressTypeEnum.SC_ADDRESS_TYPE_CONTRACT => SCContractId.FromXdr(xdrSCAddress),
        };
    }

    public static SCAddress FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_ADDRESS)
            throw new ArgumentException("Not an SCAddress", nameof(xdrVal));

        return FromXdr(xdrVal.Address);
    }

    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_ADDRESS,
            },
            Address = ToXdr(),
        };
    }
    
    public abstract xdr.SCAddress ToXdr();
}


public class SCAccountId : SCAddress
{
    public SCAccountId(string value)
    {
        if (!StrKey.IsValidEd25519PublicKey(value))
            throw new InvalidOperationException("Invalid account id");

        InnerValue = value;
    }
    
    public string InnerValue { get; set; }
    
    public static SCAccountId FromXdr(xdr.SCAddress xdr)
    {
        return new SCAccountId(
            KeyPair.FromXdrPublicKey(xdr.AccountId.InnerValue).AccountId);
    }

    public override xdr.SCAddress ToXdr()
    {
        return new xdr.SCAddress
        {
            Discriminant = new xdr.SCAddressType
            {
                InnerValue = xdr.SCAddressType.SCAddressTypeEnum.SC_ADDRESS_TYPE_ACCOUNT,
            },
            AccountId = new xdr.AccountID(KeyPair.FromAccountId(InnerValue).XdrPublicKey),
        };
    }
}

public class SCContractId : SCAddress
{
    public SCContractId(string value)
    {
        if (!StrKey.IsValidContractId(value))
            throw new InvalidOperationException("Invalid contract id");

        InnerValue = value;
    }
    
    public string InnerValue { get; }
    
    public static SCContractId FromXdr(xdr.SCAddress xdr)
    {
        var value = StrKey.EncodeContractId(xdr.ContractId.InnerValue);

        if (!StrKey.IsValidContractId(value))
            throw new InvalidOperationException("Invalid contract id");
            
        return new SCContractId(value);
    }

    public override xdr.SCAddress ToXdr()
    {
        if (!StrKey.IsValidContractId(InnerValue))
            throw new InvalidOperationException("Invalid contract id");

        return new xdr.SCAddress
        {
            Discriminant = new xdr.SCAddressType
            {
                InnerValue = xdr.SCAddressType.SCAddressTypeEnum.SC_ADDRESS_TYPE_CONTRACT,
            },
            ContractId = new xdr.Hash(StrKey.DecodeContractId(InnerValue))
        };
    }
}

public class SCLedgerKeyContractInstance : SCVal
{
    public void ToXdr() { }

    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_LEDGER_KEY_CONTRACT_INSTANCE,
            },
        };
    }

    public static SCLedgerKeyContractInstance FromXdr()
    {
        return new SCLedgerKeyContractInstance();
    }

    public static SCLedgerKeyContractInstance FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_LEDGER_KEY_CONTRACT_INSTANCE)
            throw new ArgumentException("Not an SCLedgerKeyContractInstance", nameof(xdrVal));

        return FromXdr();
    }
}

public class SCContractInstance : SCVal
{
    public ContractExecutable Executable { get; set; }
    public SCMap Storage { get; set; } = new();
    
    public static SCContractInstance FromXdr(xdr.SCContractInstance xdr)
    {
        return new SCContractInstance
        {
            Executable = ContractExecutable.FromXdr(xdr.Executable),
            Storage = SCMap.FromXdr(xdr.Storage),
        };
    }
    
    public static SCContractInstance FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_CONTRACT_INSTANCE)
            throw new ArgumentException("Not an SCContractInstance", nameof(xdrVal));

        return FromXdr(xdrVal.Instance);
    }
    
    public xdr.SCContractInstance ToXdr()
    {
        return new xdr.SCContractInstance
        {
            Executable = Executable.ToXdr(),
            Storage = Storage.ToXdr(),
        };
    }

    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_CONTRACT_INSTANCE,
            },
            Instance = ToXdr(),
        };
    }
}

public abstract class ContractExecutable
{
    public string WasmHash { get; set; }
    
    public static ContractExecutable FromXdr(xdr.ContractExecutable xdrContractExecutable)
    {
        return xdrContractExecutable.Discriminant.InnerValue switch
        {
            ContractExecutableType.ContractExecutableTypeEnum.CONTRACT_EXECUTABLE_WASM => ContractExecutableWasm.FromXdr(xdrContractExecutable),
            ContractExecutableType.ContractExecutableTypeEnum.CONTRACT_EXECUTABLE_STELLAR_ASSET => ContractExecutableStellarAsset.FromXdr(xdrContractExecutable)
        };
    }
    public abstract xdr.ContractExecutable ToXdr();
}

public class ContractExecutableWasm : ContractExecutable
{
    public ContractExecutableWasm(string value)
    {
        WasmHash = value;
    }

    public static ContractExecutableWasm FromXdr(xdr.ContractExecutable xdr)
    {
        return new ContractExecutableWasm(Convert.ToBase64String(xdr.WasmHash.InnerValue));
    }

    public override xdr.ContractExecutable ToXdr()
    {
        return new xdr.ContractExecutable
        {
            Discriminant = new xdr.ContractExecutableType()
            {
                InnerValue = xdr.ContractExecutableType.ContractExecutableTypeEnum.CONTRACT_EXECUTABLE_WASM,
            },
            WasmHash = new Hash(Convert.FromBase64String(WasmHash)),
        };
    }
}

public class ContractExecutableStellarAsset : ContractExecutable
{
    public ContractExecutableStellarAsset(string value)
    {
        WasmHash = value;
    }
    public static ContractExecutableStellarAsset FromXdr(xdr.ContractExecutable xdr)
    {
        return new ContractExecutableStellarAsset(Convert.ToBase64String(xdr.WasmHash.InnerValue));
    }

    public override xdr.ContractExecutable ToXdr()
    {
        return new xdr.ContractExecutable
        {
            Discriminant = new xdr.ContractExecutableType()
            {
                InnerValue = xdr.ContractExecutableType.ContractExecutableTypeEnum.CONTRACT_EXECUTABLE_WASM,
            },
            WasmHash = new Hash(Convert.FromBase64String(WasmHash)),
        };
    }
}

public class SCNonceKey : SCVal
{
    public SCNonceKey(long value)
    {
        Nonce = value;
    }
    
    public long Nonce { get; set; }
    
    public xdr.SCNonceKey ToXdr()
    {
        return new xdr.SCNonceKey
        {
            Nonce = new xdr.Int64(Nonce),
        };
    }

    public xdr.SCVal ToSCValXdr()
    {
        return new xdr.SCVal
        {
            Discriminant = new xdr.SCValType
            {
                InnerValue = xdr.SCValType.SCValTypeEnum.SCV_LEDGER_KEY_NONCE,
            },
            NonceKey = ToXdr(),
        };
    }
    
    public static SCNonceKey FromXdr(xdr.SCNonceKey xdr)
    {
        return new SCNonceKey(xdr.Nonce.InnerValue);
    }
    
    public static SCNonceKey FromSCValXdr(xdr.SCVal xdrVal)
    {
        if (xdrVal.Discriminant.InnerValue != SCValType.SCValTypeEnum.SCV_LEDGER_KEY_NONCE)
            throw new ArgumentException("Not an SCNonceKey", nameof(xdrVal));

        return FromXdr(xdrVal.NonceKey);
    }
}
