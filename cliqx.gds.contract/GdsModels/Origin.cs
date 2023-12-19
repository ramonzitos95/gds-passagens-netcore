using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.contract.GdsModels;

public class Origin
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PluginConfiguration PluginConfiguration { get; set; }
}
