using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class BloqueioPoltronaRequest : ConsultaCategoriaServicoRequest
    {
        [JsonProperty("poltrona")]
        public string Poltrona { get; set; }

        public BloqueioPoltronaRequest()
        {
            
        }


        public BloqueioPoltronaRequest(string origem, string destino, string data, string servico, string poltrona) : base(origem, destino, data, servico)
        {
            Poltrona = poltrona;
        }
    }
}
