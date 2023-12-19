using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{
    public class SeatReservationCreateRelationship
    {
        [JsonProperty("booking", NullValueHandling = NullValueHandling.Ignore)]
        public Booking Booking { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public Errors Errors { get; set; }
    }
}
