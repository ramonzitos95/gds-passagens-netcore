using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contexts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class ShoppingCartController : ControllerBase
{
    private readonly DefaultContext context;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;

    public ShoppingCartController(
        DefaultContext context
        , IHttpContextAccessor acessor
        , IMapper mapper)
    {
        this.context = context;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> CreateAsync(ShoppingCart cart)
    {
        cart.UserCreatedId = userId;
        var result = await this.context.ShoppingCartServices.AddAsync(cart);

        if (await this.context.SaveChangesAsync() > 0)
            return Ok(cart);
        else
            return BadRequest();
    }


    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<List<ShoppingCart>>> GetAll()
    {
        try
        {
            var result = await this.context.ShoppingCartServices
                .AsQueryable()
                .AsNoTracking()
                .ToArrayAsync();

            return Ok(result);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

 


}
