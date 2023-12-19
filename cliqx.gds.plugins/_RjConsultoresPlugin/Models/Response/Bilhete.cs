using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Bilhete
    {
        [JsonProperty("numeroBilhete")]
        public string NumeroBilhete { get; set; }

        [JsonProperty("localizador")]
        public string Localizador { get; set; }

        [JsonProperty("poltrona")]
        public string Poltrona { get; set; }

        [JsonProperty("servico")]
        public string Servico { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }

        [JsonProperty("dataViagem")]
        public string DataViagem { get; set; }

        [JsonProperty("bpe")]
        public Bpe Bpe { get; set; }

        [JsonProperty("origem")]
        public OrigemDestino Origem { get; set; }

        [JsonProperty("destino")]
        public OrigemDestino Destino { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cancelado")]
        public bool Cancelado { get; set; }

        [JsonProperty("motivoCancelamento")]
        public string MotivoCancelamento { get; set; }

        [JsonProperty("dadosGatewayPagamento")]
        public List<object> DadosGatewayPagamento { get; set; }
    }
}
