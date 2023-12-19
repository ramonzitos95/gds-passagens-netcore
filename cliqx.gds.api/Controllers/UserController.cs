using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;
    public UserManager<User> _userManager { get; }
    public RoleManager<Role> _roleManager { get; }
    public UserController(
        UserManager<User> userManager
        , RoleManager<Role> roleManager
        , IHttpContextAccessor acessor, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<User>>> GetAll(
        [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage
        )
    {
        try
        {
            var count = _userManager.Users
            .AsQueryable()
            .Count();

            var result = _userManager.Users
            .AsQueryable();

            var totalPages = (int)Math.Ceiling((double)count / itemsPerPage);

            var ret = result.Skip(page * itemsPerPage).Take(itemsPerPage);

            return ret.ToPagedList(page < totalPages - 1);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateUser createUser)
    {
        try
        {
            var userMapped = _mapper.Map<User>(createUser);

            userMapped.SecurityStamp = Guid.NewGuid().ToString().Replace("-", "");

            var result = await _userManager.CreateAsync(userMapped, createUser.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join("Error: ", result.Errors.Select(x => x.Description));
                throw new Exception(errorMessages);
            }

            var role = await _roleManager.FindByNameAsync(createUser.Role);

            if (String.IsNullOrEmpty(role.Name))
                throw new Exception("Role não encontrada");

            var addInRole = await _userManager.AddToRoleAsync(userMapped, createUser.Role);


            return Created("GetUser", userMapped);

            throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault().ToString());
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }



}
