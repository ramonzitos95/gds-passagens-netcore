using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.contract.Models;

public class ListCity
{
    public IEnumerable<CustomCity> Cities { get; set; }
    public bool StillFetchable { get; set; }

    public ListCity(IEnumerable<CustomCity> collection, bool stillFetchable = false)
    {
        Cities = new List<CustomCity>();
    }

    public ListCity(bool stillFetchable = false)
    {
        StillFetchable = stillFetchable;
        Cities = new List<CustomCity>();
    }
}