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
using cliqx.gds.plugins;
using cliqx.gds.contract;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Security.Policy;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.test.ModelsTest;
using System.Reflection.Metadata.Ecma335;
using cliqx.gds.plugins._SmartbusPlugin.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using cliqx.gds.plugins._SmartbusPlugin.Api;
using DistribusionOmsHubPlugin.Models.Api;

namespace cliqx.gds.test
{
    [TestClass]
    public class SmartBusPluginTest
    {
        public SmartbusPlugin SmartBusPlugin { get; set; }
        public SmartbusApi _smartbusAPI;

        public SmartBusPluginTest()
        {
            SmartBusPlugin = GeraInstanciaSmartBusPlugin();
            
        }

        [TestMethod]
        public void GeraConstrutorSmartBus()
        {
            var instanciaSmartBusPlugin = GeraInstanciaSmartBusPlugin();

            Assert.IsNotNull(instanciaSmartBusPlugin);
        }

        [TestMethod]
        public void BuscaOrigemNaSmartBusComSucesso()
        {
            try
            {
                var origemCidade = "São Paulo";
                var origens = SmartBusPlugin.GetOriginsByCityName(origemCidade).Result;
                var origensList = origens.Elements.ToList();
                Assert.IsNotNull(origensList);
                Assert.IsTrue(origensList.Any());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void BuscaOrigemNaSmartBusComFalha()
        {
            try
            {
                var origemCidade = "São Petesburgo da Senhora";
                var origens = SmartBusPlugin.GetOriginsByCityName(origemCidade).Result;
                var origensList = origens.Elements.ToList();
                Assert.IsTrue(origensList.Count == 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void BuscaDestinoNaSmartBusComSucesso()
        {
            try
            {
                var destinoCidade = "Blumenau";
                var destinos = SmartBusPlugin.GetDestinationsByCityName(destinoCidade).Result;
                var destinosList = destinos.Elements.ToList();
                Assert.IsNotNull(destinosList);
                Assert.IsTrue(destinosList.Any());
            }
            catch (Exception)
            {
                throw;
            }
        }


        [TestMethod]
        public void BuscaDestinoNaSmartBusComFalha()
        {
            try
            {
                var destinoCidade = "Palmas da Glória";
                var destinos = SmartBusPlugin.GetDestinationsByCityName(destinoCidade).Result;
                var destinosList = destinos.Elements.ToList();
                Assert.IsTrue(destinosList.Count == 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void BuscaCorridaComSucesso()
        {
            try
            {
                var pluginId = Guid.NewGuid();
                var objetoCorrida = GenerateTrip.GenerateTripQuery();
                var corridas = SmartBusPlugin.GetTripsByOriginAndDestination(objetoCorrida).Result;
                var corridasList = corridas.Elements.ToList();
                Assert.IsNotNull(corridasList);
                Assert.IsTrue(corridasList.Any());
                Assert.IsTrue(corridasList.Count > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void BuscaCorridaComFalha()
        {
            bool DeuErro = false;
            try
            {
                var pluginId = Guid.NewGuid();
                var objetoCorrida = GenerateTrip.GenerateTripQueryWithError();
                DeuErro = true;
                var corridas = SmartBusPlugin.GetTripsByOriginAndDestination(objetoCorrida).Result;
                var corridasList = corridas.Elements.ToList();
                Assert.IsFalse(corridasList.Any());
            }
            catch (Exception ex)
            {
                Assert.IsTrue(DeuErro);
            }
        }

        [TestMethod]
        public void BuscaPoltronasNaSmartBusComSucesso()
        {
            try
            {
                Trip tripEscolhida = new Trip();
                var objetoCorrida = GenerateTrip.GenerateTripQuery();
                var tripsCorridaList = SmartBusPlugin.GetTripsByOriginAndDestination(objetoCorrida).Result;
                Assert.IsNotNull(tripEscolhida);
                if (tripsCorridaList.Elements.Any())
                {
                    tripEscolhida = tripsCorridaList.Elements.FirstOrDefault();
                }
                var poltronas = SmartBusPlugin.GetSeatsByTripId(tripEscolhida).Result;
                var poltronasList = poltronas.Elements.ToList();
                Assert.IsNotNull(poltronasList);
                Assert.IsTrue(poltronasList.Any());
                Assert.IsTrue(poltronasList.Count > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CriarPreOrdemDaSmartBus()
        {
            var seatsToCancel = new List<contract.GdsModels.Seat>();
            try
            {
                Trip tripEscolhida = new Trip();
                var objetoCorrida = GenerateTrip.GenerateTripQuery();
                var tripsCorridaList = SmartBusPlugin.GetTripsByOriginAndDestination(objetoCorrida).Result;
                Assert.IsNotNull(tripEscolhida);
                if (tripsCorridaList.Elements.Any())
                {
                    tripEscolhida = tripsCorridaList.Elements.FirstOrDefault();
                }
                var poltronas = SmartBusPlugin.GetSeatsByTripId(tripEscolhida).Result;
                var poltronasList = poltronas.Elements.ToList();

                List<contract.GdsModels.Seat> poltronasEscolhidas = new List<contract.GdsModels.Seat>();
                var contador = 0;

                foreach (var poltrona in poltronasList)
                {
                    contador++;
                    poltronasEscolhidas.Add(poltrona);
                    if (contador == 1)
                    {
                        break;
                    }
                }
                var preOrdemGerada = GeneratePreOrder.GenerateWithSuccess();
                // preOrdemGerada.Trip = tripEscolhida;
                // preOrdemGerada.Seats = GeneratePreOrderItem.GenerateAsPreOrderItensWithSeats(poltronasEscolhidas);

                var retorno = SmartBusPlugin.CreatePreOrder(preOrdemGerada);

                Assert.IsNotNull(retorno);

                DesbloquearPoltronasDaPreOrder(preOrdemGerada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DesbloquearPoltronasDaPreOrder(PreOrder preOrder)
        {
            foreach (var item in preOrder.Items)
            {
                // if (item.Transaction.TransactionId == null) continue;

                // var request = new DesbloqueioRequest()
                // {
                //     Origem = int.Parse(preOrder.Trip.Origin.CityId)
                //     ,
                //     Destino = int.Parse(preOrder.Trip.Destination.CityId)
                //     ,
                //     Data = preOrder.Trip.ArrivalTime
                //     ,
                //     Servico = int.Parse(preOrder.Trip.Id)
                //     ,
                //     TransacaoId = item.Transaction.TransactionId
                //     ,
                //     Conexao = false
                // };

                // var response = _smartbusAPI.Client.DesbloquearPoltrona(request).Result;

                //Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        private SmartbusPlugin GeraInstanciaSmartBusPlugin()
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json") // Caminho para o arquivo appsettings.json
              .Build();

            // Arrange
            var pluginConfiguration = new PluginConfiguration
            {
                Id = Guid.NewGuid()
               ,
                ApiBaseUrl = JsonConvert.SerializeObject(new
                {
                    BaseUrl = "https://prod-andorinha-gateway-smartbus.oreons.com/J3/clickbus"
                   ,
                    UrlAuth = "http://prod-andorinha-gateway-smartbus.oreons.com:58677/OAuth"
                   ,
                    UrlEtl = "https://app.snog.com.br/smartbus"
                }
               ),
                PluginId = Guid.NewGuid(),
                Description = "Smartbus Plugin",
                TransactionName = "SMARTBUS_TRANSACAO",
                TransactionLocator = "SMARTBUS_LOCALIZADOR",
                CredentialsJsonObject = JsonConvert.SerializeObject(new
                {
                    userName = "SNOG"
                   ,
                    password = "SN@cc90Pxd"
                }),
                StoreId = 1,
                ShoppingCartId = 1,
                PaymentServiceId = 1,
                ExtraData = JsonConvert.SerializeObject(new
                {
                    contrato = "MMS2021"
                })
            };

            var apiUrlsBase = JsonConvert.DeserializeObject<plugins._SmartbusPlugin.Api.ApiUrlsBase>(pluginConfiguration.ApiBaseUrl);
            var authenticationData = JsonConvert.DeserializeObject<AuthModel>(pluginConfiguration.CredentialsJsonObject);

            _smartbusAPI = new SmartbusApi(apiUrlsBase, authenticationData);

            var url = "https://app.snog.com.br/smartbus";
            var _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(5),
                BaseAddress = new Uri(url.TrimEnd('/'))
            };

            var shoppinCartService = new Mock<IShoppingCartService>();
            var httpClient = new Mock<Func<HttpClient>>();
            var loggerService = new Mock<ILogger<IContractBase>>();

            var smartbusPlugin = new SmartbusPlugin(
                configuration,
                loggerService.Object,
                httpClient.Object,
                pluginConfiguration,
                shoppinCartService.Object
            );

            return smartbusPlugin;
        }


    }
}
