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
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class PaymentServiceController : ControllerBase
{
    private readonly DefaultContext context;
    private readonly IPaymentServiceRepository _repo;
    public readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;
    private int userId = 0;

    public PaymentServiceController(
        DefaultContext context
        , IHttpContextAccessor acessor
        , IMapper mapper
        ,IPaymentServiceRepository repo)
    {
        this.context = context;
        _accessor = acessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        _mapper = mapper;
        _repo = repo;
    }

    [HttpPost]
    public async Task<ActionResult<PaymentService>> Create(PaymentService payment)
    {
        payment.UserCreatedId = userId;
        var result = await this.context.PaymentServices.AddAsync(payment);

        if (await this.context.SaveChangesAsync() > 0)
            return Ok(payment);
        else
            return BadRequest();
    }


    [Authorize("Bearer", Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<List<PaymentService>>> GetAll()
    {
        try
        {
            var result = await this.context.PaymentServices
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

    [HttpPut]
    public async Task<ActionResult<PaymentService>> Update([FromBody] PaymentService plugin)
    {
        try
        {
            var found = await _repo.GetById(plugin.Id);

            if (found is null)
                return BadRequest("Plugin não encontrado pelo ID informado");


            found.UserUpdatedId = userId;
            found.Name = plugin.Name;
            found.CredentialsJsonObject = plugin.CredentialsJsonObject;
            found.ApiBaseUrl = plugin.ApiBaseUrl;

            _repo.Update(found);

            if (await _repo.SaveChangesAsync())
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

    [HttpDelete]
    public async Task<ActionResult<Plugin>> DeleteById(int id)
    {
        try
        {
            var found = await _repo.GetById(id);

            if (found is null)
                return BadRequest("Plugin não encontrado pelo ID informado");

            _repo.Delete(found);

            if (await _repo.SaveChangesAsync())
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
