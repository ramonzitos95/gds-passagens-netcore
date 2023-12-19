using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request
{
    public class PedidoRequest
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
        public decimal ValorTotal { get; set; }

        [JsonProperty("itens")]
        public List<ItemPedido> Itens { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        public long? VendedorId { get; set; } 
        public long? ParceiroId { get; set; }

        public PedidoRequest() { }

        public PedidoRequest(PedidoResponse pedido, Order order, string uidPagamento)
        {
            PedidoId = pedido.PedidoId;
            ClienteId = pedido.ClienteId;
            LojaId = pedido.LojaId;
            TipoPedidoId = pedido.TipoPedidoId;
            ValorTotal = order.TotalValueAsDecimal; //+ order.DeliveryFeeAsDecimal;
            Itens = new List<ItemPedido>();
            VendedorId = 0;

            var itemPedido = new ItemPedido()
            {
                Nome = "PAGAMENTO",
                Quantidade = 1,
                ValorUnitario = 0,
                Descricoes = new List<Descricao>()
            };

            Descricao itemDescricao = new Descricao()
            {
                Chave = "LINK",
                Valor = order.Payment.Url,
                Exibir = "S",
                Posicao = 0,
                Titulo = "Link pagamento"
            };

            itemPedido.Descricoes.Add(itemDescricao);

            itemDescricao = new Descricao()
            {
                Chave = "CHAVE_REFERENCIA",
                Valor = uidPagamento,
                Exibir = "S",
                Posicao = 0,
                Titulo = "Link referencia"
            };

            itemPedido.Descricoes.Add(itemDescricao);
            Itens.Add(itemPedido);
        }
    }

    public class Descricao
    {
        [JsonProperty("chave")]
        public string Chave { get; set; }

        [JsonProperty("valor")]
        public string? Valor { get; set; }

        [JsonProperty("exibir")]
        public string Exibir { get; set; } = "N";

        [JsonProperty("posicao")]
        public int Posicao { get; set; } = 0;

        [JsonProperty("titulo")]
        public string Titulo { get; set; }
    }

    public class Item
    {
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

        public Item() { }
    }
}
