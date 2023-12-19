using System.Globalization;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class Corrida
    {
        [JsonProperty("origem")]
        public OrigemDestino Origem { get; set; }

        [JsonProperty("destino")]
        public OrigemDestino Destino { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("lsServicos")]
        public List<Servico> LsServicos { get; set; }

    }
}
