using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class CityController : ControllerBase
{
    public readonly ICityService _cityService;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;
    public CityController(ICityService cityService, IHttpContextAccessor acessor, IMapper mapper)
    {
        _cityService = cityService;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<City>>> GetAllByName([FromQuery] string name, [Range(1, int.MaxValue)] int page = 1, [Range(1, int.MaxValue)] int itemsPerPage = 15)
    {
        try
        {
            var result = await _cityService.GetAllByName(name, page, itemsPerPage);

            return Ok(result);
        }
        catch (System.Exception)
        {
            return BadRequest();
            throw;
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<City>>> GetAll(
        [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage
        )
    {
        try
        {
            var result = await _cityService.GetAll(page, itemsPerPage);

            return Ok(result);
        }
        catch (System.Exception)
        {
            return BadRequest();
            throw;
        }
    }



}
