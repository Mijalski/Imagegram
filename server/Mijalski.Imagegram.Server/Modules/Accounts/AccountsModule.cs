using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;
using Mijalski.Imagegram.Server.Modules.Accounts.Mappers;
using Mijalski.Imagegram.Server.Modules.Accounts.QueryHandlers;

namespace Mijalski.Imagegram.Server.Modules.Accounts;

public record AccountDto(string Name);

class AccountsModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services.AddTransient<AccountByNameQueryHandler>()
            .AddTransient<CreateAccountCommandHandler>()
            .AddTransient<IMapper<Account, DbAccount>, AccountMapper>()
            .AddTransient<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/accounts/by-name/{name}", 
            async (HttpContext context, AccountByNameQueryHandler handler, string name) =>
            {
                if (string.IsNullOrEmpty(name))
                {
                    return Results.BadRequest();
                }

                var account = await handler.GetAccountOrDefaultByName(name, context.RequestAborted);
                if (account is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new AccountDto(account.Name));
            })
            .WithName("GetAccountByName")
            .Produces<AccountDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        endpoints.MapPost("/accounts",
            async (HttpContext context, CreateAccountCommand command, CreateAccountCommandHandler handler) =>
            {
                if (command is null)
                {
                    return Results.BadRequest();
                }

                await handler.CreateAsync(command, context.RequestAborted);

                return Results.CreatedAtRoute("GetAccountByName", new { command.Name }, command);
            })
            .WithName("CreateAccount")
            .ProducesValidationProblem()
            .Produces<CreateAccountCommand>(StatusCodes.Status201Created);

        return endpoints;
    }
}