// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct SCSpecTypeTuple
    //  {
    //      SCSpecTypeDef valueTypes<12>;
    //  };

    //  ===========================================================================
    public class SCSpecTypeTuple
    {
        public SCSpecTypeTuple() { }
        public SCSpecTypeDef[] ValueTypes { get; set; }

        public static void Encode(XdrDataOutputStream stream, SCSpecTypeTuple encodedSCSpecTypeTuple)
        {
            int valueTypessize = encodedSCSpecTypeTuple.ValueTypes.Length;
            stream.WriteInt(valueTypessize);
            for (int i = 0; i < valueTypessize; i++)
            {
                SCSpecTypeDef.Encode(stream, encodedSCSpecTypeTuple.ValueTypes[i]);
            }
        }
        public static SCSpecTypeTuple Decode(XdrDataInputStream stream)
        {
            SCSpecTypeTuple decodedSCSpecTypeTuple = new SCSpecTypeTuple();
            int valueTypessize = stream.ReadInt();
            decodedSCSpecTypeTuple.ValueTypes = new SCSpecTypeDef[valueTypessize];
            for (int i = 0; i < valueTypessize; i++)
            {
                decodedSCSpecTypeTuple.ValueTypes[i] = SCSpecTypeDef.Decode(stream);
            }
            return decodedSCSpecTypeTuple;
        }
    }
}
