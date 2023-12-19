using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class User
{

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
