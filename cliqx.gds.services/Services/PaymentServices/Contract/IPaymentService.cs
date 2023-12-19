using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;

namespace cliqx.gds.services.Services.PaymentServices.Contract
{
    public interface IPaymentService
    {
        Task<string> GetPaymentMechanisms();
        Task<string> CalculatePaymentTax();
        Task<Payment> GenerateUrlPayment(Order order);
        Task<PagamentoCartaoResponse> GerarCheckoutTransparente(PagamentoCartaoRequest data);
    }
}