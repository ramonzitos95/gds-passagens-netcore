using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;

public class TaxaPagamentoRequest
{
    [JsonProperty("lojaId")]
    public long LojaId { get; set; }

    [JsonProperty("tipoPedidoId")]
    public int TipoPedidoId { get; set; }

    [JsonProperty("total")]
    public decimal Total { get; set; }
}
