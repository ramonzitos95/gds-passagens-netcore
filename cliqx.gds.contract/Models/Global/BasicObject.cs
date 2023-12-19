using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.contract.Models.Global;

public abstract class BasicObject
{
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    [NotMapped]
    public string Key { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public int? UserCreatedId { get; set; }
    [JsonIgnore]
    public int? UserUpdatedId { get; set; }
    [JsonIgnore]
    public User? UserCreated { get; set; }
    [JsonIgnore]
    public User? UserUpdated { get; set; }

    [JsonIgnore]
    public bool IsActive { get; set; } = true;
    [JsonIgnore]
    public bool InternalProperty { get; set; } = false;
}

public abstract class BasicObjectWithPlugin : BasicObjectInt
{
    [NotMapped]
    public PluginConfiguration? PluginConfiguration { get; set; }
}

public abstract class BasicObjectInt : BasicObject
{
    public int Id { get; set; }
}

public abstract class BasicObjectGuid : BasicObject
{
    [Column(TypeName ="varchar(36) COLLATE ascii_general_ci")]
    public Guid Id { get; set; }
}
