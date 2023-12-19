using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class Seat : BaseObjectPlugin
{
    public string Number { get; set; }
    public string Class { get; set; }
    public string Type { get; set; }
    public bool Vacant { get; set; }
    public long Value { get; set; }
    public long ArrivalTax { get; set; }
    public decimal ArrivalTaxAsDecimal => ArrivalTax / 100M;
    public decimal ValueAsDecimal => Value / 100M;
    public long DiscountValue { get; set; }
    public decimal DiscountValueAsDecimal => DiscountValue / 100M;
    public string Image { get; set; }
    public SeatLayout Layout { get; set; }

    public string TripId { get; set; }

    /// <summary>
    /// Transação
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Transaction Transaction { get; set; }
    public Ticket Ticket { get; set; }
    public Client Client { get; set; }
}
