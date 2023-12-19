using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class PrecoCorrida
    {
        [JsonProperty("tarifa")]
        public decimal? Tarifa { get; set; }

        [JsonProperty("outros")]
        public string Outros { get; set; }

        [JsonProperty("pedagio")]
        public decimal? Pedagio { get; set; }

        [JsonProperty("seguro")]
        public decimal? Seguro { get; set; }

        [JsonProperty("preco")]
        public decimal? Preco { get; set; }

        [JsonProperty("tarifaComPricing")]
        public decimal? TarifaComPricing { get; set; }

        [JsonProperty("taxaEmbarque")]
        public decimal? TaxaEmbarque { get; set; }

        [JsonProperty("seguroW2I")]
        public decimal? SeguroW2I { get; set; }
    }
}
