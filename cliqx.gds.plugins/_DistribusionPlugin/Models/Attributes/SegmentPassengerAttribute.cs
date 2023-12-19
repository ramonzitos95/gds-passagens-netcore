using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class SegmentPassengerAttribute:BasicAttribute
    {
        [JsonProperty("seat_number", NullValueHandling = NullValueHandling.Ignore)]
        public string SeatNumber { get; set; }
    }
}
