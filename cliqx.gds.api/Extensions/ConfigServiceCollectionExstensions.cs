
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.plugins;
using cliqx.gds.repository;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;


namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigServiceCollectionExtensions
{

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        services.AddScoped<GdsHubSelectorService>();
        services.AddScoped<GdsHubHandle>();
        services.AddScoped<IPluginRepository, PluginRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        services.AddScoped<IPaymentServiceRepository, PaymentServiceRepository>();
        services.AddScoped<IPluginConfigurationRepository, PluginConfigurationRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IUserPluginRepository, UserPluginRepository>();

        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IPluginService, PluginService>();
        services.AddScoped<CityService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddSingleton<PluginConfiguration>();

  

        return services;
    }
}
