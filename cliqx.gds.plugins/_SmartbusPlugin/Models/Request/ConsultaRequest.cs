using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Request;

public partial class ConsultaRequest
{
    [JsonProperty("origem")]
    public long Origem { get; set; }

    [JsonProperty("destino")]
    public long Destino { get; set; }

    [JsonProperty("data")]
    public DateTimeOffset Data { get; set; }
}
