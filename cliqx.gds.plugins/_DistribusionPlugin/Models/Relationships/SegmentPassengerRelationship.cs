using System;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{
    public class SegmentPassengerRelationship : BasicRelationshipObject
    {
        [JsonProperty("passenger")]
        public BasicRelationshipObject Passenger { get; set; }
    }
}
