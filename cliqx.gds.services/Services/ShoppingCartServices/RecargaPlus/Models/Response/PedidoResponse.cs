
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
public class PedidoResponse
{
    [JsonProperty("pedidoId")]
    public int PedidoId { get; set; }

    [JsonProperty("clienteId")]
    public int? ClienteId { get; set; }

    [JsonProperty("lojaId")]
    public int LojaId { get; set; }

    [JsonProperty("tipoPedidoId")]
    public int TipoPedidoId { get; set; }

    [JsonProperty("valorTotal")]
    public decimal? ValorTotal { get; set; }

    [JsonProperty("itens")]
    public List<ItemPedido> Itens { get; set; }

    [JsonProperty("dataCadastro")]
    public DateTime? DataCadastro { get; set; }

    [JsonProperty("statusPedidoId")]
    public long? StatusPedidoId { get; set; }
}
