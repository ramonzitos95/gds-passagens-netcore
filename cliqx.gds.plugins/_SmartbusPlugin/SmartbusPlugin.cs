
using cliqx.gds.contract;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.plugins._SmartbusPlugin.Api;
using cliqx.gds.plugins._SmartbusPlugin.Extensions;
using cliqx.gds.plugins._SmartbusPlugin.Extra;
using cliqx.gds.plugins._SmartbusPlugin.Models.Request;
using cliqx.gds.plugins.Util;
using cliqx.gds.services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace cliqx.gds.plugins
{
    public class SmartbusPlugin : PluginBase, IContractBase
    {
        private readonly AuthModel _authenticationData;
        public PluginConfiguration _pluginConfiguration { get; set; }
        private readonly SmartbusApi _smartbusAPI;
        private readonly CliqxSmartbusApi _cliqxSmartbusAPI;
        private readonly ExtraData _extraData;


        public SmartbusPlugin(
            IConfiguration configuration
            , ILogger<IContractBase> logger
            , Func<HttpClient> newHttpClient
            , PluginConfiguration pluginConfiguration
            , IShoppingCartService shoppingCartService
            ) : base(configuration, logger, newHttpClient, pluginConfiguration, shoppingCartService)
        {
            ApiUrlsBase apiUrlsBase = JsonConvert.DeserializeObject<ApiUrlsBase>(pluginConfiguration.ApiBaseUrl);
            _authenticationData = JsonConvert.DeserializeObject<AuthModel>(pluginConfiguration.CredentialsJsonObject);
            _pluginConfiguration = pluginConfiguration;
            _smartbusAPI = new SmartbusApi(apiUrlsBase, _authenticationData);
            _cliqxSmartbusAPI = new CliqxSmartbusApi(newHttpClient(), apiUrlsBase.UrlEtl);
            _extraData = JsonConvert.DeserializeObject<ExtraData>(pluginConfiguration.ExtraData);

        }


        public async Task<PagedList<CustomCity>> GetOriginsByCityName(string cityName, int page = 1, int itemsPerPage = 15, string letterCode = "")
        {
            try
            {
                var retorno = await _cliqxSmartbusAPI.Client.GetAllByCityName(cityName.ToLower());

                if (!retorno.IsSuccessStatusCode)
                {
                    Logger.LogError($"GetOriginsByCityName - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                    Logger.LogError(retorno.Error, $"Retorno do Distribusion: {retorno.Error.Content}");
                    return null;
                }
                var tst = retorno.Content.Select(x => x.AsCustomCity()).ToPagedList(false);
                return retorno.Content.Select(x => x.AsCustomCity()).ToPagedList(false);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Request Smartbus - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                Logger.LogError(ex, $"Retorno do Smartbus: {ex.Message}");
                return null;
            }
        }

        public Task<PagedList<CustomCity>> GetOriginsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<CustomCity>> GetDestinationsByCityName(string cityName, int page = 1, int itemsPerPage = 15, string letterCode = "")
        {
            try
            {
                var retorno = await _cliqxSmartbusAPI.Client.GetAllByCityName(cityName.ToLower());

                if (!retorno.IsSuccessStatusCode)
                {
                    Logger.LogError($"GetDestinationsByCityName - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                    Logger.LogError(retorno.Error, $"Retorno do Smartbus: {retorno.Error.Content}");
                    return null;
                }

                return retorno.Content.Select(x => x.AsCustomCity()).ToPagedList(false);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Request Smartbus - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                Logger.LogError(ex, $"Retorno do Smartbus: {ex.Message}");
                return null;
            }
        }

        public Task<PagedList<CustomCity>> GetDestinationsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> GetTripById(TripQuery trip)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Trip>> GetTripsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Trip>> GetTripsByOriginAndDestination(TripQuery query, int page = 1, int itemsPerPage = 15)
        {
            try
            {
                var originId = query.Origin.Id;
                var destinationid = query.Destination.Id;

                var consultaRequest = new ConsultaRequest()
                {
                    Origem = int.Parse(originId)
                    ,
                    Destino = int.Parse(destinationid)
                    ,
                    Data = query.TravelDate
                };

                var response = await _smartbusAPI.Client.Consulta(consultaRequest);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"GetTripsByOriginAndDestination");
                    Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                    return null;
                }
                if (response.Content.Data == null)
                    return null;

                query.Origin.Id = originId;
                query.Destination.Id = destinationid;

                return response.Content.AsTrip(query).ToPagedList(false);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error GetTripsByOriginAndDestination {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
                return null;
            }
        }

        public Task<PagedList<Seat>> GetSeatsByCustomExpression(Dictionary<string, string> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Seat>> GetSeatsByTripId(Trip trip)
        {
            try
            {
                var request = new RequestMapa()
                {
                    Origem = int.Parse(trip.Origin.CityId)
                    ,
                    Destino = int.Parse(trip.Destination.CityId)
                    ,
                    Data = trip.DepartureTime
                    ,
                    Servico = int.Parse(trip.Id)
                };

                var response = await _smartbusAPI.Client.BuscarPoltronas(request);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"GetSeatsByTripId");
                    Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                    return null;
                }
                var seats = response.Content.AsSeat();
                var ret = seats.ToPagedList(false);

                string environment = Configuration["Environment"];

                if (environment != "Development")
                {
                    var image = GenerationImage.montaLayout(seats, "URL_IMAGE");
                    ret.ExtraData = image;
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error GetSeatsByTripId {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
                throw;
            }
        }

        public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
        {
            try
            {
                long totalValue = 0;
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var request = new RequestReserva()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId)
                        ,
                            Destino = int.Parse(item.Trip.Destination.CityId)
                        ,
                            Data = item.Trip.ArrivalTime
                        ,
                            Servico = int.Parse(item.Trip.Id)
                        ,
                            Assento = long.Parse(seat.Number)
                        ,
                            Conexao = false
                        };

                        var response = await _smartbusAPI.Client.CriarReserva(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"CreatePreOrder");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Content);
                        }

                        seat.Transaction = new Transaction
                        {
                            TransactionId = response.Content.Data.TransacaoId
                            ,
                            ReservationTotalPrice = seat.Value
                        };

                        totalValue += seat.Value;
                    }

                }
                preOrder.TotalValue = totalValue;

                return await ShoppingCartService.CreatePreOrder(preOrder);
            }
            catch (Exception ex)
            {
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction.TransactionId == null) continue;

                        var request = new DesbloqueioRequest()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId)
                            ,Destino = int.Parse(item.Trip.Destination.CityId)
                            ,Data = item.Trip.ArrivalTime
                            ,Servico = int.Parse(item.Trip.Id)
                            ,TransacaoId = seat.Transaction.TransactionId
                            ,Conexao = false
                        };

                        var response = await _smartbusAPI.Client.DesbloquearPoltrona(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"CreatePreOrder");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Content);
                        }
                    }
                    
                }

                throw new Exception($"Error CreatePreOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }


        public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrder)
        {
            try
            {
                long totalValue = 0;
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var request = new RequestReserva()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId),
                            Destino = int.Parse(item.Trip.Destination.CityId),
                            Data = item.Trip.ArrivalTime,
                            Servico = int.Parse(item.Trip.Id),
                            Assento = long.Parse(seat.Number),
                            Conexao = false
                        };

                        var response = await _smartbusAPI.Client.CriarReserva(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"AddPreOrderItems");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Message);
                        }

                        totalValue += seat.Value;
                    }
                    
                }
                //preOrder.TotalValue = totalValue;
                return await ShoppingCartService.AddPreOrderItems(preOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error AddPreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public async Task<Client> CreateClient(Client client)
        {
            try
            {
                return await ShoppingCartService.CreateClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error CreateClient {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
        {
            try
            {
                long totalValue = 0;

                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var request = new ConfirmacaoRequest()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId),
                            Destino = int.Parse(item.Trip.Destination.CityId),
                            Data = item.Trip.ArrivalTime,
                            Servico = int.Parse(item.Trip.Id),
                            Assento = long.Parse(seat.Number),
                            NomePassageiro = seat.Client.FullName,
                            DocumentoPassageiro = Convert.ToInt64(seat.Client.Documents.FirstOrDefault(x => x.DocumentType == DocumentTypeEnum.CPF).Value),
                            Seguro = preOrder.HasInsurance,
                            TransacaoId = seat.Transaction.TransactionId,
                            Conexao = false,
                            RetornaBpe = item.Trip.IsBpe,
                            Contrato = _extraData.Contrato
                        };

                        var response = await _smartbusAPI.Client.CriarConfirmacao(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"CreateOrderByPreOrder");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Message);
                        }

                        seat.Ticket = response.Content.AsTicket();

                        totalValue += Convert.ToInt64(seat.Ticket.TotalAmount * 100);
                    }
                    
                }
                preOrder.TotalValue = totalValue;

                return await ShoppingCartService.CreateOrderByPreOrder(preOrder);
            }
            catch (Exception ex)
            {
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction.TransactionId == null) continue;

                        var request = new CancelamentoRequest();
                        request.Params = new List<Param>();

                        var param = new Param()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId)
                            ,
                            Destino = int.Parse(item.Trip.Destination.CityId)
                            ,
                            Data = item.Trip.ArrivalTime
                            ,
                            Servico = int.Parse(item.Trip.Id)
                            ,
                            TransacaoId = seat.Transaction.TransactionId
                            ,
                            Conexao = false
                        };

                        request.Params.Add(param);

                        var response = await _smartbusAPI.Client.CancelarBilhete(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"CreatePreOrder");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Content);
                        }
                    }
                    
                }

                throw new Exception($"Error CreateOrderByPreOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }

        }



        public async Task<Order> DeleteOrder(string orderId, Guid pluginId)
        {

            try
            {
                // foreach (var item in order.Seats)
                // {
                //     if (item.Transaction.TransactionId == null) continue;

                //     var request = new CancelamentoRequest();
                //     request.Params = new List<Param>();

                //     var param = new Param()
                //     {
                //         Origem = int.Parse(order.Trip.Origin.CityId),
                //         Destino = int.Parse(order.Trip.Destination.CityId),
                //         Data = order.Trip.ArrivalTime,
                //         Servico = int.Parse(order.Trip.Id),
                //         TransacaoId = item.Transaction.TransactionId,
                //         Conexao = false
                //     };

                //     request.Params.Add(param);

                //     var response = await _smartbusAPI.Client.CancelarBilhete(request);

                //     if (!response.IsSuccessStatusCode)
                //     {
                //         Logger.LogError($"DeleteOrder");
                //         Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                //         throw new Exception(response.Error.Content);
                //     }
                // }

                var order = new Order();

                return await ShoppingCartService.DeleteOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DeleteOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }


        public async Task<PreOrder> DeletePreOrder(PreOrder preOrder)
        {
            try
            {
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction.TransactionId == null) continue;

                        var request = new DesbloqueioRequest()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId)
                            ,
                            Destino = int.Parse(item.Trip.Destination.CityId)
                            ,
                            Data = item.Trip.ArrivalTime
                            ,
                            Servico = int.Parse(item.Trip.Id)
                            ,
                            TransacaoId = seat.Transaction.TransactionId
                            ,
                            Conexao = false
                        };

                        var response = await _smartbusAPI.Client.DesbloquearPoltrona(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"CreatePreOrder");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Content);
                        }
                    }
                    
                }

                return await ShoppingCartService.DeletePreOrder(preOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DeletePreOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public Task<Client> GetClientByProperty(Dictionary<string, string> property)
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



        public async Task<Order> GetOrderById(string orderId)
        {
            try
            {
                return await ShoppingCartService.GetOrderById(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error GetOrderById {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }



        public async Task<PagedList<PaymentMethod>> GetPaymentsMethods(int page = 1, int itemsPerPage = 15)
        {
            throw new NotImplementedException();
        }

        public async Task<PreOrder> GetPreOrderById(string id)
        {
            try
            {
                return await ShoppingCartService.GetPreOrderById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error GetPreOrderByPreOrderId {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }



        public async Task<PreOrder> RemovePreOrderItems(PreOrder preOrder)
        {
            try
            {
                foreach (var item in preOrder.Items)
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction.TransactionId == null) continue;

                        var request = new DesbloqueioRequest()
                        {
                            Origem = int.Parse(item.Trip.Origin.CityId),
                            Destino = int.Parse(item.Trip.Destination.CityId),
                            Data = item.Trip.ArrivalTime,
                            Servico = int.Parse(item.Trip.Id),
                            TransacaoId = seat.Transaction.TransactionId,
                            Conexao = false
                        };

                        var response = await _smartbusAPI.Client.DesbloquearPoltrona(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.LogError($"RemovePreOrderItems");
                            Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
                            throw new Exception(response.Error.Content);
                        }
                    }
                    
                }
                await ShoppingCartService.RemovePreOrderItems(preOrder);
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error RemovePreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public async Task<Client> UpdateClient(Client client)
        {
            try
            {
                return await ShoppingCartService.UpdateClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error UpdateClient {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                return await ShoppingCartService.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error UpdateOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public Task<PreOrder> UpdatePreOrder(PreOrder preOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf)
        {
            try
            {
                return await ShoppingCartService.GetOrderByIdByCpfClient(orderId, cpf);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error GetPreOrderById {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }

        public async Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf)
        {
            try
            {
                return await ShoppingCartService.ValidaDadosSuspeitoPorCpf(cpf);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ValidaDadosSuspeitoPorCpf {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            }
        }
    }
}