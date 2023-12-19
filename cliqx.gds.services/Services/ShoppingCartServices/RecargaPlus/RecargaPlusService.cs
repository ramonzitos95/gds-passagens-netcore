using cliqx.gds.contract.GdsModels;
using cliqx.gds.services.Services.Base;
using Microsoft.Extensions.Configuration;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Enums;
using cliqx.gds.services.Services.Email;
using Newtonsoft.Json;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Enum;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using Refit;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Util;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus;

public class RecargaPlusService : ServiceBase, IShoppingCartService
{
    private readonly RecargaPlusApi _recargaPlusApi;
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly Func<Task<string>> getTokenFunc;
    private readonly IConfiguration _configuration;
    private readonly IPaymentService _paymentService;
    private AuthenticationData _authenticationData;

    public RecargaPlusService(
        IConfiguration configuration
        , PluginConfiguration pluginConfiguration
        , IPaymentService paymentService)
    : base(configuration)
    {
        _configuration = configuration;
        _pluginConfiguration = pluginConfiguration;
        _recargaPlusApi = new RecargaPlusApi(configuration, configuration.GetSection("RecargaPlus")["UrlBase"]);
        _paymentService = paymentService;
        _authenticationData = JsonConvert.DeserializeObject<AuthenticationData>(pluginConfiguration.CredentialsJsonObject);
    }

    public RecargaPlusService(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<contract.Models.Client> GetClientByLojaAndPhone(string idLoja, string telefone)
    {
        try
        {
            var retorno = await _recargaPlusApi.Client.GetClient(idLoja, telefone);

            if (retorno.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine($"Error GetClientByProperty: Cliente não encontrado pelo: {telefone}");
                return null;
            }

            if (!retorno.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error Refit: {retorno.StatusCode} URL: {retorno.RequestMessage}");
                throw new Exception($"Error GetClientByProperty: {retorno.Error.Content}");
            }



            return retorno.Content.AsGdsClient();
        }
        catch (Exception e)
        {
            throw new Exception($"Error GetClientByProperty: {e.Message}");
        }
    }

    public async Task<contract.Models.Client> CreateClient(contract.Models.Client client)
    {
        try
        {

            Console.WriteLine($"----------------CreateClient - {client.Id}------------------------------");
            client.Id = "0";
            var clientModel = client.AsRecargaClient(_pluginConfiguration.StoreId);

            Console.WriteLine("----------------CreateClient antes recarga------------------------------");
            var retorno = await _recargaPlusApi.Client.CreateClient(clientModel);
            if (retorno is { IsSuccessStatusCode: false } || retorno.Content == null)
            {
                Console.WriteLine($"Request RecargaPlus - InnerProductsByProductUidAndStoreUid - BuscaOnibus");
                Console.WriteLine($"Retorno do RecargaPlus: {retorno.Error?.Content}");
                throw new Exception($"resposta nao esperada da RecargaPlus api: {retorno.Error?.Message}", retorno.Error);
            }

            return retorno.Content.AsGdsClient();
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"Error CreateClient: {e.Message} - StackTrace:{e.StackTrace}");
            throw new Exception($"Error CreateClient: {e.Message}");
        }
    }

    public async Task<contract.Models.Client> UpdateClient(contract.Models.Client client)
    {
        try
        {
            var clientMapped = client.AsRecargaClient(_pluginConfiguration.StoreId);
            var retorno = await _recargaPlusApi.Client.UpdateClient(
                clientMapped.Id
                , clientMapped.Telefone
                , clientMapped.Nome
                , client.FullName.Split(" ").Last()
                , clientMapped.Cpf
                , clientMapped.Email
            );

            if (!retorno.IsSuccessStatusCode)
                throw new Exception($"Error UpdateClient: {retorno.Error}");
            else
                return new contract.Models.Client();
        }
        catch (System.Exception e)
        {
            throw new Exception($"Error UpdateClient: {e.Message}");
        }
    }

    public async Task<contract.Models.Client> GetClientById(contract.Models.Client client)
    {
        var retorno = await this._recargaPlusApi.Client.GetClient(client.Id);
        return retorno.Content.AsGdsClient();
    }

    public async Task<contract.Models.Client> DeleteClient(contract.Models.Client client)
    {
        var retorno = await this._recargaPlusApi.Client.DeleteClient(client.Id);
        return retorno.Content.AsGdsClient();
    }

