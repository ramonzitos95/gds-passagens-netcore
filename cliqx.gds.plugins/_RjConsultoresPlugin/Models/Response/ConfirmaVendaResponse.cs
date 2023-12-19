using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class ConfirmaVendaResponse
    {
        [JsonProperty("poltrona")]
        public string Poltrona { get; set; }

        [JsonProperty("servico")]
        public string Servico { get; set; }

        [JsonProperty("transacao")]
        public string Transacao { get; set; }

        [JsonProperty("localizador")]
        public string Localizador { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }

        [JsonProperty("numeroBilhete")]
        public string NumeroBilhete { get; set; }

        [JsonProperty("certificado")]
        public string Certificado { get; set; }

        [JsonProperty("xmlBPE")]
        public string XmlBPE { get; set; }

        [JsonProperty("bpe")]
        public Bpe Bpe { get; set; }

        [JsonProperty("numeroSistema")]
        public string NumeroSistema { get; set; }

        [JsonProperty("agencia")]
        public string Agencia { get; set; }
    }
}
