using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Enums;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Extensions
{
    public static class ModoCapturaExtensions
    {
        public static string AsModoCaptura(this PaymentModeEnum paymentMode)
        {
            if(paymentMode == PaymentModeEnum.PIX)
                return PaymentMode.PIX;

            if(paymentMode == PaymentModeEnum.CREDIT_CARD_IN_FULL || paymentMode == PaymentModeEnum.CREDIT_CARD_IN_INSTALLMENTS)
                return PaymentMode.CARTAO;

            return "";
        }
    }
}