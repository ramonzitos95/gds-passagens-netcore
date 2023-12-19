
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.contract.Models;

public class CreateClientDto
{
    public string Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Contact>? Contacts { get; set; }
    public ICollection<Address>? Addresses { get; set; }
    public ICollection<Document>? Documents { get; set; }
    public Plugin Plugin { get; set; }
}