using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Extensions
{
    public static class PaymentExtensions
    {
        public static Payment AsPayment(this CardCapture.Response resp, Payment paymentReq, AuthModel authModel)
        {
            var payment = new Payment();

            payment.Url = resp.Link;
            payment.Id = resp.Uuid;

            payment.PaymentMechanism = paymentReq.PaymentMechanism;
            payment.MaxInstalments = Convert.ToInt32(authModel.MaxParcelas);
            payment.Instruction = paymentReq.Instruction;
            payment.Instalments = paymentReq.Instalments;

            payment.ExpirationDate = DateTime.Now.AddSeconds(authModel.ExpirationSeconds);

            return payment;
        }
    }
}