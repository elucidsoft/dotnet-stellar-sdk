// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  struct ConfigSettingContractHistoricalDataV0
//  {
//      int64 feeHistorical1KB; // Fee for storing 1KB in archives
//  };

//  ===========================================================================
public class ConfigSettingContractHistoricalDataV0
{
    public Int64 FeeHistorical1KB { get; set; }

    public static void Encode(XdrDataOutputStream stream,
        ConfigSettingContractHistoricalDataV0 encodedConfigSettingContractHistoricalDataV0)
    {
        Int64.Encode(stream, encodedConfigSettingContractHistoricalDataV0.FeeHistorical1KB);
    }

    public static ConfigSettingContractHistoricalDataV0 Decode(XdrDataInputStream stream)
    {
        var decodedConfigSettingContractHistoricalDataV0 = new ConfigSettingContractHistoricalDataV0();
        decodedConfigSettingContractHistoricalDataV0.FeeHistorical1KB = Int64.Decode(stream);
        return decodedConfigSettingContractHistoricalDataV0;
    }
}