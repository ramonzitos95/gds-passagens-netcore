using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.repository.Contracts;

public interface IPluginConfigurationRepository : IRepositoryBase<PluginConfiguration>
{
    public Task<List<PluginConfiguration>> GetAll();
    public Task<PluginConfiguration> GetById(Guid id);
    public Task<List<PluginConfiguration>> GetAllByUserId(int userId);
    public Task<List<PluginConfiguration>> GetAllByExcludeUserId(int userId);
}