    public async Task<PreOrder> CreatePreOrder(PreOrder preOrder)
    {
        try
        {
            bool validaClient = false;
            ApiResponse<ClientResponse> clientRecargaPlus = null;
            ApiResponse<LojaResponse> storeRecarga = null;

            ValidadorPreOrder.Validar(preOrder, validaClient: validaClient);

            if (validaClient)
            {
                var clientNumber = preOrder.Client.Contacts.FirstOrDefault(x => x.ContactType == ContactTypeEnum.CelularPessoal)?.Value;
                if (string.IsNullOrEmpty(clientNumber))
                    throw new Exception("Número de telefone do cliente não informado.");

                clientRecargaPlus = await _recargaPlusApi.Client.GetClient(
                        _pluginConfiguration.StoreId.ToString()
                        , clientNumber
                    );

                if (!clientRecargaPlus.IsSuccessStatusCode)
                {
                    if (clientRecargaPlus.StatusCode == System.Net.HttpStatusCode.NotFound)
                        throw new Exception($"Cliente não localizado pelo telefone: {clientNumber}");

                    throw new Exception(clientRecargaPlus.Error.Content);
                }

                if (clientRecargaPlus.Content == null)
                    throw new Exception($"Cliente não localizado pelo telefone: {clientNumber}");

            }

            if (!string.IsNullOrEmpty(preOrder.TokenParceiro))
            {
                var resultParceiro = await _recargaPlusApi.Client.ObterParceiroPorToken(preOrder.TokenParceiro);

                if (resultParceiro.IsSuccessStatusCode)
                {
                    if (resultParceiro.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        var parceiro = resultParceiro.Content;

                        preOrder.ParceiroId = parceiro.Id;
                        preOrder.PartnerFee = parceiro.TaxaServico.Value;

                        var valuePartnerFee = Math.Round((preOrder.ValueAsDecimal * preOrder.PartnerFee) / 100, 4);

                        preOrder.ValuePartnerFee = Convert.ToInt64((valuePartnerFee * 100));
                    }
                    else
                    {
                        Console.WriteLine("Revendedor não localizado pela chave: " + preOrder.TokenParceiro);
                    }

                }
                else
                {
                    Console.WriteLine("Erro ao buscar revendedor. Error: " + resultParceiro.Error.Content);
                }
            }

            storeRecarga = await _recargaPlusApi.Client.GetLoja(_pluginConfiguration.StoreId.ToString());
            if (storeRecarga == null)
                throw new Exception($"Loja não localizada pelo Id: {_pluginConfiguration.StoreId}");


            List<ItemPedido> itemsPed = new();

            var calcularTaxaServicoReq = new TaxaPagamentoRequest
            {
                LojaId = _pluginConfiguration.StoreId
                ,
                TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria
                ,
                Total = preOrder.TotalValueAsDecimal
            };

            var retCalculoTaxaServico = await _recargaPlusApi.Client.CalcularTaxaPagamento(calcularTaxaServicoReq);

            if (!retCalculoTaxaServico.IsSuccessStatusCode)
                throw new Exception("Error CalcularTaxaPagamento: " + retCalculoTaxaServico.Error.Content);

            foreach (var item in preOrder.Items)
            {
                if (item?.Trip?.TotalConnections > 0)
                {
                    var itensGeradosConnection = preOrder.AsRecargaPlusItemConnectionsFromPreOrderItem(_pluginConfiguration);
                    itemsPed.AddRange(itensGeradosConnection);
                }
                else
                {
                    itemsPed.AddRange(item.Trip.Seats
                            .Select(x => x.AsRecargaPlusItemFromPreOrderItem(item, preOrder))
                        );
                }
            }

            Console.WriteLine("Recarga - CreatePreOrder - após TotalConnections");
            //Tratamento para as descrições nulas ou vazias
            itemsPed?.Select(rpi =>
            {
                var listaDescricaoValidas = rpi.Descricoes
                    .Where(desc => !string.IsNullOrEmpty(desc.Valor))
                    .ToList();
                rpi.Descricoes = listaDescricaoValidas;
                return rpi;
            })?.ToList();

            /*Console.WriteLine("Recarga - CreatePreOrder - após Pedido item");
            int? idCliente = null;
            if (clientRecargaPlus != null)
            {
                idCliente = (int)clientRecargaPlus.Content.Id;
            }

            Console.WriteLine("Recarga - CreatePreOrder - após idCliente");*/
            int? clienteId = (clientRecargaPlus?.Content?.Id != null && clientRecargaPlus?.Content?.Id != 0) ? (int)(clientRecargaPlus?.Content?.Id) : null;

            var pedidoRequest = new PedidoRequest()
            {
                ClienteId = clienteId,
                LojaId = (int)_pluginConfiguration.StoreId,
                TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria,
                Uuid = null,
                ValorTotal = preOrder.TotalValueAsDecimal + preOrder.ValuePartnerFeeAsDecimal,
                Itens = itemsPed,
                ParceiroId = preOrder.ParceiroId,
            };

            Console.WriteLine("Recarga - CreatePreOrder - após pedidoRequest");
            var retorno = await _recargaPlusApi.Client.CreatePedidoGDS(pedidoRequest);
            if (!retorno.IsSuccessStatusCode)
                throw new Exception($"Error CreatePreOrder: {retorno.Error}");

            Console.WriteLine("Recarga - CreatePreOrder - após CreatePedidoGDS");
            return retorno.Content.AsGdsPreOrder(preOrder, _pluginConfiguration, clientRecargaPlus?.Content, storeRecarga.Content, retCalculoTaxaServico.Content.ToList());
        }
        catch (Exception e)
        {
            throw new Exception($"Error CreatePreOrder: {e.Message}");
        }
    }

