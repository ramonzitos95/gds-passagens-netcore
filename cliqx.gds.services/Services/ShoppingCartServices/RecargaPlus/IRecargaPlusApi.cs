using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using Refit;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public interface IRecargaPlusApi
    {
        [Post("/v1/account/GenerateToken")]
        public Task<ApiResponse<AuthenticatedUser>> GenerateToken(User user);

        [Get("/v1/cliente/{idLoja}/{telefone}")]
        public Task<ApiResponse<ClientResponse>> GetClient(string idLoja, string telefone);

        [Get("/v1/cliente/{id}")]
        public Task<ApiResponse<ClientResponse>> GetClientById(long id);

        [Post("/v1/cliente")]
        public Task<ApiResponse<ClientResponse>> CreateClient(ClientRequest user);


        //TODO Alterar o m�todo v2 do recargaplus de atualizar cliente pelo body
        [Put("/v1/cliente")]
        public Task<ApiResponse<ClientResponse>> UpdateClient(long id, string telefone, string nome, string sobrenome, string cpf, string email);

        [Get("/v1/loja/forma-pagamento/{idLoja}")]
        public Task<ApiResponse<IEnumerable<FormaPagamento>>> GetFormaPagamento(string idLoja);

        [Get("/v1/loja/{idLoja}")]
        public Task<ApiResponse<LojaResponse>> GetLoja(string idLoja);

        [Post("/v1/pedido")]
        public Task<ApiResponse<PedidoResponse>> CreatePedido(PedidoRequest pedidoRequest);

        [Post("/v1/pedido/AddPedidoGDS")]
        public Task<ApiResponse<PedidoResponse>> CreatePedidoGDS(PedidoRequest pedidoRequest);

        [Get("/v1/cliente/{idCliente}")]
        public Task<ApiResponse<ClientResponse>> GetClient(string idCliente);

        [Delete("/v1/cliente/{idCliente}")]
        public Task<ApiResponse<ClientResponse>> DeleteClient(string idCliente);

        [Get("/v1/pedido/{idPedido}")]
        public Task<ApiResponse<PedidoResponse>> GetPedido(string idPedido);

        [Get("/v1/pedido/findByUid/{uidPedido}")]
        public Task<ApiResponse<PedidoResponse>> GetPedidoByUid(string uidPedido);

        [Post("/v1/taxa/realizar-calculo/todas-formas-pagamento")]
        public Task<ApiResponse<IEnumerable<TaxaPagamentoResponse>>> CalcularTaxaPagamento(TaxaPagamentoRequest taxaPagamentoRequest);

        [Delete("/v1/pedido-itens/{itemId}/{pedidoId}/{clienteId}")]
        public Task<ApiResponse<Item>> RemoveItemPedido(string itemId, string pedidoId, string clienteId);

        [Put("/v1/pedido/status/{idPedido}/{novoStatus}")]
        public Task<ApiResponse<bool>> UpdateStatusPedido(int idPedido, long novoStatus);

        [Put("/v1/pedido/AtualizarStatusPedidoGDS/{idpedido}/{novostatus}")]
        public Task<ApiResponse<PedidoResponse>> UpdateStatusPedidoGDS(int idpedido, long novostatus);

        [Put("/v1/pedido")]
        public Task<ApiResponse<bool>> UpdateMetodoPagamentoPedido(string idPedido, int tipoFormaPagamentoId);

        [Put("/v1/pedido/UpdatePedidoGDS/{idPedido}/{tipoFormaPagamentoId}/{idCliente}")]
        public Task<ApiResponse<bool>> UpdatePedido(long idPedido, long tipoFormaPagamentoId, long idCliente);

        [Put("/v1/pedido/AssociarCliente/{pedidoId}/{clienteId}")]
        public Task<ApiResponse<PedidoResponse>> AssociarClienteAoPedido(string pedidoId, long clienteId);


        [Get("/v1/config/{idLoja}")]
        public Task<ApiResponse<IEnumerable<ConfiguracaoResponse>>> GetConfiguracao(string idLoja);


        [Put("/v1/pedido-itens/{itemId}")]
        public Task<ApiResponse<bool>> AddItemDescription(string itemId, List<Descricao> listaDescricao);


        [Get("/v1/pedido-itens/{valor}")]
        public Task<ApiResponse<PedidoResponse>> GetPedidoByValorItemDescricao(string valor);


        [Get("/v1/parceiro/{id}")] 
        public Task<ApiResponse<ParceiroResponse>> ObterParceiroPorId(int id);


        [Get("/v1/parceiro/ObterParceiroPorToken/{token}")]
        public Task<ApiResponse<ParceiroResponse>> ObterParceiroPorToken(string token);


        [Get("/v1/blacklist/dado-suspeito/valida/{cpf}")]
        public Task<ApiResponse<ValidaDadosSuspeito>> ValidaDadosSuspeitoBlacklist(string cpf);
    }
}