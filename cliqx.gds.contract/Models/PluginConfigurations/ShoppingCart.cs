using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Global;

namespace cliqx.gds.contract.Models.PluginConfigurations;

public class ShoppingCart : BasicObjectInt
{
    public string Name { get; set; }

    public string ApiBaseUrl { get; set; }

    [MaxLength(8192)]
    public string CredentialsJsonObject { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }
}
