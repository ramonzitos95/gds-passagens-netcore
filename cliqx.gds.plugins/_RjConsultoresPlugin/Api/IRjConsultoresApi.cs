
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Request;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;
using Refit;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Api
{
    public interface IRjConsultoresApi
    {
        [Get("/localidade/buscaOrigem")]
        public Task<ApiResponse<IEnumerable<OrigemDestino>>> BuscaOrigem();

        [Get("/localidade/buscaDestino/{origemId}")]
        public Task<ApiResponse<IEnumerable<OrigemDestino>>> BuscaDestino(string origemId);

        [Get("/categoria/consultarCategorias")]
        public Task<ApiResponse<IEnumerable<CategoriaServico>>> ConsultarCategorias();

        [Get("/catalogo/consultarMoedas")]
        public Task<ApiResponse<IEnumerable<Moeda>>> ConsultarMoedas();

        [Post("/consultacorrida/buscaCorrida")]
        public Task<ApiResponse<Corrida>> BuscaCorrida(BuscaCorridaRequest buscaCorridaRequest);

        [Post("/categoria/consultarCategoriasCorrida")]
        public Task<ApiResponse<IEnumerable<CategoriaCorrida>>> ConsultarCategoriasCorrida(ConsultaCategoriaServicoRequest consultaCategoriaServicoRequest);

        [Post("/consultaonibus/buscaOnibus")]
        public Task<ApiResponse<Onibus>> BuscaOnibus(ConsultaCategoriaServicoRequest consultaCategoriaServicoRequest);

        [Post("/consultabilhete/consultarBilhete")]
        public Task<ApiResponse<IEnumerable<Bilhete>>> ConsultarBilhete(ConsultaBilheteRequest consultarBilheteRequest);

        [Post("/bloqueiopoltrona/bloquearPoltrona")]
        public Task<ApiResponse<BloqueioPoltronaResponse>> BloquearPoltrona(BloqueioPoltronaRequest bloquearPoltronaRequest);

        [Post("/desbloqueiopoltrona/desbloquearPoltrona")]
        public Task<ApiResponse<StatusTransacao>> DesbloquearPoltrona(TransacaoRequest transacaoRequest);

        [Post("/confirmavenda/confirmarVenda")]
        public Task<ApiResponse<ConfirmaVendaResponse>> ConfirmarVenda(ConfirmaVendaRequest confirmaVendaRequest);

        [Post("/cancelavenda/cancelarVenda")]
        public Task<ApiResponse<StatusTransacao>> CancelarBilhete(CancelaBilheteRequest cancelaBilheteRequest);
    }
}