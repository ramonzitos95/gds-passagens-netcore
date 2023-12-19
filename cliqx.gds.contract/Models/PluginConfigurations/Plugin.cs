using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Global;

namespace cliqx.gds.contract.Models.PluginConfigurations;

public class Plugin : BasicObjectGuid
{
    public string Name { get; set; }
}
