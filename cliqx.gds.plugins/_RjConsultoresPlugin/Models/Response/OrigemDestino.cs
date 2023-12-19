using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class OrigemDestino
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cidade")]
        public string Cidade { get; set; }

        [JsonProperty("sigla")]
        public string Sigla { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("empresas")]
        public string Empresas { get; set; }
    }

        
}
