using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.repository
{
    public class UserPluginRepository : RepositoryBase<PaymentService>, IUserPluginRepository
    {
        public UserPluginRepository(DefaultContext Context) : base(Context)
        {
        }

        public async Task<List<UserPlugin>> GetAllByIdPluginId(Guid pluginId)
        {
            return await _context.UsersPlugins
                .Include(x => x.User)
                .Include(x => x.PluginConfigurations)
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.PluginConfigurationsId == pluginId)
                .ToListAsync();
        }

        public async Task<List<UserPlugin>> GetAllByIdUserId(int userId)
        {
            return await _context.UsersPlugins
                .Include(x => x.User)
                .Include(x => x.PluginConfigurations)
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<PagedList<User>> GetAllUser(int page, int itemsPerPage, bool includePlugins = true, bool includeRoles = false)
        {

            var query = _context.Users
                .AsQueryable()
                .AsNoTracking();

            if(includePlugins)
            {
                query = query
                .Include(x => x.UsersPlugins)
                    .ThenInclude(x => x.PluginConfigurations)
                        .ThenInclude(x => x.Plugin);
            }

            if(includeRoles)
            {
                query = query
                    .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role);
            }

            var count = query.Count();
            var totalPages = (int)Math.Ceiling((double)count / itemsPerPage);
            var ret = query.Skip(page * itemsPerPage).Take(itemsPerPage);

            return ret.ToPagedList(page < totalPages - 1);
        }
        
        public async Task<PagedList<User>> GetAllUserByUserName(string userName, int page, int itemsPerPage, bool includePlugins = true, bool includeRoles = false)
        {

            var query = _context.Users
                .AsQueryable();

            if(includePlugins)
            {
                query = query
                .Include(x => x.UsersPlugins)
                    .ThenInclude(x => x.PluginConfigurations)
                        .ThenInclude(x => x.Plugin);
            }

            if(includeRoles)
            {
                query = query
                    .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role);
            }

             query = query
                .Where(x => x.FullName.ToLower().Contains(userName.ToLower()))
                .AsNoTracking();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)count / itemsPerPage);
            var ret = query.Skip(page * itemsPerPage).Take(itemsPerPage);

            return ret.ToPagedList(page < totalPages - 1);
        }


    }
}