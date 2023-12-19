using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.GdsModels;

public class PreOrder : OrderBase
{   
    public List<PreOrderItem> Items { get; set; }
    
}
