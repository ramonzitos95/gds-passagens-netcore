using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class ValidadorPayment
    {
        public static void Validar(Payment payment)
        {
            if (payment == null)
                throw new Exception("O meio de pagamento não foi informado");

            if (!Enum.IsDefined(typeof(PaymentModeEnum), payment.PaymentMechanism))
                throw new Exception("Informe um tipo de pagamento ID válido");
        }
    }
}