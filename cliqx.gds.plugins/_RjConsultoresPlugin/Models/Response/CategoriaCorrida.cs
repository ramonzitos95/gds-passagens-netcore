using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class CategoriaCorrida
    {
        [JsonProperty("categoriaId")]
        public int CategoriaId { get; set; }

        [JsonProperty("desccategoria")]
        public string Desccategoria { get; set; }

        [JsonProperty("gratuidadeCrianca")]
        public bool GratuidadeCrianca { get; set; }

        [JsonProperty("estudante")]
        public bool Estudante { get; set; }

        [JsonProperty("idoso")]
        public bool Idoso { get; set; }

        [JsonProperty("orgaoConcedente")]
        public List<OrgaoConcedente> OrgaoConcedente { get; set; }

        [JsonProperty("desconto")]
        public DescontoTarifa Desconto { get; set; }

        [JsonProperty("vendeApi")]
        public bool VendeApi { get; set; }
    }
}
