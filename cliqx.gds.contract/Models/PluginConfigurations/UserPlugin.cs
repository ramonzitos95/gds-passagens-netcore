using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Identity;

namespace cliqx.gds.contract.Models.PluginConfigurations
{
    public class UserPlugin
    {
        public long Id { get; set; }
        public int Index { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        [Column(TypeName ="varchar(36) COLLATE ascii_general_ci")]
        public Guid PluginConfigurationsId { get; set; }
        public PluginConfiguration PluginConfigurations { get; set; }
    }
}