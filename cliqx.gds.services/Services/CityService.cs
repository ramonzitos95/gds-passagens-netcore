using cliqx.gds.contract.Models;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using System;

namespace cliqx.gds.services.Services;

public class CityService : ICityService
{
    private readonly ICityRepository cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        this.cityRepository = cityRepository;
    }

    public Task<PagedList<City>> GetAll(int page, int itemsPerPage)
    {
        return cityRepository.GetAll(page, itemsPerPage);
    }

    public Task<PagedList<City>> GetAllByName(string name, int page, int itemsPerPage)
    {
        return cityRepository.GetAllByName(name, page, itemsPerPage);
    }

    public Task<City> GetById(int id)
    {
        return GetById(id);
    }
}
