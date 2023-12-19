

using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Request
{
    public partial class StatusBilheteRequest
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

        [JsonProperty("nomePassageiro")]
        public string NomePassageiro { get; set; }

        [JsonProperty("documentoPassageiro")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DocumentoPassageiro { get; set; }

        [JsonProperty("seguro")]
        public bool Seguro { get; set; }

        [JsonProperty("transacaoId")]
        public string TransacaoId { get; set; }
    }


}