using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class SegmentAttribute : BasicAttribute
    {
        public int Index { get; set; }

        [JsonProperty("departure_time")] 
        public DateTime DepartureTime { get; set; }

        [JsonProperty("arrival_time")] 
        public DateTime ArrivalTime { get; set; }

        [JsonProperty("vehicle_number")]
        public string VehicleNumber { get; set; }

        [JsonProperty("departure_platform")]
        public string DeparturePlatform { get; set; }

        public string Line { get; set; }
    }
}