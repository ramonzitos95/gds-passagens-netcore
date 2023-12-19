using distribusion.api.client.Models.Basic;
using distribusion.api.client.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace distribusion.api.client.Models.Attributes
{
    public class StationAttribute : BasicAttribute
    {
        [JsonProperty("station_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StationType StationType { get; set; }

        [JsonProperty("street_and_number")] 
        public string StreetAndNumber { get; set; }

        [JsonProperty("zip_code")] 
        public string ZipCode { get; set; }

        [JsonProperty("time_zone")] 
        public string TimeZone { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}