    public async Task<PreOrder> AddPreOrderItems(AddPreOrderItemsDto preOrderItems)
    {
        //ValidadorPreOrder.Validar(preOrderItems);

        var pedidoRecargaPlus = await _recargaPlusApi.Client.GetPedido(preOrderItems.PreOrderId);

        if (!pedidoRecargaPlus.IsSuccessStatusCode)
            throw new Exception(pedidoRecargaPlus.Error.Content);

        if (pedidoRecargaPlus == null)
            throw new Exception($"Pedido não localizado pelo ID: {preOrderItems.PreOrderId}");

        var itemsPed = new List<ItemPedido>();

        var preOrder = new PreOrder();

        preOrder.Channel = preOrderItems.Channel;
        preOrder.Items = new List<PreOrderItem>();

        var partnerFee = pedidoRecargaPlus.Content.Itens
                .FirstOrDefault(x => x.Nome.Equals("IDA"))
                    .GetItemPedidoAsDecimal("TAXA_PARCEIRO");

        var valuePartnerFee = pedidoRecargaPlus.Content.Itens
            .FirstOrDefault(x => x.Nome.Equals("IDA"))
                .GetItemPedidoAsDecimal("VALOR_TAXA_PARCEIRO");

        preOrder.PartnerFee = partnerFee;
        preOrder.ValuePartnerFee = Convert.ToInt64(valuePartnerFee * 100); ;

        preOrder.Items = preOrderItems.Items;
        preOrder.Value = preOrderItems.Items.Sum(x => x.Value);

        preOrder.TotalValue = preOrderItems.Items.Sum(x => x.TotalValue)
            - preOrder.ValuePartnerFee;



        var calcularTaxaServicoReq = new TaxaPagamentoRequest
        {
            LojaId = _pluginConfiguration.StoreId
                ,
            TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria
                ,
            Total = preOrder.TotalValueAsDecimal
        };

        var retCalculoTaxaServico = await _recargaPlusApi.Client.CalcularTaxaPagamento(calcularTaxaServicoReq);

        if (!retCalculoTaxaServico.IsSuccessStatusCode)
            throw new Exception("Error CalcularTaxaPagamento: " + retCalculoTaxaServico.Error.Content);


        foreach (var item in preOrderItems.Items)
        {
            if (item?.Trip?.TotalConnections > 0)
            {
                //var itensGeradosConnection = preOrder.AsRecargaPlusItemConnectionsFromPreOrderItem(_pluginConfiguration);
                //itemsPed.AddRange(itensGeradosConnection);
            }
            else
            {
                itemsPed.AddRange(item.Trip.Seats
                        .Select(x => x.AsRecargaPlusItemFromPreOrderItem(item, preOrder))
                    );
            }
        }

        itemsPed?.Select(rpi =>
        {
            var listaDescricaoValidas = rpi.Descricoes
                .Where(desc => !string.IsNullOrEmpty(desc.Valor))
                .ToList();
            rpi.Descricoes = listaDescricaoValidas;
            return rpi;
        })?.ToList();


        preOrder.TotalValue = Convert.ToInt64(
                preOrder.TotalValue
                + preOrder.ValueServiceTax
                + preOrder.ValuePartnerFee);

        var valorAtualizado = preOrderItems.Items.Sum(x => x.TotalValueAsDecimal);
        var valorAntigo = pedidoRecargaPlus.Content.ValorTotal.Value - valuePartnerFee;

        valorAtualizado += valorAntigo;
        valorAtualizado = valorAtualizado + (valorAtualizado * (partnerFee / 100));

        var pedidoRequest = new PedidoRequest()
        {
            PedidoId = Convert.ToInt32(preOrderItems.PreOrderId),
            ClienteId = null,
            LojaId = (int)_pluginConfiguration.StoreId,
            TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria,
            Uuid = null,
            ValorTotal = valorAtualizado,
            Itens = itemsPed
        };


        var response = await _recargaPlusApi.Client.CreatePedidoGDS(pedidoRequest);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error AddPreOrderItems: {response.Error}");

        Console.WriteLine("Recarga - AddPreOrderItems - após CreatePedidoGDS");

        var pedidoAtualizadoRes = await _recargaPlusApi.Client.GetPedido(preOrderItems.PreOrderId);

        if (!pedidoAtualizadoRes.IsSuccessStatusCode)
            throw new Exception(pedidoRecargaPlus.Error.Content);


        var retorno = pedidoAtualizadoRes.Content.AsGdsPreOrderFromPedidoResponse(null, retCalculoTaxaServico.Content.ToList());


        return retorno;
    }

