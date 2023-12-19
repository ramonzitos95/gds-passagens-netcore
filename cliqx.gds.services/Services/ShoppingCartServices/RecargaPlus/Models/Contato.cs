using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class Contato
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("telefone")]
    public string Telefone { get; set; }
}

