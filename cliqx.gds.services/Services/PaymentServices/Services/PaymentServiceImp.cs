using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.PaymentServices.Services.DefaultPay;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services
{
    public class PaymentServiceImp : IPaymentService
    {
        private readonly IPaymentService _paymentService;
        private PluginConfiguration pluginConfiguration;

        public PaymentServiceImp(PluginConfiguration pluginConfiguration)
        {
            _paymentService = pluginConfiguration.PaymentServiceId switch
            {
                1 => new DefaultPayService(pluginConfiguration.PaymentService),
                2 => new FacilitaPayService(pluginConfiguration.PaymentService),
                _ => throw new Exception("Pagamento service não configurado")
            };
        }

        public Task<string> CalculatePaymentTax()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GenerateUrlPayment(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPaymentMechanisms()
        {
            throw new NotImplementedException();
        }

        public async Task<PagamentoCartaoResponse> GerarCheckoutTransparente(PagamentoCartaoRequest data)
        {
            return await _paymentService.GerarCheckoutTransparente(data);
        }
    }
}
