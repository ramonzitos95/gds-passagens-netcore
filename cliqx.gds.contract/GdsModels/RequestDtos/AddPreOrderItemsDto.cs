using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cliqx.gds.contract.GdsModels.RequestDtos
{
    public class AddPreOrderItemsDto : BaseObjectPlugin
    {
        public string PreOrderId { get; set; }
        public string Channel { get; set; }
        public List<PreOrderItem> Items { get; set; }
    }
}