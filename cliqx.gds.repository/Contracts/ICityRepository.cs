using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models;

namespace cliqx.gds.repository.Contracts
{
    public interface ICityRepository: IRepositoryBase<City>
    {
        public Task<City> GetById(int id);
        public Task<PagedList<City>> GetAll(int page = 0, int itemsPerPage = 15);
        public Task<PagedList<City>> GetAllByName(string name,int page = 0, int itemsPerPage = 15);
    }
}