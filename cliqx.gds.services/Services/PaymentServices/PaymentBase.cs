using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.services.Services.PaymentServices
{
    public class PaymentBase
    {
        private readonly PaymentService paymentService;

        protected PaymentBase(
            PaymentService paymentService
        )
        {
            this.paymentService = paymentService;
        }
    }
}