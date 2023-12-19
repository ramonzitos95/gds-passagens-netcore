using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{

    public class MarketingCarrierRelationship
    {
        [JsonProperty("passenger_types")]
        public BasicRelationshipObjectArray PassengerTypes { get; set; }
        
        [JsonProperty("fare_classes")]
        public BasicRelationshipObjectArray FareClasses { get; set; }
        
        [JsonProperty("extra_types")]
        public BasicRelationshipObjectArray ExtraTypes { get; set; }
    }

}