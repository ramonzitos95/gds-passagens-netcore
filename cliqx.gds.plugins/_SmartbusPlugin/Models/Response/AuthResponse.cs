using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Response;

public partial class AuthResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("idGateway")]
    public long IdGateway { get; set; }

    [JsonProperty("userName")]
    public string UserName { get; set; }

    [JsonProperty(".issued")]
    public string Issued { get; set; }

    [JsonProperty(".expires")]
    public string Expires { get; set; }
}
