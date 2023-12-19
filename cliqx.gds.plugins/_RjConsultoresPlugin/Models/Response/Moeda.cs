using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Moeda
    {
        [JsonProperty("monedaId")]
        public int MonedaId { get; set; }

        [JsonProperty("descmoneda")]
        public string Descmoneda { get; set; }
    }
}
