using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.plugins.Extensions
{
    public static class PreOrderItemExtensions
    {
        public static IEnumerable<PreOrderItem> AsPreOrderItems(this List<Connection> conns)
        {
            var list = new List<PreOrderItem>();

            foreach (var item in conns)
            {
                foreach (var seat in item.Seats)
                {
                    var preOrderItem = new PreOrderItem();

                    preOrderItem.TravelDirectionType = seat.TravelDirectionType;
                    preOrderItem.Trip.Seats[0] = seat.Seat;

                    list.Add(preOrderItem);
                }

            }

            return list;
        }
    }
}