using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Infrastructures.Databases;

static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEntityFrameworkNpgsql()
            .AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("Default")))
            .AddIdentityCore<DbAccount>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}