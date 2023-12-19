using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using cliqx.gds.contract;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.Models;
using cliqx.gds.plugins;
using cliqx.gds.repository;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace cliqx.gds.benchmark.Tests
{
    [MemoryDiagnoser]
    public class Test
    {
        public CustomCity _SelectedCity { get; set; }
        public const int _totalRequestsTest = 1;

        [Benchmark]
        public void GetCityByCityName()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var httpClient = new Mock<IHttpClientFactory>();
            var loggerService = new Mock<ILogger<IContractBase>>();
            var repository = new Mock<IPluginRepository>();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPluginRepository, PluginRepository>();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<DefaultContext>(x => x
                .UseMySql(
                    configuration.GetConnectionString("MariaDBContext"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDBContext")))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            var serviceProvider = services.BuildServiceProvider();

            var cache = serviceProvider.GetRequiredService<IMemoryCache>();
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var repo = serviceProvider.GetRequiredService<IPluginRepository>();

            GdsHubSelectorService hubSelector = new GdsHubSelectorService(
                configuration,
                loggerService.Object,
                repo,
                cache,
                httpClient.Object,
                accessor);

            GdsHubHandle handle = new GdsHubHandle(
                hubSelector,
                repo,
                cache,
                loggerService.Object,
                null,
                accessor);

            var tasks = new List<Task<PagedList<CustomCity>>>();

            for (int i = 0; i < _totalRequestsTest; i++)
            {
                tasks.Add(handle.GetOriginsByCityName("salvador"));
            }

            Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    var result = task.Result;

                    if (result is not null)
                        Console.WriteLine(true);
                }
            }
        }
    }
}
