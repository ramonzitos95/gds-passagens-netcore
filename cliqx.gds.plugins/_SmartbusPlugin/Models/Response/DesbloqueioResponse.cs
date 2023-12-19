

using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{

    public partial class DesbloqueioResponse
    {
        [JsonProperty("data")]
        public DataDesbloqueio Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class DataDesbloqueio
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
        public object Assento { get; set; }
    }

}
