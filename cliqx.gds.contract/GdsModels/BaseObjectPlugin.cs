using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;

namespace cliqx.gds.contract.GdsModels;

public class BaseObjectPlugin : Packer
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExtraData { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    [JsonIgnore]
    public string? Key { get; set; }

    /// <summary>
    /// Este campo será utilizado para configurar a forma como o robô imprime as informações de acordo
    /// com cada cliente, sua configuração deverá vir do PluginConfiguration.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Display { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid PluginId { get; set; }
}
