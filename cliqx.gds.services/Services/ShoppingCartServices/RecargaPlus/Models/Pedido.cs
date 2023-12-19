using cliqx.gds.contract.Enums;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models
{
    public class Pedido
    {
        [JsonProperty("pedidoId")]
        public long PedidoId;

        [JsonProperty("clienteId")]
        public int ClienteId;

        [JsonProperty("lojaId")]
        public int LojaId;

        [JsonProperty("tipoPedidoId")]
        public TipoPedido TipoPedidoId;

        [JsonProperty("valorTotal")]
        public decimal ValorTotal;

        [JsonProperty("itens")]
        public List<Iten> Itens;
    }


    public class Iten
    {

        [JsonProperty("id")]
        public long id;

        [JsonProperty("nome")]
        public string Nome;

        [JsonProperty("quantidade")]
        public int Quantidade;

        [JsonProperty("valorUnitario")]
        public decimal ValorUnitario;

        [JsonProperty("descricoes")]
        public List<Descrico> Descricoes;
    }


    public class Descrico
    {
        [JsonProperty("chave")]
        public string Chave;

        [JsonProperty("valor")]
        public string Valor;

        [JsonProperty("exibir")]
        public string Exibir { get; set; } = "S";

        [JsonProperty("posicao")]
        public int Posicao { get; set; } = 0;

        [JsonProperty("titulo")]
        public string Titulo { get; set; } = string.Empty;
    }
}
