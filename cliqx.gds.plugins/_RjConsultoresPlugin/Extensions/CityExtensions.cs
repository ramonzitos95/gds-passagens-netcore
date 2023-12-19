using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;
using cliqx.gds.plugins.Util;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Extensions
{
    public static class CityExtensions
    {
        public static CustomCity AsCustomCity(this OrigemDestino city) => new CustomCity
        {
            Name = $"{city.Cidade}",
            Id = city.Id.ToString(),
            Description = $"{city.Cidade}",
            LetterCode = city.Uf,
            Key = KeyGenerator.GenerateCityKey(city.Cidade,city.Uf)
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