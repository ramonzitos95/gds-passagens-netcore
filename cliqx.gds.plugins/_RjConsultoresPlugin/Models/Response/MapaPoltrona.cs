using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class MapaPoltrona
    {
        [JsonProperty("x")]
        public string X { get; set; }

        [JsonProperty("y")]
        public string Y { get; set; }

        [JsonProperty("disponivel")]
        public bool Disponivel { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }
    }
}
