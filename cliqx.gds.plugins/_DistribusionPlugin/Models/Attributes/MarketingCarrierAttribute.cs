using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class MarketingCarrierAttribute : BasicAttribute
    {
        [JsonProperty("trade_name")]
        public string TradeName { get; set; }

        [JsonProperty("legal_name")]
        public string LegalName { get; set; }
    }
}