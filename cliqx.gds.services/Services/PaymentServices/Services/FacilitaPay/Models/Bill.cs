using cliqx.gds.contract.GdsModels;
using Newtonsoft.Json;

namespace cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models
{
    public class Bill
    {
        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }
        
        [JsonProperty("instructions")]
        public string Instructions { get; set; }
        
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        
        public Bill(Order order)
        {
            ExpirationDate= order.Payment.ExpirationDate;
            Instructions = order.Payment.Instruction;
            Customer = new Customer(order);
        }
    }
}