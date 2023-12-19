
using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response;

public partial class LocalidadeResponse
{
    [JsonProperty("localidadeId")]
    public long LocalidadeId { get; set; }

    [JsonProperty("localidade")]
    public string Localidade { get; set; }

    [JsonProperty("uf")]
    public string Uf { get; set; }

    [JsonProperty("latitude")]
    public string Latitude { get; set; }

    [JsonProperty("longitude")]
    public string Longitude { get; set; }

    [JsonProperty("pontosDeInteresse")]
    public object[] PontosDeInteresse { get; set; }

}


