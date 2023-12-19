using cliqx.gds.contract.GdsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public static class GeneratePreOrderItem
    {
        public static PreOrderItem GenerateAsPreOrderItem(Seat seat)
        {
            if (seat == null) throw new ArgumentNullException("Poltrona estava nula");

            return new PreOrderItem() 
            { 
                //Seat = seat 
            };
        }

        public static List<PreOrderItem> GenerateAsPreOrderItensWithSeats(List<Seat> seats)
        {
            if (seats == null) throw new ArgumentNullException("Poltrona estava nula");

            List<PreOrderItem> result = new List<PreOrderItem>();
            foreach (var seat in seats)
            {
                result.Add(new PreOrderItem()
                {
                    //Seat = seat
                });
            }

            return result;
        }
    }
}
