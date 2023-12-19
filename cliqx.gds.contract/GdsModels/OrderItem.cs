using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class OrderItem : BaseObjectPlugin
{
    [Required]
    public TravelDirectionEnum TravelDirectionType { get; set; }

    [Required]
    public Trip Trip { get; set; }
    public long TotalValue { get; set; }
    public long DiscountValue { get; set; }
    public long Value { get; set; }

    public decimal TotalValueAsDecimal => TotalValue / 100M;
    public decimal ValueAsDecimal => Value / 100M;

    /// <summary>
    /// Valor do ped�gio
    /// </summary>
    public decimal PedagogueValue { get; set; }

    public decimal DepartureTax { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
