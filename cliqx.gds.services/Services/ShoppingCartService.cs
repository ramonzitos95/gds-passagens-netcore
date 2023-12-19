
using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.PaymentServices.Services.DefaultPay;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus;
using Microsoft.Extensions.Configuration;

namespace cliqx.gds.services.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartService _service;
    private readonly IPaymentService _paymentService;
    private readonly IConfiguration _configuration;
    private readonly PluginConfiguration pluginConfiguration;

    public ShoppingCartService(
        IConfiguration configuration
        ,PluginConfiguration pluginConfiguration
        )
    {
        _configuration = configuration;
        this.pluginConfiguration = pluginConfiguration;

        _paymentService = pluginConfiguration.PaymentServiceId switch
        {
            1 => new DefaultPayService(pluginConfiguration.PaymentService),
            2 => new FacilitaPayService(pluginConfiguration.PaymentService),
            _ => throw new Exception("Pagamento service não configurado")
        };

        _service = pluginConfiguration.ShoppingCartId switch
        {
            1 => new RecargaPlusService(_configuration, pluginConfiguration, _paymentService),
            _ => throw new Exception("ShoppingCart service não configurado")
        };

    }

    public async Task<Client> CreateClient(Client client)
    {
        var ret = await _service.CreateClient(client);
        ret.PluginId = client.PluginId;
        return ret;
    }

    

    public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
    {
        var ret = await _service.CreateOrderByPreOrder(preOrder);
        
        ret.PluginId = preOrder.PluginId;

        return ret;
    }

    public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
    {
        ValidadorPreOrder.Validar(preOrder);

        return await _service.CreatePreOrder(preOrder);
    }

    public async Task<Client> DeleteClient(Client client)
    {
        return await _service.DeleteClient(client);
    }

    public async Task<Order> DeleteOrder(Order order)
    {
        return await _service.DeleteOrder(order);
    }

    public async Task<PreOrder> DeletePreOrder(PreOrder preOrder)
    {
        return await _service.DeletePreOrder(preOrder);
    }

    public async Task<Client> GetClientById(Client client)
    {
        return await _service.GetClientById(client);
    }

    public async Task<Client> GetClientByLojaAndPhone(string idLoja, string telefone)
    {
        return await _service.GetClientByLojaAndPhone(idLoja, telefone);
    }

    public async Task<Order> GetOrderById(string id)
    {
        var retorno = await _service.GetOrderById(id);
        retorno.PluginId = pluginConfiguration.Id;

        return retorno;
    }

    public async Task<PreOrder> GetPreOrderById(string id)
    {
        var retorno = await _service.GetPreOrderById(id);
        retorno.PluginId = pluginConfiguration.Id;

        return retorno;
    }

    public async Task<PreOrder> RemovePreOrderItems(PreOrder preOrder)
    {
        return await _service.RemovePreOrderItems(preOrder);
    }

    public async Task<Client> UpdateClient(Client client)
    {
        return await _service.UpdateClient(client);
    }

    public async Task<Order> UpdateOrder(Order Order)
    {
        return await _service.UpdateOrder(Order);
    }

    // public async Task<bool> UpdateStatusPedido(int pedidoId, StatusPedido statusPedido)
    // {
    //     return await _service.UpdateStatusPedido(pedidoId, statusPedido);
    // }

    public Dictionary<PaymentMethod, string> GetPaymentsMethods(int page = 1, int itemsPerPage = 15)
    {
        return _service.GetPaymentsMethods(page, itemsPerPage);
    }

    // public Task<bool> AddItemDescription(string itemId, List<services.RecargaPlus.Models.Descricao> listaDescricao)
    // {
    //     return this._service.AddItemDescription(itemId, listaDescricao);
    // }

    public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrder)
    {
        var ret = await _service.AddPreOrderItems(preOrder);
        ret.PluginId = preOrder.PluginId;
        return ret;
    }

    public Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf)
    {
        return _service.GetOrderByIdByCpfClient(orderId, cpf);
    }

    public Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf)
    {
        return _service.ValidaDadosSuspeitoPorCpf(cpf);
    }
}
