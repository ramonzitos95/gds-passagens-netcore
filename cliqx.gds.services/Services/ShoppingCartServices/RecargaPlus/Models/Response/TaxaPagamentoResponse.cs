using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
public class TaxaPagamentoResponse
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("valor")]
    public decimal Valor { get; set; }

    [JsonProperty("valorTotal")]
    public decimal ValorTotal { get; set; }
}
