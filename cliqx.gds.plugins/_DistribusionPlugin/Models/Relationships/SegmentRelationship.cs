using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{

    public class SegmentRelationship
    {
        [JsonProperty("departure_station")]
        public BasicRelationshipObject DepartureStation { get; set; }
        
        [JsonProperty("arrival_station")]
        public BasicRelationshipObject ArrivalStation { get; set; }
        
        [JsonProperty("operating_carrier")]
        public BasicRelationshipObject OperatingCarrier { get; set; }
        
        public BasicRelationshipObject Vehicle { get; set; }
        
        [JsonProperty("marketing_carrier")]
        public BasicRelationshipObject MarketingCarrier { get; set; }
    }

}