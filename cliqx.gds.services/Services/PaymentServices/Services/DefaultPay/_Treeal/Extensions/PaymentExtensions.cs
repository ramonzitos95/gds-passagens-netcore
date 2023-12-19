using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal.Extensions
{
    public static class PaymentExtensions
    {
        public static Payment AsPayment(this _Treeal.Response response)
        {
            var payment = new Payment()
            {
                ExpirationDate = DateTime.Now.AddSeconds(response.Data.ExpirationSeconds)
                , Image = response.Data.Image
                , Url = response.Data.PixUri
                ,PaymentMechanism = contract.Enums.PaymentModeEnum.PIX
                ,Instruction = "Pague em qualquer banco que aceite pix"
                ,Name = "PIX"
            };

            return payment;
        }
    }
}