
using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Request;

public class RequestReserva
{
    [JsonProperty("origem")]
    public long Origem { get; set; }

    [JsonProperty("destino")]
    public long Destino { get; set; }

    [JsonProperty("data")]
    public DateTimeOffset Data { get; set; }

    [JsonProperty("servico")]
    public long Servico { get; set; }

    [JsonProperty("assento")]
    public long Assento { get; set; }

    [JsonProperty("conexao")]
    public bool Conexao { get; set; }
}
