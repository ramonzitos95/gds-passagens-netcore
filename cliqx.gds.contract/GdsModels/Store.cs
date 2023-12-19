using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.contract.GdsModels;

public class Store : BaseObjectPlugin
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsOpen { get; set; }
}
