using distribusion.api.client.Models.Enums;

namespace distribusion.api.client.Extensions
{

    public static class PassengerTypeExtensions
    {
        public static string GetApiType(this PassengerType passengerType) => passengerType switch
        {
            PassengerType.Infant => "PINT",
            PassengerType.Child => "PCIL",
            PassengerType.Youth => "PYPO",
            PassengerType.Adult => "PNOS",
            PassengerType.Senior => "PSOE",
            _ => "unknown"
        };
    }
}