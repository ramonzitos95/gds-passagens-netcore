using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class ConsultaBilheteRequest
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("localizador")]
        public string Localizador { get; set; }
    }
}
