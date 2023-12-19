using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Enums;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions
{
    public static class PaymentExtensions
    {
        public static PaymentModeEnum AsPaymentMode(this string name)
        {
            if(name.Equals("Cartão credito - parcelado"))
                return PaymentModeEnum.CREDIT_CARD_IN_INSTALLMENTS;

            if(name.Equals("Pix"))
                return PaymentModeEnum.PIX;

            if(name.Equals("Cartão credito - à vista"))
                return PaymentModeEnum.CREDIT_CARD_IN_FULL;

            return PaymentModeEnum.PIX;
        }
    }
}