    public async Task<Order> CreateOrderByPreOrder(PreOrder preOrder)
    {
        try
        {
            ValidadorPreOrder.Validar(preOrder);

            ValidadorDadosSuspeitoParaOCliente(preOrder);

            ValidadorDadosSuspeitoParaOsPassageiros(preOrder.Items);

            var pedidoRecargaPlus = await _recargaPlusApi.Client.GetPedido(preOrder.Id);

            if (!pedidoRecargaPlus.IsSuccessStatusCode)
                throw new Exception(pedidoRecargaPlus.Error.Content);

            if (preOrder.Store is null)
                preOrder.Store = new Store()
                {
                    Id = _pluginConfiguration.StoreId.ToString()
                };
            else
                preOrder.StoreId = preOrder.Store.Id;

            if (pedidoRecargaPlus == null)
                throw new Exception($"Pedido não localizado pelo ID: {preOrder.Id}");

            if (preOrder.Store == null || string.IsNullOrEmpty(preOrder.Store.Id))
                throw new Exception($"Id da Loja não informado.");

            var storeFound = await _recargaPlusApi.Client.GetLoja(preOrder.Store.Id);

            if (!storeFound.IsSuccessStatusCode)
                throw new Exception(storeFound.Error.Content);

            if (storeFound == null)
                throw new Exception($"Loja não localizada pelo ID: {preOrder.Store.Id}");


            var clientRecargaPlus = await _recargaPlusApi.Client.GetClientById(
                Convert.ToInt64(preOrder.Client.Id)
                );

            if (!clientRecargaPlus.IsSuccessStatusCode)
                throw new Exception(clientRecargaPlus.Error.Content);

            if (clientRecargaPlus == null)
                throw new Exception($"Cliente não localizado pelo ID: {preOrder.Client.Id}");

            if (clientRecargaPlus.Content.Ativo.ToUpper() == "B") //cliente bloqueado na base - possível fraude
            {
                throw new Exception("Pedido não aprovado - Cliente bloqueado");
            }

            var itemsPed = pedidoRecargaPlus.Content.Itens;

            var calcularTaxaServicoReq = new TaxaPagamentoRequest
            {
                LojaId = _pluginConfiguration.StoreId,
                TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria,
                Total = preOrder.TotalValueAsDecimal
            };

            var retCalculoTaxaServico = await _recargaPlusApi.Client.CalcularTaxaPagamento(calcularTaxaServicoReq);

            if (!retCalculoTaxaServico.IsSuccessStatusCode)
                throw new Exception("Error CalcularTaxaPagamento: " + retCalculoTaxaServico.Error.Content);

            var paymentTax = retCalculoTaxaServico.Content.FirstOrDefault(
                x => x.Nome.AsPaymentMode() == preOrder.Payment.PaymentMechanism
                );

            if (paymentTax == null)
                throw new Exception("Erro ao buscar taxa de pagamento");

            var newOrder = new Order
            {
                Value = preOrder.Value,
                ValueServiceTax = Convert.ToInt64(paymentTax.Valor * 100)
            };

            var partnerFee = pedidoRecargaPlus.Content.Itens
                .FirstOrDefault(x => x.Nome.Equals("IDA"))
                    .GetItemPedidoAsDecimal("TAXA_PARCEIRO");

            var valuePartnerFee = pedidoRecargaPlus.Content.Itens
                .FirstOrDefault(x => x.Nome.Equals("IDA"))
                    .GetItemPedidoAsDecimal("VALOR_TAXA_PARCEIRO");

            newOrder.PartnerFee = partnerFee;

            var novaTaxa = (newOrder.Value / 100) * (partnerFee / 100);
            newOrder.ValuePartnerFee = (novaTaxa * 100).AsLongFromDecimal();


            newOrder.TotalValue = Convert.ToInt64(
                preOrder.TotalValue
                + newOrder.ValueServiceTax
                + newOrder.ValuePartnerFee);

            newOrder.Id = pedidoRecargaPlus.Content.PedidoId.ToString();
            newOrder.Client = clientRecargaPlus.Content.AsGdsClient();
            newOrder.ExtraData = preOrder.ExtraData;
            newOrder.Payment = preOrder.Payment;

            var payment = await _paymentService.GenerateUrlPayment(newOrder);

            newOrder.Payment = payment;

            ///TODO: CRIAR IDA/VOLTA TAXA

            var pedidoItens = pedidoRecargaPlus.Content.Itens.Select(item => new ItemPedido(item)).ToList();

            itemsPed = new List<ItemPedido>();

            foreach (var item in preOrder.Items)
            {
                if (item?.Trip?.TotalConnections > 0)
                {
                    //var itensGeradosConnection = preOrder.AsRecargaPlusItemFromPreOrderItem(item, preOrder);
                    //itemsPed.AddRange(itensGeradosConnection);
                }
                else
                {
                    foreach (var seat in item.Trip.Seats)
                    {
                        var itemTravelDirection = Enum.GetName(typeof(TravelDirectionEnum), item.TravelDirectionType);

                        var pedItem = pedidoItens.FirstOrDefault(x =>
                            x.Nome == itemTravelDirection
                            && x.Descricoes.FirstOrDefault(x => x.Chave == "POLTRONA").Valor == seat.Number);

                        var pedDescricoes = seat
                            .AsRecargaPlusItemFromPreOrderItemToOrder(item, preOrder, newOrder)
                            .Where(x => !String.IsNullOrEmpty(x.Valor))
                            .ToList();

                        var ret = await _recargaPlusApi.Client.AddItemDescription(pedItem.Id.ToString(), pedDescricoes);

                        if (!ret.IsSuccessStatusCode)
                            throw new Exception(ret.Error.Content);
                    }
                }

                itemsPed.Add(item.AsRecargaPlusItemPedidoTaxa(paymentTax.Valor));
                itemsPed.Add(newOrder.AsRecargaPlusItemPedidoPagamento());
            }

            itemsPed?.Select(rpi =>
            {
                var listaDescricaoValidas = rpi.Descricoes
                    .Where(desc => !string.IsNullOrEmpty(desc.Valor))
                    .ToList();
                rpi.Descricoes = listaDescricaoValidas;
                return rpi;
            })?.ToList();

            var pedidoRequest = new PedidoRequest()
            {
                PedidoId = Convert.ToInt32(preOrder.Id),
                ClienteId = (int)clientRecargaPlus.Content.Id,
                LojaId = (int)_pluginConfiguration.StoreId,
                TipoPedidoId = TipoPedidoEnum.CompraPassagemRodoviaria,
                ValorTotal = newOrder.TotalValueAsDecimal,
                Itens = itemsPed,
                VendedorId = 1
            };

            var retorno = await _recargaPlusApi.Client.CreatePedidoGDS(pedidoRequest);
            if (!retorno.IsSuccessStatusCode)
                throw new Exception($"Error Refit: {retorno.Error}");


            /* var updatePedidoClient = await _recargaPlusApi.Client.AssociarClienteAoPedido(preOrder.Id, clientRecargaPlus.Content.Id);
             Console.WriteLine($"updatePedidoClient.IsSuccessStatusCode {(updatePedidoClient.IsSuccessStatusCode)}");

             if (!updatePedidoClient.IsSuccessStatusCode)
                 throw new Exception($"Erro ao tentar associar cliente ao pedido: {updatePedidoClient.Error.Content}");

             */
            int paymentId = 5; //Não informado/outros

            if (preOrder.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_FULL
                    || preOrder.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_INSTALLMENTS)
            {
                paymentId = 2;
            }
            else if (preOrder.Payment.PaymentMechanism == PaymentModeEnum.PIX)
            {
                paymentId = 4;
            }

            var updateFormaPagamento = await _recargaPlusApi.Client.UpdatePedido(Convert.ToInt64(preOrder.Id), paymentId, clientRecargaPlus.Content.Id);
            Console.WriteLine($"updateFormaPagamento.IsSuccessStatusCode {(updateFormaPagamento.IsSuccessStatusCode)}");

            if (!updateFormaPagamento.IsSuccessStatusCode)
                throw new Exception($"Erro ao tentar associar cliente e forma de pagamento ao pedido: {updateFormaPagamento.Error.Content}");

            var statusPagamento = (int)StatusPedido.AGUARDANDO_PAGAMENTO;

            var retornoStatusPedido = await _recargaPlusApi.Client.UpdateStatusPedidoGDS(
                pedidoRequest.PedidoId, statusPagamento
            );

            if (!retornoStatusPedido.IsSuccessStatusCode)
                throw new Exception($"Error Atualizar status pedido: {retorno.Error}");

            return
                pedidoRecargaPlus.Content
                    .AsGdsOrder(preOrder
                        , _pluginConfiguration
                        , newOrder
                        , storeFound.Content
                        , clientRecargaPlus.Content);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error CreatePedido: {e.Message} - StackTrace:{e.StackTrace}");
            throw new Exception($"Error CreatePedido: {e.Message}");
        }
    }

