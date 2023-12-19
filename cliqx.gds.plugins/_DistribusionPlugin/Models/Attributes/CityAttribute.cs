using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class CityAttribute : BasicAttribute
    {
        [JsonProperty("subdivision_code")] 
        public string SubdivisionCode { get; set; }
    }
}