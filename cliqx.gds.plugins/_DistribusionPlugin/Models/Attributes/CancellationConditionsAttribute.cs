using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class CancellationConditionsAttribute : BasicAttribute
    {
        [JsonProperty("allowed", NullValueHandling = NullValueHandling.Ignore)]
        public bool Allowed { get; set; }

        [JsonProperty("fee", NullValueHandling = NullValueHandling.Ignore)]
        public int Fee { get; set; }

        [JsonProperty("cutoff", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Cutoff { get; set; }
    }
}
