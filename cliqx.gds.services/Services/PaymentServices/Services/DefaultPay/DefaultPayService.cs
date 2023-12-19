using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal.Api;
using cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal.Extensions;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay
{
    public class DefaultPayService : PaymentBase, IPaymentService
    {
        private readonly PaymentService _paymentService;
        private readonly _Treeal.AuthModel _authModelTreeal;
        private readonly _Treeal.ApiUrlsBase _urlsBaseTreeal;
        private readonly TreealApi _treealApi;

        public DefaultPayService(
            PaymentService paymentService
        ) : base (paymentService)
        {
            _paymentService = paymentService;
            _authModelTreeal = JsonConvert.DeserializeObject<_Treeal.AuthModel>(paymentService.CredentialsJsonObject);
            _urlsBaseTreeal = JsonConvert.DeserializeObject<_Treeal.ApiUrlsBase>(paymentService.ApiBaseUrl);

            _treealApi = new TreealApi(_urlsBaseTreeal,_authModelTreeal);

        }
        public Task<string> CalculatePaymentTax()
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> GenerateUrlPayment(Order order)
        {
            try
            {
                if (order.Payment.PaymentMechanism.Equals(PaymentModeEnum.PIX))
                    return await GeneratePix(order);

                throw new Exception("Mecanismo de pagamento inválido!");
            }
            catch (System.Exception e)
            {
                var err = $"Error GenerateUrlPayment: {e.Message}";
                Console.WriteLine(err);
            }


            return new Payment();
        }

        public Task<PagamentoCartaoResponse> GerarCheckoutTransparente(PagamentoCartaoRequest data)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPaymentMechanisms()
        {
            throw new NotImplementedException();
        }

        private async Task<Payment> GeneratePix(Order order)
        {
            var request = new _Treeal.Request()
            {
                Amount = order.TotalValueAsDecimal
                ,ExpirationSeconds = _authModelTreeal.ExpirationSecondsTreeal
                ,PayerDocumentNumber = order.Client.Documents.FirstOrDefault(x => x.DocumentType.Equals(DocumentTypeEnum.CPF)).Value
                ,PayerName = order.Client.FullName
                ,PayerPersonType = "natural"
                ,PayerRequest = "none"
                ,PixKey = _authModelTreeal.PixKeyTreeal
                ,QrCodeType = "dynamic_instant"
                ,UrlReturn = _authModelTreeal.UrlReturnTreeal
                ,AdditionalData = new List<_Treeal.AdditionalDatum>()
                    { 
                        new _Treeal.AdditionalDatum(){KeyName = "Pedido", Value = order.Id}
                        ,new _Treeal.AdditionalDatum(){KeyName = "Empresa", Value = "Snog"}
                    }
            };

            var response = await _treealApi.Client.GeneratePix(request);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error GeneratePix: ");
                var err = (response.Error, $"Retorno da treeal: {response.Error.Content}");
                Console.WriteLine(err);
                throw new Exception(err.ToString());
            }

            return response.Content.AsPayment();
        }
    }
}