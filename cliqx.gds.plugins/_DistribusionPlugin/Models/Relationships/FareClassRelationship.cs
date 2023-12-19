using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{

    public class FareClassRelationship
    {
        [JsonProperty("fare_features")]
        public BasicRelationshipObjectArray FareFeatures { get; set; }
    }

}