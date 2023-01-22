// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  union CreateAccountResult switch (CreateAccountResultCode code)
    //  {
    //  case CREATE_ACCOUNT_SUCCESS:
    //      void;
    //  case CREATE_ACCOUNT_MALFORMED:
    //  case CREATE_ACCOUNT_UNDERFUNDED:
    //  case CREATE_ACCOUNT_LOW_RESERVE:
    //  case CREATE_ACCOUNT_ALREADY_EXIST:
    //      void;
    //  };

    //  ===========================================================================
    public class CreateAccountResult
    {
        public CreateAccountResult() { }

        public CreateAccountResultCode Discriminant { get; set; } = new CreateAccountResultCode();

        public static void Encode(XdrDataOutputStream stream, CreateAccountResult encodedCreateAccountResult)
        {
            stream.WriteInt((int)encodedCreateAccountResult.Discriminant.InnerValue);
            switch (encodedCreateAccountResult.Discriminant.InnerValue)
            {
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_SUCCESS:
                    break;
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_MALFORMED:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_UNDERFUNDED:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_LOW_RESERVE:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_ALREADY_EXIST:
                    break;
            }
        }
        public static CreateAccountResult Decode(XdrDataInputStream stream)
        {
            CreateAccountResult decodedCreateAccountResult = new CreateAccountResult();
            CreateAccountResultCode discriminant = CreateAccountResultCode.Decode(stream);
            decodedCreateAccountResult.Discriminant = discriminant;
            switch (decodedCreateAccountResult.Discriminant.InnerValue)
            {
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_SUCCESS:
                    break;
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_MALFORMED:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_UNDERFUNDED:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_LOW_RESERVE:
                case CreateAccountResultCode.CreateAccountResultCodeEnum.CREATE_ACCOUNT_ALREADY_EXIST:
                    break;
            }
            return decodedCreateAccountResult;
        }
    }
}
