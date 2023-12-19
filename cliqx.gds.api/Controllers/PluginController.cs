using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class PluginController : ControllerBase
{

    public readonly IPluginService _pluginService;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;

    public PluginController(
        IPluginService pluginService
        , IHttpContextAccessor acessor
        , IMapper mapper)
    {
        _pluginService = pluginService;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Plugin>> CreateAsync(CreatePlugin plugin)
    {
        var result = await _pluginService.CreateAsync(plugin, userId);

        if (result.Id != Guid.Empty)
            return Ok(result);
        else
            return BadRequest();
    }


    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<Plugin>> AllPlugins()
    {
        try
        {
            return Ok(await _pluginService.GetAll());
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult<Plugin>> UpdatePlugin(Plugin plugin)
    {
        try
        {
            var result = await _pluginService.UpdatePlugin(plugin, userId);

            if (result.Id != Guid.Empty) return Ok(result);
            else
                return BadRequest();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Plugin>> DeletePluginById(Guid id)
    {
        try
        {
            var result = await _pluginService.DeletePluginById(id);

            if (result.Id != Guid.Empty) return Ok(result);
            else
                return BadRequest();
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


}
