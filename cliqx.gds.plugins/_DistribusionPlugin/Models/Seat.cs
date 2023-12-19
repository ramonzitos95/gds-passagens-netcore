using distribusion.api.client.Models.Attributes;
using distribusion.api.client.Models.Basic;
using distribusion.api.client.Models.Relationships;
using Newtonsoft.Json;

namespace distribusion.api.client.Models
{
    public class Seat : BasicObject<SeatAttribute, NoneRelationship>
    {
        [JsonProperty("segment_index", NullValueHandling = NullValueHandling.Ignore)]
        public int SegmentIndex { get; set; }

        [JsonProperty("seat_code", NullValueHandling = NullValueHandling.Ignore)]
        public string SeatCode { get; set; }
    }
}