using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class Order : OrderBase
{
    public List<OrderItem> Items { get; set; }
}
