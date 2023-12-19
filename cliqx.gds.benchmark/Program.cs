using Microsoft.Extensions.DependencyInjection;
using BenchmarkDotNet.Running;
using cliqx.gds.benchmark.Tests;
using Microsoft.Extensions.Caching.Memory;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        var benchmarkTests = new Test();

        BenchmarkRunner.Run<Test>();

    }

    static void ConfigureServices(IServiceCollection services)
    {
        // Adicione aqui as configurações e serviços necessários para sua aplicação
        services.AddMemoryCache();
        services.AddTransient<Test>();
    }
}
