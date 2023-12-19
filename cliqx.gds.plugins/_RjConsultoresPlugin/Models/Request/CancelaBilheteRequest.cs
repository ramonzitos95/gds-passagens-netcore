using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class CancelaBilheteRequest
    {
        [JsonProperty("transacao")]
        public string Transacao { get; set; }

        [JsonProperty("validarMulta")]
        public bool ValidarMulta { get; set; }
    }
}
