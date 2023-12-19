using System.Runtime.Serialization;

namespace distribusion.api.client.Models.Enums
{
    public enum StationType
    {
        Unknown,
        [EnumMember(Value = "bus_station")]
        Bus,
        [EnumMember(Value = "ferry_station")] 
        Ferry,
        [EnumMember(Value = "train_station")] 
        Train,
        [EnumMember(Value = "tram_station")] 
        Tram
    }
}