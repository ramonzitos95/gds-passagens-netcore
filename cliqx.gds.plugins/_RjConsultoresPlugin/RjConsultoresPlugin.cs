
using cliqx.gds.contract;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.plugins._RjConsultoresPlugin.Api;
using cliqx.gds.plugins._RjConsultoresPlugin.Extensions;
using cliqx.gds.plugins._RjConsultoresPlugin.Extra;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Request;
using cliqx.gds.plugins._RjConsultoresPlugin.Models.Response;
using cliqx.gds.services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace cliqx.gds.plugins;

public class RjConsultoresPlugin : PluginBase, IContractBase
{
    public PluginConfiguration _pluginConfiguration { get; set; }
    private string urlRecarga;
    private readonly ExtraData _extraData;
    private readonly AuthModel _authenticationData;
    private readonly RjConsultoresApi _rjAPI;
    public RjConsultoresPlugin(
        IConfiguration configuration
        , ILogger<IContractBase> logger
        , Func<HttpClient> newHttpClient
        , PluginConfiguration pluginConfiguration
        , IShoppingCartService shoppingCartService
        ) : base(configuration, logger, newHttpClient, pluginConfiguration, shoppingCartService)
    {
        _pluginConfiguration = pluginConfiguration;
        ApiUrlsBase apiUrlsBase = JsonConvert.DeserializeObject<ApiUrlsBase>(pluginConfiguration.ApiBaseUrl);
        _pluginConfiguration = pluginConfiguration;
        _authenticationData = JsonConvert.DeserializeObject<AuthModel>(pluginConfiguration.CredentialsJsonObject);
        _rjAPI = new RjConsultoresApi(apiUrlsBase, _authenticationData, NewHttpClient());

        if (pluginConfiguration.ExtraData != null)
            _extraData = JsonConvert.DeserializeObject<ExtraData>(pluginConfiguration.ExtraData);
    }

