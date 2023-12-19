using cliqx.gds.contract.GdsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.test.ModelsTest
{
    public class GenerateSeat
    {
        public static Seat GenerateMockSeat()
        {
            Seat seat = new();

            seat.Key = Guid.NewGuid().ToString();
            seat.Value = seat.Value;
            seat.Value = 100;
            seat.DiscountValue = 10;
            seat.ArrivalTax = 10;
            seat.Vacant = false;
            seat.Description = "Teste";
            seat.Number = "28";
            return seat;

        }
    }
}
