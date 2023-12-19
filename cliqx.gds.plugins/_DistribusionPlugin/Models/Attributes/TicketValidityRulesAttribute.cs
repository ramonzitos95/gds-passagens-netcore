using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class TicketValidityRulesAttribute : BasicAttribute
    {
        [JsonProperty("rule_type", NullValueHandling = NullValueHandling.Ignore)]
        public string RuleType { get; set; }

        [JsonProperty("reference_time_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceTimeType { get; set; }

        [JsonProperty("use_type", NullValueHandling = NullValueHandling.Ignore)]
        public string UseType { get; set; }

        [JsonProperty("offset_type", NullValueHandling = NullValueHandling.Ignore)]
        public string OffsetType { get; set; }

        [JsonProperty("duration_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DurationType { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public int Duration { get; set; }
    }
}
