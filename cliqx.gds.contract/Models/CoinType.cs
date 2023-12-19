using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Global;

namespace cliqx.gds.contract.Models;

public class CoinType : BasicObjectInt
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();

    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(300)")]
    public string? Description { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Label { get; set; }

}
