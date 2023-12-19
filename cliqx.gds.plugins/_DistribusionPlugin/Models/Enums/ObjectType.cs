using System.Runtime.Serialization;

namespace distribusion.api.client.Models.Enums
{
    public enum ObjectType
    {
        Unknown,
        [EnumMember(Value = "areas")] 
        Area,
        [EnumMember(Value = "cities")] 
        City,
        [EnumMember(Value = "stations")] 
        Station,

        [EnumMember(Value = "marketing_carriers")]
        MarketingCarrier,
        [EnumMember(Value = "segments")] 
        Segment,
        [EnumMember(Value = "connections")] 
        Connection,
        [EnumMember(Value = "seat_layouts")] 
        SeatLayout,
        [EnumMember(Value = "cars")] 
        Car,
        [EnumMember(Value = "passenger_types")]
        PassengerType,
        [EnumMember(Value = "fare_classes")]
        FareClass,
        [EnumMember(Value = "fares")]
        Fare,
        [EnumMember(Value = "fare_features")]
        FareFeature,
        [EnumMember(Value = "extra_types")]
        ExtraType,
        [EnumMember(Value = "vehicles")]
        Vehicle,
        [EnumMember(Value = "vehicle_types")]
        VehicleType,
        [EnumMember(Value = "operating_carriers")]
        OperatingCarrier,
        [EnumMember(Value = "seats")]
        Seat,
        [EnumMember(Value = "reservations")]
        SeatReservatoionCreateResponse,
        [EnumMember(Value = "bookings")]
        Book,
        [EnumMember(Value = "fees")]
        Fee,
        [EnumMember(Value = "segment_passengers")]
        SegmentPassenger,
        [EnumMember(Value = "passengers")]
        PassengerBasicObject,
        [EnumMember(Value= "ticket_validity_rules")]
        TicketValidityRules,
        [EnumMember(Value = "cancellation_conditions")]
        CancellationConditionsResponse,
        [EnumMember(Value = "cancellations")]
        CancellationCreateResponse
    }
}