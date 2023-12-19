using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class ConfirmaVendaRequest
    {
        [JsonProperty("transacao")]
        public string Transacao { get; set; }

        [JsonProperty("nomePassageiro")]
        public string NomePassageiro { get; set; }

        [JsonProperty("documentoPassageiro")]
        public string DocumentoPassageiro { get; set; }
    }
}
