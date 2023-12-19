using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using cliqx.gds.contract.Models;
using Newtonsoft.Json;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Enums;
using distribusion.api.client.Models;
using cliqx.gds.test.ModelsTest;
using cliqx.gds.services.Services.PaymentServices.Contract;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models.Request;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Extensions;
using System.Configuration;

namespace cliqx.gds.test
{
    [TestClass]
    public class RecargaPlusServiceTest
    {
        public IConfigurationRoot configuration;

        public RecargaPlusService RecargaPlusService { get; set; }
        public RecargaPlusApi RecargaPlusApi { get; set; }

        public RecargaPlusServiceTest()
        {
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json") // Caminho para o arquivo appsettings.json
              .Build();

            RecargaPlusService = GeraInstanciaRecargaPlus();
        }

        [TestMethod]
        public void GeraConstrutorRecargaPlus()
        {
            var instanciaRecarga = GeraInstanciaRecargaPlus();

            // Assert
            Assert.IsNotNull(instanciaRecarga);
        }

        public void GeraInstanciaRecargaPlusApi()
        {
            RecargaPlusApi = new RecargaPlusApi(configuration, configuration.GetSection("RecargaPlus")["UrlBase"]);
        }

        [TestMethod]
        public void BuscaClientePorTelefoneELoja()
        {
            try
            {
                var resultado = RecargaPlusService.GetClientByLojaAndPhone("2", "5511959284000").Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }

        }

        [TestMethod]
        public void BuscaClientePorId()
        {
            try
            {
                var cliente = InstanceCliente();
                var resultado = RecargaPlusService.GetClientById(cliente).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }

        }

