using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.PaymentServices.Services.Models
{
    public class PaymentPayloadRequest : PagamentoCartaoRequest
    {

        [JsonProperty("pluginid")]
        public Guid PluginId { get; set; }
    }
}
