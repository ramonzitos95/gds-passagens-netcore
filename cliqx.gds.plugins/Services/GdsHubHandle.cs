
using cliqx.gds.contract;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Util;
using cliqx.gds.plugins.Extensions;
using cliqx.gds.plugins.Services;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using cliqx.gds.utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Client = cliqx.gds.contract.Models.Client;

namespace cliqx.gds.plugins;

public partial class GdsHubHandle : PluginCacheService, IContractBase
{
    protected readonly GdsHubSelectorService _hubSelector;
    protected IContractBase Hub { get; private set; }
    private readonly ILogger<IContractBase> _logger;
    public readonly CityService _cityService;
    public readonly IHttpContextAccessor _accessor;

    public GdsHubHandle
    (
        GdsHubSelectorService hubSelector
        , IPluginRepository repository
        , IMemoryCache cache
        , ILogger<IContractBase> logger
        , CityService cityService
        , IHttpContextAccessor accessor) : base(repository, cache, accessor)
    {
        _hubSelector = hubSelector;
        _logger = logger;
        _cityService = cityService;
        _accessor = accessor;
    }

    public async Task<Client> GetClientByProperty(Dictionary<string, string> property)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins.FirstOrDefault().Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            var ret = await Hub.GetClientByProperty(property);

            return ret;
        }
        catch (System.Exception e)
        {
            var err = $"Error GetClientByProperty: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<contract.Models.Client> CreateClient(contract.Models.Client client)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins.FirstOrDefault().Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.CreateClient(client);
        }
        catch (System.Exception e)
        {
            var err = $"Error CreateClient: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<contract.Models.Client> UpdateClient(contract.Models.Client client)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins.FirstOrDefault().Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.UpdateClient(client);
        }
        catch (System.Exception e)
        {
            var err = $"Error UpdateClient: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }


    public async Task<Trip> GetTripById(TripQuery trip)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(trip.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.GetTripById(trip);
        }
        catch (System.Exception e)
        {
            var err = $"Error GetTripById: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PagedList<Trip>> GetTripsByOriginAndDestination(TripQuery query, int page = 1, int itemsPerPage = 15)
    {
        var list = new List<Trip>();
        var tasks = new List<(Task<PagedList<Trip>>, Guid)>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                var componentsOrigin = ModelUtility<CustomCity.Component>.Unpack(query.Origin.ExtraData);
                var componentsDestination = ModelUtility<CustomCity.Component>.Unpack(query.Destination.ExtraData);
                var cityOrigin = componentsOrigin.FirstOrDefault(x => x.PluginId == item.Id && x.NormalizedName == query.Origin.NormalizedName);
                var cityDestination = componentsDestination.FirstOrDefault(x => x.PluginId == item.Id && x.NormalizedName == query.Destination.NormalizedName);

                if (cityDestination != null && cityOrigin != null)
                {
                    query.Origin.Id = cityOrigin.CityId;
                    query.Destination.Id = cityDestination.CityId;

                    Hub = await _hubSelector.GetHubPlugin(item.Id);

                    if (Hub != null)
                    {
                        var task = (Hub.GetTripsByOriginAndDestination(query), item.Id);
                        tasks.Add(task);
                    }
                }


            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetTripsByOriginAndDestination não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetTripsByOriginAndDestination: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1).ToArray());

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;

                if (retorno is null) continue;

                foreach (Trip item in retorno.Elements)
                {

                    item.PluginId = pluginId;
                    if (!list.Any(a => a.Key == item.Key))
                    {
                        list.Add(item);
                    }
                }
            }
        }

        var totalPages = (int)Math.Ceiling((double)list.Count / itemsPerPage);

        return list.Skip(page * itemsPerPage).Take(itemsPerPage).ToPagedList(page < totalPages - 1);
    }

    public async Task<PagedList<Trip>> GetTripsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
    {
        var list = new List<Trip>();
        var tasks = new List<(Task<PagedList<Trip>>, Guid)>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                Hub = await _hubSelector.GetHubPlugin(item.Id);
                if (Hub != null)
                {
                    var task = (Hub.GetTripsByCustomExpression(expression), item.Id);
                    tasks.Add(task);
                }
            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetTripsByCustomExpression não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetTripsByCustomExpression: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1));

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;
                foreach (var item in retorno.Elements)
                {

                    item.PluginId = pluginId;
                    if (!list.Any(a => a.Key == item.Key))
                        list.Add(item);
                }
            }
        }

        return list.Skip(page).Take(itemsPerPage).ToPagedList(true);
    }

    public async Task<PagedList<Seat>> GetSeatsByTripId(Trip trip)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(trip.PluginId);

            if (Hub == null)
                throw new SnogException("Erro ao buscar plugin", ErrorIntegrationGDS.PLUGIN_NOT_FOUND);
            var retorno = await Hub.GetSeatsByTripId(trip);

            if (retorno is null)
                throw new SnogException($"Mapa de poltronas indisponível no momento!", ErrorIntegrationGDS.SEAT_UNAVAILABLE);

            retorno.Elements.ToList().ForEach(x => x.PluginId = trip.PluginId);

            return retorno;
        }
        catch (System.Exception e)
        {
            var err = $"Error GetSeatsByTripId: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PagedList<Seat>> GetSeatsByCustomExpression(Dictionary<string, string> expression)
    {
        try
        {
            var pluginId = "";
            if (!expression.ContainsKey("pluginId"))
                throw new Exception("Plugin não informado");

            pluginId = expression["pluginId"];

            Hub = await _hubSelector.GetHubPlugin(Guid.Parse(pluginId));

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.GetSeatsByCustomExpression(expression);
        }
        catch (System.Exception e)
        {
            var err = $"Error GetSeatsByCustomExpression: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(preOrder.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            // foreach (var item in preOrder.Items)
            // {
            //     if (!item.Trip.Seats.Any() && item.Trip.TotalConnections > 0)
            //     {
            //         var preOrderItem = new PreOrderItem();
            //         preOrderItem = item.Trip.Connections.AsPreOrderItems();
            //     }
            // }

            preOrder.Items.ForEach(
                x => x.Trip.Seats.ForEach(s => {
                    var number = s.Number.Length < 2 
                    ? "0" + s.Number 
                    : s.Number;

                    s.Number = number;
                })
            );


            return await Hub.CreatePreOrder(preOrder);
        }
        catch (System.Exception e)
        {
            var err = $"Error CreatePreOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PreOrder> UpdatePreOrder(PreOrder preOrder)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(preOrder.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            // if (!preOrder.Seats.Any() && preOrder.Trip.TotalConnections > 0)
            // {
            //     preOrder.Seats = preOrder.Trip.Connections.AsPreOrderItems();
            // }

            return await Hub.UpdatePreOrder(preOrder);
        }
        catch (System.Exception e)
        {
            var err = $"Error UpdatePreOrder Handle: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto AddPreOrderItems)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(AddPreOrderItems.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.AddPreOrderItems(AddPreOrderItems);
        }
        catch (System.Exception e)
        {
            var err = $"Error AddPreOrderItems: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PreOrder> RemovePreOrderItems(PreOrder preOrder)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(preOrder.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.RemovePreOrderItems(preOrder);
        }
        catch (System.Exception e)
        {
            var err = $"Error RemovePreOrderItems: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<PreOrder> DeletePreOrder(PreOrder preOrder)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(preOrder.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.DeletePreOrder(preOrder);
        }
        catch (System.Exception e)
        {
            var err = $"Error DeletePreOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<Order> GetOrderById(string orderId)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins[0].Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.GetOrderById(orderId);
        }
        catch (System.Exception e)
        {
            var err = $"Error GetOrderByOrderId: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(preOrder.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.CreateOrderByPreOrder(preOrder);
        }
        catch (System.Exception e)
        {
            var err = $"Error CreateOrderByPreOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<Order> UpdateOrder(Order Order)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(Order.PluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.UpdateOrder(Order);
        }
        catch (System.Exception e)
        {
            var err = $"Error UpdateOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<Order> DeleteOrder(string orderId, Guid pluginId)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(pluginId);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.DeleteOrder(orderId, pluginId);
        }
        catch (System.Exception e)
        {
            var err = $"Error DeleteOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

   

    public Task<PagedList<PaymentMethod>> GetPaymentsMethods(int page = 1, int itemsPerPage = 15)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<Company>> GetCompanies(int page = 1, int itemsPerPage = 15)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<Company>> GetCompaniesByExpression(Dictionary<string, string> query, int page = 1, int itemsPerPage = 15)
    {
        throw new NotImplementedException();
    }

    


    public async Task<PagedList<CustomCity>> GetOriginsByCityName(string cityName, int page = 1, int itemsPerPage = 15, string letterCode = "")
    {
        var tasks = new List<(Task<PagedList<CustomCity>>, Guid)>();
        var groupCities = new List<CustomCity>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                Hub = await _hubSelector.GetHubPlugin(item.Id);
                if (Hub != null)
                {
                    var task = (Hub.GetOriginsByCityName(cityName, page, itemsPerPage, letterCode), item.Id);
                    tasks.Add(task);
                }
            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetOriginsByCityName não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetOriginsByCityName: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1));

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;
                if (retorno == null)
                    continue;
                foreach (var item in retorno.Elements)
                {
                    item.PluginId = pluginId;

                    if (groupCities.Any(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        var index = groupCities.FindIndex(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                        groupCities[index].Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                    }
                    else
                    {
                        var itm = item;
                        if (itm.Components == null) itm.Components = new List<CustomCity.Component>();
                        itm.Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                        groupCities.Add(itm);
                    }
                }
            }
        }

        foreach (var city in groupCities)
        {
            city.ExtraData = ModelUtility<CustomCity.Component>.Pack(city.Components);
        }

        var ret = groupCities;

        if (!String.IsNullOrEmpty(letterCode))
            ret = ret.Where(x => x.LetterCode.ToUpper() == letterCode.ToUpper()).ToList();

        var totalPages = (int)Math.Ceiling((double)ret.Count / itemsPerPage);

        return ret.Skip(page * itemsPerPage).Take(itemsPerPage).ToPagedList(page < totalPages - 1);
    }

    public async Task<PagedList<CustomCity>> GetOriginsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
    {
        var tasks = new List<(Task<PagedList<CustomCity>>, Guid)>();
        var groupCities = new List<CustomCity>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                Hub = await _hubSelector.GetHubPlugin(item.Id);
                if (Hub != null)
                {
                    var task = (Hub.GetOriginsByCustomExpression(expression, page, itemsPerPage), item.Id);
                    tasks.Add(task);
                }
            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetOriginsByCustomExpression não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetOriginsByCustomExpression: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1));

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;

                if (retorno == null)
                    continue;

                foreach (var item in retorno.Elements)
                {
                    item.PluginId = pluginId;

                    if (groupCities.Any(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        var index = groupCities.FindIndex(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                        groupCities[index].Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                    }
                    else
                    {
                        var itm = item;
                        if (itm.Components == null) itm.Components = new List<CustomCity.Component>();
                        itm.Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                        groupCities.Add(itm);
                    }
                }
            }
        }

        foreach (var city in groupCities)
        {
            city.ExtraData = ModelUtility<CustomCity.Component>.Pack(city.Components);
        }

        return groupCities.ToPagedList();
    }

    public async Task<PagedList<CustomCity>> GetDestinationsByCityName(string cityName, int page = 0, int itemsPerPage = 15, string letterCode = "")
    {
        var tasks = new List<(Task<PagedList<CustomCity>>, Guid)>();
        var groupCities = new List<CustomCity>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                Hub = await _hubSelector.GetHubPlugin(item.Id);
                if (Hub != null)
                {
                    var task = (Hub.GetDestinationsByCityName(cityName, page, itemsPerPage, letterCode), item.Id);
                    tasks.Add(task);
                }
            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetDestinationsByCityName não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetDestinationsByCityName: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1));

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;

                if (retorno == null)
                    continue;

                foreach (var item in retorno.Elements)
                {
                    item.PluginId = pluginId;

                    if (groupCities.Any(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        var index = groupCities.FindIndex(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                        groupCities[index].Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                    }
                    else
                    {
                        var itm = item;
                        if (itm.Components == null) itm.Components = new List<CustomCity.Component>();
                        itm.Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                        groupCities.Add(itm);
                    }
                }
            }
        }

        foreach (var city in groupCities)
        {
            city.ExtraData = ModelUtility<CustomCity.Component>.Pack(city.Components);
        }

        var ret = groupCities;

        if (!String.IsNullOrEmpty(letterCode))
            ret = ret.Where(x => x.LetterCode.ToUpper() == letterCode.ToUpper()).ToList();

        var totalPages = (int)Math.Ceiling((double)ret.Count / itemsPerPage);

        return ret.Skip(page * itemsPerPage).Take(itemsPerPage).ToPagedList(page < totalPages - 1);
    }

    public async Task<PagedList<CustomCity>> GetDestinationsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
    {
        var tasks = new List<(Task<PagedList<CustomCity>>, Guid)>();
        var groupCities = new List<CustomCity>();

        foreach (var item in _listaPlugins)
        {
            try
            {
                Hub = await _hubSelector.GetHubPlugin(item.Id);
                if (Hub != null)
                {
                    var task = (Hub.GetDestinationsByCustomExpression(expression, page, itemsPerPage), item.Id);
                    tasks.Add(task);
                }
            }
            catch (NotImplementedException ex)
            {
                _logger.LogWarning($"Método GetDestinationsByCustomExpression não implementado no plugin {item.Plugin.Name}: {ex.Message}");
            }
            catch (Exception e)
            {
                var err = $"impossivel obter as GetDestinationsByCustomExpression: {e.Message}";
                _logger.LogError(e, err);
            }
        }

        await Task.WhenAll(tasks.Select(t => t.Item1));

        foreach (var (task, pluginId) in tasks)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var retorno = task.Result;

                if (retorno == null)
                    continue;

                foreach (var item in retorno.Elements)
                {
                    item.PluginId = pluginId;

                    if (groupCities.Any(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        var index = groupCities.FindIndex(x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                        groupCities[index].Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                    }
                    else
                    {
                        var itm = item;
                        if (itm.Components == null) itm.Components = new List<CustomCity.Component>();
                        itm.Components.Add(new CustomCity.Component() { CityId = item.Id, PluginId = pluginId, NormalizedName = item.NormalizedName });
                        groupCities.Add(itm);
                    }
                }
            }
        }

        foreach (var city in groupCities)
        {
            city.ExtraData = ModelUtility<CustomCity.Component>.Pack(city.Components);
        }

        return groupCities.ToPagedList();
    }

    public async Task<PreOrder> GetPreOrderById(string id)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins[0].Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.GetPreOrderById(id);
        }
        catch (System.Exception e)
        {
            var err = $"Error GetPreOrderById: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins[0].Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.GetOrderByIdByCpfClient(orderId, cpf);
        }
        catch (System.Exception e)
        {
            var err = $"Error GetOrderByOrderId: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

    public async Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf)
    {
        try
        {
            Hub = await _hubSelector.GetHubPlugin(_listaPlugins[0].Id);

            if (Hub == null)
                throw new Exception("Erro ao buscar plugin");

            return await Hub.ValidaDadosSuspeitoPorCpf(cpf);
        }
        catch (System.Exception e)
        {
            var err = $"Error DeleteOrder: {e.Message}";
            _logger.LogError(e, err);
            throw;
        }
    }

}
