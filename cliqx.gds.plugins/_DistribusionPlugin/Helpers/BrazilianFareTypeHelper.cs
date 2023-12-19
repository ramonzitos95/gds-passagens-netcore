using distribusion.api.client.Models.Enums;

namespace distribusion.api.client.Helpers
{

    public class BrazilianFareTypeHelper
    {
        public const FareClassType Convencional = FareClassType.Fare1;
        public const FareClassType Leito = FareClassType.Fare2;
        public const FareClassType DoubleDeckConvencional = FareClassType.Fare3;
        public const FareClassType DoubleDeckLeito = FareClassType.Fare4;

        public static string GetFareTypeLabel(FareClassType fareClassType) =>
            fareClassType switch
            {
                FareClassType.Fare1 => "Convencional (standard)",
                FareClassType.Fare2 => "Leito (bed)",
                FareClassType.Fare3 => "Double Decker - Convencional",
                FareClassType.Fare4 => "Double Decker - Leito",
                _ => "Desconhecido"
            };

    }
}