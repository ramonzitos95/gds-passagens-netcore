using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class DescontoCategoria
    {
        [JsonProperty("vendeApi")]
        public bool VendeApi { get; set; }

        [JsonProperty("dataModificacao")]
        public DateTime DataModificacao { get; set; }

        [JsonProperty("desconto")]
        public int Desconto { get; set; }

        [JsonProperty("descontoPorc")]
        public int DescontoPorc { get; set; }

        [JsonProperty("descontoTarifa")]
        public bool DescontoTarifa { get; set; }

        [JsonProperty("descontoSeguro")]
        public bool DescontoSeguro { get; set; }

        [JsonProperty("descontoTMR")]
        public bool DescontoTMR { get; set; }

        [JsonProperty("descontoTaxaEmbarque")]
        public bool DescontoTaxaEmbarque { get; set; }

        [JsonProperty("descontoPedagio")]
        public bool DescontoPedagio { get; set; }
    }
}
