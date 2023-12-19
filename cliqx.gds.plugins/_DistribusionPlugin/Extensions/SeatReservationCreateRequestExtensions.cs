using distribusion.api.client.Extensions;
using distribusion.api.client.Models;
using distribusion.api.client.Request;


namespace cliqx.gds.plugins._DistribusionPlugin.Extensions
{
    public static class SeatReservationCreateRequestExtensions
    {
        public static SeatReservationCreateRequest SeatReservationCreateRequest(this SeatReservationCreateRequest request, cliqx.gds.contract.GdsModels.Seat seatReq, cliqx.gds.contract.GdsModels.Trip trip)
        {
            try
            {
                var retorno = new SeatReservationCreateRequest()
                {
                    Locale = "pt",
                    Currency = "BRL",
                    MarketingCarrier =trip.Company.Id,
                    DepartureStation =trip.Station.OriginId,
                    ArrivalStation =trip.Station.DestinationId,
                    DepartureTime =trip.DepartureTime.ToString("o").Substring(0, 16),
                    ArrivalTime =trip.ArrivalTime.ToString("o").Substring(0, 16),
                    FareClass =trip.Class.Id,
                    Passengers = new List<Passenger>()
                };

                Passenger passenger = new Passenger()
                {
                    Type = distribusion.api.client.Models.Enums.PassengerType.Adult.GetApiType(),
                    Seats = new List<Seat>()
                };

                Seat seat = new Seat()
                {
                    SegmentIndex = 0,
                    SeatCode = seatReq.Number
                };

                passenger.Seats.Add(seat);
                retorno.Passengers.Add(passenger);
                return retorno;
            }
            catch (System.Exception e)
            {
                throw;
            }

        }
    }
}
