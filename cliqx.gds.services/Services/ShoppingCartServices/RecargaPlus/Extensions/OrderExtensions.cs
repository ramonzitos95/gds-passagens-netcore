
using cliqx.gds.contract.Enums;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Util;
using System.Drawing;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions
{
    public static class OrderExtensions
    {

        public static Order AsGdsOrderFromPedidoResponse(this PedidoResponse ped
       , ClientResponse client = null
       , List<TaxaPagamentoResponse> txPagamentoRes = null)
        {
            var order = new Order();
            order.Items = new List<OrderItem>();
            var listItems = new List<OrderItem>();
            var formaPagamento = string.Empty;
            DateTime? dataLimitePagamento = null;

            order.StatusPedidoId = ped.StatusPedidoId;
            order.Status = ped.StatusPedidoId.ToString();
            var urlPagamento = "";
            decimal valorTaxaParceiro = 0;
            decimal taxaParceiro = 0;

            foreach (var item in ped.Itens)
            {

                if (item.Nome == "PAGAMENTO")
                {

                    formaPagamento = item.GetItemPedidoAsString("METODO_PAGAMENTO");
                    dataLimitePagamento = item.GetItemPedidoAsDate("LIMITE_PAGAMENTO");

                    urlPagamento = item.GetItemPedidoAsString("CHAVE_COPIAR_COLAR");

                    if (String.IsNullOrEmpty(urlPagamento))
                        urlPagamento = item.GetItemPedidoAsString("LINK");

                }
                if (
                    item.Nome != "IDA" && item.Nome != "VOLTA"
                    && item.Nome != "IDA_CONEXAO" && item.Nome != "VOLTA_CONEXAO"
                )
                {
                    continue;
                }

                var corridaId = item.GetItemPedidoAsString("SERVICO_ID");
                var dataSaida = item.GetItemPedidoAsDate("ORIGEM_DATA_SAIDA");
                var dataChegada = item.GetItemPedidoAsDate("DESTINO_DATA_CHEGADA");
                var poltrona = item.GetItemPedidoAsString("POLTRONA");

                valorTaxaParceiro = item.GetItemPedidoAsDecimal("VALOR_TAXA_PARCEIRO");
                taxaParceiro = item.GetItemPedidoAsDecimal("TAXA_PARCEIRO");

                var itemKey = corridaId + poltrona;

                if (!order.Items.Any(x => x.Trip.Id == corridaId))
                {
                    var poi = new OrderItem
                    {
                        TravelDirectionType = item.Nome == "IDA"
                            ? TravelDirectionEnum.IDA
                            : item.Nome == "VOLTA"
                                ? TravelDirectionEnum.VOLTA
                            : TravelDirectionEnum.IDA_CONEXAO,

                        Trip = new Trip
                        {
                            Id = item.GetItemPedidoAsString("SERVICO_ID")
                                ,
                            Origin = new TripOriginDestination
                            {
                                CityId = item.GetItemPedidoAsString("ORIGEM_ID")
                                        ,
                                CityName = item.GetItemPedidoAsString("ORIGEM_CIDADE"),
                                LetterCode = item.GetItemPedidoAsString("ORIGEM_ESTADO")
                            }
                                ,
                            Destination = new TripOriginDestination
                            {
                                CityId = item.GetItemPedidoAsString("DESTINO_ID")
                                        ,
                                CityName = item.GetItemPedidoAsString("DESTINO_CIDADE"),
                                LetterCode = item.GetItemPedidoAsString("DESTINO_ESTADO")
                            }
                                ,
                            Class = new TripClass
                            {
                                Id = item.GetItemPedidoAsString("CLASSE_ID")
                                        ,
                                Name = item.GetItemPedidoAsString("CLASSE_NOME")
                            }
                                ,
                            Company = new Company
                            {
                                Id = item.GetItemPedidoAsString("EMPRESA_ID")
                                        ,
                                Name = item.GetItemPedidoAsString("EMPRESA_NOME")
                            }
                                ,
                            DepartureTime = dataSaida
                                ,
                            ArrivalTime = dataChegada
                                ,
                            DepartureTax = item.GetItemPedidoAsDecimal("TAXA_EMBARQUE")
                                ,
                            Value = Convert.ToInt64(item.ValorUnitario * 100)
                        }
                        ,
                        DepartureTax = item.GetItemPedidoAsDecimal("TAXA_EMBARQUE")
                        ,
                        PedagogueValue = item.GetItemPedidoAsDecimal("TAXA_PEDAGIO")
                        ,
                        TotalValue = Convert.ToInt64(item.ValorUnitario * 100)
                        ,
                        Value = Convert.ToInt64(item.ValorUnitario * 100)

                    };

                    poi.Trip.Seats = new List<Seat>();

                    var seat = new Seat
                    {
                        Number = item.GetItemPedidoAsString("POLTRONA"),
                        Id = item.GetItemPedidoAsString("POLTRONA_ID"),
                        Class = item.GetItemPedidoAsString("CLASSE_POLTRONA_ID"),
                        Transaction = new Transaction
                        {
                            TransactionId = item.GetItemPedidoAsString("TRANSACAO_ID"),
                            ReservationId = item.GetItemPedidoAsString("RESERVA_ID"),
                            LocatorId = item.GetItemPedidoAsString("LOCALIZADOR"),
                            BookingNumber = item.GetItemPedidoAsString("BOOKING_NUMBER"),
                            ReservationExpiresAt = item.GetItemPedidoAsDate("DATA_EXP_RESERVA"),
                            ReservationTotalPrice = item.GetItemPedidoAsLong("VALOR_RESERVA")
                        },
                        Value = item.GetItemPedidoAsLong("VALOR_RESERVA"),
                        Client = new contract.Models.Client
                        {
                            FullName = item.GetItemPedidoAsString("PASSAGEIRO_NOME"),
                            Documents = new List<Document>()
                            {
                                new Document
                                {
                                    DocumentType = DocumentTypeEnum.RG
                                    ,Value = item.GetItemPedidoAsString("PASSAGEIRO_DOCUMENTO")
                                }
                            }
                        },
                        Ticket = new Ticket
                        {
                            UrlPDF = item.GetItemPedidoAsString("TICKET_ENVIADO")
                        }
                    };

                    poi.Trip.Seats.Add(seat);

                    order.Items.Add(poi);
                }
                else
                {
                    var trip = order.Items.FirstOrDefault(x => x.Trip.Id == corridaId);
                    var poiIndex = order.Items.FindIndex(x => x.Trip.Id == trip.Trip.Id);

                    var seat = new Seat
                    {
                        Number = item.GetItemPedidoAsString("POLTRONA")
                        ,
                        Id = item.GetItemPedidoAsString("POLTRONA_ID")
                        ,
                        Class = item.GetItemPedidoAsString("CLASSE_POLTRONA_ID")
                        ,
                        Transaction = new Transaction
                        {
                            TransactionId = item.GetItemPedidoAsString("TRANSACAO_ID"),
                            ReservationId = item.GetItemPedidoAsString("RESERVA_ID"),
                            LocatorId = item.GetItemPedidoAsString("LOCALIZADOR"),
                            BookingNumber = item.GetItemPedidoAsString("BOOKING_NUMBER"),
                            ReservationExpiresAt = item.GetItemPedidoAsDate("DATA_EXP_RESERVA"),
                            ReservationTotalPrice = item.GetItemPedidoAsLong("VALOR_RESERVA")
                        }
                        ,
                        Value = item.GetItemPedidoAsLong("VALOR_RESERVA")
                        ,
                        Ticket = new Ticket
                        {
                            UrlPDF = item.GetItemPedidoAsString("TICKET_ENVIADO")
                        }
                    };

                    order.Items[poiIndex].Trip.Seats.Add(seat);

                    order.Items[poiIndex].TotalValue = Convert.ToInt64(item.ValorUnitario * 100);
                    order.Items[poiIndex].Value = Convert.ToInt64(item.ValorUnitario * 100);

                }



            }

            order.Items.ForEach(x => x.Id = ped.PedidoId.ToString());
            order.Items.ForEach(x => x.ExpirationDate = (DateTime)order.Items.Min(x => x.Trip.Seats.Min(s => s.Transaction.ReservationExpiresAt)));
            order.IssuedAt = (DateTime)ped.DataCadastro;
            order.ExpirationDate = order.Items.Min(x => x.ExpirationDate.Value);
            order.Id = ped.PedidoId.ToString();

            order.Channel = ped.Itens[0].GetItemPedidoAsString("CANAL_ORIGEM");
            order.HasInsurance = ped.Itens[0].GetItemPedidoAsBool("CONTRATOU_SEGURO");
            order.Payment = new Payment();
            order.Payment.Url = urlPagamento;

            if (dataLimitePagamento.HasValue)
            {
                order.Payment.ExpirationDate = dataLimitePagamento.Value;
            }

            if (txPagamentoRes is not null)
            {
                //order.Payment = new Payment();

                order.Payment.PaymentMechanism = (PaymentModeEnum)Enum.Parse(typeof(PaymentModeEnum), formaPagamento);
                order.Payment = order.Payment;
                order.Payment.PaymentsTax = new List<PaymentTax>();
                order.Payment.ExpirationDate = ped.Itens[0].GetItemPedidoAsDate("PAGAMENTO_EXP_DATA");
                order.Payment.Url = urlPagamento;

                foreach (var item in txPagamentoRes)
                {
                    var pTax = new PaymentTax
                    {
                        MechanismDescription = item.Nome,
                        Value = item.Valor,
                        PaymentMechanism = item.Nome.AsPaymentMode()
                    };

                    order.Payment.PaymentsTax.Add(pTax);
                }
            }

            order.ValuePartnerFee = Convert.ToInt64(valorTaxaParceiro * 100);
            order.PartnerFee = taxaParceiro;

            order.Value = order.Items.Sum(x => x.Trip.Seats.Sum(x => x.Value));
            order.TotalValue = order.Items.Sum(item =>
                item.Trip.Seats.Sum(seat => seat.Value) 
                + item.DepartureTax.AsLongFromDecimal()
                + item.PedagogueValue.AsLongFromDecimal()
                + order.ValuePartnerFee
                );
            return order;
        }

        public static List<ItemPedido> AsRecargaPlusItemConnectionsFromPreOrderItem(this PreOrder preOrder, PluginConfiguration plugin)
        {
            List<ItemPedido> itensPedidoConexoes = new();

            var descricoes = new List<Descricao>();
            // if (preOrder.Trip.TotalConnections > 0)
            // {
            //     foreach (var item in preOrder.Trip.Connections)
            //     {
            //         foreach (var seat in item.Seats)
            //         {
            //             var itemPedido = new ItemPedido()
            //             {
            //                 Nome = Enum.GetName(typeof(TravelDirectionEnum), seat.TravelDirectionType),
            //                 Quantidade = 1,
            //                 ValorUnitario = seat.Seat.ValueAsDecimal,
            //                 Descricoes = new List<Descricao>()
            //             };

            //             var descricoesConexaoSeats = new List<Descricao>()
            //             {
            //                 new Descricao
            //                 {
            //                     Chave = Enum.GetName(typeof(TravelDirectionEnum), seat.TravelDirectionType),
            //                     Valor = seat.TravelDirectionType.ToString(),
            //                     Titulo = "Direção da viagem (TravelDirectionType)"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_TRANSACTION_ID",
            //                     Valor = seat.Transaction.TransactionId.ToString(),
            //                     Titulo = "Conexão poltrona id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_TRANSACTION_EXPIRES_AT",
            //                     Valor = seat.Transaction.TransactionExpiresAt.ToString(),
            //                     Titulo = "Conexão Seat Transaction Expires At"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_TRANSACTION_LOCATOR_ID",
            //                     Valor = seat.Transaction.LocatorId.ToString(),
            //                     Titulo = "Conexão Seat locator Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_RESERVATION_ID",
            //                     Valor = seat.Transaction.ReservationId.ToString(),
            //                     Titulo = "Conexão Seat Reservation Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_TOTAL_PRICE",
            //                     Valor = seat.Transaction.ReservationTotalPrice.ToString(),
            //                     Titulo = "Conexão Seat Preço total"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_NUMBER",
            //                     Valor = seat.Seat.Number,
            //                     Titulo = "Conexão Seat número"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_TYPE",
            //                     Valor = seat.Seat.Type,
            //                     Titulo = "Conexão Seat type"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_CLASS",
            //                     Valor = seat.Seat.Class,
            //                     Titulo = "Conexão Seat Class"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_ARRIVAL_TAX",
            //                     Valor = seat.Seat.ArrivalTax.ToString(),
            //                     Titulo = "Conexão Seat ArrivalTax"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_DISCOUNT_VALUE",
            //                     Valor = seat.Seat.DiscountValue.ToString(),
            //                     Titulo = "Conexão Seat Discount"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_SEAT_VACANT",
            //                     Valor = seat.Seat.Vacant.ToString(),
            //                     Titulo = "Conexão Seat Vacant"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CANAL_ORIGEM",
            //                     Valor = "",
            //                     Exibir = "S",
            //                     Posicao = 0,
            //                     Titulo = "Canal origem"
            //                 }
            //             };

            //             itemPedido.Descricoes.AddRange(descricoesConexaoSeats);

            //             var descricoesConexaoTrip = new List<Descricao>
            //             {
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_COMPANY_NAME",
            //                     Valor = item?.Company?.Name,
            //                     Titulo = "Conexão Company Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CLASS_ID",
            //                     Valor = item?.Class?.Id?.ToString(),
            //                     Titulo = "Conexão Class Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CLASS_NAME",
            //                     Valor = item?.Class?.Name?.ToString(),
            //                     Titulo = "Conexão Class Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_ARRIVAL_TIME",
            //                     Valor = item?.ArrivalTime.ToString(),
            //                     Titulo = "Conexão Arrival Time"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_DEPARTURE_TIME",
            //                     Valor = item?.DepartureTime.ToString(),
            //                     Titulo = "Conexão Departure Time"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_ORIGIN_ID",
            //                     Valor = item?.OriginId.ToString(),
            //                     Titulo = "Conexão Origin Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_DESTINATION_ID",
            //                     Valor = item?.DestinationId.ToString(),
            //                     Titulo = "Conexão Destination Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CITY_ORIGIN_ID",
            //                     Valor = item?.Origin?.CityId?.ToString(),
            //                     Titulo = "Conexão City Origin Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CITY_ORIGIN_NAME",
            //                     Valor = item?.Origin?.CityName?.ToString(),
            //                     Titulo = "Conexão City Origin Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_STATION_ORIGIN_ID",
            //                     Valor = item ?.Origin ?.StationId ?.ToString(),
            //                     Titulo = "Conexão Station Origin Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_STATION_ORIGIN_NAME",
            //                     Valor = item?.Origin?.StationName?.ToString(),
            //                     Titulo = "Conexão Station Origin Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CITY_DESTINATION_ID",
            //                     Valor = item ?.Destination?.CityId?.ToString(),
            //                     Titulo = "Conexão City Destiny Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_CITY_DESTINATION_NAME",
            //                     Valor = item ?.Destination?.CityName ?.ToString(),
            //                     Titulo = "Conexão City Destiny Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_STATION_DESTINATION_ID",
            //                     Valor = item?.Destination?.StationId?.ToString(),
            //                     Titulo = "Conexão Station Destiny Id"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_STATION_DESTINATION_NAME",
            //                     Valor = item?.Destination?.StationName?.ToString(),
            //                     Titulo = "Conexão Station Destiny Name"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_IS_BPE",
            //                     Valor = item?.IsBpe.ToString(),
            //                     Titulo = "Conexão IsBpe"
            //                 },
            //                 new Descricao
            //                 {
            //                     Chave = "CONECTION_AVAILABLE_SEATS",
            //                     Valor = item?.AvailableSeats.ToString(),
            //                     Titulo = "Conexão Available Seats"
            //                 }
            //             };

            //             itemPedido.Descricoes.AddRange(descricoesConexaoTrip);

            //             if(seat.Client != null)
            //             {
            //                 var descricoesCliente = new List<Descricao>()
            //                 {
            //                     new Descricao
            //                     {
            //                         Chave = "PASSAGEIRO_NOME",
            //                         Valor = seat?.Client?.FullName,
            //                         Titulo = "Nome do passageiro"
            //                     },
            //                     new Descricao
            //                     {
            //                         Chave = "PASSAGEIRO_DOCUMENTO",
            //                         Valor = seat?.Client?.Documents?.FirstOrDefault(X => X.DocumentType == DocumentTypeEnum.CPF)?.Value ?? "SEM DOCUMENTO",
            //                         Titulo = "Documento do Passageiro"
            //                     },
            //                     new Descricao
            //                     {
            //                         Chave = "PASSAGEIRO_TIPO",
            //                         Valor = preOrder.Trip.PassengerType,
            //                         Titulo = "Tipo do passageiro"
            //                     },
            //                     new Descricao
            //                     {
            //                         Chave = "PASSAGEIRO_CELULAR",
            //                         Valor = seat?.Client?.Contacts?.FirstOrDefault(X => X.ContactType == ContactTypeEnum.CelularPessoal)?.Value ?? "SEM CELULAR",
            //                         Titulo = "Celular do passageiro"
            //                     },
            //                 };
            //             }

            //             //var descricoesTicket = seat.Ticket.AsListDescricoesByTicket();
            //             //itemPedido.Descricoes.AddRange(descricoesTicket);

            //             itensPedidoConexoes.Add(itemPedido);
            //         }
            //     }
            // }

            return itensPedidoConexoes;
        }

        public static Order AsGdsOrder(this PedidoResponse pr
        , PreOrder preOrder
        , PluginConfiguration plugin
        , Order retOrder
        , LojaResponse store
        , ClientResponse client)
        {
            var ret = new Order();

            var retItems = new List<OrderItem>();

            ret.Id = pr.PedidoId.ToString();
            ret.Payment = preOrder.Payment;
            if (preOrder?.Address != null)
            {
                ret.Address = new Address()
                {
                    City = preOrder?.Address?.City ?? string.Empty,
                    Complement = preOrder?.Address?.Complement ?? string.Empty,
                    Country = preOrder?.Address?.Country ?? string.Empty,
                    District = preOrder?.Address?.District ?? string.Empty,
                    ExternalId = preOrder.Address.ExternalId,
                    Street = preOrder?.Address?.Street ?? string.Empty,
                    IsDefault = preOrder.Address.IsDefault,
                    Number = preOrder?.Address?.Number ?? string.Empty,
                    ReferencePoint = preOrder?.Address?.ReferencePoint ?? string.Empty,
                    State = preOrder?.Address?.State ?? string.Empty,
                    ZipCode = preOrder?.Address?.ZipCode ?? string.Empty
                };
            }

            ret.Client = client.AsGdsClient();
            ret.Client.Contacts = preOrder.Client.Contacts;
            ret.Store = new Store
            {
                Id = store.Id.ToString(),
                Name = store.Nome
            };

            var payment = new Payment()
            {
                Description = retOrder.Payment.Description,
                Display = retOrder.Payment.Display,
                ExpirationDate = retOrder.Payment.ExpirationDate,
                ExternalId = retOrder.Payment.ExternalId,
                ExtraData = retOrder.Payment.ExtraData,
                Id = retOrder.Payment.Id,
                Instalments = retOrder.Payment.Instalments,
                Image = retOrder.Payment.Image,
                Instruction = retOrder.Payment.Instruction,
                Key = retOrder.Payment.Key,
                MaxInstalments = retOrder.Payment.MaxInstalments,
                Name = retOrder.Payment.Name,
                PaymentMechanism = retOrder.Payment.PaymentMechanism,
                PaymentsTax = retOrder.Payment.PaymentsTax,
                Url = retOrder.Payment.Url
            };

            ret.Payment = payment;

            ret.ExpirationDate = payment.ExpirationDate;



            ret.Items = new List<OrderItem>();
            var listItems = new List<OrderItem>();

            foreach (var item in preOrder.Items)
            {
                var orderItem = new OrderItem
                {
                    Description = item.Description,
                    DiscountValue = item.DiscountValue,
                    ExternalId = item.ExternalId,
                    ExtraData = item.ExtraData,
                    Display = item.Display,
                    Id = item.Id,
                    Key = item.Key,
                    PedagogueValue = item.PedagogueValue,
                    PluginId = item.PluginId,
                    TotalValue = item.TotalValue,
                    TravelDirectionType = item.TravelDirectionType,
                    Trip = item.Trip,
                    Value = item.Value,
                    DepartureTax = item.DepartureTax
                };

                listItems.Add(orderItem);

            }

            ret.Items = listItems;

            ret.IssuedAt = (DateTime)pr.DataCadastro;
            ret.Channel = preOrder.Channel;
            ret.HasInsurance = preOrder.HasInsurance;
            ret.InvoiceCpfCnpj = preOrder.InvoiceCpfCnpj;
            ret.InvoiceName = preOrder.InvoiceName;
            ret.Notes = preOrder.Notes;
            ret.Status = preOrder.Status;
            ret.DiscountCoupon = preOrder.DiscountCoupon;
            ret.MarketPlace = preOrder.MarketPlace;
            ret.ParceiroId = preOrder.ParceiroId;
            ret.TokenParceiro = preOrder.TokenParceiro;

            ret.TotalValue = retOrder.TotalValue;
            ret.Value = preOrder.Value;
            ret.ValueServiceTax = retOrder.ValueServiceTax;
            ret.DiscountValue = preOrder.DiscountValue;
            ret.ValuePartnerFee = retOrder.ValuePartnerFee;
            ret.PartnerFee = retOrder.PartnerFee;


            return ret;
        }

        public static Order AsGdsOrderFromPreOrder(this PreOrder pOrser, Order retOrder)
        {

            return retOrder;
        }

        public static Order AsGdsOrder(this PedidoResponse pr)
        {
            var ret = new Order();
            var retItems = new List<OrderItem>();

            // TODO: IMPLEMENTAR BUSCAR CLIENTE
            ret.Client = new contract.Models.Client();
            ret.Id = pr.PedidoId.ToString();
            ret.Payment = new Payment();

            // TODO: Verificar tempo de expiração
            ret.Payment.ExpirationDate = DateTime.Now.AddMinutes(15);

            ret.IssuedAt = (DateTime)pr.DataCadastro;

            if (ret?.Items != null)
            {
                foreach (var item in ret?.Items)
                {
                    var index = 0;
                    var prItem = pr.Itens[index];
                    item.TotalValue = (long)(prItem.ValorUnitario * 100);
                }
            }

            ret.TotalValue = (long)pr.ValorTotal.Value;


            return ret;
        }

        public static List<Descricao> AsListDescricoesByTicket(this Ticket ticket)
        {
            var retorno = new List<Descricao>();
            foreach (var prop in ticket.GetType().GetProperties())
            {
                if (prop?.GetValue(ticket) != null)
                {
                    retorno.Add(new Descricao()
                    {
                        Chave = prop.Name,
                        Valor = prop.GetValue(ticket).ToString()
                    });
                }
            }
            return retorno;
        }

        public static ItemPedido AsRecargaPlusItemPedidoTaxa(this PreOrderItem pItem, decimal value)
        {
            var taxaItem = new ItemPedido
            {
                Nome = Enum.GetName(typeof(TravelDirectionEnum), pItem.TravelDirectionType) + "_TAXA",
                ValorUnitario = value,
                Quantidade = 1,
                Descricoes = new List<Descricao>()
            };

            taxaItem.Descricoes.Add(new()
            {
                Chave = "ORIGEM",
                Valor = "PIX",
                Exibir = "S",
                Titulo = "Origem"
            });

            return taxaItem;
        }

        public static ItemPedido AsRecargaPlusItemPedidoPagamento(this Order retOrder)
        {
            var taxaItem = new ItemPedido
            {
                Nome = "PAGAMENTO",
                ValorUnitario = 0,
                Quantidade = 1,
                Descricoes = new List<Descricao>()
            };

            taxaItem.Descricoes.Add(new()
            {
                Chave = retOrder.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_FULL ||
                    retOrder.Payment.PaymentMechanism == PaymentModeEnum.CREDIT_CARD_IN_INSTALLMENTS
                    ? "LINK"
                    : "CHAVE_COPIAR_COLAR",

                Valor = retOrder.Payment.Url,
                Exibir = "S",
                Titulo = "Link de pagamento"
            });

            taxaItem.Descricoes.Add(new()
            {
                Chave = "CHAVE_REFERENCIA",
                Valor = retOrder.Payment.Id,
                Exibir = "S",
                Titulo = "Chave de referência"
            });

            taxaItem.Descricoes.Add(new()
            {
                Chave = "METODO_PAGAMENTO",
                Valor = Enum.GetName(typeof(PaymentModeEnum), retOrder.Payment.PaymentMechanism),
                Exibir = "S",
                Titulo = "Método de pagamento"
            });

            taxaItem.Descricoes.Add(new()
            {
                Chave = "LIMITE_PAGAMENTO",
                Valor = DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data limite para efetuar o pagamento"
            });

            return taxaItem;
        }

        public static List<Descricao> AsRecargaPlusItemFromPreOrderItemToOrder(this Seat seat, PreOrderItem pItem, PreOrder pOrder, Order newOrder)
        {

            var iPed = new List<Descricao>();

            /// TODO: REVISAR
            iPed.Add(new()
            {
                Chave = "VALOR_COM_TAXA",
                Valor = (pItem.TotalValueAsDecimal + newOrder.ValueServiceTaxAsDecimal).ToString(),
                Exibir = "S",
                Titulo = "Valor com taxa"
            });

            iPed.Add(new()
            {
                Chave = "VALOR_COM_TAXA_IDA",
                Valor = (pItem.TotalValueAsDecimal + newOrder.ValueServiceTaxAsDecimal).ToString(),
                Exibir = "S",   
                Titulo = "Valor com taxa"
            });

            /// TODO: REVISAR
            iPed.Add(new()
            {
                Chave = "VALOR_SEM_TAXA_" + Enum.GetName(typeof(TravelDirectionEnum), pItem.TravelDirectionType),
                Valor = pItem.TotalValueAsDecimal.ToString(),
                Exibir = "S",
                Titulo = "Valor sem taxa " + Enum.GetName(typeof(TravelDirectionEnum), pItem.TravelDirectionType)
            });

            iPed.Add(new Descricao()
            {
                Chave = "TRANSACAO_ID",
                Valor = seat.Transaction?.TransactionId,
                Exibir = "S",
                Titulo = "Transação"
            });

            iPed.Add(new()
            {
                Chave = "TRANSACAO_EXP_DATA",
                Valor = seat.Transaction?.TransactionExpiresAt.ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data expiração transação"
            });

            iPed.Add(new()
            {
                Chave = "PAGAMENTO_EXP_DATA",
                Valor = pOrder.Payment?.ExpirationDate.ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data expiração transação"
            });

            iPed.Add(new()
            {
                Chave = "PASSAGEIRO_NOME",
                Valor = seat.Client?.FullName,
                Exibir = "S",
                Titulo = "Nome passageiro"
            });

            iPed.Add(new()
            {
                Chave = "PASSAGEIRO_DOCUMENTO",
                Valor = seat.Client?.Documents.FirstOrDefault().Value,
                Exibir = "S",
                Titulo = "Documento passageiro"
            });

            iPed.Add(new()
            {
                Chave = "PASSAGEIRO_TIPO",
                Valor = "PNOS",
                Exibir = "N",
                Titulo = "Passageiro tipo"
            });

            return iPed;
        }

        public static List<Descricao> AsRecargaPlusItemCancelationFromOrderItem(this Seat seat)
        {

            var iPed = new List<Descricao>();

            iPed.Add(new()
            {
                Chave = "CANCELATION_TOTAL_PRICE",
                Valor = seat.Transaction.ReservationTotalPrice.ToString(),
                Exibir = "S",
                Titulo = "Valor total cancelamento"
            });

            iPed.Add(new()
            {
                Chave = "CANCELATION_FEE",
                Valor = seat.Transaction.CancelationFee.ToString(),
                Exibir = "S",
                Titulo = "Valor taxa cancelamento"
            });

            iPed.Add(new()
            {
                Chave = "CANCELATION_REFUND",
                Valor = seat.Transaction.ReservationTotalPrice.ToString(),
                Exibir = "S",
                Titulo = "Reembolso cancelamento"
            });

            iPed.Add(new()
            {
                Chave = "CANCELATION_CREATED_AT",
                Valor = seat.Transaction.CancelationCreatedAt.ToString(),
                Exibir = "S",
                Titulo = "Data cancelamento"
            });

            

            return iPed;
        }

        public static ItemPedido AsRecargaPlusItemFromPreOrderItemToPreOrder(this Seat seat, PreOrderItem pItem, PreOrder pOrder)
        {
            var itemPedido = new ItemPedido()
            {
                Id = long.Parse(pItem.Id),
                Nome = pItem.TravelDirectionType.ToString(),
                Quantidade = 1,
                ValorUnitario = pItem.ValueAsDecimal,
                Descricoes = new List<Descricao>()
            };

            var descricoes = new List<Descricao>
            {
                new Descricao()
                {
                    Chave = "TRANSACAO_ID",
                    Valor = seat.Transaction?.TransactionId,
                    Exibir = "S",
                    Titulo = "Transação"
                },

                new()
                {
                    Chave = "TRANSACAO_EXP_DATA",
                    Valor = seat.Transaction?.TransactionExpiresAt.ToString("yyyy-MM-dd HH:mm"),
                    Exibir = "S",
                    Titulo = "Data expiração transação"
                },


                new()
                {
                    Chave = "PASSAGEIRO_NOME",
                    Valor = seat.Client?.FullName,
                    Exibir = "S",
                    Titulo = "Nome passageiro"
                },

                new()
                {
                    Chave = "PASSAGEIRO_DOCUMENTO",
                    Valor = seat.Client?.Documents.FirstOrDefault().Value,
                    Exibir = "S",
                    Titulo = "Documento passageiro"
                },

               new()
               {
                Chave = "PASSAGEIRO_TIPO",
                Valor = "PNOS",
                Exibir = "N",
                Titulo = "Passageiro tipo"
               }
            };

            itemPedido.Descricoes = descricoes;

            return itemPedido;
        }

        private static string GetItemPedidoAsString(this ItemPedido item, string chave)
        {
            try
            {
                var ret = item?.Descricoes?.FirstOrDefault(x => x.Chave == chave)?.Valor ?? string.Empty;

                if (ret is null)
                    return "";

                return ret;
            }
            catch (System.Exception)
            {
                return "";
            }
        }

    }
}