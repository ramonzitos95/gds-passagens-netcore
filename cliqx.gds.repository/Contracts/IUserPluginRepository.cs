using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.repository.Contracts
{
    public interface IUserPluginRepository: IRepositoryBase<UserPlugin>
    {
        public Task<List<UserPlugin>> GetAllByIdUserId(int userId);
        public Task<List<UserPlugin>> GetAllByIdPluginId(Guid pluginId);
        public Task<PagedList<User>> GetAllUser(int page, int itemsPerPage, bool includePlugins = true, bool includeRoles = false);
        public Task<PagedList<User>> GetAllUserByUserName(string userName,int page, int itemsPerPage, bool includePlugins = true, bool includeRoles = false);
    }
}