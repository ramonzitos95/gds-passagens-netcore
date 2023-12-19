using System.Collections.Generic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models
{
    public class Errors
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Data { get; set; }
    }
}
