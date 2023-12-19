using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using cliqx.gds.plugins.Util;

namespace cliqx.gds.plugins._SmartbusPlugin.Extensions
{
    public static class CityExtensions
    {
        public static CustomCity AsCustomCity(this LocalidadeResponse city) => new CustomCity
        {
            Name = $"{city.Localidade.Trim()}",
            Id = city.LocalidadeId.ToString(),
            Description = city.Localidade.Trim() + city.Uf,
            LetterCode = city.Uf,
            Key = KeyGenerator.GenerateCityKey(city.Localidade,city.Uf)
        };

        public static TripOriginDestination AsTrip(this CustomCity city) => new TripOriginDestination
        {
            CityId = city.Id,
            CityName = city.Name,
            StationId = city.Station,
            StationName = city.Station,
        };
    }
}