using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class PluginOptions
    {
        [JsonProperty("navigateCategory", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool NavigateCategory { get; set; }

        [JsonProperty("orderOriginFilterType", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string OrderOriginFilterType { get; set; }

        [JsonProperty("setEnterpriseNumber", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool SetEnterpriseNumber { get; set; }

        [JsonProperty("sortOrderBy", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string SortOrderBy { get; set; }
    }
}