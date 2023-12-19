using cliqx.gds.api.Controllers;
using cliqx.gds.contract;
using cliqx.gds.plugins;
using cliqx.gds.repository;
using Microsoft.Extensions.DependencyInjection;

namespace cliqx.gds.test;

[TestClass]
public class DistribusionTest
{
    public DistribusionPlugin _distribusionPlugin;
    private IServiceScopeFactory scopeFactory;
    private IServiceProvider serviceProvider;

    public DistribusionTest()
    {
        ExecutaContainer();
    }

    public void ExecutaContainer()
    {
        // Configura o contêiner de injeção de dependência
        var services = new ServiceCollection();

        // Registra os serviços necessários
        services.AddScoped<DistribusionPlugin>();

        ConfigServiceCollectionExtensions.AddMyDependencyGroup(services);

        // Cria o provedor de serviços
        serviceProvider = services.BuildServiceProvider();

        // Obtém o escopo de serviço para resolução de dependência
        //scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }


    [TestMethod]
    public void CriarClienteDistribusionComSucesso()
    {
        //using (var scope = scopeFactory.CreateScope())
        //{
        // Obtém a instância resolvida da interface
        var _distribusionPlugin = serviceProvider.GetRequiredService<DistribusionPlugin>();
        var cliente = new contract.Models.Client()
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Alex Silva",
            BirthDate = DateTime.Now
        };

        var retorno = _distribusionPlugin.CreateClient(cliente).Result;
        Assert.IsNotNull(retorno);
        //}
    }
}