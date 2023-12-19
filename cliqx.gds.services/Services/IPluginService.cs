using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models.PluginConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services;

public interface IPluginService
{
    public Task<List<Plugin>> GetAll();
    public Task<PluginConfiguration> GetById(Guid id);
    Task<Plugin> CreateAsync(CreatePlugin plugin, int userId);
    Task<Plugin> UpdatePlugin(Plugin plugin, int userId);
    Task<PluginConfiguration> DeletePluginById(Guid id);
    Task<PluginConfiguration> GetByStoreId(int storeId);
}

