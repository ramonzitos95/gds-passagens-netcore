using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class PassengerBasicObjectAttribute : BasicAttribute
    {
        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("serial_code", NullValueHandling = NullValueHandling.Ignore)]
        public string SerialCode { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
