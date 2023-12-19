using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Attributes
{
    public class SeatAttribute : BasicAttribute
    {
        public string Label { get; set; }
        [JsonProperty("fare_class")] 
        public string FareClass { get; set; }
        public bool Vacant { get; set; }
        public SeatCoordinates Coordinates { get; set; }
    }
}