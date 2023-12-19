using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class AuthenticatedUser
{
    [JsonProperty("user")]
    public User User { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}
