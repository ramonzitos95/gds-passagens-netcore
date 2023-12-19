using System.Collections.Generic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models
{
    public class Passenger
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("seats", NullValueHandling = NullValueHandling.Ignore)]
        public List<Seat> Seats { get; set; }

        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("government_id_type", NullValueHandling = NullValueHandling.Ignore)]
        public string GovernmentIdType { get; set; }

        [JsonProperty("government_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GovernmentId { get; set; }
    }
}
