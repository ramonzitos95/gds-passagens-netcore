using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Api
{
    public class AuthModel
    {
        public string Authorization { get; set; }
        public string TenantId { get; set; }
        public string RecargaPlusUser { get; set; }
        public string RecargaPlusPassword { get; set; }
        public string RecargaPlusStore { get; set; }
    }
}