using distribusion.api.client.Models.Basic;

namespace distribusion.api.client.Models.Relationships
{
    public class SeatLayoutRelationship
    {
        public BasicRelationshipObject Segment { get; set; }
        public BasicRelationshipObjectArray Cars { get; set; }
    }
}