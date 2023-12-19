using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class SeatReservationCreateAttribute : BasicAttribute
    {
        [JsonProperty("total_price", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPrice { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("confirmed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ConfirmedAt { get; set; }

        [JsonProperty("cancelled_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CancelledAt { get; set; }

        [JsonProperty("failed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FailedAt { get; set; }

        [JsonProperty("processing_started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ProcessingStartedAt { get; set; }

        [JsonProperty("processing_deadline", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ProcessingDeadline { get; set; }

        [JsonProperty("expires_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpiresAt { get; set; }
    }
}
