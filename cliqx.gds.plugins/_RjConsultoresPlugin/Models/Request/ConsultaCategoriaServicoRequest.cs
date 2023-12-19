using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class ConsultaCategoriaServicoRequest : BuscaCorridaRequest
    {

        [JsonProperty("servico")]
        public string Servico { get; set; }

        public ConsultaCategoriaServicoRequest() { }

        public ConsultaCategoriaServicoRequest(string origem, string destino, string data, string servico): base(origem, destino, data)
        {
            Servico = servico;
        }
    }
}
