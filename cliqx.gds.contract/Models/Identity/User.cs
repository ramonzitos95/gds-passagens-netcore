using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using cliqx.gds.contract.Models.PluginConfigurations;
using Microsoft.AspNetCore.Identity;

namespace cliqx.gds.contract.Models.Identity;

[NotMapped]
public class User : IdentityUser<int>
{
    public string FullName { get; set; }
    public string Departamento { get; set; }
    public DateTime DataUltimoLogin { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public bool Active { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    public int CompanyId { get; set; }
    public List<UserPlugin> UsersPlugins { get; set; }
}
