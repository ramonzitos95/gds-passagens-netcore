using System.Runtime.Serialization;

namespace distribusion.api.client.Models.Enums
{

    public enum JourneyType
    {
        Unknown,
        [EnumMember(Value = "single")] 
        Single,
        [EnumMember(Value = "open_return")] 
        OpenReturn,
        [EnumMember(Value = "fixed_return")] 
        FixedReturn
    }
}