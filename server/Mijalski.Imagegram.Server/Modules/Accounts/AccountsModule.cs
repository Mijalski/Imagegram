using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;
using Mijalski.Imagegram.Server.Modules.Accounts.Mappers;
using Mijalski.Imagegram.Server.Modules.Accounts.Passwords;
using Mijalski.Imagegram.Server.Modules.Accounts.QueryHandlers;

namespace Mijalski.Imagegram.Server.Modules.Accounts;

class AccountsModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services.AddTransient<AccountByNameQueryHandler>()
            .AddTransient<CreateAccountCommandHandler>()
            .AddTransient<LoginCommandHandler>()
            .AddTransient<IAccountPasswordService, AccountBCryptPasswordService>()
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
                return account is null ? Results.NotFound() : Results.Ok(new AccountDto(account.Name));
            })
            .WithName("GetAccountByName")
            .RequireAuthorization()
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

        endpoints.MapPost("/accounts/login",
                async (HttpContext context, LoginCommand command, LoginCommandHandler handler) =>
                {
                    if (command is null)
                    {
                        return Results.BadRequest();
                    }

                    var token = await handler.AttemptLoginAsync(command, context.RequestAborted);

                    return string.IsNullOrEmpty(token) ? Results.BadRequest() : Results.Ok(token);
                })
            .WithName("Login")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status200OK);

        return endpoints;
    }
}