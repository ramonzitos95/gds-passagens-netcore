using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin,support")]
[Route("api/[controller]/[action]")]
public class UserPluginController : ControllerBase
{
    private readonly IUserPluginRepository _userPluginRepo;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int _userId = 0;

    public UserPluginController(
        IUserPluginRepository userPluginRepo
        , IHttpContextAccessor acessor
        , IMapper mapper)
    {
        _userPluginRepo = userPluginRepo;
        _accessor = acessor;
        _userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<List<UserPlugin>>> Create(List<UserPlugin> usersPlugins)
    {
        try
        {
            var pluginsFound = await _userPluginRepo.GetAllByIdUserId(usersPlugins[0].UserId);

            if(pluginsFound.Count() > 0)
                _userPluginRepo.DeleteRange(pluginsFound);

            _userPluginRepo.AddRange(usersPlugins);

            if (await _userPluginRepo.SaveChangesAsync())
                return Ok(usersPlugins);

            throw new Exception("Erro desconhecido");
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

    }


    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<List<UserPlugin>>> GetAllByIdPluginId(Guid pluginId)
    {
        try
        {
            return Ok(await _userPluginRepo.GetAllByIdPluginId(pluginId));
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<List<UserPlugin>>> GetAllByIdUserId(int userId)
    {
        try
        {
            return Ok(await _userPluginRepo.GetAllByIdUserId(userId));
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<User>>> GetAllUser(int page, int itemsPerPage, bool includePlugins = false, bool includeRoles = false)
    {
        try
        {
            var ret = await _userPluginRepo.GetAllUser(page, itemsPerPage, includePlugins, includeRoles);
            if(ret.Elements.Count() == 0)
                return NoContent();
            return Ok(ret);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<User>>> GetAllUserByUserName(string userName,int page, int itemsPerPage, bool includePlugins = false, bool includeRoles = false)
    {
        try
        {
            var ret = await _userPluginRepo.GetAllUserByUserName(userName,page, itemsPerPage, includePlugins, includeRoles);
            if(ret.Elements.Count() == 0)
                return NoContent();
            return Ok(ret);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }



    [HttpDelete]
    public async Task<ActionResult<UserPlugin>> DeleteAllByUserId(int userId)
    {
        try
        {
            var found = await _userPluginRepo.GetAllByIdUserId(userId);

            if (found is null)
                return BadRequest("Plugin não encontrado pelo ID informado");

            _userPluginRepo.DeleteRange(found);

            if (await _userPluginRepo.SaveChangesAsync())
                return NoContent();
            else
                return BadRequest(_userPluginRepo.SaveChangesAsync().Exception.Data);
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


}
