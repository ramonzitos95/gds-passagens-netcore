

using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{

    public class StatusBilheteResponse
    {
        [JsonProperty("data")]
        public DataStatusBilheteResponse Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class DataStatusBilheteResponse
    {
        [JsonProperty("origem")]
        public long Origem { get; set; }

        [JsonProperty("destino")]
        public long Destino { get; set; }

        [JsonProperty("data")]
        public DateTimeOffset DataData { get; set; }

        [JsonProperty("servico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Servico { get; set; }

        [JsonProperty("assento")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Assento { get; set; }

        [JsonProperty("nomePassageiro")]
        public string NomePassageiro { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
