using distribusion.api.client.Request;
using distribusion.api.client.Response;
using Refit;

namespace distribusion.api.client.Api
{
    public interface IDistribusionApi
    {
        [Get("/retailers/v4/marketing_carriers/{marketingCarrier}/stations")]
        public Task<ApiResponse<MarketingCarrierStationsResponse>> GetMarketingCarrierStations(string marketingCarrier);

        [Get("/retailers/v4/stations")]
        public Task<ApiResponse<StationsResponse>> GetAllStations(string locale = "pt");

        [Get("/retailers/v4/marketing_carriers")]
        public Task<ApiResponse<MarketingCarrierResponse>> GetMarketingCarrier(string locale = "pt");

        [Get("/retailers/v4/connections/find")]
        public Task<ApiResponse<ConnectionsResponse>> GetConnections(
            [AliasAs("departure_stations[]")] [Query(CollectionFormat.Multi)] IEnumerable<string> departureStationIds,
            [AliasAs("arrival_stations[]")] [Query(CollectionFormat.Multi)] IEnumerable<string> arrivalStationIds,
            [AliasAs("departure_date")] string date,
            [AliasAs("departure_start_time")] TimeSpan searchStartTime,
            [AliasAs("departure_end_time")] TimeSpan searchEndTime,
            [AliasAs("departure_city")] string departureCityCode,
            [AliasAs("arrival_city")] string arrivalCityCode,
            int pax = 1,
            string locale = "pt",
            string currency = "BRL"
        );

        [Get("/retailers/v4/connections/seats")]
        public Task<ApiResponse<SeatLayoutResponse>> GetSeatMap(
            [AliasAs("marketing_carrier")] string marketingCarrierId,
            [AliasAs("departure_station")] string departureStationId,
            [AliasAs("arrival_station")] string arrivalStationId,
            [AliasAs("departure_time")] string departureDateTime,
            [AliasAs("arrival_time")] string arrivalDateTime
        );
        
        [Post("/retailers/v4/reservations/create")]
        public Task<ApiResponse<SeatReservationCreateResponse>> PostSeatReservationCreate(SeatReservationCreateRequest seatReservationCreateRequest);

        [Put("/retailers/v4/reservations/cancel")]
        public Task<ApiResponse<SeatReservationCreateResponse>> SeatReservationCancel(string reservation_id);

        [Put("/retailers/v4/reservations/confirm")]
        public Task<ApiResponse<SeatReservationCreateResponse>> ConfirmReservationCreate(ConfirmReservationCreateRequest confirmReservationCreateRequest);

        [Get("/retailers/v4/reservations/{reservation_id}")]
        public Task<ApiResponse<SeatReservationCreateResponse>> GetReservation(string reservation_id);

        [Get("/retailers/v4/bookings/{reservation_id}/tickets")]
        public Task<HttpContent> GetTicket(string reservation_id);

        [Get("/retailers/v4/cancellations/conditions")]
        public Task<ApiResponse<CancellationConditionsResponse>> GetCancellationConditions(string booking);

        [Post("/retailers/v4/cancellations/create")]
        public Task<ApiResponse<CancellationCreateResponse>> CancellationCreate(CancellationCreateRequest cancellationCreateRequest);
    }
}