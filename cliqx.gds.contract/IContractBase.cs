using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Util;
using Client = cliqx.gds.contract.Models.Client;

namespace cliqx.gds.contract;

public interface IContractBase
{
    public Task<Client> GetClientByProperty(Dictionary<string, string> property);
    public Task<Client> CreateClient(Client client);
    public Task<Client> UpdateClient(Client client);

 
    public Task<PagedList<CustomCity>> GetOriginsByCityName(string cityName, int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage, string letterCode = "");
    public Task<PagedList<CustomCity>> GetOriginsByCustomExpression(Dictionary<string, string> expression,int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);
    
    public Task<PagedList<CustomCity>> GetDestinationsByCityName(string cityName, int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage, string letterCode = "");
    public Task<PagedList<CustomCity>> GetDestinationsByCustomExpression(Dictionary<string, string> expression,int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);

    public Task<Trip> GetTripById(TripQuery trip);
    public Task<PagedList<Trip>> GetTripsByOriginAndDestination(TripQuery query,int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);
    public Task<PagedList<Trip>> GetTripsByCustomExpression(Dictionary<string, string> expression,int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);

    public Task<PagedList<Seat>> GetSeatsByTripId(Trip trip);
    public Task<PagedList<Seat>> GetSeatsByCustomExpression(Dictionary<string, string> expression);

    public Task<PreOrder> GetPreOrderById(string id);
    
    public Task<PreOrder> CreatePreOrder(PreOrder preOrder);
    public Task<PreOrder> UpdatePreOrder(PreOrder preOrder);
    public Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrderItems);
    public Task<PreOrder> RemovePreOrderItems(PreOrder preOrder);
    public Task<PreOrder> DeletePreOrder(PreOrder preOrder);

    public Task<Order> GetOrderById(string orderId);
    public Task<Order> CreateOrderByPreOrder(PreOrder preOrder);
    public Task<Order> UpdateOrder(Order Order);
    public Task<Order> DeleteOrder(string orderId, Guid pluginId);

    public Task<PagedList<PaymentMethod>> GetPaymentsMethods(int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);

    public Task<PagedList<Company>> GetCompanies(int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);
    public Task<PagedList<Company>> GetCompaniesByExpression(Dictionary<string, string> query,int page = 1, int itemsPerPage = Constants.DefaultItemsPerPage);
    public Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf);
    public Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf);


}