        [TestMethod]
        public void BuscaPedidoPorId()
        {
            try
            {
                var pedido = InstanceOrder();
                // var resultado = RecargaPlusService.GetOrderByOrderId(pedido).Result;

                // // Assert
                // Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }

        }

        [TestMethod]
        public void BuscaPreOrderPorId()
        {
            try
            {
                var pedido = InstanceOrder();
                // var resultado = RecargaPlusService.GetPreOrderByPreOrderId(pedido).Result;

                // // Assert
                // Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void BuscaMetodosPagamento()
        {
            try
            {
                var resultado = RecargaPlusService.GetPaymentsMethods();

                // Assert
                Assert.IsNotNull(resultado);

                foreach (KeyValuePair<PaymentMethod, string> entry in resultado)
                {
                    Console.WriteLine($"Valor: {entry.Key}, Nome: {entry.Value}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void UpdateClient()
        {
            try
            {
                var cliente = InstanceCliente();
                var resultado = RecargaPlusService.UpdateClient(cliente).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }


        [TestMethod]
        public void AtualizarPedido()
        {
            try
            {
                var pedido = InstanceOrder();
                var resultado = RecargaPlusService.UpdateOrder(pedido).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void AtualizaStatusPedido()
        {
            try
            {
                var pedido = new Order() { Id = "83", Status = StatusPedido.PRE_PEDIDO.ToString() };
                StatusPedido status = StatusPedido.AGUARDANDO_PAGAMENTO;
                var resultado = RecargaPlusService.UpdateStatusPedido(int.Parse(pedido.Id), status).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void AdicionaPreOrderItems()
        {
            try
            {
                var preOrdem = new PreOrder()
                {
                    Id = "83",
                    Status = StatusPedido.PRE_PEDIDO.ToString(),
                    ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                    PluginId = Guid.NewGuid(),
                    StoreId = "2",
                    Store = new Store()
                    {
                        Id = "2",
                        Name = "Loja"
                    }
                };
                preOrdem.Client = InstanceCliente();
                preOrdem.Payment = Payment;

                //var resultado = RecargaPlusService.AddPreOrderItems(preOrdem).Result;

                // Assert
                //Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CriarOPrePedidoPeloPedido()
        {
            try
            {
                var preOrdem = new PreOrder()
                {
                    Id = "83",
                    Status = StatusPedido.AGUARDANDO_PAGAMENTO.ToString(),
                    ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                    PluginId = Guid.NewGuid(),
                    StoreId = "2",
                    Store = new Store()
                    {
                        Id = "2",
                        Name = "Loja"
                    }
                };
                preOrdem.Client = InstanceCliente();
                preOrdem.Client.Addresses = new List<Address>()
                {
                     new Address()
                     {
                         ZipCode = "77818-822",
                         City = "Palmas",
                         Country = "Brasil",
                         Complement = "Rua Águas Cristalinas, APTO 58",
                         ReferencePoint = "Fim da Rua",
                         District = "Parque Sonhos Dourados",
                         ExternalId = Guid.NewGuid(),
                         IsDefault = true,
                         Number = "500",
                         State = "TO",
                         Street = "Rua Águas Cristalinas"
                     }
                };
                preOrdem.Payment = Payment;
                //preOrdem.Trip = GenerateDataPreOrdem.GenerateTrip();
                var itens = GenerateDataPreOrdem.GetGeneratePreOrderItens();
                //preOrdem.Seats = itens;

                var resultado = RecargaPlusService.CreateOrderByPreOrder(preOrdem).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void AdicionaItemDescricaoNoItemPedido()
        {
            try
            {

                string itemId = "1900"; //Item id da tabela pedido item
                List<Descricao> listaDescricao = new List<Descricao>();
                listaDescricao.Add(new Descricao() { Chave = "CANCELATION_TOTAL_PRICE", Valor = "R$200.00", Exibir = "N", Posicao = 0, Titulo = "Preço total do cancelamento" });
                listaDescricao.Add(new Descricao() { Chave = "CANCELATION_FEE", Valor = "R$1.50", Exibir = "N", Posicao = 0, Titulo = "Taxa de cancelamento" });
                listaDescricao.Add(new Descricao() { Chave = "CANCELATION_REFUND", Valor = "R$200.00", Exibir = "N", Posicao = 0, Titulo = "Valor a ser extornado" });
                listaDescricao.Add(new Descricao() { Chave = "CANCELATION_CREATED_AT", Valor = DateTime.Now.ToString("dd/MM/yyyy"), Exibir = "N", Posicao = 0, Titulo = "Data de cancelamento" });

                var resultado = RecargaPlusService.AddItemDescription(itemId, listaDescricao).Result;

                // Assert
                Assert.IsTrue(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CriaPreOrdem()
        {
            try
            {

                var preOrdem = new PreOrder()
                {
                    Id = "83",
                    Status = StatusPedido.PRE_PEDIDO.ToString(),
                    ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                    PluginId = Guid.NewGuid(),
                    StoreId = "2",
                    Store = new Store()
                    {
                        Id = "2",
                        Name = "Loja"
                    }
                };
                preOrdem.Client = InstanceCliente();
                preOrdem.Payment = Payment;

                var resultado = RecargaPlusService.CreatePreOrder(preOrdem).Result;

                // Assert
                Assert.IsNotNull(resultado);
                Assert.IsNotNull(resultado.Id);
                Assert.IsInstanceOfType(resultado.Id, typeof(string));
                var IdMaiorQueZero = int.Parse(resultado.Id) >= 0;
                Assert.IsTrue(IdMaiorQueZero);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CriaPreOrdemComConexao()
        {
            try
            {

                var preOrdem = new PreOrder
                {
                    Id = "83",
                    Status = StatusPedido.PRE_PEDIDO.ToString(),
                    ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                    PluginId = Guid.NewGuid(),
                    StoreId = "2",
                    TotalValue = 15000,
                    Store = new Store()
                    {
                        Id = "2",
                        Name = "Loja"
                    },
                    Client = InstanceCliente(),
                    Payment = Payment,
                    // Trip = new Trip()
                    // {
                    //     Id = "25",
                    //     ArrivalTime = DateTime.Now,
                    //     AvailableSeats = 15,
                    //     Class = new TripClass()
                    //     {
                    //         Description = "Teste",
                    //         Id = "2",
                    //         Name = "Teste"
                    //     },
                    //     Company = new Company()
                    //     {
                    //         Description = "Teste",
                    //         Id = "25",
                    //         ExternalId = Guid.NewGuid(),
                    //         Key = "Teste",
                    //         Name = "CompanyName",
                    //     },
                    //     PluginId = Guid.NewGuid(),
                    //     ClassId = 200,
                    //     CompanyId = 100,
                    //     Connections = GenerateConnections.Generate()
                    // }
                };

                var resultado = RecargaPlusService.CreatePreOrder(preOrdem).Result;

                // Assert
                Assert.IsNotNull(resultado);
                Assert.IsNotNull(resultado.Id);
                Assert.IsInstanceOfType(resultado.Id, typeof(string));
                var IdMaiorQueZero = int.Parse(resultado.Id) >= 0;
                Assert.IsTrue(IdMaiorQueZero);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CriaPreOrdemComVariasConexao()
        {
            try
            {

                var preOrdem = new PreOrder
                {
                    Id = "83",
                    Status = StatusPedido.PRE_PEDIDO.ToString(),
                    ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                    PluginId = Guid.NewGuid(),
                    StoreId = "2",
                    TotalValue = 15000,
                    Store = new Store()
                    {
                        Id = "2",
                        Name = "Loja"
                    },
                    Client = InstanceCliente(),
                    Payment = Payment,
                    // Trip = new Trip()
                    // {
                    //     Id = "25",
                    //     ArrivalTime = DateTime.Now,
                    //     AvailableSeats = 15,
                    //     Class = new TripClass()
                    //     {
                    //         Description = "Teste",
                    //         Id = "2",
                    //         Name = "Teste"
                    //     },
                    //     Company = new Company()
                    //     {
                    //         Description = "Teste",
                    //         Id = "25",
                    //         ExternalId = Guid.NewGuid(),
                    //         Key = "Teste",
                    //         Name = "CompanyName",
                    //     },
                    //     PluginId = Guid.NewGuid(),
                    //     ClassId = 200,
                    //     CompanyId = 100,
                    //     Connections = GenerateConnections.GenerateManyConnections()
                    // }
                };

                var resultado = RecargaPlusService.CreatePreOrder(preOrdem).Result;

                // Assert
                Assert.IsNotNull(resultado);
                Assert.IsNotNull(resultado.Id);
                Assert.IsInstanceOfType(resultado.Id, typeof(string));
                var IdMaiorQueZero = int.Parse(resultado.Id) >= 0;
                Assert.IsTrue(IdMaiorQueZero);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CriaCliente()
        {
            try
            {
                var client = InstanceCliente();
                var resultado = RecargaPlusService.CreateClient(client).Result;
                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void CancelaPreOrdem()
        {
            try
            {

                var order = new Order()
                {
                    Id = "84"
                };
                order.Client = InstanceCliente();
                order.Payment = Payment;
                var itens = GenerateDataPreOrdem.GetGenerateOrderItens();
                order.Items = itens;


                var resultado = RecargaPlusService.DeleteOrder(order).Result;

                // Assert
                Assert.IsNotNull(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void MontaDescricoesTicket()
        {
            try
            {
                var ticket = new Ticket()
                {
                    SeatTicketNumber = "teste",
                    AgencyAddress = "Rua Sei la",
                    AgencyCity = "Palmeiras",
                    AgencyCnpj = "20292902092902900001",
                    AgencyCompanyName = "Cliqx"
                };

                var descricoes = ticket.AsListDescricoesByTicket();

                // Assert
                Assert.IsNotNull(descricoes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu erro: " + e.Message);
            }
        }

        [TestMethod]
        public void TestaObterParceiroPorToken()
        {
            GeraInstanciaRecargaPlusApi();
            var resposta = RecargaPlusApi.Client.ObterParceiroPorToken("TesteSnog").Result;
            if(!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro na requisição {resposta.Error.Content} ");
                
            }
            Assert.IsNotNull(resposta.Content);
        }

        [TestMethod]
        public void TestaObterParceiroPorId()
        {
            GeraInstanciaRecargaPlusApi();
            var resposta = RecargaPlusApi.Client.ObterParceiroPorId(1).Result;
            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro na requisição {resposta.Error.Content} ");

            }
            Assert.IsNotNull(resposta.Content);
        }

        private contract.Models.Client InstanceCliente()
        {
            var documento = new Document() { Value = "95057818066", DocumentType = contract.GdsModels.Enum.DocumentTypeEnum.CPF };
            var documents = new List<Document>() { documento };
            var contatoTelefone = new Contact() { Value = "5511981617830", ContactType = contract.GdsModels.Enum.ContactTypeEnum.CelularPessoal };
            var contatoEmail = new Contact() { Value = "rodrigosilva@gmail.com", ContactType = contract.GdsModels.Enum.ContactTypeEnum.EmailPessoal };
            var contacts = new List<Contact>() { contatoEmail, contatoTelefone };
            return new contract.Models.Client()
            {
                Id = "261",
                Documents = documents,
                FullName = "Rodrigo da Silva",
                Contacts = contacts
            };
        }

        private Order InstanceOrder()
        {
            return new Order()
            {
                Id = "13",
                ExternalId = Guid.Parse("2eedc48c-1f28-48c3-8b67-696e9edef07f"),
                Payment = new Payment()
                {
                    PaymentMechanism = PaymentModeEnum.CREDIT_CARD_IN_FULL,
                    Name = "Cartão de crédito"
                },
                Status = ""
            };
        }

        private Payment Payment => new Payment()
        {
            PaymentMechanism = PaymentModeEnum.CREDIT_CARD_IN_FULL,
            Name = "Cartão de crédito"
        };

        private RecargaPlusService GeraInstanciaRecargaPlus()
        {
            

            // Arrange
            var pluginConfiguration = new PluginConfiguration
            {
                ApiBaseUrl = "https://localhost:44335/v1/",
                CredentialsJsonObject = JsonCredentialsObjectPlugin(),
                StoreId = 1,
                TransactionName = "PLUGIN_NAME",
                TransactionLocator = "TESTERXX"
            };
            var paymentServiceMock = new Mock<IPaymentService>();

            // Act
            var recargaPlusService = new RecargaPlusService(
                configuration,
                pluginConfiguration,
                paymentServiceMock.Object
            );

            return recargaPlusService;
        }

        private string JsonCredentialsObjectPlugin()
        {
            AuthenticationData data = new AuthenticationData
            {
                ApiKey = "o6tLyEqo12zLetXXyZ0eM2kKr2C1abrfe3jhO74j",
                RecargaPlusStore = "1",
                RetailerPartnerNumber = "609180",
                SmtpPassword = "eyAasdas123"
            };

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }


    }
}
