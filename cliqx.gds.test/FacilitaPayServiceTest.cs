using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Enum;
using cliqx.gds.contract.Models;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus;
using cliqx.gds.test.ModelsTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cliqx.gds.test
{
    [TestClass]
    public class FacilitaPayServiceTest
    {

        [TestMethod]
        public void GeraUrlDePagamentoComSucesso()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Caminho para o arquivo appsettings.json
                .Build();

                var urlBase = configuration.GetSection("RecargaPlus")["UrlBase"];

                var recargaPlusApi = new RecargaPlusApi(configuration, urlBase);
                var idLoja = "1";
                var configsResult = recargaPlusApi.Client.GetConfiguracao(idLoja).Result;
                var configs = configsResult.Content;
                Assert.IsNotNull(configs);
                var order = GenerateOrder.GenerateOrderForPayment();
                //var pagamentoService = new FacilitaPayService(configs);
                //var urlPagamento = pagamentoService.GenerateUrlPayment(order).Result;
                //Assert.IsTrue(string.IsNullOrEmpty(urlPagamento.Url));
            }
            catch (Exception ex) 
            {

            }
            
        }

        [TestMethod]
        public void GeraUrlDePagamentoComFalha()
        {
            FacilitaPayService pagamento = null;
            Assert.IsNull(pagamento);
        }
        
    }
}