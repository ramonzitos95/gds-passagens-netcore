using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Global;

namespace cliqx.gds.contract.Models;

public class ContactType : BasicObjectInt
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();

    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

}
