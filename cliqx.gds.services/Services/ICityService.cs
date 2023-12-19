using cliqx.gds.contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services;

public interface ICityService
{
    public Task<City> GetById(int id);
    public Task<PagedList<City>> GetAll(int page, int itemsPerPage);
    public Task<PagedList<City>> GetAllByName(string name, int page, int itemsPerPage);
}

