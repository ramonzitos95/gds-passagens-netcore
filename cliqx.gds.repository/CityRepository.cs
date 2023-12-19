using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.repository
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(DefaultContext Context) : base(Context)
        {
        }

        Task<City> ICityRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<PagedList<City>> GetAll(int page, int itemsPerPage)
        {
            var query = _context.Cities.AsQueryable().AsNoTracking();
            var totalRecords = await query.CountAsync();

            var result = query.Skip(page-1).Take(itemsPerPage);

            var list = await result.ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / itemsPerPage);

            return list.ToPagedList(page < totalPages - 1);
        }

        public async Task<PagedList<City>> GetAllByName(string name, int page, int itemsPerPage)
        {
            var query = _context.Cities.AsQueryable().AsNoTracking();
            var totalRecords = await query.CountAsync();

            var result = query.Skip(page-1).Take(itemsPerPage);

            result =result
                .Where(x => x.NormalizedName.Contains(StringUtils.RemoveDiacriticsAndSpecialCharacters(name)));

            var list = await result.ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / itemsPerPage);

            return list.ToPagedList(page < totalPages - 1);

        }


    }
}