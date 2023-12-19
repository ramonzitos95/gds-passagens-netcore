using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class ConnectionAttribute : BasicAttribute
    {
        [JsonProperty("electronic_ticket_available")]
        public bool ElectronicTicketAvailable { get; set; }

        public long Duration { get; set; }

        [JsonProperty("departure_time")] 
        public DateTime DepartureTime { get; set; }

        [JsonProperty("cheapest_total_adult_price")]
        public long CheapestTotalAdultPrice { get; set; }

        [JsonProperty("cheapest_fare_class_code")]
        public string CheapestFareClassCode { get; set; }

        [JsonProperty("booked_out")] 
        public bool BookedOut { get; set; }

        [JsonProperty("arrival_time")] 
        public DateTime ArrivalTime { get; set; }
    }
}