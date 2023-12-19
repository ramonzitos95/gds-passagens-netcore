
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
public class ItemPedido
{
    private ItemPedido item;

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("quantidade")]
    public int Quantidade { get; set; }

    [JsonProperty("valorUnitario")]
    public decimal ValorUnitario { get; set; }

    [JsonProperty("descricoes")]
    public List<Descricao> Descricoes { get; set; }

    public ItemPedido() { }

    public ItemPedido(ItemPedido item)
    {
        this.item = item;
        this.Id = item.Id;
        this.Nome = item.Nome;
        this.Quantidade = item.Quantidade;
        this.ValorUnitario = item.ValorUnitario;
        this.Descricoes = item.Descricoes;
    }
}
