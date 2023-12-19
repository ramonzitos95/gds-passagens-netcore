using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels.Enum;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class PluginFacilita
    {
         public const int DEFAULT_PAGE_SIZE = 5;
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Version HubVersion { get; }

        [JsonIgnore]
        public Version PluginVersion { get; set; }

        public string Version => $"Hub: {HubVersion}, Plugin: {PluginVersion}";

        public Dictionary<CheckoutType, List<string>> CheckoutType { get; set; }
        public string CheckoutUrl { get; set; }
        public string ProductFormat { get; set; }
        public string CheckoutExtraParameters { get; set; }
        public int CategoryPageSize { get; set; }
        public int ProductPageSize { get; set; }

        public PluginOptions Options { get; set; }
    }
}