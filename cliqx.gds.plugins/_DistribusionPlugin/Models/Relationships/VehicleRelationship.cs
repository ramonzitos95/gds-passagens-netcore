using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{

    public class VehicleRelationship
    {
        [JsonProperty("vehicle_type")]
        public BasicRelationshipObject VehicleType { get; set; }
    }

}