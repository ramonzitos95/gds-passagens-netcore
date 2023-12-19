using distribusion.api.client.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace distribusion.api.client.Models.Basic
{
    public class BasicObject : IDumpable
    {
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectType Type { get; set; }
    }
}