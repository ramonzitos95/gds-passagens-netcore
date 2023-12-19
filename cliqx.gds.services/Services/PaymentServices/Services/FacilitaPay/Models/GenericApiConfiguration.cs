namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class GenericApiConfiguration
    {
        public string MediaId { get; set; }
        public string ApiBaseUrl { get; set; }
        public string PluginConfiguration { get; set; }
        public PluginFacilita Plugin { get; set; }
    }
}