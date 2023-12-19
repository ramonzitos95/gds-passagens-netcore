


using cliqx.gds.contract.GdsModels;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;

namespace cliqx.gds.plugins._SmartbusPlugin.Extensions
{
    public static class SeatLayoutExtensions
    {
        public static IEnumerable<Seat> AsSeat (this MapaResponse seatLayoutResponse )
        {
            var retorno =  new List<Seat>();


           foreach (var item in seatLayoutResponse.Data.assentos)
           {
                var seat = new Seat();
                var layout = new SeatLayout();

                seat.Id = item.Numero;
                seat.Description = $"Poltrona: {item.Numero}";
                seat.Number = item.Numero;
                seat.Value = Convert.ToInt64(seatLayoutResponse.Data.Preco * 100) ;
                seat.Vacant = !item.Ocupado ? true : false;
                seat.Class = seatLayoutResponse.Data.ClasseServico;

                seat.ArrivalTax = Convert.ToInt64(seatLayoutResponse.Data.DetalhePreco.Tarifa * 100);
                

                layout.X = item.PosicaoX;
                layout.Y = item.PosicaoY;
                layout.Z = item.PosicaoZ;

                seat.Layout = layout;

                retorno.Add(seat);
           }

            return retorno;
        }
    }
}
