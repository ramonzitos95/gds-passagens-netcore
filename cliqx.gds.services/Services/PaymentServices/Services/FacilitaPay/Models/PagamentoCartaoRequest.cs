using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class PagamentoCartaoRequest
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("mes")]
        public string Mes { get; set; }

        [JsonProperty("ano")]
        public string Ano { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("parcela")]
        public int Parcela { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("securityCode")]
        public string SecurityCode { get; set; }
    }
}
