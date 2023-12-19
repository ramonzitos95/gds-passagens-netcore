using System;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Request
{
    public class BuscaCorridaRequest
    {
        [JsonProperty("origem")]
        public string Origem { get; set; }

        [JsonProperty("destino")]
        public string Destino { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        public BuscaCorridaRequest() { }

        public BuscaCorridaRequest(string origem, string destino, string data)
        {
            Origem = origem;
            Destino = destino;
            Data = data;
        }
    }
}
