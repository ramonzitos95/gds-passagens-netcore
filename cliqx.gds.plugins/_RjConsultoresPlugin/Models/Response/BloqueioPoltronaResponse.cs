using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
	public class BloqueioPoltronaResponse
	{
        [JsonProperty("origem")]
        public OrigemDestino Origem { get; set; }

        [JsonProperty("destino")]
        public OrigemDestino Destino { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("servico")]
        public string Servico { get; set; }

        [JsonProperty("assento")]
        public string Assento { get; set; }

        [JsonProperty("duracao")]
        public int Duracao { get; set; }

        [JsonProperty("transacao")]
        public string Transacao { get; set; }

        [JsonProperty("preco")]
        public PrecoCorrida Preco { get; set; }

        [JsonProperty("rutaid")]
        public string Rutaid { get; set; }

        [JsonProperty("seguroOpcional")]
        public SeguroOpcional SeguroOpcional { get; set; }

        [JsonProperty("numOperacion")]
        public string NumOperacion { get; set; }

        [JsonProperty("localizador")]
        public string Localizador { get; set; }

        [JsonProperty("boletoId")]
        public long BoletoId { get; set; }

        [JsonProperty("empresaCorridaId")]
        public int EmpresaCorridaId { get; set; }

        [JsonProperty("dataSaida")]
        public DateTime DataSaida { get; set; }

        [JsonProperty("dataChegada")]
        public DateTime DataChegada { get; set; }

        [JsonProperty("dataCorrida")]
        public DateTime DataCorrida { get; set; }

        [JsonProperty("classeServicoId")]
        public int ClasseServicoId { get; set; }
    }

    public class SeguroOpcional
    {
    }
}
