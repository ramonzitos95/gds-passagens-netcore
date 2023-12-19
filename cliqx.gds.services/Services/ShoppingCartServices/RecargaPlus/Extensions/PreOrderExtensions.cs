
using System.Globalization;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Response;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Util;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions
{
    public static class PreOrderExtensions
    {


        public static PreOrder AsPreOrder(PedidoResponse pedidoResponse, ClientResponse cliente)
        {
            PreOrder retorno = new PreOrder();
            long valorTotal = 0;

            retorno.IssuedAt = pedidoResponse.DataCadastro.Value;
            retorno.Client = new contract.Models.Client()
            {
                Id = cliente.Id.ToString(),
                FullName = !string.IsNullOrEmpty(cliente.Nome) ? cliente?.Nome + " " + cliente?.Sobrenome : string.Empty,
                Contacts = new List<Contact>() {
                    new Contact () { Value = cliente?.Contato?.Telefone, ContactType = contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal   },
                    new Contact () { Value = cliente?.Contato?.Email, ContactType = contract.GdsModels.Enum.ContactTypeEnum.EmailPessoal   }},
                Documents = new List<Document>() {
                    new Document () { Value = cliente?.Cpf, DocumentType = contract.GdsModels.Enum.DocumentTypeEnum.CPF }}
            };

            retorno.Store = new Store()
            {
                Id = cliente?.Loja?.Id.ToString(),
                Name = cliente?.Loja?.Nome
            };

            retorno.Items = new List<PreOrderItem>();
            List<PreOrderItem> products = new List<PreOrderItem>();

            var itensTaxa = pedidoResponse.Itens.FindAll(x => x.Nome.Equals("IDA_TAXA") || x.Nome.Equals("VOLTA_TAXA"));

            foreach (var itemTaxa in itensTaxa)
            {
                retorno.DiscountValue += (long)(itemTaxa.ValorUnitario * 100M);
            }

            foreach (var item in pedidoResponse.Itens)
            {
                if (!item.Nome.Equals("IDA") && !item.Nome.Equals("VOLTA"))
                {
                    continue;
                }

                var itemTransacao = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("DISTRIBUSION_TRANSACAO"));
                var poltrona = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("POLTRONA"));

                var cidadeOrigemId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CIDADE_ORIGEM_ID"));
                var cidadeOrigemNome = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ORIGEM_CIDADE"));
                var estacaoOrigemId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ESTACAO_ORIGEM_ID"));
                var estacaoOrigemNome = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ESTACAO_ORIGEM_NOME"));
                var origemDataSaida = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ORIGEM_DATA_SAIDA"));

                var cidadeDestinoId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CIDADE_DESTINO_ID"));
                var cidadeDestinoNome = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("DESTINO_CIDADE"));
                var estacaoDestinoId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ESTACAO_DESTINO_ID"));
                var estacaoDestinoNome = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("ESTACAO_DESTINO_NOME"));
                var destinoDataChegada = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("DESTINO_DATA_CHEGADA"));

                var corridaId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CORRIDA_ID"));
                var corridaClasse = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CORRIDA_CLASSE"));
                var corridaClasseId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CORRIDA_CLASSE_ID"));
                var corridaEmpresa = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CORRIDA_EMPRESA"));
                var corridaEmpresaId = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("CORRIDA_EMPRESA_ID"));

                var valorComTaxa = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("VALOR_COM_TAXA_IDA"));
                var nomePassageiro = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("PASSAGEIRO_NOME"));
                var documentoPassageiro = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("PASSAGEIRO_DOCUMENTO"));
                var tipoPassageiro = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("PASSAGEIRO_TIPO"));

                var bookingNumber = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("BOOKING_NUMBER"));
                var localizador = item.Descricoes.FirstOrDefault(x => x.Chave.Equals("LOCALIZADOR"));

                valorTotal += (long)(item.ValorUnitario * 100M);

                PreOrderItem pop = new PreOrderItem()
                {
                    Id = item.Id.ToString(),
                    DiscountValue = (long)(item.ValorUnitario * 100M),
                    Value = (long)(item.ValorUnitario * 100M),
                    Display = $"De: {cidadeOrigemNome.Valor}, Para: {cidadeDestinoNome.Valor}, Saída: {DateTime.Parse(origemDataSaida.Valor).ToString("dd/MM/yyyy HH:mm:ss")}, {corridaClasse}",
                    Description = $"📍 Origem: {estacaoOrigemNome.Valor}, {cidadeOrigemNome.Valor} \n" +
                                  $"🏁 Destino: {estacaoDestinoNome.Valor}, {cidadeDestinoNome.Valor} \n" +
                                  $"⏰ Saída: {DateTime.Parse(origemDataSaida.Valor).ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                  $"🏁 Chegada: {DateTime.Parse(destinoDataChegada.Valor).ToString("dd/MM/yyyy HH:mm:ss")} \n" +
                                  $"💰 R$ {item.ValorUnitario.ToString("N2", new CultureInfo("pt-BR"))} \n" +
                                  $"🚌 Classe: {corridaClasse.Valor} \n" +
                                  $"⚙️ Empresa: {corridaEmpresa.Valor}",
                };
                products.Add(pop);
            }
            retorno.IssuedAt = pedidoResponse.DataCadastro != null ? pedidoResponse.DataCadastro.Value : DateTime.MinValue;
            retorno.Items = products;
            retorno.TotalValue = valorTotal;
            return retorno;
        }

        public static PreOrder AsGdsPreOrderFromPedidoResponse(this PedidoResponse ped
        , ClientResponse client = null
        , List<TaxaPagamentoResponse> txPagamentoRes = null)
        {
            var preOrder = new PreOrder();
            preOrder.Items = new List<PreOrderItem>();
            var listItems = new List<PreOrderItem>();

            decimal valorTaxaParceiro = 0;
            decimal taxaParceiro = 0;
            

            foreach (var item in ped.Itens)
            {
      
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

                if (!preOrder.Items.Any(x => x.Trip.Id == corridaId))
                {
                    var poi = new PreOrderItem
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
                        Number = item.GetItemPedidoAsString("POLTRONA")
                        ,
                        Id = item.GetItemPedidoAsString("POLTRONA_ID")
                        ,
                        Class = item.GetItemPedidoAsString("CLASSE_POLTRONA_ID")
                        ,
                        Transaction = new Transaction
                        {
                            TransactionId = item.GetItemPedidoAsString("TRANSACAO_ID")
                                ,
                            ReservationId = item.GetItemPedidoAsString("RESERVA_ID")
                                ,
                            ReservationExpiresAt = item.GetItemPedidoAsDate("DATA_EXP_RESERVA")
                                ,
                            ReservationTotalPrice = item.GetItemPedidoAsLong("VALOR_RESERVA")
                        }
                        ,
                        Value = item.GetItemPedidoAsLong("VALOR_RESERVA")
                    };

                    poi.Trip.Seats.Add(seat);

                    preOrder.Items.Add(poi);
                }
                else
                {
                    var trip = preOrder.Items.FirstOrDefault(x => x.Trip.Id == corridaId);
                    var poiIndex = preOrder.Items.FindIndex(x => x.Trip.Id == trip.Trip.Id);

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
                            TransactionId = item.GetItemPedidoAsString("TRANSACAO_ID")
                                ,
                            ReservationId = item.GetItemPedidoAsString("RESERVA_ID")
                                ,
                            ReservationExpiresAt = item.GetItemPedidoAsDate("DATA_EXP_RESERVA")
                                ,
                            ReservationTotalPrice = item.GetItemPedidoAsLong("VALOR_RESERVA")
                        }
                        ,
                        Value = item.GetItemPedidoAsLong("VALOR_RESERVA")
                    };

                    preOrder.Items[poiIndex].Trip.Seats.Add(seat);

                    preOrder.Items[poiIndex].TotalValue = Convert.ToInt64(item.ValorUnitario * 100);
                    preOrder.Items[poiIndex].Value = Convert.ToInt64(item.ValorUnitario * 100);

                }



            }

            preOrder.Items.ForEach(x => x.PreOrderId = ped.PedidoId.ToString());
            preOrder.Items.ForEach(x => x.Id = ped.PedidoId.ToString());
            preOrder.Items.ForEach(x => x.ExpirationDate = (DateTime)preOrder.Items.Min(x => x.Trip.Seats.Min(s => s.Transaction.ReservationExpiresAt)));
            preOrder.IssuedAt = (DateTime)ped.DataCadastro;
            preOrder.ExpirationDate = preOrder.Items.Min(x => x.ExpirationDate);
            preOrder.Id = ped.PedidoId.ToString();

            preOrder.Channel = ped.Itens[0].GetItemPedidoAsString("CANAL_ORIGEM");
            preOrder.HasInsurance = ped.Itens[0].GetItemPedidoAsBool("CONTRATOU_SEGURO");

            if (txPagamentoRes is not null)
            {
                preOrder.Payment = new Payment();

                

                preOrder.Payment = preOrder.Payment;
                preOrder.Payment.PaymentsTax = new List<PaymentTax>();

                foreach (var item in txPagamentoRes)
                {
                    var pTax = new PaymentTax();

                    pTax.MechanismDescription = item.Nome;
                    pTax.Value = item.Valor;
                    pTax.PaymentMechanism = item.Nome.AsPaymentMode();

                    preOrder.Payment.PaymentsTax.Add(pTax);
                }
            }

            preOrder.PartnerFee = taxaParceiro;


            preOrder.Value = preOrder.Items.Sum(x => x.Trip.Seats.Sum(x => x.Value));
            
            var novoValorTaxa = ((preOrder.Value / 100) * (taxaParceiro / 100));
            preOrder.ValuePartnerFee = novoValorTaxa.AsLongFromDecimal();
            
            preOrder.TotalValue = preOrder.Items.Sum(item =>
                item.Trip.Seats.Sum(seat => seat.Value) 
                    + item.DepartureTax.AsLongFromDecimal());

            var novoValorTaxaAsLong = (novoValorTaxa*100).AsLongFromDecimal();

            preOrder.TotalValue += novoValorTaxaAsLong;
            preOrder.ValuePartnerFee = novoValorTaxaAsLong;
                
            return preOrder;
        }



        public static contract.GdsModels.PreOrder AsGdsPreOrder(this PedidoResponse pr
        , PreOrder preOrder, PluginConfiguration plugin
        , ClientResponse client
        , LojaResponse store
        , List<TaxaPagamentoResponse> taxasPagamento)
        {
            var ret = new PreOrder();
            var retItems = new List<PreOrderItem>();

            ret = preOrder;

            if (client != null)
            {
                ret.Client = client.AsGdsClient();
            }
            ret.Id = pr.PedidoId.ToString();
            ret.Payment = preOrder.Payment;
            ret.Items = preOrder.Items;


            ret.IssuedAt = (DateTime)pr.DataCadastro;

            ret.Store = new Store()
            {
                Id = store?.Id.ToString() ?? string.Empty
                ,
                Name = store?.Nome ?? string.Empty
                ,
                IsOpen = store?.Ativo == "S" ? true : false
            };

            ret.Address = new Address()
            {
                Street = "Rua Luis Coelho",
                Number = "233",
                Complement = "Escritório, 2º Andar",
                ZipCode = "01309001",
                District = "Consolação",
                City = "São Paulo",
                State = "SP",
                Country = "BRASIL"
            };

            ret.Payment = new Payment();

            ret.Payment = preOrder.Payment;
            ret.Payment.PaymentsTax = new List<PaymentTax>();

            foreach (var item in taxasPagamento)
            {
                var pTax = new PaymentTax();

                pTax.MechanismDescription = item.Nome;
                pTax.Value = item.Valor;
                pTax.PaymentMechanism = item.Nome.AsPaymentMode();

                ret.Payment.PaymentsTax.Add(pTax);
            }

            // if(ret.Trip.TotalConnections == 0)
            // {
            //     foreach (var item in ret.Seats)
            //     {
            //         var index = 0;
            //         var prItem = pr.Itens[index];

            //         item.Ticket = preOrder.Seats.ToList()[index].Ticket;

            //         item.TotalValue = (long)(prItem.ValorUnitario * 100);
            //         item.Id = prItem.Id.ToString();
            //         index++;
            //     }
            // }
            // else
            // {
            //     if(ret.Trip.Connections != null)
            //     {
            //         foreach (var seats in preOrder.Trip.Connections)
            //         {
            //             var indexTrip = 0;
            //             foreach (var item in seats.Seats)
            //             {
            //                 var seat = item.Seat;
            //                 var index = 0;
            //                 var prItem = pr.Itens[index];

            //                 item.Ticket = preOrder?.Trip?.Connections?.ToList()[indexTrip]?.Seats?.ToList()[index]?.Ticket;

            //                 seat.Value = (long)(prItem.ValorUnitario * 100);
            //                 seat.Id = prItem.Id.ToString();
            //                 index++;
            //             }
            //             indexTrip++;
            //         }
            //     }
            // }

            ret.Items.ForEach(x => x.PreOrderId = preOrder.Id);


            ret.Value = preOrder.Items.Sum(x => x.Trip.Seats.Sum(x => x.Value));
            ret.TotalValue = preOrder.Items.Sum(item =>
                item.Trip.Seats.Sum(seat => seat.Value)
                + item.DepartureTax.AsLongFromDecimal()
                + item.PedagogueValue.AsLongFromDecimal()
                + preOrder.ValuePartnerFee);

            ret.ParceiroId = preOrder.ParceiroId;

            return ret;
        }



        public static ItemPedido AsRecargaPlusItemFromPreOrderItem(this Seat seat, PreOrderItem pItem, PreOrder pOrder)
        {

            var itemPed = new ItemPedido()
            {
                Nome = Enum.GetName(typeof(TravelDirectionEnum), pItem.TravelDirectionType),
                Quantidade = 1,
                ValorUnitario = seat.Transaction.ReservationTotalPriceAsDecimal,
                Descricoes = new List<Descricao>()
            };

            itemPed.Descricoes.Add(new()
            {
                Chave = "POLTRONA",
                Valor = seat.Number,
                Exibir = "S",
                Titulo = "Número da poltrona"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "POLTRONA_ID",
                Valor = seat.Id,
                Exibir = "S",
                Titulo = "ID da poltrona"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "CLASSE_POLTRONA_ID",
                Valor = seat.Class,
                Exibir = "S",
                Titulo = "Classe da poltrona"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "TRANSACAO_ID",
                Valor = seat.Transaction?.TransactionId,
                Exibir = "S",
                Titulo = "Transação"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "RESERVA_ID",
                Valor = seat.Transaction?.ReservationId,
                Exibir = "S",
                Titulo = "Reserva ID"
            });


            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "DISTRIBUSION_TRANSACAO",
                Valor = seat.Transaction?.ReservationId,
                Exibir = "S",
                Titulo = "Reserva ID distribusion"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "DATA_EXP_RESERVA",
                Valor = seat.Transaction?.ReservationExpiresAt?.ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data expiração reserva"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "VALOR_RESERVA",
                Valor = Convert.ToDecimal(seat.Transaction?.ReservationTotalPriceAsDecimal).ToString(),
                Exibir = "S",
                Titulo = "Valor reserva"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "LOCATOR",
                Valor = seat.Transaction?.LocatorId,
                Exibir = "S",
                Titulo = "Locator"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ORIGEM_ID",
                Valor = pItem.Trip.Origin.CityId,
                Exibir = "S",
                Titulo = "Origem ID"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CIDADE_ORIGEM_ID",
                Valor = pItem.Trip.Origin.CityId,
                Exibir = "S",
                Titulo = "Cidade Origem ID"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ORIGEM_CIDADE",
                Valor = pItem.Trip.Origin.CityName,
                Exibir = "S",
                Titulo = "Origem cidade"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ORIGEM_ESTADO",
                Valor = pItem.Trip.Origin.LetterCode,
                Exibir = "S",
                Titulo = "Origem estado"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ORIGEM_DATA_SAIDA",
                Valor = pItem.Trip.DepartureTime.ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data saída"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "DESTINO_ID",
                Valor = pItem.Trip.Destination.CityId,
                Exibir = "S",
                Titulo = "Destino ID"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CIDADE_DESTINO_ID",
                Valor = pItem.Trip.Destination.CityId,
                Exibir = "S",
                Titulo = "Cidade Destino ID"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "DESTINO_CIDADE",
                Valor = pItem.Trip.Destination.CityName,
                Exibir = "S",
                Titulo = "Destino cidade"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "DESTINO_ESTADO",
                Valor = pItem.Trip.Destination.LetterCode,
                Exibir = "S",
                Titulo = "Destino estado"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "DESTINO_DATA_CHEGADA",
                Valor = pItem.Trip.ArrivalTime.ToString("yyyy-MM-dd HH:mm"),
                Exibir = "S",
                Titulo = "Data chegada"
            });


            itemPed.Descricoes.Add(new()
            {
                Chave = "SERVICO_ID",
                Valor = pItem.Trip.Id,
                Exibir = "S",
                Titulo = "Serviço ID"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CORRIDA_ID",
                Valor = pItem.Trip.Id,
                Exibir = "S",
                Titulo = "Serviço Corrida ID"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "CANAL_ORIGEM",
                Valor = pOrder.Channel,
                Exibir = "S",
                Titulo = "Canal Origem"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "TAXA_EMBARQUE",
                Valor = pItem.DepartureTax.ToString(),
                Exibir = "S",
                Titulo = "Taxa embarque"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "TAXA_PEDAGIO",
                Valor = pItem.PedagogueValue.ToString(),
                Exibir = "S",
                Titulo = "Taxa pedagio"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "CLASSE_ID",
                Valor = pItem.Trip.Class?.Id,
                Exibir = "S",
                Titulo = "ID da classe"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CORRIDA_CLASSE_ID",
                Valor = pItem.Trip.Class?.Id,
                Exibir = "S",
                Titulo = "ID da classe corrida"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "CLASSE_NOME",
                Valor = pItem.Trip.Class?.Name,
                Exibir = "S",
                Titulo = "Nome da classe"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CORRIDA_CLASSE",
                Valor = pItem.Trip.Class?.Name,
                Exibir = "S",
                Titulo = "Nome da classe corrida"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "EMPRESA_ID",
                Valor = pItem.Trip.Company?.Id,
                Exibir = "S",
                Titulo = "ID da empresa"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CORRIDA_EMPRESA_ID",
                Valor = pItem.Trip.Company?.Id,
                Exibir = "S",
                Titulo = "ID da empresa corrida"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "EMPRESA_NOME",
                Valor = pItem.Trip.Company?.Name,
                Exibir = "S",
                Titulo = "Nome da empresa"
            });

            //TODO: ver melhor forma. Está salvando duplicado para funcionar na confirmação
            itemPed.Descricoes.Add(new()
            {
                Chave = "CORRIDA_EMPRESA",
                Valor = pItem.Trip.Company?.Name,
                Exibir = "S",
                Titulo = "Nome da empresa"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "TOTAL_CONEXOES",
                Valor = pItem.Trip.TotalConnections.ToString(),
                Exibir = "S",
                Titulo = "Total de conexões"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "CONTRATOU_SEGURO",
                Valor = pOrder.HasInsurance.ToString(),
                Exibir = "S",
                Titulo = "Contratou seguro"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "PARCEIRO_ID",
                Valor = pOrder.ParceiroId.ToString() ?? "",
                Exibir = "S",
                Titulo = "Parceiro ID"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "TAXA_PARCEIRO",
                Valor = pOrder.PartnerFee.ToString() ?? "",
                Exibir = "S",
                Titulo = "Taxa parceiro"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "VALOR_TAXA_PARCEIRO",
                Valor = pOrder.ValuePartnerFeeAsDecimal.ToString() ?? "",
                Exibir = "S",
                Titulo = "Valor taxa parceiro"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ESTACAO_ORIGEM_ID",
                Valor = pItem.Trip.Station?.OriginId,
                Exibir = "S",
                Titulo = "ID estação origem"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ESTACAO_ORIGEM_NOME",
                Valor = pItem.Trip.Station?.OriginName,
                Exibir = "S",
                Titulo = "Nome estação origem"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ESTACAO_DESTINO_ID",
                Valor = pItem.Trip.Station?.DestinationId,
                Exibir = "S",
                Titulo = "Id estação destino"
            });

            itemPed.Descricoes.Add(new()
            {
                Chave = "ESTACAO_DESTINO_NOME",
                Valor = pItem.Trip.Station?.DestinationName,
                Exibir = "S",
                Titulo = "Nome estação destino"
            });

            return itemPed;
        }
    }
}