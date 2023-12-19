using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;

namespace cliqx.gds.contract.GdsModels;

public class BaseObjectWithOutPlugin : Packer
{
    public string? Id { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    public string? ExtraData { get; set; }
    public string? Description { get; set; }
    public string? Key { get; set; }

    /// <summary>
    /// Este campo será utilizado para configurar a forma como o robô imprime as informações de acordo
    /// com cada cliente, sua configuração deverá vir do PluginConfiguration.
    /// </summary>
    public string? Display { get; set; }

}