    public Task<contract.Models.Client> GetClientByProperty(Dictionary<string, string> property)
    {
        try
        {
            string data = "";

            if (property.ContainsKey("phone")) data = property["phone"];

            return ShoppingCartService.GetClientByLojaAndPhone(_pluginConfiguration.StoreId.ToString(), data);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public Task<contract.Models.Client> CreateClient(contract.Models.Client client)
    {
        throw new NotImplementedException();
    }

    public async Task<contract.Models.Client> UpdateClient(contract.Models.Client client)
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

    public async Task<PagedList<CustomCity>> GetOriginsByCityName(string cityName, int page = 1, int itemsPerPage = 15, string letterCode = "")
    {
        try
        {
            var response = await _rjAPI.Client.BuscaOrigem();

            if (response is { IsSuccessStatusCode: false } || response.Content == null)
            {
                Logger.LogError($"GetOriginsByCityName - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                return null;
            }

            return response.Content.Select(x => x.AsCustomCity()).Where(x => x.NormalizedName.Contains(cityName.ToLower())).ToPagedList(false);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Request {nameof(RjConsultoresPlugin)} - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
            Logger.LogError(ex, $"Retorno do {nameof(RjConsultoresPlugin)}: {ex.Message}");
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
            var response = await _rjAPI.Client.BuscaOrigem();

            if (response is { IsSuccessStatusCode: false } || response.Content == null)
            {
                Logger.LogError($"GetDestinationsByCityName - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                return null;
            }

            return response.Content.Select(x => x.AsCustomCity()).Where(x => x.NormalizedName.Contains(cityName.ToLower())).ToPagedList(false);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Request {nameof(RjConsultoresPlugin)} - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
            Logger.LogError(ex, $"Retorno do {nameof(RjConsultoresPlugin)}: {ex.Message}");
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

    public async Task<PagedList<Trip>> GetTripsByOriginAndDestination(TripQuery query, int page = 1, int itemsPerPage = 15)
    {
        try
        {
            var originId = query.Origin.Id;
            var destinationid = query.Destination.Id;

            var consultaRequest = new BuscaCorridaRequest()
            {
                Origem = originId
                ,
                Destino = destinationid
                ,
                Data = query.TravelDate.ToString("yyyy-MM-dd")
            };

            var response = await _rjAPI.Client.BuscaCorrida(consultaRequest);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"GetTripsByOriginAndDestination");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                return null;
            }

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

    public Task<PagedList<Trip>> GetTripsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<Seat>> GetSeatsByTripId(Trip trip)
    {
        try
        {
            var request = new ConsultaCategoriaServicoRequest()
            {
                Origem = trip.Origin.CityId,
                Destino = trip.Destination.CityId,
                Data = trip.DepartureTime.ToString("yyyy-MM-dd"),
                Servico = trip.Id
            };

            var response = await _rjAPI.Client.BuscaOnibus(request);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"GetSeatsByTripId");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content);
            }
            var ret = response.Content.AsSeat(trip);
            return ret.ToPagedList(false);

        }
        catch (System.Exception e)
        {
            var errorMessage = $"Error GetSeatsByTripId: {e.Message}";
            Logger.LogError(errorMessage);
            throw;
        }
    }

    public async Task<PagedList<Seat>> GetSeatsByCustomExpression(Dictionary<string, string> query)
    {
        try
        {
            string originId = "";
            string destinationId = "";
            string travelDate = "";
            string tripId = "";

            if (query.ContainsKey("originId")) originId = query["originId"];
            if (query.ContainsKey("destinationId")) destinationId = query["destinationId"];
            if (query.ContainsKey("travelDate")) travelDate = query["travelDate"];
            if (query.ContainsKey("tripId")) tripId = query["tripId"];

            var request = new ConsultaCategoriaServicoRequest()
            {
                Origem = originId,
                Destino = destinationId,
                Data = travelDate,
                Servico = tripId
            };

            var response = await _rjAPI.Client.BuscaOnibus(request);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"GetSeatsByCustomExpression");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content);
            }
            var ret = response.Content.AsSeat();
            return ret.ToPagedList(false);

        }
        catch (System.Exception e)
        {
            var errorMessage = $"Error GetSeatsByCustomExpression: {e.Message}";
            Logger.LogError(errorMessage);
            throw;
        }
    }

