using distribusion.api.client.Models.Basic;

namespace distribusion.api.client.Models.Attributes
{
    public class FareAttribute : BasicAttribute
    {
        public decimal Price { get; set; }
    }
}
