using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class DescontoTarifa
    {
        [JsonProperty("importeTarifa")]
        public double ImporteTarifa { get; set; }

        [JsonProperty("importePedagio")]
        public double ImportePedagio { get; set; }

        [JsonProperty("importeSeguro")]
        public double ImporteSeguro { get; set; }

        [JsonProperty("importeTaxaEmbarque")]
        public double ImporteTaxaEmbarque { get; set; }

        [JsonProperty("importeOutros")]
        public double ImporteOutros { get; set; }

        [JsonProperty("importeTarifaSeguro")]
        public double ImporteTarifaSeguro { get; set; }

        [JsonProperty("importeTarifaTaxa")]
        public double ImporteTarifaTaxa { get; set; }

        [JsonProperty("importeTarifaSeguroTaxa")]
        public double ImporteTarifaSeguroTaxa { get; set; }

        [JsonProperty("importeDesconto")]
        public double ImporteDesconto { get; set; }

        [JsonProperty("pricingEspecifico")]
        public int PricingEspecifico { get; set; }

        [JsonProperty("pricingAplicado")]
        public string PricingAplicado { get; set; }

        [JsonProperty("cotaObrigatoria")]
        public bool CotaObrigatoria { get; set; }

        [JsonProperty("quantidadeCota")]
        public int QuantidadeCota { get; set; }

        [JsonProperty("assentosReservados")]
        public string AssentosReservados { get; set; }

        [JsonProperty("exigirNome")]
        public bool ExigirNome { get; set; }

        [JsonProperty("exigirDocumento")]
        public bool ExigirDocumento { get; set; }

        [JsonProperty("exigirTelefone")]
        public bool ExigirTelefone { get; set; }

        [JsonProperty("exigirDataNascimento")]
        public bool ExigirDataNascimento { get; set; }

        [JsonProperty("exigirEndereco")]
        public bool ExigirEndereco { get; set; }

        [JsonProperty("exigirEmail")]
        public bool ExigirEmail { get; set; }

        [JsonProperty("clientePcd")]
        public bool ClientePcd { get; set; }

        [JsonProperty("naoPermiteVendaMesmoDocViagem")]
        public bool NaoPermiteVendaMesmoDocViagem { get; set; }

        [JsonProperty("naoPermiteVendaDuasGratuidades")]
        public bool NaoPermiteVendaDuasGratuidades { get; set; }

        [JsonProperty("naoAplicaTarifaMinima")]
        public bool NaoAplicaTarifaMinima { get; set; }
    }
}
