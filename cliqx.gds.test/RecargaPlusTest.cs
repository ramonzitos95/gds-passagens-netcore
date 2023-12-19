
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;
using cliqx.gds.test.ModelsTest;
using Microsoft.Extensions.Configuration;

namespace cliqx.gds.test
{
    [TestClass]
    public class RecargaPlusTest
    {
        private readonly IConfiguration config;

        public RecargaPlusService RecargaPlusService { get; set; }

        public RecargaPlusTest(IConfiguration config)
        {
            RecargaPlusService = new RecargaPlusService(config);
            this.config = config;
        }

        [TestMethod]
        public void CriarPedidoComSucesso()
        {
            RecargaPlusService = new RecargaPlusService(config);
            var itens = new List<Iten>();

            itens.Add(new Iten() {
                Nome = "Item 1",
                Quantidade = 50,
                ValorUnitario = 250,
                Descricoes = GenerateDescricoes.GenerateListDescricoes(),
            });

            var pedido = new Pedido() {
                ClienteId = 27,
                LojaId = 1,
                TipoPedidoId = contract.Enums.TipoPedido.COMPRA_PASSAGEM_RODOVIARIA,
                PedidoId = 0,
                ValorTotal = 200,
                Itens = itens,
            };

            //var pedidoCriado = RecargaPlusService.CreateOrder(pedido).Result;
            Console.WriteLine("Pedido: " +  pedido.PedidoId);
            //Assert.IsNotNull(pedidoCriado);
        }

        [TestMethod]
        public void AtualizaPedidoComSucesso()
        {
            RecargaPlusService = new RecargaPlusService(config);
            var itens = new List<Iten>();

            itens.Add(new Iten()
            {
                Nome = "Item 1",
                Quantidade = 50,
                ValorUnitario = 250,
                Descricoes = GenerateDescricoes.GenerateListDescricoes(),
            });

            var pedido = new Pedido()
            {
                ClienteId = 27,
                LojaId = 1,
                TipoPedidoId = contract.Enums.TipoPedido.COMPRA_PASSAGEM_RODOVIARIA,
                PedidoId = 1960,
                ValorTotal = 200,
                Itens = itens,
            };

            //var pedidoCriado = RecargaPlusService.UpdateOrder(pedido, 1).Result;
            //Assert.IsNotNull(pedidoCriado);
        }

        [TestMethod]
        public void ObterPedidoPorIdComSucesso()
        {
            RecargaPlusService = new RecargaPlusService(config);

            var idPedido = 60;
            var order = new Pedido()
            {
                PedidoId = idPedido
            };

            //var pedidoCriado = RecargaPlusService.GetOrderByOrderId(order).Result;
            //Assert.IsNotNull(pedidoCriado);
        }

        [TestMethod]
        public void CriarClienteComSucesso()
        {
            RecargaPlusService = new RecargaPlusService(config);

            var cliente = new CadastrarClienteModel()
            {
                Cpf = "95057818066",
                Nome = "Jo�o",
                Telefone = "63984232365",
                Documento = "6157326",
                Email = "joaodasilva@gmail.com",
                LojaId = 1,
                Sobrenome = "da Silva"
            };

            //var clienteCriado = RecargaPlusService.CreateClient(cliente).Result;
            //Console.WriteLine("Cliente: " + clienteCriado.Id);
            //Assert.IsNotNull(clienteCriado);
        }

        [TestMethod]
        public void AtualizarClienteComSucesso()
        {
            RecargaPlusService = new RecargaPlusService(config);

            var cliente = new CadastrarClienteModel()
            {
                Cpf = "95057818066",
                Nome = "Jo�o",
                Telefone = "63984232365",
                Documento = "6157326",
                Email = "joaodasilva@gmail.com",
                LojaId = 1,
                Sobrenome = "da Silva",
                Id = 2578
            };

            //var clienteCriado = RecargaPlusService.UpdateClient(cliente).Result;
            //Console.WriteLine("Cliente: " + clienteCriado.Id);
            //Assert.IsNotNull(clienteCriado);
        }

        [TestMethod]
        public void BuscarClientePorId()
        {
            RecargaPlusService = new RecargaPlusService(config);

            var clienteId = 2578;

            //var clienteCriado = RecargaPlusService.GetClientById(clienteId).Result;
            //Console.WriteLine("Cliente: " + clienteCriado.Id);
            //Assert.IsNotNull(clienteCriado);
        }

        [TestMethod]
        public void DeletarClientePorId()
        {
            RecargaPlusService = new RecargaPlusService(config);

            var clienteId = 2578;

            //var clienteCriado = RecargaPlusService.DeleteClient(clienteId).Result;
            //Console.WriteLine("Cliente: " + clienteCriado.Id);
            //Assert.IsNotNull(clienteCriado);
            //var resultado = CityController.GetAllByName(nomeCidade);
        }

     
    }
}