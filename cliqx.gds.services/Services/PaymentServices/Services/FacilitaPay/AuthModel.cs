using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay
{
    public class AuthModel
    {
        public string Token { get; set; }
        public string PixKey { get; set; }
        public int ExpirationSeconds { get; set; }
        public string UrlReturn { get; set; }
        public decimal CartaoJuros { get; set; }
        public string MaxParcelas { get; set; }
        public string CartaoExpiracaoLink { get; set; }
        public string PixExpiracaoLink { get; set; }
    }
}