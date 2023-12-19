using Newtonsoft.Json;

namespace distribusion.api.client.Request
{
    public class CancellationCreateRequest
    {
        [JsonProperty("booking", NullValueHandling = NullValueHandling.Ignore)]
        public string Booking { get; set; }
    }
}
