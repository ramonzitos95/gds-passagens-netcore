using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cliqx.gds.plugins._SmartbusPlugin.Models.Request
{
    public class AuthRequest
    {
        [JsonProperty("Username")]
        public string UserName { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; private set; } = "password";
    }
}