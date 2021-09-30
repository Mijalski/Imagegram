using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;
using Mijalski.Imagegram.Server.Modules.Accounts.Passwords;

namespace Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;

public record LoginCommand(string Name, string Password);

class LoginCommandHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAccountPasswordService _accountPasswordService;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;

    public LoginCommandHandler(ApplicationDbContext dbContext, 
        IAccountPasswordService accountPasswordService, 
        IJwtTokenGeneratorService jwtTokenGeneratorService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _accountPasswordService = accountPasswordService ?? throw new ArgumentNullException(nameof(accountPasswordService));
        _jwtTokenGeneratorService = jwtTokenGeneratorService ?? throw new ArgumentNullException(nameof(jwtTokenGeneratorService));
    }

    public async Task<string?> AttemptLoginAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var dbAccount = await _dbContext.Set<DbAccount>().SingleOrDefaultAsync(a => a.Name == command.Name, cancellationToken);

        if (dbAccount is null)
        {
            return null;
        }

        var isValid = _accountPasswordService.VerifyPasswordHash(dbAccount, command.Password);

        return isValid
            ? await _jwtTokenGeneratorService.GenerateTokenAsync(dbAccount, cancellationToken) 
            : null;
    }
}