using cliqx.gds.plugins._SmartbusPlugin.Models.Request;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Refit;

namespace cliqx.gds.plugins._SmartbusPlugin.Api;

public interface ISmartbusApi
{
    [Get("/localidades")]
    public Task<ApiResponse<LocalidadeResponse>> GetLocalidades();

    [Post("/consulta")]
    public Task<ApiResponse<ConsultaResponse>> Consulta(ConsultaRequest consulta);
    
    [Post("/viagem")]
    public Task<ApiResponse<MapaResponse>> BuscarPoltronas(RequestMapa request);

    [Post("/reserva")]
    public Task<ApiResponse<ReservaResponse>> CriarReserva(RequestReserva request);

    [Post("/confirmacao")]
    public Task<ApiResponse<ConfirmacaoResponse>> CriarConfirmacao(ConfirmacaoRequest request);

    [Post("/cancelamentobilhete")]
    public Task<ApiResponse<CancelamentoResponse>> CancelarBilhete(CancelamentoRequest request);

    [Post("/statusbilhete")]
    public Task<ApiResponse<StatusBilheteResponse>> BuscarStatusBilhete(StatusBilheteRequest request);

    [Post("/cancelamento")]
    public Task<ApiResponse<DesbloqueioResponse>> DesbloquearPoltrona(DesbloqueioRequest request);


}
