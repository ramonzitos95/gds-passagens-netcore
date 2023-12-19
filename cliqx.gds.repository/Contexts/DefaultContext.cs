using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace cliqx.gds.repository.Contexts;

public class DefaultContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {

    }
    public DbSet<Plugin> Plugin { get; set; }
    public DbSet<PluginConfiguration> PluginConfiguration { get; set; }
    public DbSet<ShoppingCart> ShoppingCartServices { get; set; }
    public DbSet<PaymentService> PaymentServices { get; set; }
    public DbSet<UserPlugin> UsersPlugins { get; set; }
    public DbSet<UserForgetCode> UserForgetCodes {get; set;}
    //public DbSet<CoinType> CoinType { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<IdentityRole<int>>();
        //modelBuilder.Ignore<User>();
        modelBuilder.Ignore<IdentityUserRole<int>>();
        //modelBuilder.Ignore<IdentityUser>();
        //modelBuilder.Ignore<IdentityUserRole<int>>();
        //modelBuilder.Ignore<UserForgetCode>();

        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityUserLogin<int>>();
        modelBuilder.Ignore<IdentityUserToken<int>>();
        modelBuilder.Ignore<IdentityRoleClaim<int>>();
        modelBuilder.Ignore<City>();

        modelBuilder.Entity<UserRole>(ur =>
            {
                ur.HasKey(k => new { k.UserId, k.RoleId });

                ur
                .HasOne(x => x.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(fk => fk.RoleId)
                .IsRequired();

                ur
                .HasOne(x => x.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(fk => fk.UserId)
                .IsRequired();
            }
            );




        var plugins = new[]
        {
            new { Id = Guid.NewGuid(), Name = "RjConsultores" },
            new { Id = Guid.NewGuid(), Name = "Distribusion" },
            new { Id = Guid.NewGuid(), Name = "ClickBus" },
            new { Id = Guid.NewGuid(), Name = "Rodosoft" },
            new { Id = Guid.NewGuid(), Name = "Smartbus" }
        };

        var shoppingCarts = new[]
        {
            new { Id = 1, Name = "RecargaPlus" },
        };

        var paymentServices = new[]
        {
            new
            {
                Id = 1
                , Name = "DefaultPay"
                , ApiBaseUrl = JsonConvert.SerializeObject(
                    new
                    {
                        UrlBaseTreeal = "https://bank-api-production-dot-snog-382317.ue.r.appspot.com/api/v1/bank"
                    })
                ,CredentialsJsonObject = JsonConvert.SerializeObject(
                    new {
                        TokenTreeal = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU"
                        ,PixKeyTreeal = "ce276502-0206-4b9c-abfe-57e9db60f01e"
                        ,ExpirationSecondsTreeal = 600
                        ,UrlReturnTreeal = "https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal"
                    }
                )
            }
            ,new
            {
                Id = 2
                , Name = "FacilitaPay"
                , ApiBaseUrl = JsonConvert.SerializeObject(
                    new
                    {
                        UrlBase = "https://checkout.cliqx.com.br"
                    })
                ,CredentialsJsonObject = JsonConvert.SerializeObject(
                    new {
                        Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU"
                        ,PixKey = "ce276502-0206-4b9c-abfe-57e9db60f01e"
                        ,ExpirationSeconds = 600
                        ,UrlReturn = "https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal"
                        ,CartaoJuros = 0.0299
                        ,MaxParcelas = "3"
                        ,CartaoExpiracaoLink = ""
                        ,PixExpiracaoLink = ""
                    }
                )
            }
        };

        modelBuilder.Entity<ShoppingCart>()
        .HasData(
            shoppingCarts.Select(p => new ShoppingCart { Id = p.Id, Name = p.Name })
        );

        modelBuilder.Entity<PaymentService>()
        .HasData(
            paymentServices.Select(p => new PaymentService
            {
                Id = p.Id,
                Name = p.Name,
                ApiBaseUrl = p.ApiBaseUrl,
                CredentialsJsonObject = p.CredentialsJsonObject,
            }
            )
        );

        modelBuilder.Entity<Plugin>()
        .HasData(
            plugins.Select(p => new Plugin
            {
                Id = p.Id,
                Name = p.Name
            })
        );

        modelBuilder.Entity<PluginConfiguration>()
        .HasData(
            new PluginConfiguration
            {
                Id = Guid.NewGuid()
                ,
                ApiBaseUrl = JsonConvert.SerializeObject(new
                {
                    BaseUrl = "http://3.141.251.80"
                }
                )
                ,
                PluginId = plugins.Single(p => p.Name == "RjConsultores").Id
                ,
                Description = "",
                TransactionName = "RJ_TRANSACAO",
                TransactionLocator = "RJ_LOCALIZADOR",
                CredentialsJsonObject = JsonConvert.SerializeObject(
                    new
                    {
                        Authorization = "Basic c25vZzpzbm9n"
                        ,
                        TenantId = "e9ef81ac-1fd0-40a2-84c0-df077dc46e4b"
                        ,
                        RecargaPlusUser = "omshub-api"
                        ,
                        RecargaPlusPassword = "999999"
                        ,
                        RecargaPlusStore = "23"
                    }
                    ),
                StoreId = 1,
                ShoppingCartId = shoppingCarts.Single(p => p.Name == "RecargaPlus").Id,
                PaymentServiceId = paymentServices.Single(p => p.Name == "FacilitaPay").Id,
            }
            , new PluginConfiguration
            {
                Id = Guid.NewGuid()
                ,
                ApiBaseUrl = JsonConvert.SerializeObject(new
                {
                    BaseUrl = "https://api.distribusion.com"
                    ,
                    RecargaPlusUrl = "https://app.snog.com.br/recarga-plus"
                    ,
                    DistribusionEtlUrl = "http://localhost:20231"
                }
                )
                ,
                PluginId = plugins.Single(p => p.Name == "Distribusion").Id
                ,
                Description = "Distribusion",
                TransactionName = "DISTRIBUSION_TRANSACAO",
                TransactionLocator = "DISTRIBUSION_LOCALIZADOR",
                CredentialsJsonObject = JsonConvert.SerializeObject(new
                {
                    ApiKey = "o6tLyEqo12zLetXXyZ0eM2kKr2C1abrfe3jhO74j"
                        ,
                    RecargaPlusStore = "1"
                        ,
                    RetailerPartnerNumber = "609180"
                        ,
                    SmtpPassword = "eyAasdas123"
                }
                ),
                StoreId = 1,
                ShoppingCartId = shoppingCarts.Single(p => p.Name == "RecargaPlus").Id,
                PaymentServiceId = paymentServices.Single(p => p.Name == "FacilitaPay").Id,
            }
            , new PluginConfiguration
            {
                Id = Guid.NewGuid()
                ,
                ApiBaseUrl = JsonConvert.SerializeObject(new
                {
                    BaseUrl = "http://rjconsultores:2023/api"
                        ,
                    UrlClickBus = "http://localhost:2023/api"
                        ,
                    BaseUrlRecargaPlus = "http://recargaplus:2023/api"
                }
                )
                ,
                PluginId = plugins.Single(p => p.Name == "ClickBus").Id
                ,
                Description = "",
                TransactionName = "CLICKBUS_TRANSACAO",
                TransactionLocator = "CLICKBUS_LOCALIZADOR",
                CredentialsJsonObject = JsonConvert.SerializeObject(new
                {
                    apiKey = "eyAasdas123"
                }
                ),
                StoreId = 1,
                ShoppingCartId = shoppingCarts.Single(p => p.Name == "RecargaPlus").Id,
                PaymentServiceId = paymentServices.Single(p => p.Name == "FacilitaPay").Id,
            }
            , new PluginConfiguration
            {
                Id = Guid.NewGuid()
                ,
                ApiBaseUrl = JsonConvert.SerializeObject(new
                {
                    BaseUrl = "http://rjconsultores:2023/api"
                    ,
                    UrlRodosoft = "http://localhost:2023/api"
                    ,
                    BaseUrlRecargaPlus = "http://recargaplus:2023/api"
                }
                )
                ,
                PluginId = plugins.Single(p => p.Name == "Rodosoft").Id
                ,
                Description = "",
                TransactionName = "RODOSOFT_TRANSACAO",
                TransactionLocator = "RODOSOFT_LOCALIZADOR",
                CredentialsJsonObject = JsonConvert.SerializeObject(new { apiKey = "eyAasdas123" }),
                StoreId = 1,
                ShoppingCartId = shoppingCarts.Single(p => p.Name == "RecargaPlus").Id,
                PaymentServiceId = paymentServices.Single(p => p.Name == "FacilitaPay").Id,
            }, new PluginConfiguration
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
    )
    ,
                PluginId = plugins.Single(p => p.Name == "Smartbus").Id
    ,
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
                ShoppingCartId = shoppingCarts.Single(p => p.Name == "RecargaPlus").Id,
                PaymentServiceId = paymentServices.Single(p => p.Name == "FacilitaPay").Id,
                ExtraData = JsonConvert.SerializeObject(new
                {
                    contrato = "MMS2021"
                })
            }
        );


        modelBuilder.Entity<CoinType>()
            .HasData(
                new CoinType { Id = 1, ExternalId = Guid.NewGuid(), Name = "Real", Label = "R$", InternalProperty = true }
                , new CoinType { Id = 2, ExternalId = Guid.NewGuid(), Name = "Dólar", Label = "US$", InternalProperty = true }
                , new CoinType { Id = 3, ExternalId = Guid.NewGuid(), Name = "Euro", Label = "€", InternalProperty = true }

            );
    }

}
