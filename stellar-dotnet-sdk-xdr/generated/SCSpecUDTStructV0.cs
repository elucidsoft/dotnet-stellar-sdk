// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct SCSpecUDTStructV0
    //  {
    //      string lib<80>;
    //      string name<60>;
    //      SCSpecUDTStructFieldV0 fields<40>;
    //  };

    //  ===========================================================================
    public class SCSpecUDTStructV0
    {
        public SCSpecUDTStructV0() { }
        public String Lib { get; set; }
        public String Name { get; set; }
        public SCSpecUDTStructFieldV0[] Fields { get; set; }

        public static void Encode(XdrDataOutputStream stream, SCSpecUDTStructV0 encodedSCSpecUDTStructV0)
        {
            stream.WriteString(encodedSCSpecUDTStructV0.Lib);
            stream.WriteString(encodedSCSpecUDTStructV0.Name);
            int fieldssize = encodedSCSpecUDTStructV0.Fields.Length;
            stream.WriteInt(fieldssize);
            for (int i = 0; i < fieldssize; i++)
            {
                SCSpecUDTStructFieldV0.Encode(stream, encodedSCSpecUDTStructV0.Fields[i]);
            }
        }
        public static SCSpecUDTStructV0 Decode(XdrDataInputStream stream)
        {
            SCSpecUDTStructV0 decodedSCSpecUDTStructV0 = new SCSpecUDTStructV0();
            decodedSCSpecUDTStructV0.Lib = stream.ReadString();
            decodedSCSpecUDTStructV0.Name = stream.ReadString();
            int fieldssize = stream.ReadInt();
            decodedSCSpecUDTStructV0.Fields = new SCSpecUDTStructFieldV0[fieldssize];
            for (int i = 0; i < fieldssize; i++)
            {
                decodedSCSpecUDTStructV0.Fields[i] = SCSpecUDTStructFieldV0.Decode(stream);
            }
            return decodedSCSpecUDTStructV0;
        }
    }
}
