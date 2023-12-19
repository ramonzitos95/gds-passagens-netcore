using System.Runtime.Serialization;

namespace distribusion.api.client.Models.Enums
{

    public enum FareClassType
    {
        Unknown,
        [EnumMember(Value = "FARE-1")]
        Fare1,
        [EnumMember(Value = "FARE-2")]
        Fare2,
        [EnumMember(Value = "FARE-3")]
        Fare3,
        [EnumMember(Value = "FARE-4")]
        Fare4
    }
}