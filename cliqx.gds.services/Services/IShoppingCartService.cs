
using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Util;
namespace cliqx.gds.services.Services;

public interface IShoppingCartService
{

    public Task<contract.Models.Client> GetClientByLojaAndPhone(string idLoja, string telefone);
    public Task<contract.Models.Client> CreateClient(contract.Models.Client client);
    public Task<contract.Models.Client> UpdateClient(contract.Models.Client client);
    Task<contract.Models.Client> GetClientById(contract.Models.Client client);
    Task<contract.Models.Client> DeleteClient(contract.Models.Client client);
    
    public Task<PreOrder> GetPreOrderById(string id);
    public Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf);
    public Task<PreOrder> CreatePreOrder(PreOrder preOrder);
    public Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrderItems);
    public Task<PreOrder> RemovePreOrderItems(PreOrder preOrder);
    public Task<PreOrder> DeletePreOrder(PreOrder preOrder);

    public Task<Order> GetOrderById(string orderId);
    public Task<Order> CreateOrderByPreOrder(PreOrder preOrder);
    public Task<Order> UpdateOrder(Order Order);
    public Task<Order> DeleteOrder(Order order);
    public Dictionary<PaymentMethod, string> GetPaymentsMethods(int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);
    public Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf);


}
