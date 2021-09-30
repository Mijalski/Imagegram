using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;

namespace Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;

class DeleteAccountHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentAccountService _currentAccountService;

    public DeleteAccountHandler(ApplicationDbContext dbContext,
        ICurrentAccountService currentAccountService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _currentAccountService = currentAccountService ?? throw new ArgumentNullException(nameof(currentAccountService));
    }

    public async Task DeleteAccount(CancellationToken cancellationToken = default)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccountAsync(cancellationToken);

        var dbAccount = await _dbContext.Set<DbAccount>()
            .Include(_ => _.Posts)
            .Include(_ => _.Comments)
            .SingleAsync(a => a.Name == currentAccount.Name, cancellationToken);

        _dbContext.Set<DbAccount>().Remove(dbAccount);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}