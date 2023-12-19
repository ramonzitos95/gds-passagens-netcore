using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class PreOrderItem : BaseObjectPlugin
{
    public string PreOrderId { get; set; }
    [Required]
    public TravelDirectionEnum TravelDirectionType { get; set; }

    [Required]
    public Trip Trip { get; set; }

    public long TotalValue { get; set; }
    public long DiscountValue { get; set; }
    public long Value { get; set; }
    
    public decimal TotalValueAsDecimal => TotalValue / 100M;
    public decimal ValueAsDecimal => Value / 100M;
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Valor do pedágio
    /// </summary>
    public decimal PedagogueValue { get; set; }
    public decimal DepartureTax { get; set; }
    
}
