using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal
{
    public class AuthModel
    {
        public string TokenTreeal { get; set; }
        public string PixKeyTreeal { get; set; }
        public int ExpirationSecondsTreeal { get; set; }
        public string UrlReturnTreeal { get; set; }
    }
}