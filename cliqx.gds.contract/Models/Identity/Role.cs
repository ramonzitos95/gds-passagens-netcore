using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace cliqx.gds.contract.Models.Identity;

[NotMapped]
public class Role : IdentityRole<int>
{
    public List<UserRole>? UserRoles { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();

}
