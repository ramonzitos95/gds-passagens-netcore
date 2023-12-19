using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class CategoriaServico
    {
        [JsonProperty("categoriaId")]
        public int CategoriaId { get; set; }

        [JsonProperty("desccategoria")]
        public string Desccategoria { get; set; }

        [JsonProperty("vendeApi")]
        public bool VendeApi { get; set; }

        [JsonProperty("dataModificacao")]
        public DateTime DataModificacao { get; set; }

        [JsonProperty("descontos")]
        public List<DescontoCategoria> Descontos { get; set; }
    }
}
