
using System.ComponentModel.DataAnnotations;
using cliqx.gds.contract.Components;

namespace cliqx.gds.contract.GdsModels.Query;


public class TripQuery : BaseObjectPlugin
{
    [Required]
    public CustomCity Origin { get; set; }

    [Required]
    public CustomCity Destination { get; set; }

    [DateValidation]
    public DateTime TravelDate { get; set; }
    public DateTime? StartTravelDate { get; set; }
    public DateTime? EndTravelDate { get; set; }
    public string? SortBy { get; set; }
    public string? FilterBy { get; set; }

}
