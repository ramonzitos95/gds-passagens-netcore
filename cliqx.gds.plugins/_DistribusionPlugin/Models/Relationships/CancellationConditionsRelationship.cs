using distribusion.api.client.Models.Basic;
using Newtonsoft.Json;

namespace distribusion.api.client.Models.Relationships
{
    public class CancellationConditionsRelationship
    {
        [JsonProperty("deductions")]
        public ListBasicRelationshipObject Deductions { get; set; }
    }
}
