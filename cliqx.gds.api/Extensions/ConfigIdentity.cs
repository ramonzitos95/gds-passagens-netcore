using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.repository.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigIdentity
{

    public static IServiceCollection AddConfigIdentity(
         this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;

        })
        .AddRoles<Role>()
        .AddEntityFrameworkStores<DefaultContext>()
        .AddRoleValidator<RoleValidator<Role>>()
        .AddRoleManager<RoleManager<Role>>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

        return services;
    }
}
