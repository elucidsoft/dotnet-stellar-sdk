// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  union SCError switch (SCErrorType type)
//  {
//  case SCE_CONTRACT:
//      uint32 contractCode;
//  case SCE_WASM_VM:
//  case SCE_CONTEXT:
//  case SCE_STORAGE:
//  case SCE_OBJECT:
//  case SCE_CRYPTO:
//  case SCE_EVENTS:
//  case SCE_BUDGET:
//  case SCE_VALUE:
//  case SCE_AUTH:
//      SCErrorCode code;
//  };

//  ===========================================================================
public class SCError
{
    public SCErrorType Discriminant { get; set; } = new();

    public Uint32 ContractCode { get; set; }
    public SCErrorCode Code { get; set; }

    public static void Encode(XdrDataOutputStream stream, SCError encodedSCError)
    {
        stream.WriteInt((int)encodedSCError.Discriminant.InnerValue);
        switch (encodedSCError.Discriminant.InnerValue)
        {
            case SCErrorType.SCErrorTypeEnum.SCE_CONTRACT:
                Uint32.Encode(stream, encodedSCError.ContractCode);
                break;
            case SCErrorType.SCErrorTypeEnum.SCE_WASM_VM:
            case SCErrorType.SCErrorTypeEnum.SCE_CONTEXT:
            case SCErrorType.SCErrorTypeEnum.SCE_STORAGE:
            case SCErrorType.SCErrorTypeEnum.SCE_OBJECT:
            case SCErrorType.SCErrorTypeEnum.SCE_CRYPTO:
            case SCErrorType.SCErrorTypeEnum.SCE_EVENTS:
            case SCErrorType.SCErrorTypeEnum.SCE_BUDGET:
            case SCErrorType.SCErrorTypeEnum.SCE_VALUE:
            case SCErrorType.SCErrorTypeEnum.SCE_AUTH:
                SCErrorCode.Encode(stream, encodedSCError.Code);
                break;
        }
    }

    public static SCError Decode(XdrDataInputStream stream)
    {
        var decodedSCError = new SCError();
        var discriminant = SCErrorType.Decode(stream);
        decodedSCError.Discriminant = discriminant;
        switch (decodedSCError.Discriminant.InnerValue)
        {
            case SCErrorType.SCErrorTypeEnum.SCE_CONTRACT:
                decodedSCError.ContractCode = Uint32.Decode(stream);
                break;
            case SCErrorType.SCErrorTypeEnum.SCE_WASM_VM:
            case SCErrorType.SCErrorTypeEnum.SCE_CONTEXT:
            case SCErrorType.SCErrorTypeEnum.SCE_STORAGE:
            case SCErrorType.SCErrorTypeEnum.SCE_OBJECT:
            case SCErrorType.SCErrorTypeEnum.SCE_CRYPTO:
            case SCErrorType.SCErrorTypeEnum.SCE_EVENTS:
            case SCErrorType.SCErrorTypeEnum.SCE_BUDGET:
            case SCErrorType.SCErrorTypeEnum.SCE_VALUE:
            case SCErrorType.SCErrorTypeEnum.SCE_AUTH:
                decodedSCError.Code = SCErrorCode.Decode(stream);
                break;
        }

        return decodedSCError;
    }
}