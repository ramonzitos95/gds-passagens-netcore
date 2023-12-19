
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;

namespace cliqx.gds.contract.GdsModels;

public class Connection : BaseObjectPlugin
{
    public bool IsBpe { get; set; }
    public Company Company { get; set; }
    public TripClass Class { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public int OriginId { get; set; }
    public TripOriginDestination Origin { get; set; }
    public int DestinationId { get; set; }
    public TripOriginDestination Destination { get; set; }
    public decimal ValueAsDecimal { get; }
    public long Value { get; set; }
    public long AvailableSeats { get; set; }
    public List<ConnectionItem> Seats { get; set; }


    public class ConnectionItem
    {
        [Required]
        public TravelDirectionEnum TravelDirectionType { get; set; }
        public Client Client { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Transaction Transaction { get; set; }
        public Seat Seat { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Ticket Ticket { get; set; }
    }
}
