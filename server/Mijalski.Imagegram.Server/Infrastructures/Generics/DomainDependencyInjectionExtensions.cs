using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

public static class DomainDependencyInjectionExtensions
{
    public static IServiceCollection RegisterDomain(this WebApplicationBuilder builder)
    {
        return builder.Services.AddTransient<IAccountManager, AccountManager>();
    }

}