using System.ComponentModel.DataAnnotations;
using AutoMapper;
using cliqx.gds.api.Components;
using cliqx.gds.api.Filters;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Util;
using cliqx.gds.plugins;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using cliqx.gds.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Client = cliqx.gds.contract.Models.Client;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize(Roles = "admin,external_client")]
[Route("api/v1/snog/[action]")]
public partial class SnogController : MyBaseController
{
    private readonly ILogger<SnogController> _logger;
    private readonly GdsHubHandle _handle;
    private readonly IMapper _mapper;

    public SnogController(
        ILogger<SnogController> logger,
        GdsHubSelectorService hubSelector,
        IConfiguration configuration,
        GdsHubHandle handle
        , IMapper mapper) : base(configuration, hubSelector)
    {
        _logger = logger;
        _handle = handle;
        _mapper = mapper;
    }


    /// <summary>
    /// Busca um cliente por uma das propriedades
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<Client>> GetClientByProperty(
        [Base64EncodedJsonDictionary]
        [Required]
        [FromQuery] string query)
    {
        try
        {
            var ret = await _handle.GetClientByProperty(DecodeJsonDictionary(query));
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error GetClientByProperty: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Cria um cliente novo cliente
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Client>> CreateClient(Client client)
    {
        try
        {
            return Ok(await _handle.CreateClient(client));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error CreateClient: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Atualiza um cliente novo cliente
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<Client>> UpdateClient(Client client)
    {
        try
        {
            return Ok(await _handle.UpdateClient(client));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error UpdateClient: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }



    /// <summary>
    /// Busca uma origem pelo nome da cidade
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<CustomCity>>> GetOriginsByCityName(
        [FromQuery] string cityName
        , [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage
        ,[FromQuery] string? letterCode = ""
        )

    {

        try
        {
            var ret = await _handle.GetOriginsByCityName(cityName, page, itemsPerPage, letterCode);
            if (ret is null) return BadRequest();
            if (ret.Elements.Count() == 0) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetOriginsByCityName: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca todas as origens por uma expressão customizada
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ListCity>> GetOriginsByCustomExpression(
        [Base64EncodedJsonDictionary][Required] string expression
        , [Range(0, int.MaxValue)] int page = 1
        , [Range(1, Constants.MaxItemsPerPage)] int itemsPerPage = 15)
    {
        try
        {
            var ret = await _handle.GetOriginsByCustomExpression(DecodeJsonDictionary(expression), page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetOriginsByCustomExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca um destino pelo nome da cidade
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ListCity>> GetDestinationsByCityName(
        [Required] string cityName
        , [Range(0, int.MaxValue)] int page = 1
        , [Range(1, Constants.MaxItemsPerPage)] int itemsPerPage = 15
        ,[FromQuery] string? letterCode = ""
        )
        
    {
        try
        {
            var ret = await _handle.GetDestinationsByCityName(cityName, page, itemsPerPage, letterCode);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetDestinationsByCityName: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }


    /// <summary>
    /// Busca todos as destinos por uma expressão customizada
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ListCity>> GetDestinationsByCustomExpression(
        [Base64EncodedJsonDictionary][Required] string expression
        , [Range(0, int.MaxValue)] int page = 1
        , [Range(1, Constants.MaxItemsPerPage)] int itemsPerPage = 15)
    {

        try
        {
            var ret = await _handle.GetDestinationsByCustomExpression(DecodeJsonDictionary(expression), page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetDestinationsByCustomExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }


    /// <summary>
    /// Busca uma Corrida pelo Id
    /// </summary>
    /// <remarks>
    /// <b>** Preencher a Origem e Destino com os IDs que foram retornados na busca de origens e destinos.</b>
    /// <br/>
    /// <para> Deverá buscar a Id dentro do objeto AllCities pelo PluginId da Corrida escolhida pelo cliente</para>
    /// <br/>
    /// <para> Isso irá garantir que a corrida será da empresa escolhida pelo cliente</para>
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<Trip>> GetTripById([FromBody] TripQuery trip)
    {
        try
        {
            var ret = await _handle.GetTripById(trip);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetTripById: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca corridas por Origem e destino
    /// </summary>
    /// <remarks>
    /// <b>** Preencher a Origem e Destino com o extra data completo que foi enviado pelo buscar origem e destino</b>
    /// <br/>

    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<PagedList<Trip>>> GetTripsByOriginAndDestination(
        [FromBody][Required] TripQuery query
        , [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage)
    {
        try
        {
            var ret = await _handle.GetTripsByOriginAndDestination(query, page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetTripsByOriginAndDestination: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca corridas por uma expressão customizada.
    /// </summary>
    /// <remarks>
    /// <b>** Sempre deve ser informado o pluginId dentro da expressão com a variável: pluginId.</b>
    /// <br/>
    /// <para>Formato para informar variáveis em expressões: { "pluginId" : "1" }</para>
    /// <br/>
    /// <para>A expressão com todas as variáveis deve ser convertida para base64 encoded</para>
    /// </remarks>

    [HttpGet]
    public async Task<ActionResult<PagedList<Trip>>> GetTripsByCustomExpression(
        [Base64EncodedJsonDictionary][Required] string expression
        , [Range(0, int.MaxValue)] int page = 1
        , [Range(1, Constants.MaxItemsPerPage)] int itemsPerPage = 15)
    {
        try
        {
            var ret = await _handle.GetTripsByCustomExpression(DecodeJsonDictionary(expression), page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetTripsByCustomExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca poltronas pelo Id da corrida
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PagedList<Seat>>> GetSeatsByTripId([FromBody] Trip trip)
    {
        try
        {
            var ret = await _handle.GetSeatsByTripId(trip);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501, ResultOperationHttp.CriarResult(e.Message, ErrorIntegrationGDS.METHOD_NOT_IMPLEMENTED));

            if (e is SnogException)
            {
                var snogException = (SnogException)e;
                return StatusCode(500, ResultOperationHttp.CriarResult(e.Message, snogException.Code));
            }
           
            var err = $"Error GetSeatsByTripId: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca poltronas por uma expressão customizada
    /// </summary>
    /// <remarks>
    /// Sempre deve ser informado o pluginId dentro da expressão com a variável: pluginId.
    /// <para>A expressão com todas as variáveis deve ser convertida para base64 e enviada</para>
    /// <br/>
    /// <para><b>Parametros: </b></para>
    /// <br/>
    /// <para>  originId (string)</para>
    /// <para>  destinationId (string)</para>
    /// <para>  travelDate (string)</para>
    /// <para>  tripId (string)</para>
    /// <br/>
    /// <para>Formato para informar variáveis em expressões:  </para>
    /// <para>{</para>
    /// <para>  "pluginId" : "1",</para>
    /// <para> "originId" : "1",</para>
    /// <para>  "destinationId" : "1",</para>
    /// <para>  "travelDate" : "2024-12-23",</para>
    /// <para>  "tripId" : "1234"</para>
    /// <para>}</para>
    /// <br/>
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<PagedList<Seat>>> GetSeatsByCustomExpression(
        [Base64EncodedJsonDictionary][Required][FromQuery] string expression
        )
    {
        try
        {
            var ret = await _handle.GetSeatsByCustomExpression(DecodeJsonDictionary(expression));
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetSeatsByCustomExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca carrinho por Id do carrinho
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PreOrder>> GetPreOrderById([FromQuery] string id)
    {
        try
        {
            var ret = await _handle.GetPreOrderById(id);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetPreOrderId: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Cria carrinho. Ao criar um carrinho, as poltronas selecionadas serão bloqueadas temporariamente.
    /// A data da expiração fica no atributo ExpirationDate
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PreOrder>> CreatePreOrder(CreatePreOrderDto preOrder)
    {   
        try
        {
            var preOrderMapped = _mapper.Map<PreOrder>(preOrder);
            return Ok(await _handle.CreatePreOrder(preOrderMapped));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error CreatePreOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Atualiza forma de pagamento, adiciona ou altera o cliente
    /// A data da expiração fica no atributo ExpirationDate
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PreOrder>> UpdatePreOrder(PreOrder preOrder)
    {
        try
        {
            return Ok(await _handle.UpdatePreOrder(preOrder));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error UpdatePreOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Adiciona poltronas ao carrinho existente
    /// </summary>
    /// <remarks>
    /// <b>** Informar Id = 0 para adicionar items</b>
    /// <br/>
    /// <para>Este método cria reserva da poltrona</para>
    /// </remarks>
    [HttpPut]
    public async Task<ActionResult<PreOrder>> AddPreOrderItems(
         [FromBody] AddPreOrderItemsDto preOrderItems
        )
    {
        try
        {
            return Ok(await _handle.AddPreOrderItems(preOrderItems));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error AddPreOrderItems: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Remove itens ao carrinho existente
    /// </summary>
    /// <summary>
    /// Adiciona itens ao carrinho existente
    /// </summary>
    /// <remarks>
    /// <b>** Informar Id do item que deve ser removido</b>
    /// <br/>
    /// <para>Sempre passar o preOrderId via query</para>
    /// </remarks>
    [HttpPut]
    public async Task<ActionResult<Order>> RemovePreOrderItems(
         [FromBody] PreOrder preOrder
        )
    {
        try
        {
            return Ok(await _handle.RemovePreOrderItems(preOrder));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error RemovePreOrderItems: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Deleta o carrinho existente
    /// </summary>
    /// <remarks>
    /// <b>** Também cancela a reserva das poltronas</b>
    /// <br/>
    /// </remarks>
    [HttpDelete]
    public async Task<ActionResult<PreOrder>> DeletePreOrder(
        [FromBody] PreOrder preOrder
        )
    {
        try
        {
            var ret = await _handle.DeletePreOrder(preOrder);
            if (ret is null)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error DeletePreOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }


    /// <summary>
    /// Busca uma venda pelo Id
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<Order>> GetOrderById([FromQuery] string id)
    {
        try
        { 
            var ret = await _handle.GetOrderById(id);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetOrderId: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca um pedido e verifica se ele existe pelo cpf
    /// </summary>
    [HttpGet("{orderId}/{cpf}")]
    public async Task<ActionResult<ResultOperation>> GetOrderByIdByCpfClient(string orderId, string cpf)
    {
        try
        {
            var ret = await _handle.GetOrderByIdByCpfClient(orderId, cpf);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetOrderId: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Cria uma venda através de um carrinho de compras
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrderByPreOrder(PreOrder preOrder)
    {
        try
        {
            return Ok(await _handle.CreateOrderByPreOrder(preOrder));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error CreateOrderByPreOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// <br> Atualiza o tipo de pagamento do pedido </br>
    /// <br> Para atualizar o tipo de pagamento do pedido deve ser enviado a propriedade order.Payment.IdPayment </br>
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder([FromBody] Order order)
    {
        try
        {
            return Ok(await _handle.UpdateOrder(order));
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error UpdateOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }


    /// <summary>
    /// Deleta uma venda existente
    /// </summary>
    /// <remarks>
    /// <b>** Também cancela a reserva das poltronas</b>
    /// <b>** Este método é responsável por cancelar a reserva das poltronas</b>
    /// <b>** Ao mesmo tempo vai gerar o cancelamento do pedido na Snog</b>
    /// <br/>
    /// </remarks>
    [HttpDelete]
    public async Task<ActionResult<Order>> CancelOrder(
        [FromQuery] string orderId
        ,[FromQuery] Guid pluginId
        )
    {
        try
        {
            var ret = await _handle.DeleteOrder(orderId, pluginId);
            if (ret is null)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException) return StatusCode(501);
            var err = $"Error CancelOrder: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca todos os métodos de pagamentos disponíveis
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<PaymentMethod>>> GetPaymentsMethods(
        [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage)
    {
        try
        {
            var ret = await _handle.GetPaymentsMethods(page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetPaymentsMethods: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca todas as empresas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<Company>>> GetCompanies(
        [FromQuery] int page
        , [FromQuery][Range(1, Constants.MaxItemsPerPage)] int itemsPerPage)
    {
        try
        {
            var ret = await _handle.GetCompanies(page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetCompanies: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Busca todas as empresas por uma expressão customizada
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<Company>>> GetCompaniesByExpression(
        [Base64EncodedJsonDictionary][Required] string expression
        , [Range(0, int.MaxValue)] int page = 1
        , [Range(1, Constants.MaxItemsPerPage)] int itemsPerPage = 15)
    {
        try
        {
            var ret = await _handle.GetCompaniesByExpression(DecodeJsonDictionary(expression), page, itemsPerPage);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetCompaniesByExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Valida dados suspeito por cpf
    /// </summary>
    [HttpGet("{cpf}")]
    public async Task<ActionResult<ResultOperation>> ValidaDadosSuspeitoPorCpf(string cpf)
    {
        try
        {
            var ret = await _handle.ValidaDadosSuspeitoPorCpf(cpf);
            if (ret is null) return NoContent();
            return Ok(ret);
        }
        catch (Exception e)
        {
            if (e is NotImplementedException)
                return StatusCode(501);
            var err = $"Error GetCompaniesByExpression: {e.Message}";
            _logger.LogError(e, err);
            return BadRequest(err);
        }
    }

    /// <summary>
    /// Teste
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ResultOperation>> Teste()
    {
        try
        {
            throw new NullReferenceException("Erro de referência nula de objeto");
        }
        catch (Exception e)
        {
            return TratadorExcessao.TrataExcessao(e);
        }
    }


}
