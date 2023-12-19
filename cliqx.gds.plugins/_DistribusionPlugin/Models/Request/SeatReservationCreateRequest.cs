using System.Collections.Generic;
using distribusion.api.client.Models;
using Newtonsoft.Json;

namespace distribusion.api.client.Request
{
    public class SeatReservationCreateRequest
    {

        [JsonProperty("marketing_carrier", NullValueHandling = NullValueHandling.Ignore)]
        public string MarketingCarrier { get; set; }

        [JsonProperty("departure_station", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartureStation { get; set; }

        [JsonProperty("arrival_station", NullValueHandling = NullValueHandling.Ignore)]
        public string ArrivalStation { get; set; }

        [JsonProperty("departure_time", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartureTime { get; set; }

        [JsonProperty("arrival_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ArrivalTime { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("locale", NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; }

        [JsonProperty("fare_class", NullValueHandling = NullValueHandling.Ignore)]
        public string FareClass { get; set; }

        [JsonProperty("retailer_partner_number", NullValueHandling = NullValueHandling.Ignore)]
        public string RetailerPartnerNumber { get; set; }

        [JsonProperty("passengers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Passenger> Passengers { get; set; }
    }
}
