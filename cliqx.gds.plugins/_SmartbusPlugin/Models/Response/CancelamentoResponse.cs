

using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response
{


    public partial class CancelamentoResponse
    {
        [JsonProperty("data")]
        public DataCancelamento Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class DataCancelamento
    {
        [JsonProperty("data")]
        public DataCancelamentoPoltrona[] Poltronas { get; set; }
    }

    public class DataCancelamentoPoltrona
    {
        [JsonProperty("origem")]
        public long Origem { get; set; }

        [JsonProperty("destino")]
        public long Destino { get; set; }

        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("servico")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Servico { get; set; }

        [JsonProperty("assento")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Assento { get; set; }
    }

}
