
using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Onibus
    {
        [JsonProperty("origem")]
        public OrigemDestino Origem { get; set; }

        [JsonProperty("destino")]
        public OrigemDestino Destino { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("servico")]
        public string Servico { get; set; }

        [JsonProperty("dataSaida")]
        public string DataSaida { get; set; }

        [JsonProperty("dataChegada")]
        public string DataChegada { get; set; }

        [JsonProperty("mapaPoltrona")]
        public List<MapaPoltrona> MapaPoltrona { get; set; }

        [JsonProperty("lsLocalidadeEmbarque")]
        public List<object> LsLocalidadeEmbarque { get; set; }

        [JsonProperty("lsLocalidadeDesembarque")]
        public List<object> LsLocalidadeDesembarque { get; set; }

        [JsonProperty("pricingSequencia")]
        public List<object> PricingSequencia { get; set; }

        [JsonProperty("pricingPoltrona")]
        public List<object> PricingPoltrona { get; set; }

        [JsonProperty("poltronasLivres")]
        public int PoltronasLivres { get; set; }

        [JsonProperty("empresaCorridaId")]
        public int EmpresaCorridaId { get; set; }

        [JsonProperty("classeServico")]
        public string ClasseServico { get; set; }

        [JsonProperty("dataCorrida")]
        public string DataCorrida { get; set; }

        
    }
}
