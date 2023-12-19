using cliqx.gds.contract;
using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.plugins._DistribusionPlugin.Extensions;
using cliqx.gds.plugins.Util;
using cliqx.gds.services.Services;
using cliqx.gds.utils;
using distribusion.api.client.Api;
using distribusion.api.client.Models;
using distribusion.api.client.Request;
using distribusion.api.client.Response;
using DistribusionOmsHubPlugin.Models.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;

namespace cliqx.gds.plugins;

public class DistribusionPlugin : PluginBase, IContractBase
{
    public PluginConfiguration _pluginConfiguration { get; set; }
    private string urlRecarga;
    private readonly DistribusionApi _distribusionApi;
    private AuthenticationData _authenticationData;
    private readonly ClientCliqxDistribusionApi _cliqxDistribusionClient;

    public DistribusionPlugin(IConfiguration configuration,
        ILogger<IContractBase> logger,
        Func<HttpClient> newHttpClient,
        PluginConfiguration pluginConfiguration,
        IShoppingCartService shoppingCartService
        )
        : base(configuration, logger, newHttpClient, pluginConfiguration, shoppingCartService)
    {
        ApiUrlsBase apiUrlsBase = JsonConvert.DeserializeObject<ApiUrlsBase>(pluginConfiguration.ApiBaseUrl);
        _authenticationData = JsonConvert.DeserializeObject<AuthenticationData>(pluginConfiguration.CredentialsJsonObject);
        _pluginConfiguration = pluginConfiguration;
        _distribusionApi = new DistribusionApi(newHttpClient(), apiUrlsBase.BaseUrl, _authenticationData.ApiKey);
        _cliqxDistribusionClient = new ClientCliqxDistribusionApi(apiUrlsBase.DistribusionEtlUrl, newHttpClient());

        Logger.LogDebug($"DistribusionPlugin");
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

    public async Task<contract.Models.Client> CreateClient(contract.Models.Client client)
    {
        try
        {
            return await ShoppingCartService.CreateClient(client);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<contract.Models.Client> UpdateClient(contract.Models.Client client)
    {
        try
        {
            return await ShoppingCartService.UpdateClient(client);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PagedList<CustomCity>> GetOriginsByCityName(string cityName, int page = 1, int itemsPerPage = 15, string letterCode = "")
    {
        try
        {
            var apiResponse = await _cliqxDistribusionClient.ListCityAsync(letterCode, cityName.ToLower(), page, itemsPerPage);
            return apiResponse.Items.Select(x => x.AsCustomCity()).ToPagedList(apiResponse.TotalRows > (apiResponse.ItemsPerPage * apiResponse.Page));
        }
        catch (Exception ex)
        {
            Logger.LogError($"Request Distribusion - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
            Logger.LogError(ex, $"Retorno do Distribusion: {ex.Message}");
            return null;
            //throw new Exception($"resposta nao esperada da Distribusion api: {ex.Message}", ex);
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
            var apiResponse = await _cliqxDistribusionClient.ListCityAsync(letterCode, cityName, page, itemsPerPage);
            return apiResponse.Items.Select(x => x.AsCustomCity()).ToPagedList(apiResponse.TotalRows > (apiResponse.ItemsPerPage * apiResponse.Page));
        }
        catch (Exception ex)
        {
            Logger.LogError($"Request Distribusion - ListCities: letterCode: {cityName} ; cityName: {cityName}; page: {page}; itemsPerPage: {itemsPerPage}");
            Logger.LogError(ex, $"Retorno do Distribusion: {ex.Message}");
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
        var trips = new List<Trip>();

        string travelDate = query.TravelDate.ToString("yyyy-MM-dd");

        //var cidadeOrigemId = "BRSAO";
        var cidadeOrigemId = query.Origin.Id;
        //var cidadeDestinoId = "BRLZS";
        var cidadeDestinoId = query.Destination.Id;
        TimeSpan searchStartTime = new TimeSpan(0, 0, 0);
        TimeSpan searchEndTime = new TimeSpan(23, 59, 59);

        try
        {
            var retorno = await _distribusionApi.Client.GetConnections(
            null,
            null,
            travelDate,
            searchStartTime,
            searchEndTime,
            cidadeOrigemId,
            cidadeDestinoId);

            ConnectionsResponse connectionResponse = retorno.Content;

            var includedList = ConvertToList(connectionResponse.Included);

            query.Origin.Id = cidadeOrigemId;
            query.Destination.Id = cidadeDestinoId;

            foreach (var connection in connectionResponse.Data)
            {
                foreach (var fare in connection.Relationships.Fares.Data)
                {
                    var cidadeOrigem = includedList
                        .Where(cidade => cidade is distribusion.api.client.Models.City && ((distribusion.api.client.Models.City)cidade).Id.Equals(cidadeOrigemId))
                        .Select(cidade => (distribusion.api.client.Models.City)cidade).FirstOrDefault();

                    var cidadeDestino = includedList
                        .Where(cidade => cidade is distribusion.api.client.Models.City && ((distribusion.api.client.Models.City)cidade).Id.Equals(cidadeDestinoId))
                        .Select(cidade => (distribusion.api.client.Models.City)cidade).FirstOrDefault();

                    var classeOnibus = includedList
                        .Where(fareClass => fareClass is FareClass && ((FareClass)fareClass).Id.Equals(fare.FareClass.Id))
                        .Select(fareClass => (FareClass)fareClass).FirstOrDefault();

                    var estacaoOrigem = includedList
                        .Where(station => station is distribusion.api.client.Models.Station && ((distribusion.api.client.Models.Station)station).Id.Equals(connection.Relationships.DepartureStation.Data.Id))
                        .Select(station => (distribusion.api.client.Models.Station)station).FirstOrDefault();

                    var estacaoDestino = includedList
                        .Where(station => station is distribusion.api.client.Models.Station && ((distribusion.api.client.Models.Station)station).Id.Equals(connection.Relationships.ArrivalStation.Data.Id))
                        .Select(station => (distribusion.api.client.Models.Station)station).FirstOrDefault();

                    var empresa = includedList
                        .Where(marketingCarrier => marketingCarrier is MarketingCarrier && ((MarketingCarrier)marketingCarrier).Id.Equals(connection.Relationships.MarketingCarrier.Data.Id))
                        .Select(marketingCarrier => (MarketingCarrier)marketingCarrier).FirstOrDefault();

                    var tipoPassageiro = includedList
                        .Where(passengerType => passengerType is PassengerType)
                        .Select(passengerType => (PassengerType)passengerType).FirstOrDefault();

                    var trip = new Trip();
                    trip.Id = fare.Id;
                    trip.Value = fare.Price;
                    trip.DiscountValue = fare.Price;
                    trip.Display = $"📍 Origem: {estacaoOrigem.Attributes.Name}, {cidadeOrigem.Attributes.Name} \n" +
                                  $"🏁 Destino: {estacaoDestino.Attributes.Name}, {cidadeDestino.Attributes.Name} \n" +
                                  $"⏰ Saída: {connection.Attributes.DepartureTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                  $"🏁 Chegada: {connection.Attributes.ArrivalTime.ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                  $"💰 R$ {trip.ValueAsDecimal.ToString("N2", new CultureInfo("pt-BR"))} \n" +
                                  //$"💺 Assentos Livres: {servico.PoltronasLivres} \n" +
                                  $"🚌 Classe: {classeOnibus.Attributes.Name} \n" +
                                  $"⚙️ Empresa: {empresa.Attributes.TradeName}";
                    trip.Company = new Company();
                    trip.Company.Id = connection.Relationships.MarketingCarrier.Data.Id;
                    trip.Company.Name = empresa.Attributes.TradeName;
                    trip.Station = new TripStation();
                    trip.Station.OriginId = estacaoOrigem.Attributes.Code;
                    trip.Station.DestinationId = estacaoDestino.Attributes.Code;
                    trip.Station.OriginName = estacaoOrigem.Attributes.Name;
                    trip.Station.DestinationName = estacaoDestino.Attributes.Name;

                    trip.DepartureTime = connection.Attributes.DepartureTime;
                    trip.ArrivalTime = connection.Attributes.ArrivalTime;


                    trip.Origin = query.Origin.AsTrip();
                    trip.Destination = query.Destination.AsTrip();

                    trip.Class = new TripClass();
                    trip.Class.Id = classeOnibus.Attributes.Code;
                    trip.Class.Name = classeOnibus.Attributes.Name;
                    trip.Class.Description = classeOnibus.Id;

                    trip.PassengerType = tipoPassageiro.Id;

                    trip.Key = KeyGenerator.Generate(query.Origin.NormalizedName, query.Destination.NormalizedName, trip.Company.Name, trip.DepartureTime, trip.ArrivalTime, trip.Value);

                    trips.Add(trip);
                }
            }
        }
        catch (Exception e)
        {
            var err = $"GetTripsByOriginAndDestination: {e.Message}";
            Logger.LogError(err);
            return null;
        }

        return trips.ToPagedList(false);
    }

    public Task<PagedList<Trip>> GetTripsByCustomExpression(Dictionary<string, string> expression, int page = 1, int itemsPerPage = 15)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<contract.GdsModels.Seat>> GetSeatsByTripId(Trip trip)
    {
        try
        {
            var apiResponse = await _distribusionApi.Client.GetSeatMap(
                trip.Company.Id,
                trip.Station.OriginId,
                trip.Station.DestinationId,
                trip.DepartureTime.ToString("o").Substring(0, 16),
                trip.ArrivalTime.ToString("o").Substring(0, 16));

            if (apiResponse is { IsSuccessStatusCode: false } || apiResponse.Content == null)
            {
                Logger.LogError($"Request Distribusion - InnerProductsByProductUidAndStoreUid - BuscaOnibus");
                Logger.LogError(apiResponse.Error, $"Retorno do Distribusion: {apiResponse.Error?.Content}");
                throw new SnogException($"resposta nao esperada da Distribusion api: {apiResponse.Error?.Message}" + apiResponse.Error, ErrorIntegrationGDS.SEAT_BUSY);
            }

            var seatLayouResponse = apiResponse.Content;
            List<distribusion.api.client.Models.Basic.BasicObject> includedList = ConvertToList(seatLayouResponse.Included);

            var seatTransformation = seatLayouResponse.AsSeat(trip, includedList);
            var retorno = seatTransformation.ToPagedList();

            // string environment = Configuration["Environment"];

            // if (environment != "Development")
            // {
            //     var image = GenerationImage.montaLayout(seatTransformation, "https://facilitabots.com.br:8280/Media/ea131789-02a5-4572-ac32-bb118c93cdf0/fluxo/image/novoLayoutMapaPoltronas2.0.png");
            //     retorno.ExtraData = image;
            // }

            return retorno;
        }
        catch (Exception ex)
        {
            Logger.LogError($"Erro no ProductsByCategoryUidAndExpression: {ex.Message}");
            Logger.LogError($"Erro no ProductsByCategoryUidAndExpression: {ex.StackTrace}");
        }

        return null;
    }

    public Task<PagedList<contract.GdsModels.Seat>> GetSeatsByCustomExpression(Dictionary<string, string> expression)
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
            throw new Exception(ex.Message);
        }
    }

    public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
    {
        try
        {
            Console.WriteLine("Distribusion - CreatePreOrder start");
            long totalValue = 0;
            long totalValueItem = 0;
            long totalValueSeat = 0;
            foreach (var item in preOrder.Items)
            {

                if (item.Trip.Seats is null)
                    throw new Exception("Precisa informar ao menos uma poltrona.");
                foreach (var seat in item.Trip.Seats)
                {
                    var request = new distribusion.api.client.Request.SeatReservationCreateRequest().SeatReservationCreateRequest(seat, item.Trip);
                    request.RetailerPartnerNumber = _authenticationData.RetailerPartnerNumber;
                    var apiBloqueioPoltrona = await _distribusionApi.Client.PostSeatReservationCreate(request);

                    if (apiBloqueioPoltrona is { IsSuccessStatusCode: false } || apiBloqueioPoltrona.Content == null)
                    {
                        var rjex = JsonConvert.DeserializeObject<SeatReservationCreateResponse>(apiBloqueioPoltrona.Error.Content);
                        Logger.LogError($"Request Distribusion");
                        Logger.LogError(apiBloqueioPoltrona.Error, $"Retorno da Distribusion: {apiBloqueioPoltrona.Error?.Content}");
                        if (rjex != null)
                        {
                            if (apiBloqueioPoltrona.StatusCode == HttpStatusCode.Conflict)
                            {
                                throw new Exception("Poltrona ocupada");
                            }
                            throw new Exception(apiBloqueioPoltrona.Error.ToString());
                        }

                        throw new Exception($"resposta nao esperada da Distribusion api: {apiBloqueioPoltrona.Error?.Message}", apiBloqueioPoltrona.Error);
                    }

                    Console.WriteLine("Distribusion - CreatePreOrder após bloqueio");
                    seat.Transaction = new Transaction
                    {
                        ReservationId = apiBloqueioPoltrona.Content.Data.Id
                        ,
                        ReservationExpiresAt = apiBloqueioPoltrona.Content.Data.Attributes.ExpiresAt
                        ,
                        ReservationTotalPrice = apiBloqueioPoltrona.Content.Data.Attributes.TotalPrice
                    };

                    seat.Value = apiBloqueioPoltrona.Content.Data.Attributes.TotalPrice;
                    seat.DiscountValue = seat.Value;
                    totalValueSeat += seat.Value;

                }
                item.Value = totalValueSeat;
                item.TotalValue = totalValueSeat;
            }
            preOrder.Value = preOrder.Items.Sum(x => x.Value);
            preOrder.TotalValue = preOrder.Items.Sum(x => x.TotalValue);

            Console.WriteLine("Distribusion - CreatePreOrder antes ShoppingCartService.CreatePreOrder");

            return await ShoppingCartService.CreatePreOrder(preOrder);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PreOrder> UpdatePreOrder(PreOrder preOrder)
    {
        try
        {
            //return await ShoppingCartService.UpdatePreOrder(preOrder);
            return new PreOrder();
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrder)
    {
        try
        {
            Console.WriteLine("Distribusion - CreatePreOrder start");
            long totalValue = 0;
            foreach (var item in preOrder.Items)
            {
                long totalValueSeat = 0;
                if (item.Trip.Seats is null)
                    throw new Exception("Precisa informar ao menos uma poltrona.");
                foreach (var seat in item.Trip.Seats)
                {
                    var request = new distribusion.api.client.Request.SeatReservationCreateRequest().SeatReservationCreateRequest(seat, item.Trip);
                    request.RetailerPartnerNumber = _authenticationData.RetailerPartnerNumber;
                    var apiBloqueioPoltrona = await _distribusionApi.Client.PostSeatReservationCreate(request);

                    if (apiBloqueioPoltrona is { IsSuccessStatusCode: false } || apiBloqueioPoltrona.Content == null)
                    {
                        var rjex = JsonConvert.DeserializeObject<SeatReservationCreateResponse>(apiBloqueioPoltrona.Error.Content);
                        Logger.LogError($"Request Distribusion");
                        Logger.LogError(apiBloqueioPoltrona.Error, $"Retorno da Distribusion: {apiBloqueioPoltrona.Error?.Content}");
                        if (rjex != null)
                        {
                            if (apiBloqueioPoltrona.StatusCode == HttpStatusCode.Conflict)
                            {
                                throw new Exception("Poltrona ocupada");
                            }
                            throw new Exception(apiBloqueioPoltrona.Error.ToString());
                        }

                        throw new Exception($"resposta nao esperada da Distribusion api: {apiBloqueioPoltrona.Error?.Message}", apiBloqueioPoltrona.Error);
                    }

                    Console.WriteLine("Distribusion - AddPreOrderItems após bloqueio");
                    seat.Transaction = new Transaction
                    {
                        ReservationId = apiBloqueioPoltrona.Content.Data.Id
                        ,
                        ReservationExpiresAt = apiBloqueioPoltrona.Content.Data.Attributes.ExpiresAt
                        ,
                        ReservationTotalPrice = apiBloqueioPoltrona.Content.Data.Attributes.TotalPrice
                    };

                    seat.Value = apiBloqueioPoltrona.Content.Data.Attributes.TotalPrice;
                    totalValueSeat += seat.Value;
                    seat.DiscountValue = seat.Value;
                }
                item.Value = totalValueSeat;
                item.TotalValue = totalValueSeat;
            }

            Console.WriteLine("Distribusion - AddPreOrderItems antes ShoppingCartService.CreatePreOrder");

            return await ShoppingCartService.AddPreOrderItems(preOrder);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error AddPreOrderItems {_pluginConfiguration.Plugin.Name}: {ex.Message} ");
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
                    var apiRetornoDesbloqueioPoltrona = await _distribusionApi.Client.SeatReservationCancel(seat.Transaction.ReservationId);

                    if (apiRetornoDesbloqueioPoltrona is { IsSuccessStatusCode: false } || apiRetornoDesbloqueioPoltrona.Content == null)
                    {
                        Logger.LogError($"Request Distribusion - RemovePreOrderItems: Request {seat.Transaction.ReservationId}");
                        Logger.LogError(apiRetornoDesbloqueioPoltrona.Error, $"Retorno do RjConsultores: {apiRetornoDesbloqueioPoltrona.Error?.Content}");
                        throw new Exception($"resposta nao esperada da Distribusion api: {apiRetornoDesbloqueioPoltrona.Error?.Message}", apiRetornoDesbloqueioPoltrona.Error);
                    }
                }

            }
            await ShoppingCartService.RemovePreOrderItems(preOrder);
            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
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
                    var apiRetornoDesbloqueioPoltrona = await _distribusionApi.Client.SeatReservationCancel(seat.Transaction.ReservationId);

                    if (apiRetornoDesbloqueioPoltrona is { IsSuccessStatusCode: false } || apiRetornoDesbloqueioPoltrona.Content == null)
                    {
                        Logger.LogError($"Request Distribusion - DeletePreOrder: Request {seat.Transaction.ReservationId}");
                        Logger.LogError(apiRetornoDesbloqueioPoltrona.Error, $"Retorno do RjConsultores: {apiRetornoDesbloqueioPoltrona.Error?.Content}");
                        throw new Exception($"resposta nao esperada da Distribusion api: {apiRetornoDesbloqueioPoltrona.Error?.Message}", apiRetornoDesbloqueioPoltrona.Error);
                    }
                }


            }
            await ShoppingCartService.DeletePreOrder(preOrder);
            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
    {
        var order = new Order();
        var totalValue = 0;

        foreach (var item in preOrder.Items)
        {
            var totalValueItem = 0;
            foreach (var seat in item.Trip.Seats)
            {
                var retornoReserva = await _distribusionApi.Client.GetReservation(seat.Transaction.ReservationId);

                if (!retornoReserva.IsSuccessStatusCode && retornoReserva.Content == null)
                {
                    Logger.LogError($"Request Distribusion - CreateOrderByPreOrder - Consulta Status Reserva");
                    Logger.LogError(retornoReserva.Error, $"Retorno do Distribusion: {retornoReserva.Error?.Content}");
                    throw new Exception($"resposta nao esperada da Distribusion api: {retornoReserva.Error?.Message}", retornoReserva.Error);
                }

                if (retornoReserva.Content.Data.Attributes.ExpiresAt < DateTime.UtcNow)
                    throw new Exception("Reserva Expirada, poltrona não disponível");

                if ("failed".Equals(retornoReserva.Content?.Data?.Attributes?.State))
                    throw new Exception("Ocorreu um erro ao reservar a poltrona, não é possível concluir a compra");

                seat.Value = retornoReserva.Content.Data.Attributes.TotalPrice;
                totalValue += retornoReserva.Content.Data.Attributes.TotalPrice;
                seat.DiscountValue = seat.Value;
            }
            item.Value = totalValue;
            item.TotalValue = totalValue;
        }

        preOrder.Value = totalValue;
        preOrder.TotalValue = totalValue;

        return await ShoppingCartService.CreateOrderByPreOrder(preOrder);
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        try
        {
            return await ShoppingCartService.UpdateOrder(order);
        }
        catch (Exception e)
        {
            Logger.LogError($"Request Distribusion - UpdateOrder - Update do pedido");
            Logger.LogError(e.Message);
            return new Order();
        }
    }

    public async Task<Order> DeleteOrder(string orderId, Guid pluginId)
    {
        try
        {
            var order = await ShoppingCartService.GetOrderById(orderId);
            
            if(order == null)
                throw new Exception("Erro ao buscar pedido");

            foreach (var item in order.Items)
            {
                foreach (var seat in item.Trip.Seats)
                {
                    string mensagem;
                    if (string.IsNullOrEmpty(seat.Transaction.BookingNumber))
                    {
                        mensagem = $"Pedido {order.ExternalId} Número da reserva {seat.Transaction.BookingNumber} não informada";
                        throw new Exception(mensagem);
                    }

                    CancellationCreateRequest ccr = new CancellationCreateRequest()
                    {
                        Booking = seat.Transaction.BookingNumber
                    };

                    var retornoReserva = await _distribusionApi.Client.GetCancellationConditions(seat.Transaction.BookingNumber);

                    if (!retornoReserva.IsSuccessStatusCode || retornoReserva.Content == null)
                    {
                        Logger.LogError($"Request: CancelOrder - OrderUid: {order.ExternalId}; Booking: {seat.Transaction.BookingNumber}");
                        Logger.LogError(retornoReserva.Error, $"Retorno da Distribusion: {retornoReserva.Error?.Content}");
                        throw new Exception($"resposta nao esperada da API da Distribusion: {retornoReserva.Error?.Message}", retornoReserva.Error);
                    }

                    if (!retornoReserva.Content.Data.Attributes.Allowed)
                    {
                        Logger.LogError($"Request: CancelOrder - OrderUid: {order.ExternalId}; Booking: {seat.Transaction.BookingNumber}");
                        Logger.LogError($"Booking: {seat.Transaction.BookingNumber} não pode ser cancelado");
                        continue;
                    }

                    var apiResponse = await _distribusionApi.Client.CancellationCreate(ccr);

                    if (apiResponse is { IsSuccessStatusCode: false })
                    {
                        Logger.LogError($"Request: CancelOrder - OrderUid: {order.ExternalId}");
                        Logger.LogError(apiResponse.Error, $"Retorno da Distribusion: {apiResponse.Error?.Content}");
                        throw new Exception($"resposta nao esperada da API da Distribusion: {apiResponse.Error?.Message}", apiResponse.Error);
                    }

                    seat.Transaction.CancelationFee = apiResponse.Content.Data.Attributes.Fee;
                    seat.Transaction.CancelationTotalOrder = apiResponse.Content.Data.Attributes.TotalPrice;
                    seat.Transaction.CancelationRefund = apiResponse.Content.Data.Attributes.TotalRefund;
                    seat.Transaction.CancelationCreatedAt = apiResponse.Content.Data.Attributes.CreatedAt.Value.ToString("yyyy-MM-dd");
                }
            }
            
            return await ShoppingCartService.DeleteOrder(order);
        }
        catch (Exception e)
        {
            Logger.LogError($"Request Distribusion - UpdateOrder - Update do pedido");
            Logger.LogError(e.Message);
            return new Order();
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

    private List<distribusion.api.client.Models.Basic.BasicObject> ConvertToList(IEnumerable<dynamic> included)
    {
        List<distribusion.api.client.Models.Basic.BasicObject> listaRetorno = new List<distribusion.api.client.Models.Basic.BasicObject>();
        JToken value;
        foreach (var json in included)
        {
            if (json.TryGetValue("type", out value))
            {
                if (value.ToString().Equals("segments"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Segment>());
                }
                else if (value.ToString().Equals("vehicle_types"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.VehicleType>());
                }
                else if (value.ToString().Equals("vehicles"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Vehicle>());
                }
                else if (value.ToString().Equals("fares"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Fare>());
                }
                else if (value.ToString().Equals("marketing_carriers"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.MarketingCarrier>());
                }
                else if (value.ToString().Equals("operating_carriers"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.OperatingCarrier>());
                }
                else if (value.ToString().Equals("passenger_types"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.PassengerType>());
                }
                else if (value.ToString().Equals("stations"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Station>());
                }
                else if (value.ToString().Equals("cities"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.City>());
                }
                else if (value.ToString().Equals("fare_classes"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.FareClass>());
                }
                else if (value.ToString().Equals("fare_features"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.FareFeature>());
                }
                else if (value.ToString().Equals("seats"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Seat>());
                }
                else if (value.ToString().Equals("bookings"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Book>());
                }
                else if (value.ToString().Equals("fees"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Fee>());
                }
                else if (value.ToString().Equals("segment_passengers"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.SegmentPassenger>());
                }
                else if (value.ToString().Equals("passengers"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.PassengerBasicObject>());
                }
                else if (value.ToString().Equals("ticket_validity_rules"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.TicketValidityRules>());
                }
                else if (value.ToString().Equals("cancellation_conditions"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Response.CancellationConditionsResponse>());
                }
                else if (value.ToString().Equals("cancellations"))
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Response.CancellationCreateResponse>());
                }
                else
                {
                    listaRetorno.Add(json.ToObject<distribusion.api.client.Models.Basic.BasicObject>());
                }
            }
        }

        return listaRetorno;
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
