using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

internal interface ICurrentAccountService
{
    Task<Account> GetCurrentAccountAsync(CancellationToken cancellationToken = default);
}