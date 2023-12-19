using Newtonsoft.Json;


namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class TransacaoRequest
    {

        [JsonProperty("transacao")]
        public string Transacao { get; set; }

    }
}
