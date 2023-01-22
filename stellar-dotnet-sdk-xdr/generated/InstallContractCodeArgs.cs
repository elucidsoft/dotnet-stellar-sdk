// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct InstallContractCodeArgs
    //  {
    //      opaque code<SCVAL_LIMIT>;
    //  };

    //  ===========================================================================
    public class InstallContractCodeArgs
    {
        public InstallContractCodeArgs() { }
        public byte[] Code { get; set; }

        public static void Encode(XdrDataOutputStream stream, InstallContractCodeArgs encodedInstallContractCodeArgs)
        {
            int codesize = encodedInstallContractCodeArgs.Code.Length;
            stream.WriteInt(codesize);
            stream.Write(encodedInstallContractCodeArgs.Code, 0, codesize);
        }
        public static InstallContractCodeArgs Decode(XdrDataInputStream stream)
        {
            InstallContractCodeArgs decodedInstallContractCodeArgs = new InstallContractCodeArgs();
            int codesize = stream.ReadInt();
            decodedInstallContractCodeArgs.Code = new byte[codesize];
            stream.Read(decodedInstallContractCodeArgs.Code, 0, codesize);
            return decodedInstallContractCodeArgs;
        }
    }
}