    private void ValidadorDadosSuspeitoParaOCliente(PreOrder preOrder)
    {
        var cliente = preOrder.Client;
        if (cliente != null)
        {
            if (!string.IsNullOrEmpty(cliente.FullName))
            {
                Console.WriteLine($"----------------ValidadorDadosSuspeitoParaOCliente - Nome: {cliente.FullName}---------------------------");
                var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(cliente.FullName).Result;
                if (resultNomeClientFraude.Content != null)
                {
                    if (resultNomeClientFraude.Content.Status)
                        throw new Exception($"Cliente suspeito de fraude no nome.");
                }
            }

            if (cliente.Documents != null)
            {
                foreach (var documento in cliente?.Documents)
                {
                    if (!string.IsNullOrEmpty(documento.Value))
                    {
                        var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(documento.Value.Replace("/", "").Replace("-", "").Replace(".", "")).Result;
                        if (resultNomeClientFraude.Content != null)
                        {
                            if (resultNomeClientFraude.Content.Status)
                                throw new Exception($"Cliente suspeito de fraude no documento.");
                        }
                    }
                }
            }

            if (cliente.Contacts != null)
            {
                foreach (var contato in cliente?.Contacts)
                {
                    if (!string.IsNullOrEmpty(contato.Value))
                    {
                        var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(contato.Value.Replace("/", "").Replace("-", "").Replace(".", "")).Result;
                        if (resultNomeClientFraude.Content != null)
                        {
                            if (resultNomeClientFraude.Content.Status)
                                throw new Exception($"Cliente suspeito de fraude no contato.");
                        }
                    }
                }
            }
        }
    }

