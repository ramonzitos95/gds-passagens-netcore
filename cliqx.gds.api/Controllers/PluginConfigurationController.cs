using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class PluginConfigurationController : ControllerBase
{

    public readonly IPluginService _pluginService;
    private readonly IPluginConfigurationRepository _pluginConfigurationRepo;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;

    public PluginConfigurationController(
        IPluginService pluginService
        , IPluginConfigurationRepository pluginConfigurationRepo
        , IHttpContextAccessor acessor
        , IMapper mapper)
    {
        _pluginService = pluginService;
        _pluginConfigurationRepo = pluginConfigurationRepo;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<PluginConfiguration>> Create([FromBody] PluginConfiguration plugin)
    {
        try
        {
            plugin.UserCreatedId = userId;

            _pluginConfigurationRepo.Add(plugin);

            if (await _pluginConfigurationRepo.SaveChangesAsync())
                return Ok(plugin);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut]
    public async Task<ActionResult<PluginConfiguration>> Update([FromBody] PluginConfiguration plugin)
    {
        try
        {
            var found = await _pluginConfigurationRepo.GetById(plugin.Id);

            if (found is null)
                return BadRequest("Plugin não encontrado pelo ID informado");

            plugin.PaymentService = null;
            plugin.ShoppingCart = null;
            plugin.Plugin = null;

            plugin.UserUpdatedId = userId;

            _pluginConfigurationRepo.Update(plugin);

            if (await _pluginConfigurationRepo.SaveChangesAsync())
                return Ok(plugin);
            else
                return BadRequest();
        }
        catch (System.Exception e)
        {
            Console.WriteLine("ERROR UPDATE: " + e.Message);
            Console.WriteLine("ERROR UPDATE INNER: " + e.InnerException.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }


    [Authorize("Bearer", Roles = "admin,user,external_client")]
    [HttpGet]
    public async Task<ActionResult<List<PluginConfiguration>>> GetAll()
    {
        try
        {
            return Ok(await _pluginConfigurationRepo.GetAll());
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [Authorize("Bearer", Roles = "admin,user,external_client")]
    [HttpGet]
    public async Task<ActionResult<List<PluginConfiguration>>> GetAllByExcludeUserId(int userId)
    {
        try
        {
            return Ok(await _pluginConfigurationRepo.GetAllByExcludeUserId(userId));
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }



    [HttpDelete]
    public async Task<ActionResult<Plugin>> DeleteById(Guid id)
    {
        try
        {
            var found = await _pluginConfigurationRepo.GetById(id);

            if (found is null)
                return BadRequest("Plugin não encontrado pelo ID informado");

            _pluginConfigurationRepo.Delete(found);

            if (await _pluginConfigurationRepo.SaveChangesAsync())
                return NoContent();
            else
                return BadRequest();
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


}
