using System;
using Newtonsoft.Json;

namespace distribusion.api.client.Models
{
    public class Booking
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
    }
}
