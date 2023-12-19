
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using Refit;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay
{
    public class FacilitaPayApi : IFacilitaPayApi
    {
        private readonly HttpClient _httpClient;
        
        public IFacilitaPayApi Client { get; private set; }

        public FacilitaPayApi(string baseUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl.TrimEnd('/'));
            Client = RestService.For<IFacilitaPayApi>(_httpClient);
        }

        public Task<ApiResponse<CardCapture.Response>> CardCapture(CardCapture.Resquest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PagamentoCartaoResponse>> GeneratePaymentByCard(PagamentoCartaoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}