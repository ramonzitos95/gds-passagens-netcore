using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class TipoFormaPagamento
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }
}

