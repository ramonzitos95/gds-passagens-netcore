using System.Runtime.Serialization;

namespace distribusion.api.client.Models.Enums
{

    public enum PassengerType
    {
        Unknown,

        [EnumMember(Value = "PINT")] Infant,

        [EnumMember(Value = "PCIL")] Child,

        [EnumMember(Value = "PYPO")] Youth,

        [EnumMember(Value = "PNOS")] Adult,

        [EnumMember(Value = "PSOE")] Senior
    }
}