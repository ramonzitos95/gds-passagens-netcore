
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Request
{


    public partial class DesbloqueioRequest
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

        [JsonProperty("transacaoId")]
        public string TransacaoId { get; set; }

        [JsonProperty("conexao")]
        public bool Conexao { get; set; }
    }

}
