using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Extensions;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay
{
    public class FacilitaPayService : PaymentBase, IPaymentService
    {
        private readonly FacilitaPayApi _facilitaPayApi;
        private readonly PaymentService _paymentService;
        private readonly AuthModel _authModel;
        private readonly ApiUrlsBase _urlsBase;

        public FacilitaPayService(
            PaymentService paymentService
            ) : base(paymentService)
        {
            _paymentService = paymentService;
            _authModel = JsonConvert.DeserializeObject<AuthModel>(paymentService.CredentialsJsonObject);
            _urlsBase = JsonConvert.DeserializeObject<ApiUrlsBase>(paymentService.ApiBaseUrl);

            _facilitaPayApi = new FacilitaPayApi(_urlsBase.UrlBase, new HttpClient());




        }

        public Task<string> CaptureCard()
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> GenerateUrlPayment(Order order)
        {
            try
            {
                var modoCaptura = order.Payment.PaymentMechanism.AsModoCaptura();

                var request = new CardCapture.Resquest(_authModel.Token, order, modoCaptura);
                Console.WriteLine($"Request: {JsonConvert.SerializeObject(request)}");

                if (order.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_FULL
                    || order.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_INSTALLMENTS)
                {
                    request.Juros = _authModel.CartaoJuros;
                    request.Splits = _authModel.MaxParcelas;
                    request.Bill.ExpirationDate = DateTime.Now.AddMinutes(Int32.Parse(_authModel.CartaoExpiracaoLink));
                }
                else if (order.Payment.PaymentMechanism.Equals(PaymentModeEnum.PIX))
                {
                    request.Bill.ExpirationDate = DateTime.Now.AddMinutes(Int32.Parse(_authModel.PixExpiracaoLink));
                    request.UrlCallback = _authModel.UrlReturn;
                    request.PixKey = _authModel.PixKey;
                }

                var apiResp = await _facilitaPayApi.Client.CardCapture(request);
                if (!apiResp.IsSuccessStatusCode)
                    throw new Exception(apiResp.Error.Content[0].ToString());

                return apiResp.Content.AsPayment(order.Payment, _authModel);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<PagamentoCartaoResponse> GerarCheckoutTransparente(PagamentoCartaoRequest data)
        {
            try
            {
                var retorno = await _facilitaPayApi.Client.GeneratePaymentByCard(data);

                if (!retorno.IsSuccessStatusCode)
                {
                    var erroObjeto = JsonConvert.DeserializeObject<PagamentoCartaoResponse>(retorno.Error.Content);
                    if (retorno.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        if (erroObjeto.Pagamento == "nao_aprovado")
                            return new PagamentoCartaoResponse() { Message = $"Pagamento não aprovado: {erroObjeto.Message}", Status = "400" };
                    }

                    return erroObjeto;
                }
                else
                    return retorno.Content;
            }
            catch (Exception ex)
            {
                return new PagamentoCartaoResponse() { Message = "Erro ao gerar pagamento: " + ex.Message, Status = "400" };
            }
        }

        public Task<string> GetPaymentMechanisms()
        {
            throw new NotImplementedException();
        }

        public Task<string> CalculatePaymentTax()
        {
            throw new NotImplementedException();
        }


    }
}