using System;
using System.Collections.Generic;
using System.Linq;
using distribusion.api.client.Models;
using distribusion.api.client.Models.Basic;
using distribusion.api.client.Response;


namespace cliqx.gds.plugins._DistribusionPlugin.Extensions
{
    public static class SeatLayoutExtensions
    {
        public static IEnumerable<cliqx.gds.contract.GdsModels.Seat> AsSeat (this SeatLayoutResponse seatLayoutResponse, cliqx.gds.contract.GdsModels.Trip trip, List<BasicObject> includedList )
        {
            var retorno =  new List<cliqx.gds.contract.GdsModels.Seat>();

            

            var listaPoltronasClasse = includedList
                        //.Where(seat => seat is distribusion.api.client.Models.Seat && trip.Class.Id.Equals(((distribusion.api.client.Models.Seat)seat).Attributes.FareClass))
                        .Where(seat => seat is distribusion.api.client.Models.Seat)
                        .Select(seat => (distribusion.api.client.Models.Seat)seat).ToList();


           foreach (var item in listaPoltronasClasse)
           {
                var seat = new cliqx.gds.contract.GdsModels.Seat();
                var layout = new cliqx.gds.contract.GdsModels.SeatLayout();

                var number = item.Attributes.Label.Length < 2 
                    ? "0" + item.Attributes.Label 
                    : item.Attributes.Label;

                seat.Id = item.Id;
                seat.Description = $"Poltrona: {item.Attributes.Label}";
                seat.Number = item.Attributes.Label;
                seat.Value = trip.Value;
                seat.DiscountValue = trip.DiscountValue;
                seat.Vacant = item.Attributes.Vacant ? true : false;
                seat.Class = item.Id;

                layout.X = item.Attributes.Coordinates.X;
                layout.Y = item.Attributes.Coordinates.Y;
                layout.Z = item.Attributes.Coordinates.Z;

                seat.Layout = layout;

                seat.TripId = trip.Id;

                retorno.Add(seat);
           }

            return retorno;
        }
    }
}
