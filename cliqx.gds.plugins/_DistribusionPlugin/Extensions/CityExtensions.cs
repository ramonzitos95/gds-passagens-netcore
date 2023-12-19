using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins.Util;

namespace cliqx.gds.plugins._DistribusionPlugin.Extensions
{
    public static class CityExtensions
    {
        public static CustomCity AsCustomCity(this City city) => new CustomCity
        {
            Name = $"{city.Name.ToNormalizedWithAccents().ToTittleCityName()}",
            Id = city.Id,
            Description = city.Description,
            LetterCode = city.LetterCode,
            Key = KeyGenerator.GenerateCityKey(city.Name, city.LetterCode)
        };

        public static TripOriginDestination AsTrip(this CustomCity city) => new TripOriginDestination
        {
            CityId = city.Id,
            CityName = city.Name,
            StationId = city.Station,
            StationName = city.Station,
            LetterCode = city.LetterCode
        };

        private static string ToTittleCityName(this string cityName)
        {
            string[] nameParts = cityName.Split(' ');
            for (int i = 0; i < nameParts.Length; i++)
            {
                nameParts[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nameParts[i]);
            }
            return string.Join(" ", nameParts);
        }
    }
}