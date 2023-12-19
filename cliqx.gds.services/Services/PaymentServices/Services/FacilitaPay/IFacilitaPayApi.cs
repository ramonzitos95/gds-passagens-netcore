using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using Refit;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay
{
   
    public interface IFacilitaPayApi
    {
        [Post("/pay/p/new/order2")]
        public Task<ApiResponse<CardCapture.Response>> CardCapture(CardCapture.Resquest request);

        [Post("/pay/p/v3/send/checkout")]
        public Task<ApiResponse<PagamentoCartaoResponse>> GeneratePaymentByCard(PagamentoCartaoRequest request);
    }

}