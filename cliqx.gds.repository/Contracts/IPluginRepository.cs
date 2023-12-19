using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.repository.Contracts;

public interface IPluginRepository : IRepositoryBase<Plugin>
{
    public Task<List<Plugin>> GetAll();
    public Task<PluginConfiguration> GetById(Guid id);
    public Task<List<PluginConfiguration>> GetAllByUserId(long userId);
    public Task<PluginConfiguration> GetByStoreId(int storeId);
}