    public async Task<PreOrder> GetPreOrderById(string id)
    {
        try
        {
            return await ShoppingCartService.GetPreOrderById(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error GetPreOrderById {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }

    public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
    {
        try
        {
            long totalValue = 0;

            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat == null)
                                throw new Exception("Obrigatório informar a poltrona para todas as conexões");

                            var responseConn = await BloquearPoltronaAux(
                                conn.Origin.CityId,
                                conn.Destination.CityId,
                                item.Trip.ArrivalTime,
                                conn.Id,
                                seat.Seat.Number
                            );

                            if (responseConn == null)
                                throw new Exception("Erro desconhecido ao tentar bloquear poltrona");

                            totalValue += Convert.ToInt64(responseConn.Preco.Preco * 100);

                            seat.Transaction = new Transaction
                            {
                                ReservationId = responseConn.Transacao,
                                ReservationTotalPrice = Convert.ToInt64(responseConn.Preco.Preco * 100),
                                ReservationExpiresAt = DateTime.Now.AddMinutes(responseConn.Duracao),
                                LocatorId = responseConn.Localizador
                            };
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var response = await BloquearPoltronaAux(
                            item.Trip.Origin.CityId,
                            item.Trip.Destination.CityId,
                            item.Trip.ArrivalTime,
                            item.Trip.Id,
                            seat.Number
                        );

                        if (response == null)
                            throw new Exception("Erro desconhecido ao tentar bloquear poltrona");

                        seat.Transaction = new Transaction
                        {
                            TransactionId = response.Transacao,
                            ReservationTotalPrice = Convert.ToInt64(response.Preco.Preco),
                            TransactionExpiresAt = DateTime.Now.AddMinutes(response.Duracao)
                        };

                        totalValue += seat.Value;
                    }
                }
            }

            preOrder.TotalValue = totalValue;

            return await ShoppingCartService.CreatePreOrder(preOrder);
        }
        catch (Exception ex)
        {

            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat.Transaction is null) continue;
                            await DesbloquearPoltronaAux(seat.Transaction.TransactionId);
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction is null) continue;
                        await DesbloquearPoltronaAux(seat.Transaction.TransactionId);
                    }
                }
            }

            throw new Exception($"Error CreatePreOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }


    public async Task<BloqueioPoltronaResponse> BloquearPoltronaAux(
        string originId, string destinationId, DateTime arrivalTime, string tripId, string seatNumber
    )
    {
        var transactionId = "";

        try
        {
            var request = new BloqueioPoltronaRequest()
            {
                Origem = originId,
                Destino = destinationId,
                Data = arrivalTime.ToString("yyyy-MM-dd"),
                Servico = tripId,
                Poltrona = seatNumber
            };

            var response = await _rjAPI.Client.BloquearPoltrona(request);

            if (!response.IsSuccessStatusCode)
            {
                var err = $"BloquearPoltronaAux. Poltrona: {seatNumber}, Serviço: {tripId}";
                Logger.LogError(err);
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content + "\n" + err);
            }

            transactionId = response.Content.Transacao;

            return response.Content;
        }
        catch (Exception ex)
        {
            if (!String.IsNullOrEmpty(transactionId))
                await DesbloquearPoltronaAux(transactionId);

            throw new Exception($"Error BloquearPoltronaAux {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }

    public async Task<ConfirmaVendaResponse> ConfirmarVendaAux(
        string transacao, string nomePassageiro, string documentoPassageiro
    )
    {
        try
        {
            var request = new ConfirmaVendaRequest()
            {
                Transacao = transacao,
                NomePassageiro = nomePassageiro,
                DocumentoPassageiro = documentoPassageiro
            };

            var response = await _rjAPI.Client.ConfirmarVenda(request);

            if (!response.IsSuccessStatusCode)
            {
                var err = $"ConfirmarVendaAux. Transacao: {transacao}";
                Logger.LogError(err);
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content + "\n" + err);
            }

            return response.Content;
        }
        catch (Exception ex)
        {
            if (!String.IsNullOrEmpty(transacao))
                await CancelarTicketAux(transacao, false);

            throw new Exception($"Error BloquearPoltronaAux {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }

    private async Task<bool> DesbloquearPoltronaAux(string transactionId)
    {
        try
        {
            var request = new TransacaoRequest()
            {
                Transacao = transactionId,
            };

            var response = await _rjAPI.Client.DesbloquearPoltrona(request);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"BloquearPoltronaAux");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content);
            }

            return true;
        }
        catch (System.Exception e)
        {
            Logger.LogError($"BloquearPoltronaAux");
            Logger.LogError($"Retorno do {nameof(RjConsultoresPlugin)}: {e.Message}");
            return false;
        }
    }

    private async Task<bool> CancelarTicketAux(string transacao, bool validarMulta)
    {
        try
        {
            var request = new CancelaBilheteRequest()
            {
                Transacao = transacao
                ,
                ValidarMulta = validarMulta
            };

            var response = await _rjAPI.Client.CancelarBilhete(request);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"CancelarTicketAux");
                Logger.LogError(response.Error, $"Retorno do {nameof(RjConsultoresPlugin)}: {response.Error.Content}");
                throw new Exception(response.Error.Content);
            }

            return true;
        }
        catch (System.Exception e)
        {
            Logger.LogError($"CancelarTicketAux");
            Logger.LogError($"Retorno do {nameof(RjConsultoresPlugin)}: {e.Message}");
            return false;
        }
    }


    public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrder)
    {
        try
        {
            long totalValue = 0;

            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat == null)
                                throw new Exception("Obrigatório informar a poltrona para todas as conexões");

                            var responseConn = await BloquearPoltronaAux(
                                conn.Origin.CityId,
                                conn.Destination.CityId,
                                item.Trip.ArrivalTime,
                                conn.Id,
                                seat.Seat.Number
                            );

                            if (responseConn == null)
                                throw new Exception("Erro desconhecido ao tentar bloquear poltrona");

                            totalValue += Convert.ToInt64(responseConn.Preco.Preco * 100);

                            seat.Transaction = new Transaction
                            {
                                ReservationId = responseConn.Transacao,
                                ReservationTotalPrice = Convert.ToInt64(responseConn.Preco.Preco * 100),
                                ReservationExpiresAt = DateTime.Now.AddMinutes(responseConn.Duracao),
                                LocatorId = responseConn.Localizador
                            };
                        }

                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var response = await BloquearPoltronaAux(
                            item.Trip.Origin.CityId,
                            item.Trip.Destination.CityId,
                            item.Trip.ArrivalTime,
                            item.Trip.Id,
                            seat.Number
                        );

                        if (response == null)
                            throw new Exception("Erro desconhecido ao tentar bloquear poltrona");

                        seat.Transaction = new Transaction
                        {
                            ReservationId = response.Transacao,
                            ReservationTotalPrice = Convert.ToInt64(response.Preco.Preco),
                            ReservationExpiresAt = DateTime.Now.AddMinutes(response.Duracao)
                        };

                        totalValue += seat.Value;
                    }
                }
            }

            //preOrder.TotalValue = totalValue;

            return await ShoppingCartService.AddPreOrderItems(preOrder);
        }
        catch (Exception ex)
        {
            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat.Transaction is null) continue;
                            await DesbloquearPoltronaAux(seat.Transaction.TransactionId);
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction is null) continue;
                        await DesbloquearPoltronaAux(seat.Transaction.TransactionId);
                    }
                }
            }

            throw new Exception($"Error AddPreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }

    public async Task<PreOrder> RemovePreOrderItems(PreOrder preOrder)
    {
        try
        {
            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat == null)
                                throw new Exception("Obrigatório informar a poltrona para todas as conexões");

                            var responseConn = await DesbloquearPoltronaAux(
                                seat.Transaction.ReservationId
                            );

                            if (!responseConn)
                                throw new Exception("Erro desconhecido ao tentar desbloquear poltrona");
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var response = await DesbloquearPoltronaAux(
                            seat.Transaction.ReservationId
                        );

                        if (!response)
                            throw new Exception("Erro desconhecido ao tentar bloquear poltrona");
                    }
                }
            }

            return await ShoppingCartService.RemovePreOrderItems(preOrder);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error RemovePreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");

        }
    }

    public async Task<PreOrder> DeletePreOrder(PreOrder preOrder)
    {
        try
        {
            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat == null)
                                throw new Exception("Obrigatório informar a poltrona para todas as conexões");

                            var responseConn = await DesbloquearPoltronaAux(
                                seat.Transaction.ReservationId
                            );

                            if (!responseConn)
                                throw new Exception("Erro desconhecido ao tentar desbloquear poltrona");
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var response = await DesbloquearPoltronaAux(
                            seat.Transaction.ReservationId
                        );

                        if (!response)
                            throw new Exception("Erro desconhecido ao tentar bloquear poltrona");
                    }
                }
            }


            return await ShoppingCartService.DeletePreOrder(preOrder);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error RemovePreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");

        }
    }

    public async Task<Order> GetOrderById(string orderId)
    {
        try
        {
            return await ShoppingCartService.GetOrderById(orderId);
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Error GetOrderById {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
            throw;
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

    public async Task<Order> DeleteOrder(string orderId, Guid pluginId)
    {
        try
        {
            // if (order.Trip.TotalConnections > 0)
            // {
            //     foreach (var item in order.Trip.Connections)
            //     {
            //         foreach (var seat in item.Seats)
            //         {
            //             if (seat.Transaction is null) throw new Exception("Transaction é obrigatório");

            //             var request = new CancelaBilheteRequest
            //             {
            //                 Transacao = seat.Transaction.TransactionId,
            //                 ValidarMulta = false
            //             };

            //             var response = await _rjAPI.Client.CancelarBilhete(request);

            //             if (!response.IsSuccessStatusCode)
            //             {
            //                 Logger.LogError($"DeleteOrder");
            //                 Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
            //                 throw new Exception(response.Error.Content);
            //             }
            //         }

            //     }
            // }
            // else
            // {
            //     foreach (var item in order.Seats)
            //     {
            //         if (item.Transaction is null) throw new Exception("Transaction é obrigatório");

            //         var request = new CancelaBilheteRequest
            //         {
            //             Transacao = item.Transaction.TransactionId,
            //             ValidarMulta = false
            //         };


            //         var response = await _rjAPI.Client.CancelarBilhete(request);

            //         if (!response.IsSuccessStatusCode)
            //         {
            //             Logger.LogError($"DeleteOrder");
            //             Logger.LogError(response.Error, $"Retorno do Smartbus: {response.Error.Content}");
            //             throw new Exception(response.Error.Content);
            //         }
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


    public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
    {
        try
        {
            long totalValue = 0;

            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat == null)
                                throw new Exception("Obrigatório informar a poltrona para todas as conexões");

                            var responseConn = await ConfirmarVendaAux(
                                seat.Transaction.ReservationId
                                , seat.Client.FullName
                                , seat.Client.Documents.FirstOrDefault(x => x.DocumentType == DocumentTypeEnum.CPF).Value
                            );

                            if (responseConn == null)
                                throw new Exception("Erro desconhecido ao tentar confirmar venda");

                            totalValue += Convert.ToInt64(responseConn.Bpe.ValorPagar * 100);

                            seat.Transaction.TransactionId = responseConn.Transacao;
                            seat.Transaction.ReservationTotalPrice = Convert.ToInt64(responseConn.Bpe.ValorPagar * 100);
                            seat.Transaction.LocatorId = responseConn.Localizador;

                            seat.Ticket = responseConn.AsTicket();

                            totalValue += Convert.ToInt64(seat.Ticket.TotalAmount * 100);
                        }
                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var response = await BloquearPoltronaAux(
                            item.Trip.Origin.CityId,
                            item.Trip.Destination.CityId,
                            item.Trip.ArrivalTime,
                            item.Trip.Id,
                            seat.Number
                        );

                        if (response == null)
                            throw new Exception("Erro desconhecido ao tentar bloquear poltrona");

                        seat.Transaction = new Transaction
                        {
                            TransactionId = response.Transacao,
                            ReservationTotalPrice = Convert.ToInt64(response.Preco.Preco),
                            TransactionExpiresAt = DateTime.Now.AddMinutes(response.Duracao)
                        };

                        totalValue += seat.Value;
                    }
                }
            }



            preOrder.TotalValue = totalValue;

            return await ShoppingCartService.CreateOrderByPreOrder(preOrder);
        }
        catch (Exception ex)
        {
            foreach (var item in preOrder.Items)
            {
                if (item.Trip.TotalConnections > 0)
                {
                    foreach (var conn in item.Trip.Connections)
                    {
                        foreach (var seat in conn.Seats)
                        {
                            if (seat.Transaction is null) continue;
                            await CancelarTicketAux(seat.Transaction.TransactionId, false);
                        }

                    }
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        if (seat.Transaction is null) continue;

                        await DesbloquearPoltronaAux(seat.Transaction.TransactionId);
                    }
                }
            }


            throw new Exception($"Error CreateOrderByPreOrder {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
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
            throw new Exception($"Error GetPreOrderById {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
        }
    }
}
