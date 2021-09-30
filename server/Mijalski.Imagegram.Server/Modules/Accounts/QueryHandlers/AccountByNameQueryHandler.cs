using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.QueryHandlers;

class AccountByNameQueryHandler
{
    private readonly DbSet<DbAccount> _dbAccounts;

    public AccountByNameQueryHandler(ApplicationDbContext dbContext)
    {
        _dbAccounts = dbContext.Set<DbAccount>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<DbAccount?> GetAccountOrDefaultByName(string name, CancellationToken cancellationToken = default) =>
        _dbAccounts.SingleOrDefaultAsync(a => a.Name == name, cancellationToken);
}