    //Validação de fraude dos passageiros
    private void ValidadorDadosSuspeitoParaOsPassageiros(ICollection<PreOrderItem> preOrderItens)
    {
        foreach (var item in preOrderItens)
        {
            foreach (var item2 in item.Trip.Seats)
            {
                var cliente = item2.Client;
                if (cliente != null)
                {
                    if (!string.IsNullOrEmpty(cliente.FullName))
                    {
                        var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(cliente.FullName).Result;
                        if (resultNomeClientFraude.Content != null)
                        {
                            if (resultNomeClientFraude.Content.Status)
                                throw new Exception($"Passageiro suspeito de fraude no nome.");
                        }
                    }

                    if (cliente.Documents != null)
                    {
                        foreach (var documento in cliente?.Documents)
                        {
                            if (!string.IsNullOrEmpty(documento.Value))
                            {
                                var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(documento.Value.Replace("/", "").Replace("-", "").Replace(".", "")).Result;
                                if (resultNomeClientFraude.Content != null)
                                {
                                    if (resultNomeClientFraude.Content.Status)
                                        throw new Exception($"Passageiro suspeito de fraude no documento.");
                                }
                            }
                        }
                    }

                    if (cliente.Contacts != null)
                    {
                        foreach (var contato in cliente?.Contacts)
                        {
                            if (!string.IsNullOrEmpty(contato.Value))
                            {
                                var resultNomeClientFraude = _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(contato.Value.Replace("/", "").Replace("-", "").Replace(".", "")).Result;
                                if (resultNomeClientFraude.Content != null)
                                {
                                    if (resultNomeClientFraude.Content.Status)
                                        throw new Exception($"Passageiro suspeito de fraude no contato.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public async Task<Order> UpdateOrder(Order Order)
    {
        try
        {
            ValidadorPayment.Validar(Order.Payment);

            ValidadorClient.Validar(Order.Client);

            var resposta = await this._recargaPlusApi.Client.UpdatePedido(Convert.ToInt64(Order.Id), (int)Order.Payment.PaymentMechanism, long.Parse(Order.Client.Id));
            if (resposta.IsSuccessStatusCode)
                return new Order();

            throw new Exception($"Error UpdateStatusPedido: {resposta.Error.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Error UpdateStatusPedido: {e.Message}");
        }
    }

    public async Task<bool> UpdateStatusPedido(int pedidoId, StatusPedido novoStatus)
    {
        try
        {
            if (pedidoId <= 0)
                throw new Exception($"Informe um pedido maior que zero.");

            int valorStatus = (int)novoStatus;

            var result = await this._recargaPlusApi.Client.UpdateStatusPedido(pedidoId, valorStatus);
            return result.Content;
        }
        catch (Exception e)
        {
            throw new Exception($"Error UpdateStatusPedido: {e.Message}");
        }

    }

    public async Task<Order> DeleteOrder(Order order)
    {

        var pedidoRecargaPlus = await _recargaPlusApi.Client.GetPedido(order.Id);

        if (!pedidoRecargaPlus.IsSuccessStatusCode)
            throw new Exception(pedidoRecargaPlus.Error.Content);

        var pedidoItens = pedidoRecargaPlus.Content.Itens.Select(item => new ItemPedido(item)).ToList();

        foreach (var item in order.Items)
        {
            foreach (var seat in item.Trip.Seats)
            {
                var itemTravelDirection = Enum.GetName(typeof(TravelDirectionEnum), item.TravelDirectionType);

                var pedItem = pedidoItens.FirstOrDefault(x =>
                            x.Nome == itemTravelDirection
                            && x.Descricoes.FirstOrDefault(x => x.Chave == "POLTRONA").Valor == seat.Number);

                var pedDescricoes = seat
                            .AsRecargaPlusItemCancelationFromOrderItem()
                            .Where(x => !String.IsNullOrEmpty(x.Valor))
                            .ToList();

                var ret = await _recargaPlusApi.Client.AddItemDescription(pedItem.Id.ToString(), pedDescricoes);

                if (!ret.IsSuccessStatusCode)
                    throw new Exception(ret.Error.Content);
            }
        }

        var status = (int)StatusPedido.CANCELADO;

        var retornoStatusPedido = await _recargaPlusApi.Client.UpdateStatusPedidoGDS(
            pedidoRecargaPlus.Content.PedidoId, status
        );

        if (!retornoStatusPedido.IsSuccessStatusCode)
            throw new Exception($"Error Atualizar status pedido: {retornoStatusPedido.Error}");

        EnvioEmailCancelamento.EnviarEmailNotificandoCancelamento(order, order.Items, _authenticationData);
        return order;
    }

    public Dictionary<PaymentMethod, string> GetPaymentsMethods(int page = 1, int itemsPerPage = 15)
    {
        Dictionary<PaymentMethod, string> paymentMethodNames = new Dictionary<PaymentMethod, string>()
        {
            { PaymentMethod.Undefined, "não definido" },
            { PaymentMethod.Offline, "por ex. pagamento na loja, pessoalmente" },
            { PaymentMethod.Link, "link gerado por alguma api e enviado para o cliente via bot" },
            { PaymentMethod.Code, "um código gerado para ser aplicado em algum aplicativo, ex: picpay" }
        };

        return paymentMethodNames;
    }

    public async Task<bool> AddItemDescription(string itemId, List<Descricao> listaDescricao)
    {
        var retorno = await this._recargaPlusApi.Client.AddItemDescription(itemId, listaDescricao);
        return retorno.Content;
    }

    public async Task<Order> GetOrderById(string id)
    {
        try
        {
            var retorno = await this._recargaPlusApi.Client.GetPedido(id);

            if (retorno.StatusCode == System.Net.HttpStatusCode.NoContent)
                return null;

            if (!retorno.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar pedido recarga: " + retorno.Error.Content);

            var pedido = retorno.Content.AsGdsOrder();

            ApiResponse<ClientResponse> clientResponse = null;
            if (retorno.Content.ClienteId != null)
            {
                long IdCliente = (long)(retorno?.Content?.ClienteId.Value ?? 0);
                clientResponse = await this._recargaPlusApi.Client.GetClientById(IdCliente);
            }

            return retorno.Content.AsGdsOrderFromPedidoResponse(clientResponse?.Content ?? null);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<PreOrder> GetPreOrderById(string id)
    {
        try
        {
            var retorno = await this._recargaPlusApi.Client.GetPedido(id);
            var client = new ClientResponse();

            if (retorno.StatusCode == System.Net.HttpStatusCode.NoContent)
                return null;

            var pedidoFound = retorno.Content;

            if (pedidoFound is null)
                return null;

            if (pedidoFound.ClienteId > 0)
            {
                var responseClient = await _recargaPlusApi.Client.GetClientById(Convert.ToInt64(pedidoFound.ClienteId));

                if (!responseClient.IsSuccessStatusCode)
                    throw new Exception($"Error GetClient {responseClient.Error.Content}");

                client = responseClient.Content;
            }

            return pedidoFound.AsGdsPreOrderFromPedidoResponse(client);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<PreOrder> RemovePreOrderItems(PreOrder preOrder)
    {
        ValidadorPreOrder.Validar(preOrder); ;

        foreach (var seat in preOrder.Items)
        {
            var retorno = await this._recargaPlusApi.Client.RemoveItemPedido(seat.Id, preOrder.Id, preOrder.Client.Id);
        }

        return preOrder;
    }

    public async Task<PreOrder> DeletePreOrder(PreOrder preOrder)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultOperation> GetOrderByIdByCpfClient(string orderId, string cpf)
    {
        try
        {
            var retorno = await _recargaPlusApi.Client.GetPedido(orderId);

            if (retorno.StatusCode == System.Net.HttpStatusCode.NoContent)
                return ResultOperation.CriarFalha("Pedido não encontrado no sistema!");

            var pedido = retorno.Content;

            if (pedido.ClienteId is null || pedido?.ClienteId == 0)
                return ResultOperation.CriarFalha($"O pedido {pedido.ClienteId} ainda não possui cliente!");


            var clienteEncontrado = await this._recargaPlusApi.Client.GetClientById(pedido.ClienteId.Value);

            if (clienteEncontrado != null)
            {
                var cpfSemSimbolos = cpf?.Replace(".", "")?.Replace("-", "") ?? cpf;
                if (clienteEncontrado.Content.Cpf == cpfSemSimbolos)
                {
                    return ResultOperation.CriarSucesso($"O cliente {cpf} está relacionado ao pedido em questão!");
                }
            }

            return ResultOperation.CriarFalha("Não foi possível encontrar o cliente!");
        }
        catch (System.Exception ex)
        {
            return ResultOperation.CriarFalha(ex.Message);
        }
    }

    public async Task<ResultOperation> ValidaDadosSuspeitoPorCpf(string cpf)
    {
        try
        {
            var retorno = await _recargaPlusApi.Client.ValidaDadosSuspeitoBlacklist(cpf.Replace("/", "").Replace("-", "").Replace(".", ""));

            var validaSuspeitoRetorno = retorno.Content;
            if (validaSuspeitoRetorno.Status)
                return ResultOperation.CriarFalha($"O cliente do cpf: {cpf} é suspeito de fraude");
            else
                return ResultOperation.CriarFalha($"O cliente do cpf: {cpf} não é suspeito de fraude");
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}