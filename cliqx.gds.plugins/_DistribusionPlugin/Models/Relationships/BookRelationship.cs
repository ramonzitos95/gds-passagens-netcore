using System;
using System.Collections.Generic;
using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{
    public class BookRelationship
    {
        [JsonProperty("fare_classes")]
        public BasicRelationshipObject FareClass { get; set; }

        [JsonProperty("outbound_connection")]
        public BasicRelationshipObject OutboundConnection { get; set; }

        [JsonProperty("inbound_connection")]
        public BasicRelationshipObject InboundConnection { get; set; }

        [JsonProperty("segments")]
        public ListBasicRelationshipObject Segments { get; set; }

        [JsonProperty("passengers")]
        public ListBasicRelationshipObject Passengers { get; set; }

        [JsonProperty("fees")]
        public ListBasicRelationshipObject Fees { get; set; }
    }
}
