using System;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Models.Response
{
    public class RjConsultoresException
    {
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime timestamp { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int status { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        [JsonProperty("mensagem", NullValueHandling = NullValueHandling.Ignore)]
        public string mensagem { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string error { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
    }
}
