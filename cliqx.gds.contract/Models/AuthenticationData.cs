using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.contract.Models
{
    public class AuthenticationData
    {
        public string ApiKey { get; set; }
        public string RecargaPlusStore { get; set; }
        public string RetailerPartnerNumber { get; set; }
        public string SmtpPassword { get; set; }
    }
}
