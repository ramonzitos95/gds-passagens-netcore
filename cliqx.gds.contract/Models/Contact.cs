using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels.Enum;

namespace cliqx.gds.contract.Models;

public class Contact
{
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    public ContactTypeEnum ContactType { get; set; }
    public string Value { get; set; }


}
