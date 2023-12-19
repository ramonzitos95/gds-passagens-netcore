using cliqx.gds.repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigContexts
{

    public static IServiceCollection AddConfigContexts(this IServiceCollection services,
                                                       IConfigurationSection section)
    {

        services.AddDbContext<DefaultContext>(
            x => x.UseMySql(section["MariaDBContext"], ServerVersion.AutoDetect(section["MariaDBContext"]))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );


        return services;
    }
}
