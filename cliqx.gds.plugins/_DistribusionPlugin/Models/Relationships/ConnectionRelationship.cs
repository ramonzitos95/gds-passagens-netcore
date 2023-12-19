using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{

    public class ConnectionFareClassRelationship : BasicObject
    {
        public string Code { get; set; }
    }
    
    public class ConnectionFareRelationship : BasicObject
    {
        public long Price { get; set; }
        
        [JsonProperty("fare_class")]
        public ConnectionFareClassRelationship FareClass { get; set; }

        //public string Type { get; set; }
    }
    
    public class ConnectionRelationship
    {
        public BasicRelationshipObjectArray Segments { get; set; }
        
        [JsonProperty("marketing_carrier")]
        public BasicRelationshipObject MarketingCarrier { get; set; }
        
        public BasicRelationshipObjectArrayT<ConnectionFareRelationship> Fares { get; set; }
        
        [JsonProperty("departure_station")]
        public BasicRelationshipObject DepartureStation { get; set; }
        
        [JsonProperty("arrival_station")]
        public BasicRelationshipObject ArrivalStation { get; set; }
    }

}