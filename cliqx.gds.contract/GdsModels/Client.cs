using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cliqx.gds.contract.Models;

public class Client
{
    public string Id { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? BirthDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Contact>? Contacts { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Address>? Addresses { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Document>? Documents { get; set; }

    [JsonIgnore]
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public Guid PluginId { get; set; }
}
