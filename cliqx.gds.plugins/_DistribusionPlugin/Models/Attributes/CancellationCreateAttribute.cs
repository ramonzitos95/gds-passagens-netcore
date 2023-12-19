using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class CancellationCreateAttribute : BasicAttribute
    {
        [JsonProperty("total_price", NullValueHandling = NullValueHandling.Ignore)]
        public long TotalPrice { get; set; }

        [JsonProperty("fee", NullValueHandling = NullValueHandling.Ignore)]
        public long Fee { get; set; }

        [JsonProperty("total_refund", NullValueHandling = NullValueHandling.Ignore)]
        public long TotalRefund { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }
    }
}
