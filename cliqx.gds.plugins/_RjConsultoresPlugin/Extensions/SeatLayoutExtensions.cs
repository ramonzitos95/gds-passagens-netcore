


using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Extensions
{
    public static class SeatLayoutExtensions
    {
        public static IEnumerable<Seat> AsSeat (this Onibus seatLayoutResponse , Trip trip = null)
        {
            var retorno =  new List<Seat>();


           foreach (var item in seatLayoutResponse.MapaPoltrona)
           {
                var seat = new Seat();
                var layout = new SeatLayout();

                seat.Id = item.Numero;
                seat.Description = $"Poltrona: {item.Numero}";
                seat.Number = item.Numero;

                if(trip is not null)
                {
                    seat.Value = Convert.ToInt64(trip.Value);
                    seat.Class = trip.Class.Name;
                }
                
                seat.Vacant = item.Disponivel ? true : false;

                //seat.ArrivalTax = Convert.ToInt64(seatLayoutResponse.Data.DetalhePreco.Tarifa * 100);
                

                layout.X = int.Parse(item.X);
                layout.Y = int.Parse(item.Y);
                //layout.Z = item.PosicaoZ;

                seat.Layout = layout;

                seat.TripId = trip?.Id;

                retorno.Add(seat);
           }

            return retorno;
        }
    }
}
