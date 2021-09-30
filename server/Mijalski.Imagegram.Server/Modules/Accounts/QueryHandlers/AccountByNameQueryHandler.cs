using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.QueryHandlers;

public record AccountDto(string Name);

class AccountByNameQueryHandler
{
    private readonly DbSet<DbAccount> _dbAccounts;

    public AccountByNameQueryHandler(ApplicationDbContext dbContext)
    {
        _dbAccounts = dbContext.Set<DbAccount>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<AccountDto?> GetAccountOrDefaultByName(string name, CancellationToken cancellationToken = default)
    {
        var dbAccount = await _dbAccounts.SingleOrDefaultAsync(a => a.Name == name, cancellationToken);

        return dbAccount is null ? null : new AccountDto(dbAccount.Name);
    }
}