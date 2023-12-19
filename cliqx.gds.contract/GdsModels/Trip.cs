using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class Trip : BaseObjectPlugin
{
    [JsonIgnore]
    public int OriginId { get; set; }
    public TripOriginDestination Origin { get; set; }

    [JsonIgnore]
    public int DestinationId { get; set; }
    public TripOriginDestination Destination { get; set; }

    [JsonIgnore]
    public int ClassId { get; set; }
    public TripClass Class { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TripStation Station { get; set; }

    [JsonIgnore]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public long Value { get; set; }
    public string Distance { get; set; }

    [NotMapped]
    public decimal ValueAsDecimal => Value / 100M;
    public long DiscountValue { get; set; }

    [NotMapped]
    public decimal DiscountValueAsDecimal => DiscountValue / 100M;
    public decimal ServiceTax { get; set; }
    public decimal DepartureTax { get; set; }
    public string PassengerType { get; set; }
    public long AvailableSeats { get; set; }
    public bool IsBpe { get; set; }
    public int TotalConnections
    {
        get
        {
            if (Connections == null)
                return 0;
            return Connections.Where(x => x.Id != Id).Count();
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Connection> Connections { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Seat> Seats { get; set; }
}

public record TripOriginDestination
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StationId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StationName { get; set; }
    public string CityId { get; set; }
    public string LetterCode { get; set; }
    public string _cityName;
    public string? NormalizedName { get; private set; }

    public string CityName
    {
        get { return _cityName; }
        set
        {
            _cityName = value;
            NormalizedName = RemoveDiacriticsAndSpecialCharacters(value.ToLower(CultureInfo.InvariantCulture));
        }
    }

    public string Name
    {
        get { return _cityName; }
        set
        {
            _cityName = value;
            NormalizedName = value.ToNormalizedWithAccents();
        }
    }

    private static readonly Regex diacriticRegex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
    private static string RemoveDiacriticsAndSpecialCharacters(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        string withoutDiacritics = diacriticRegex.Replace(normalizedString, string.Empty).Normalize(NormalizationForm.FormC);

        int index = withoutDiacritics.IndexOfAny(new char[] { '/', '-', '\\', ':' });
        if (index >= 0)
        {
            withoutDiacritics = withoutDiacritics.Substring(0, index);
        }

        return withoutDiacritics;
    }
}


public record TripClass
{
    public string Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }
}

public class TripStation
{
    public string OriginId { get; set; }
    public string DestinationId { get; set; }
    public string OriginName { get; set; }
    public string DestinationName { get; set; }
}